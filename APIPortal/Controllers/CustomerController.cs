using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using Model;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPortal.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private ICustomerMangementBL _cusBL;
    private IOrderManagementBL _orderBL;
    public CustomerController(ICustomerMangementBL p_cusBL, IOrderManagementBL p_orderBL)
    {
      _cusBL = p_cusBL;
      _orderBL = p_orderBL;
    }

    /*
      CUSTOMER PROFILE
    */
    // GET: api/Customers
    [HttpGet(RouteConfigs.Customers)]
    public IActionResult GetAllCustomers()
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Customers);
        return Ok(_cusBL.GetAllCustomerProfile());
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Customers);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Customer/5
    [HttpGet(RouteConfigs.CustomerProfile)]
    public IActionResult GetCustomerByID(string p_cusID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerProfile);
        return Ok(_cusBL.GetProfileByID(p_cusID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerProfile);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Customer
    [HttpPut(RouteConfigs.CustomerProfile)]
    public IActionResult UpdateProfile([FromBody] CustomerProfile p_cus)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerProfile);
        return Ok(_cusBL.UpdateProfile(p_cus));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerProfile);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    /*
      CUSTOMER ORDER MANAGEMENT
    */
    // POST: api/Order
    [HttpPost(RouteConfigs.CustomerOrder)]
    public IActionResult CreateOrder([FromBody] Order p_order)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrder);
        return Ok(_orderBL.CreateOrder(p_order));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Order
    [HttpPut(RouteConfigs.CustomerOrder)]
    public IActionResult UpdateOrder([FromBody] Order p_order)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrder);
        return Ok(_orderBL.UpdateOrder(p_order));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Orders
    [HttpGet(RouteConfigs.CustomerOrders)]
    public IActionResult GetAllCustomerOrders(string p_cusID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrders);
        return Ok(_orderBL.GetAllOrdersByCustomerID(p_cusID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrders);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Order/5
    [HttpGet(RouteConfigs.CustomerOrder)]
    public IActionResult GetCustomerOrderByOrderID(string p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrder);
        return Ok(_orderBL.GetOrderByOrderID(p_orderID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }


    // // DELETE: api/Customer/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
