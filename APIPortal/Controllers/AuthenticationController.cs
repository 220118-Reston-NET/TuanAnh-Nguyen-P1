using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIPortal.Areas.Identity.Data;
using APIPortal.AuthenticationService.Interfaces;
using APIPortal.Consts;
using APIPortal.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APIPortal.Controllers
{
  [AllowAnonymous]
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAccessTokenManager _accessTokenManager;

    public AuthenticationController(UserManager<IdentityUser> p_userManager,
                                    SignInManager<IdentityUser> p_signInManager,
                                    RoleManager<IdentityRole> p_roleManager,
                                    IAccessTokenManager p_accessTokenManager)
    {
      _userManager = p_userManager;
      _signInManager = p_signInManager;
      _roleManager = p_roleManager;
      _accessTokenManager = p_accessTokenManager;
    }

    [HttpPost(RouteConfigs.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterForm registerFrom)
    {
      if (!(await _roleManager.RoleExistsAsync(registerFrom.Role)))
      {
        await _roleManager.CreateAsync(new IdentityRole(registerFrom.Role));
      }

      IdentityUser _identity = new IdentityUser()
      {
        UserName = registerFrom.Username,
        Email = registerFrom.Email,
        EmailConfirmed = false
      };

      var result = await _userManager.CreateAsync(_identity, registerFrom.Password);

      if (result.Succeeded)
      {
        var userFromDB = await _userManager.FindByNameAsync(_identity.UserName);

        //Add role to user
        await _userManager.AddToRoleAsync(userFromDB, registerFrom.Role);

        Log.Warning("Route: " + RouteConfigs.Register);
        Log.Information("Register Sucees " + userFromDB.UserName);
        return Ok(new { Result = "Register Success!" });
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var error in result.Errors)
        {
          stringBuilder.Append(error.Description);
        }

        Log.Warning("Route: " + RouteConfigs.Register);
        Log.Warning($"Register Fail: {stringBuilder.ToString()}");
        return BadRequest(new { Result = $"Register Fail: {stringBuilder.ToString()}" });
      }
    }

    [HttpPost(RouteConfigs.Login)]
    public async Task<IActionResult> Login([FromBody] LoginForm loginForm)
    {
      var userFromDB = await _userManager.FindByNameAsync(loginForm.Username);

      if (userFromDB == null)
      {
        Log.Warning("Route: " + RouteConfigs.Login);
        Log.Warning("Login Failed! User didn't exist in the database!");
        return BadRequest();
      }

      var result = await _signInManager.CheckPasswordSignInAsync(userFromDB, loginForm.Password, false);

      if (!result.Succeeded)
      {
        Log.Warning("Route: " + RouteConfigs.Login);
        Log.Warning("Login Failed! Password didn't matched in the database!");
        return BadRequest();
      }
      var roles = await _userManager.GetRolesAsync(userFromDB);

      Log.Information("Route: " + RouteConfigs.Login);
      Log.Information("Login Succesfully!");
      return Ok(new
      {
        Result = result,
        Username = userFromDB.UserName,
        Email = userFromDB.Email,
        Token = _accessTokenManager.GenerateToken(userFromDB, roles)
      });
    }
  }
}
