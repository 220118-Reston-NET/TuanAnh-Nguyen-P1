using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPortal.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HomeController : ControllerBase
  {
    private IAdminServiceBL _adminBL;
    private IStoreManagementBL _storeBL;
    public HomeController(IAdminServiceBL p_adminBL, IStoreManagementBL p_storeBL)
    {
      _adminBL = p_adminBL;
      _storeBL = p_storeBL;
    }
    // GET: api/SearchProducts
    [HttpGet(RouteConfigs.SearchProduct)]
    public IActionResult SearchProductByName(string p_prodName)
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
    public IActionResult SearchStoreByName(string p_storeName)
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

    // // GET: api/Home/5
    // [HttpGet("{id}", Name = "Get")]
    // public string Get(int id)
    // {
    //   return "value";
    // }

    // // POST: api/Home
    // [HttpPost]
    // public void Post([FromBody] string value)
    // {
    // }

    // // PUT: api/Home/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE: api/Home/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
