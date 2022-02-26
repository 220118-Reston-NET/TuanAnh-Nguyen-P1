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
using Microsoft.AspNetCore.Identity;

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    private readonly IAdminServiceBL _adminBL;
    private readonly IStoreManagementBL _storeBL;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    public AdminController(IAdminServiceBL p_adminBL,
                            RoleManager<IdentityRole> p_roleManager,
                            UserManager<IdentityUser> p_userManager,
                            IStoreManagementBL p_storeBL)
    {
      _adminBL = p_adminBL;
      _roleManager = p_roleManager;
      _userManager = p_userManager;
      _storeBL = p_storeBL;
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
      Task taskAddProduct = _adminBL.AddNewProduct(p_prod);
      try
      {
        await taskAddProduct;
        Log.Warning("Route: " + RouteConfigs.Product);
        return Created("Created a new product success!", p_prod);
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return BadRequest(new { Results = "This product is already in the system!" });
      }
    }

    // PUT: api/Product
    [Authorize(Roles = "Admin")]
    [HttpPut(RouteConfigs.Product)]
    public async Task<IActionResult> UpdateProduct([FromBody] Product p_prod)
    {
      Task taskUpdateProduct = _adminBL.UpdateProduct(p_prod);
      try
      {
        await taskUpdateProduct;
        Log.Information("Route: " + RouteConfigs.Product);
        return Ok(new { Results = "Updated Succesfully!" });
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return BadRequest(new { Results = "Cannot find the product detail to update! Please check the Product ID" });
      }
    }

    // POST: api/Admin/UserRole
    [Authorize(Roles = "Admin")]
    [HttpPost(RouteConfigs.AddRoleToUser)]
    public async Task<IActionResult> AddStoreManagerRoleToUser([FromQuery] string p_username)
    {
      try
      {
        if (!(await _roleManager.RoleExistsAsync("StoreManager")))
        {
          await _roleManager.CreateAsync(new IdentityRole("StoreManager"));
        }

        //Get User from DB
        var userFromDB = await _userManager.FindByNameAsync(p_username);

        //Add Store Manager Role to User
        await _userManager.AddToRoleAsync(userFromDB, "StoreManager");

        StoreFrontProfile _storeF = new StoreFrontProfile();
        _storeF.StoreID = Guid.Parse(userFromDB.Id);
        _storeF.Name = $"{userFromDB.UserName}'s Store";
        await _storeBL.AddNewStoreFrontProfile(_storeF);

        Log.Information("Route: " + RouteConfigs.AddRoleToUser);
        Log.Information("Added Store Manager Role to User Successfully!");
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
