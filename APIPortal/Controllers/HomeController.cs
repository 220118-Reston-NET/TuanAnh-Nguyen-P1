using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using APIPortal.Extensions;
using APIPortal.FilterAttributes;
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIPortal.Controllers
{
  [AllowAnonymous]
  [Route("api/[controller]")]
  [RequestRateLimit(Name = "api/Home", MaximumRequests = 10, Duration = 60)]
  [ApiController]
  public class HomeController : ControllerBase
  {
    private readonly IAdminServiceBL _adminBL;
    private readonly IStoreManagementBL _storeBL;
    public HomeController(IAdminServiceBL p_adminBL, IStoreManagementBL p_storeBL)
    {
      _adminBL = p_adminBL;
      _storeBL = p_storeBL;
    }

    // GET: api/Home/Products
    [HttpGet(RouteConfigs.Products)]
    public async Task<IActionResult> GetAllProducts([FromQuery] int limit, int page)
    {
      try
      {
        var _listProducts = await _adminBL.GetAllProducts();
        if (_listProducts.Count != 0)
        {
          if (limit != 0)
          {
            PaggedExtensions<Product> _product = new PaggedExtensions<Product>();
            var result = _product.Pagged(_listProducts, limit, page);
            Log.Information("Route: " + RouteConfigs.Products);
            return Ok(result);
          }
          return Ok(_listProducts);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Products);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Home/SearchProducts
    [HttpGet(RouteConfigs.SearchProduct)]
    public async Task<IActionResult> SearchProductByName([FromQuery] string p_prodName)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.SearchProduct);
        return Ok(await _adminBL.SearchProductByName(p_prodName));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.SearchProduct);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Home/SearchStores
    [HttpGet(RouteConfigs.SearchStore)]
    public async Task<IActionResult> SearchStoreByName([FromQuery] string p_storeName)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.SearchStore);
        return Ok(await _storeBL.SearchStoreByName(p_storeName));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.SearchStore);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }
  }
}
