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
    private ICustomerManagementBL _cusBL;
    private IOrderManagementBL _orderBL;
    public CustomerController(ICustomerManagementBL p_cusBL, IOrderManagementBL p_orderBL)
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
        return NotFound(e);
      }
    }

    // GET: api/Customer/5
    [HttpGet(RouteConfigs.CustomerProfile)]
    public IActionResult GetCustomerByID([FromQuery] Guid p_cusID)
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
        return NotFound(e);
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
        return StatusCode(406, e);
      }
    }

    // POST: api/Customer
    [HttpPost(RouteConfigs.CustomerProfile)]
    public IActionResult AddProfile([FromBody] CustomerProfile p_cus)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerProfile);
        return Created("Succesfully created new customer profile!", _cusBL.AddNewCustomerProfile(p_cus));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerProfile);
        Log.Warning(e.Message);
        return StatusCode(406, e);
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
        return Created("Succesfully created new order!", _orderBL.CreateOrder(p_order));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(406, e);
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
        return StatusCode(406, e);
      }
    }

    // GET: api/Orders
    [HttpGet(RouteConfigs.CustomerOrders)]
    public IActionResult GetAllCustomerOrders([FromQuery] Guid p_cusID)
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
        return NotFound(e);
      }
    }

    // GET: api/Orders
    [HttpGet(RouteConfigs.CustomerOrdersFilter)]
    public IActionResult GetAllCustomerOrdersWithFilter(Guid p_cusID, string p_filter)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrdersFilter);
        return Ok(_orderBL.GetAllOrdersByCustomerIDWithFilter(p_cusID, p_filter));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrdersFilter);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Order/5
    [HttpGet(RouteConfigs.CustomerOrder)]
    public IActionResult GetCustomerOrderByOrderID([FromQuery] Guid p_orderID)
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
        return NotFound(e);
      }
    }


    // // DELETE: api/Customer/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
