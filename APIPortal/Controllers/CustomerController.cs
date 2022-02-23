using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using Model;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerManagementBL _cusBL;
    private readonly IOrderManagementBL _orderBL;
    public CustomerController(ICustomerManagementBL p_cusBL, IOrderManagementBL p_orderBL)
    {
      _cusBL = p_cusBL;
      _orderBL = p_orderBL;
    }

    /*
      CUSTOMER PROFILE
    */
    // GET: api/Customers
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.Customers)]
    public async Task<IActionResult> GetAllCustomers()
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Customers);
        return Ok(await _cusBL.GetAllCustomerProfile());
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Customers);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Customer/5
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerProfile)]
    public async Task<IActionResult> GetCustomerByID([FromQuery] Guid p_cusID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerProfile);
        return Ok(await _cusBL.GetProfileByID(p_cusID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerProfile);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Customer
    [Authorize(Roles = "Customer")]
    [HttpPut(RouteConfigs.CustomerProfile)]
    public async Task<IActionResult> UpdateProfile([FromBody] CustomerProfile p_cus)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerProfile);
        return Ok(await _cusBL.UpdateProfile(p_cus));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerProfile);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // POST: api/Customer
    [Authorize(Roles = "Customer")]
    [HttpPost(RouteConfigs.CustomerProfile)]
    public async Task<IActionResult> AddProfile([FromBody] CustomerProfile p_cus)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerProfile);
        return Created("Succesfully created new customer profile!", await _cusBL.AddNewCustomerProfile(p_cus));
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
    [Authorize(Roles = "Customer")]
    [HttpPost(RouteConfigs.CustomerOrder)]
    public async Task<IActionResult> CreateOrder([FromBody] Order p_order)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrder);
        return Created("Succesfully created new order!", await _orderBL.CreateOrder(p_order));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // PUT: api/Order
    [Authorize(Roles = "Customer")]
    [HttpPut(RouteConfigs.CustomerOrder)]
    public async Task<IActionResult> UpdateOrder([FromBody] Order p_order)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrder);
        return Ok(await _orderBL.UpdateOrder(p_order));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // GET: api/Orders
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerOrders)]
    public async Task<IActionResult> GetAllCustomerOrders([FromQuery] Guid p_cusID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrders);
        return Ok(await _orderBL.GetAllOrdersByCustomerID(p_cusID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrders);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Orders
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerOrdersFilter)]
    public async Task<IActionResult> GetAllCustomerOrdersWithFilter(Guid p_cusID, string p_filter)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrdersFilter);
        return Ok(await _orderBL.GetAllOrdersByCustomerIDWithFilter(p_cusID, p_filter));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrdersFilter);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Order/5
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerOrder)]
    public async Task<IActionResult> GetCustomerOrderByOrderID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CustomerOrder);
        return Ok(await _orderBL.GetOrderByOrderID(p_orderID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }
  }
}
