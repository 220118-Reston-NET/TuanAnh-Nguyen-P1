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
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using APIPortal.FilterAttributes;
using APIPortal.DataTransferObject;
using APIPortal.Extensions;

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [RequestRateLimit(Name = "api/Home", MaximumRequests = 5, Duration = 60)]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerManagementBL _cusBL;
    private readonly IOrderManagementBL _orderBL;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor httpContextAccessor;
    private string given_name;
    public CustomerController(ICustomerManagementBL p_cusBL,
                              IOrderManagementBL p_orderBL,
                              UserManager<IdentityUser> p_userManager,
                              IHttpContextAccessor httpContextAccessor)
    {
      _cusBL = p_cusBL;
      _orderBL = p_orderBL;
      _userManager = p_userManager;
      this.httpContextAccessor = httpContextAccessor;

      var token = httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
      var tokenHandler = new JwtSecurityTokenHandler();
      given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
    }

    /*
      CUSTOMER PROFILE MANAGEMENT
    */

    // GET: api/Customer/Profile
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerProfile)]
    public async Task<IActionResult> GetCustomerProfile()
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_cusID = Guid.Parse(userFromDB.Id);

      try
      {
        var result = await _cusBL.GetProfileByID(p_cusID);
        if (result != null)
        {
          Log.Information("Route: " + RouteConfigs.CustomerProfile);
          return Ok(result);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerProfile);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Customer/Profile
    [Authorize(Roles = "Customer")]
    [HttpPut(RouteConfigs.CustomerProfile)]
    public async Task<IActionResult> UpdateProfile(CustomerProfile p_cus)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_cusID = Guid.Parse(userFromDB.Id);

      if (p_cus.CustomerID == Guid.Empty)
      {
        p_cus.CustomerID = p_cusID;
      }
      if (p_cus.CustomerID != p_cusID)
      {
        return BadRequest(new { Warning = "Please don't try to do something bad! You're not allowed to edit other profile!" });
      }

      try
      {
        p_cus.CustomerID = p_cusID;
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

    /*
      CUSTOMER ORDER MANAGEMENT
    */

    // GET: api/Customer/Orders
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerOrders)]
    public async Task<IActionResult> GetAllCustomerOrders([FromQuery] int limit, int page)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_cusID = Guid.Parse(userFromDB.Id);

      try
      {
        var _listOrders = await _orderBL.GetAllOrdersByCustomerID(p_cusID);
        if (_listOrders.Count != 0)
        {
          if (limit != 0)
          {
            PaggedExtensions<Order> _order = new PaggedExtensions<Order>();
            var result = _order.Pagged(_listOrders, limit, page);
            Log.Information("Route: " + RouteConfigs.CustomerOrders);
            return Ok(result);
          }
          return Ok(_listOrders);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrders);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Customer/Orders/5
    [Authorize(Roles = "Customer")]
    [HttpGet(RouteConfigs.CustomerOrder)]
    public async Task<IActionResult> GetCustomerOrder(Guid p_orderID)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_cusID = Guid.Parse(userFromDB.Id);

      try
      {
        var _listOrders = await _orderBL.GetAllOrders();
        var result = _listOrders.Find(p => p.CustomerID.Equals(p_cusID) && p.OrderID.Equals(p_orderID));

        if (result != null)
        {
          Log.Information("Route: " + RouteConfigs.CustomerOrder);
          return Ok(result);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // POST: api/Customer/Orders
    [Authorize(Roles = "Customer")]
    [HttpPost(RouteConfigs.CustomerOrders)]
    public async Task<IActionResult> CreateOrder([FromBody] Order p_order)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_cusID = Guid.Parse(userFromDB.Id);
      CustomerProfile _cusInfo = await _cusBL.GetProfileByID(p_cusID);

      if (_cusInfo == null)
      {
        return BadRequest("Please check the customer id!");
      }

      if (string.IsNullOrEmpty(_cusInfo.FirstName))
      {
        return BadRequest(new { Result = "Firstname should not be empty! You need to update your profile before placing order!" });
      }
      if (string.IsNullOrEmpty(_cusInfo.LastName))
      {
        return BadRequest(new { Result = "Lastname should not be empty! You need to update your profile before placing order!" });
      }
      if (string.IsNullOrEmpty(_cusInfo.Address))
      {
        return BadRequest(new { Result = "Address should not be empty! You need to update your profile before placing order!" });
      }

      try
      {
        p_order.CustomerID = p_cusID;
        Log.Information("Route: " + RouteConfigs.CustomerOrders);
        return Created("Succesfully created new order!", await _orderBL.CreateOrder(p_order));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrders);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Customer/Orders/5
    [Authorize(Roles = "Customer")]
    [HttpPut(RouteConfigs.CustomerOrder)]
    public async Task<IActionResult> UpdateOrder(Guid p_orderID, [FromBody] Order p_order)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_cusID = Guid.Parse(userFromDB.Id);

      try
      {
        var _listOrder = await _orderBL.GetAllOrders();
        var result = _listOrder.Find(p => p.CustomerID.Equals(p_cusID) & p.OrderID.Equals(p_orderID));

        if (result != null)
        {
          p_order.CustomerID = p_cusID;
          p_order.OrderID = p_orderID;
          Log.Information("Route: " + RouteConfigs.CustomerOrder);
          return Ok(await _orderBL.UpdateOrder(p_order));
        }
        return BadRequest("Please check the order ID");
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CustomerOrder);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }







    //TODO FILTERRRRR
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
  }
}
