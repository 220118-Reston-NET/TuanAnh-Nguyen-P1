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

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    private readonly IAdminServiceBL _adminBL;
    public AdminController(IAdminServiceBL p_adminBL)
    {
      _adminBL = p_adminBL;
    }

    // GET: api/Products
    [Authorize(Roles = "Admin")]
    [HttpGet(RouteConfigs.Products)]
    public IActionResult GetProducts()
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Products);
        return Ok(_adminBL.GetAllProducts());
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
    public IActionResult GetProductByID([FromQuery] Guid id)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Ok(_adminBL.GetProductByID(id));
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
    public IActionResult AddProduct([FromBody] Product p_prod)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Created("Succesfully created a new product!", _adminBL.AddNewProduct(p_prod));
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
    public IActionResult UpdateProduct([FromBody] Product p_prod)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Ok(_adminBL.UpdateProduct(p_prod));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }
  }
}
