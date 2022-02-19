using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using APIPortal.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIPortal.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    private IAdminServiceBL _adminBL;
    public AdminController(IAdminServiceBL p_adminBL)
    {
      _adminBL = p_adminBL;
    }

    // GET: api/Products
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
        return StatusCode(500, e);
      }
    }

    // GET: api/Product/5
    [HttpGet(RouteConfigs.Product)]
    public IActionResult GetProductByID(string id)
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
        return StatusCode(500, e);
      }
    }

    // POST: api/Product
    [HttpPost(RouteConfigs.Product)]
    public IActionResult AddProduct([FromBody] Product p_prod)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Product);
        return Ok(_adminBL.AddNewProduct(p_prod));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Product);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Product
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
        return StatusCode(500, e);
      }
    }

    // // DELETE: api/Admin/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}