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
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;

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
    private readonly ICustomerManagementBL _cusBL;

    public AuthenticationController(UserManager<IdentityUser> p_userManager,
                                    SignInManager<IdentityUser> p_signInManager,
                                    RoleManager<IdentityRole> p_roleManager,
                                    IAccessTokenManager p_accessTokenManager,
                                    ICustomerManagementBL p_cusBL)
    {
      _userManager = p_userManager;
      _signInManager = p_signInManager;
      _roleManager = p_roleManager;
      _accessTokenManager = p_accessTokenManager;
      _cusBL = p_cusBL;
    }

    [HttpPost(RouteConfigs.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterForm registerFrom)
    {
      //Validate the phonenumber
      if (string.IsNullOrEmpty(registerFrom.PhoneNumber))
      {
        return BadRequest(new { Result = "Phone number cannot be null or empty" });
      }
      if (!registerFrom.PhoneNumber.All(Char.IsDigit))
      {
        return BadRequest(new { Result = "Phone number cannot contains any letter" });
      }
      if (registerFrom.PhoneNumber.Length < 10)
      {
        return BadRequest(new { Result = "Phone number length should be greater or equal to 10" });
      }

      //Check if default Role is existed in the database ("Customer")
      if (!(await _roleManager.RoleExistsAsync("Customer")))
      {
        await _roleManager.CreateAsync(new IdentityRole("Customer"));
      }

      IdentityUser _identity = new IdentityUser()
      {
        UserName = registerFrom.Username,
        Email = registerFrom.Email,
        PhoneNumber = registerFrom.PhoneNumber,
        EmailConfirmed = false
      };

      var result = await _userManager.CreateAsync(_identity, registerFrom.Password);

      if (result.Succeeded)
      {
        var userFromDB = await _userManager.FindByNameAsync(_identity.UserName);

        //Add default role to user ("Customer")
        await _userManager.AddToRoleAsync(userFromDB, "Customer");

        //Add a new profile for Customer
        CustomerProfile _cusInfo = new CustomerProfile();
        _cusInfo.CustomerID = Guid.Parse(userFromDB.Id);
        _cusInfo.Email = userFromDB.Email;
        _cusInfo.PhoneNumber = userFromDB.PhoneNumber;
        await _cusBL.AddNewCustomerProfile(_cusInfo);

        Log.Warning("Route: " + RouteConfigs.Register);
        Log.Information("Register Sucees " + _identity.UserName);
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
