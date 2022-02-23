using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using APIPortal.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.AspNetCore.Authorization;
using APIPortal.DataTransferObject;
using Microsoft.AspNetCore.Identity;

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    private readonly IAdminServiceBL _adminBL;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    public AdminController(IAdminServiceBL p_adminBL, RoleManager<IdentityRole> p_roleManager, UserManager<IdentityUser> p_userManager)
    {
      _adminBL = p_adminBL;
      _roleManager = p_roleManager;
      _userManager = p_userManager;
    }

    // GET: api/Products
    [Authorize(Roles = "Admin")]
    [HttpGet(RouteConfigs.Products)]
    public async Task<IActionResult> GetProducts()
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Products);
        return Ok(await _adminBL.GetAllProducts());
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Products);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Product/5
    [Authorize(Roles = "Admin")]
    [HttpGet(RouteConfigs.Product)]
    public async Task<IActionResult> GetProductByID([FromQuery] Guid id)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Ok(await _adminBL.GetProductByID(id));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // POST: api/Product
    [Authorize(Roles = "Admin")]
    [HttpPost(RouteConfigs.Product)]
    public async Task<IActionResult> AddProduct([FromBody] Product p_prod)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Created("Succesfully created a new product!", await _adminBL.AddNewProduct(p_prod));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // PUT: api/Product
    [Authorize(Roles = "Admin")]
    [HttpPut(RouteConfigs.Product)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product p_prod)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Ok(await _adminBL.UpdateProduct(p_prod));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // POST: api/Admin/UserRole
    [Authorize(Roles = "Admin")]
    [HttpPost(RouteConfigs.AddRoleToUser)]
    public async Task<IActionResult> AddRoleToUser([FromBody] UserTransferObject p_userRole)
    {
      try
      {
        if (!(await _roleManager.RoleExistsAsync(p_userRole.Role)))
        {
          await _roleManager.CreateAsync(new IdentityRole(p_userRole.Role));
        }

        //Get User from DB
        var userFromDB = await _userManager.FindByNameAsync(p_userRole.Username);

        //Add Role to User
        await _userManager.AddToRoleAsync(userFromDB, p_userRole.Role);

        Log.Information("Route: " + RouteConfigs.AddRoleToUser);
        Log.Information("Added Role to User Successfully!");
        return Ok(new { Result = "Added Role to User Successfully!" });
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.AddRoleToUser);
        Log.Warning(e.Message);
        return BadRequest();
      }
    }
  }
}
