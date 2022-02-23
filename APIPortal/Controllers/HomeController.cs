using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPortal.Controllers
{
  [AllowAnonymous]
  [Route("api/[controller]")]
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
    // GET: api/SearchProducts
    [HttpGet(RouteConfigs.SearchProduct)]
    public IActionResult SearchProductByName([FromQuery] string p_prodName)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.SearchProduct);
        return Ok(_adminBL.SearchProductByName(p_prodName));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.SearchProduct);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/SearchStores
    [HttpGet(RouteConfigs.SearchStore)]
    public IActionResult SearchStoreByName([FromQuery] string p_storeName)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.SearchStore);
        return Ok(_storeBL.SearchStoreByName(p_storeName));
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
