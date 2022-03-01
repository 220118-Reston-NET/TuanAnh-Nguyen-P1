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
    [HttpGet(RouteConfigs.SearchProduct)]
    public async Task<IActionResult> GetAllProducts([FromQuery] int limit, int page, string? name)
    {
      try
      {
        List<Product> _listProducts = new List<Product>();
        _listProducts = await _adminBL.GetAllProducts();

        if (name != null)
        {
          _listProducts = _listProducts.FindAll(p => p.Name.ToLower().Contains(name.ToLower()));
        }

        if (_listProducts.Count != 0)
        {
          if (limit != 0)
          {
            PaginationExtensions<Product> _product = new PaginationExtensions<Product>();
            var result = _product.Pagged(_listProducts, limit, page);
            Log.Information("Route: " + RouteConfigs.SearchProduct);
            return Ok(result);
          }
          return Ok(_listProducts);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.SearchProduct);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Home/Stores
    [HttpGet(RouteConfigs.SearchStore)]
    public async Task<IActionResult> SearchStoreByName([FromQuery] int limit, int page, string? name)
    {
      try
      {
        List<StoreFrontProfile> _listStores = new List<StoreFrontProfile>();
        _listStores = await _storeBL.GetAllStoresProfile();
        if (name != null)
        {
          _listStores = _listStores.FindAll(p => p.Name.ToLower().Contains(name.ToLower()));
        }

        if (_listStores.Count != 0)
        {
          if (limit != 0)
          {
            PaginationExtensions<StoreFrontProfile> _storeF = new PaginationExtensions<StoreFrontProfile>();
            var result = _storeF.Pagged(_listStores, limit, page);
            Log.Information("Route: " + RouteConfigs.SearchStore);
            return Ok(result);
          }
          return Ok(_listStores);
        }
        return NotFound();
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
