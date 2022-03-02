using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using APIPortal.DataTransferObject;
using APIPortal.Extensions;
using APIPortal.FilterAttributes;
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [RequestRateLimit(Name = "api/Store", MaximumRequests = 10, Duration = 60)]
  [ApiController]
  public class StoreController : ControllerBase
  {
    private readonly IStoreManagementBL _storeBL;
    private readonly IInventoryManagementBL _invenBL;
    private readonly IOrderManagementBL _orderBL;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string? given_name;
    public StoreController(IStoreManagementBL p_storeBL,
                            IInventoryManagementBL p_invenBL,
                            IOrderManagementBL p_orderBL,
                            UserManager<IdentityUser> p_userManager,
                            IHttpContextAccessor p_httpContextAccessor)
    {
      _storeBL = p_storeBL;
      _invenBL = p_invenBL;
      _orderBL = p_orderBL;
      _userManager = p_userManager;
      _httpContextAccessor = p_httpContextAccessor;

      var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
      var tokenHandler = new JwtSecurityTokenHandler();
      given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
    }
    /*
        STORE PROFILE MANAGEMENT
    */

    // GET: api/Store/Profile
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreProfile)]
    public async Task<IActionResult> GetStoreProfile()
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);

      try
      {
        Log.Information("Route: " + RouteConfigs.StoreProfile);
        return Ok(await _storeBL.GetStoreProfileByID(p_storeID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreProfile);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Store/Profile
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.StoreProfile)]
    public async Task<IActionResult> UpdateStoreProfile([FromBody] StoreFrontProfile p_store)
    {

      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);

      if (p_store.StoreID == Guid.Empty)
      {
        p_store.StoreID = p_storeID;
      }
      else if (p_store.StoreID != p_storeID)
      {
        return BadRequest(new { Warning = "Please don't try to do something bad! You're not allowed to edit other store!" });
      }

      try
      {
        Log.Information("Route: " + RouteConfigs.StoreProfile);
        return Accepted(await _storeBL.UpdateStoreProfile(p_store));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreProfile);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    /*
        STORE INVENTORY MANAGEMENT
    */
    // GET: api/Store/Inventories
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Inventories)]
    public async Task<IActionResult> GetAllStoreInventories([FromQuery] int limit, int page, string? orderby, int filter)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);

      try
      {
        var _listInventories = await _invenBL.GetStoreInventoryByStoreID(p_storeID);
        if (_listInventories.Count != 0)
        {
          if (orderby != null || filter != 0)
          {
            OrderByAndFilterExtension _orderFilter = new OrderByAndFilterExtension();

            _listInventories = _orderFilter.OrderByAndFilterExtensionForInventory(_listInventories, orderby, filter);
          }
          if (limit != 0)
          {
            PaginationExtensions<Inventory> _inven = new PaginationExtensions<Inventory>();
            var result = _inven.Pagged(_listInventories, limit, page);
            Log.Information("Route: " + RouteConfigs.Inventories);
            return Ok(result);
          }
          Log.Information("Route: " + RouteConfigs.Inventories);
          return Ok(_listInventories);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventories);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Inventories/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Inventory)]
    public async Task<IActionResult> GetInventory(Guid p_prodID)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);
      Inventory p_inven = new Inventory();
      p_inven.StoreID = p_storeID;
      p_inven.ProductID = p_prodID;

      try
      {
        Log.Information("Route: " + RouteConfigs.Inventory);
        return Ok(await _invenBL.GetInventory(p_inven));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventory);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Inventories/5?p_quantity=5
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.Inventory)]
    public async Task<IActionResult> ReplenishInventory(Guid p_prodID, [FromQuery] int p_quantity)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);
      Inventory p_inven = new Inventory();
      p_inven.StoreID = p_storeID;
      p_inven.ProductID = p_prodID;
      p_inven.Quantity = p_quantity;

      try
      {
        Log.Information("Route: " + RouteConfigs.Inventory);
        await _invenBL.ReplenishInventoryByID(p_inven);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventory);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // POST: api/Inventory
    [Authorize(Roles = "StoreManager")]
    [HttpPost(RouteConfigs.Inventory)]
    public async Task<IActionResult> ImportProduct(Guid p_prodID, [FromQuery] int p_quantity)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);
      Inventory p_inven = new Inventory();
      p_inven.StoreID = p_storeID;
      p_inven.ProductID = p_prodID;
      p_inven.Quantity = p_quantity;

      Task taskImportProduct = _invenBL.ImportNewProduct(p_inven);
      try
      {
        await taskImportProduct;
        Log.Information("Route: " + RouteConfigs.Inventory);
        return Ok(new { Results = "Imported new product successfully!" });
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventory);
        Log.Warning(e.Message);
        return BadRequest(new { Results = "This Product is already in the store's inventory" });
      }
    }

    /*
        STORE ORDERS MANAGEMENT
    */
    // GET: api/Store/Orders
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreOrders)]
    public async Task<IActionResult> GetAllStoreOrders([FromQuery] int limit, int page, string? orderby, string? filter)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);

      try
      {
        var _listOrders = await _orderBL.GetAllOrdersByStoreID(p_storeID);
        if (_listOrders.Count != 0)
        {
          if (orderby != null || filter != null)
          {
            OrderByAndFilterExtension _orderFilter = new OrderByAndFilterExtension();

            _listOrders = _orderFilter.OrderByAndFilterExtensionForOrder(_listOrders, orderby, filter);
          }
          if (limit != 0)
          {
            PaginationExtensions<Order> _order = new PaginationExtensions<Order>();
            var result = _order.Pagged(_listOrders, limit, page);
            Log.Information("Route: " + RouteConfigs.StoreOrders);
            return Ok(result);
          }
          return Ok(_listOrders);
        }
        return NotFound();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrders);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Store/Orders/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreOrder)]
    public async Task<IActionResult> GetStoreOrderByID(Guid p_orderID)
    {
      var userFromDB = await _userManager.FindByNameAsync(given_name);
      Guid p_storeID = Guid.Parse(userFromDB.Id);

      try
      {
        var _listOrders = await _orderBL.GetAllOrders();
        var _order = _listOrders.Find(p => p.StoreID.Equals(p_storeID) && p.OrderID.Equals(p_orderID));
        Log.Information("Route: " + RouteConfigs.StoreOrder);
        return Ok(_order);
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrder);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Order/5
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.AcceptOrder)]
    public async Task<IActionResult> AcceptOrderByOrderID(Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.AcceptOrder);
        await _orderBL.AcceptOrderByOrderID(p_orderID);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.AcceptOrder);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // PUT: api/Order/5
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.RejectOrder)]
    public async Task<IActionResult> RejectOrderByOrderID(Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.RejectOrder);
        await _orderBL.RejectOrderByOrderID(p_orderID);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.RejectOrder);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // PUT: api/Orders/5
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.CompleteOrder)]
    public async Task<IActionResult> CompleteOrderByOrderID(Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CompleteOrder);
        await _orderBL.CompleteOrderByOrderID(p_orderID);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.CompleteOrder);
        Log.Warning(e.Message);
        return BadRequest(new { Results = "Cannot complete the order! Please add shipment to the order before complete it!" });
      }
    }

    // GET: api/Orders/5/Trackings
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Trackings)]
    public async Task<IActionResult> GetAllTrackingsByOrderID(Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Trackings);
        return Ok(await _orderBL.GetAllTrackingByOrderID(p_orderID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Trackings);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // POST: api/Orders/5/Trackings?p_trackingNumber=5
    [Authorize(Roles = "StoreManager")]
    [HttpPost(RouteConfigs.Trackings)]
    public async Task<IActionResult> AddTrackingNumber(Guid p_orderID, [FromQuery] string p_trackingNumber)
    {
      try
      {
        Tracking _tracking = new Tracking();
        _tracking.OrderID = p_orderID;
        _tracking.TrackingNumber = p_trackingNumber;
        Log.Information("Route: " + RouteConfigs.Trackings);
        await _orderBL.AddTrackingToOrder(p_orderID, _tracking);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Trackings);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // GET: api/Orders/5/Trackings/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Tracking)]
    public async Task<IActionResult> GetTrackingDetailByID(Guid p_orderID, Guid p_trackingID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Tracking);
        return Ok(await _orderBL.GetTrackingNumberByID(p_trackingID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Tracking);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Tracking
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.Tracking)]
    public async Task<IActionResult> UpdateTrackingNumber(Guid p_orderID, Guid p_trackingID, [FromQuery] string p_trackingNo)
    {
      try
      {
        Tracking _updatedTracking = new Tracking()
        {
          TrackingID = p_trackingID,
          OrderID = p_orderID,
          TrackingNumber = p_trackingNo
        };
        Log.Information("Route: " + RouteConfigs.Tracking);
        await _orderBL.UpdateTracking(_updatedTracking);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Tracking);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }
  }
}
