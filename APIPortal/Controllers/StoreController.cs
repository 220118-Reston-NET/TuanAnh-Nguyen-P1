using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIPortal.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class StoreController : ControllerBase
  {
    private readonly IStoreManagementBL _storeBL;
    private readonly IInventoryManagementBL _invenBL;
    private readonly IOrderManagementBL _orderBL;
    public StoreController(IStoreManagementBL p_storeBL, IInventoryManagementBL p_invenBL, IOrderManagementBL p_orderBL)
    {
      _storeBL = p_storeBL;
      _invenBL = p_invenBL;
      _orderBL = p_orderBL;
    }
    /*
        STORE MANAGEMENT
    */
    // GET: api/Stores
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Stores)]
    public async Task<IActionResult> GetAllStores()
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Stores);
        return Ok(await _storeBL.GetAllStoresProfile());
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Stores);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Store/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreProfile)]
    public async Task<IActionResult> GetStoreProfileByStoreID([FromQuery] Guid p_storeID)
    {
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

    // PUT: api/Store
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.StoreProfile)]
    public async Task<IActionResult> UpdateStoreProfile([FromBody] StoreFrontProfile p_store)
    {
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
    // GET: api/Inventories
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Inventories)]
    public async Task<IActionResult> GetAllStoreInventories([FromQuery] Guid p_storeID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Inventories);
        return Ok(await _invenBL.GetStoreInventoryByStoreID(p_storeID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventories);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Inventory/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Inventory)]
    public async Task<IActionResult> GetInventoryByID([FromBody] Inventory p_inven)
    {
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

    // PUT: api/Inventory/5
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.Inventory)]
    public async Task<IActionResult> ReplenishInventoryByID([FromBody] Inventory p_inven)
    {
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
    public async Task<IActionResult> ImportProduct([FromBody] Inventory p_inven)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Inventory);
        return Ok(await _invenBL.ImportNewProduct(p_inven));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventory);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    /*
        STORE ORDERS MANAGEMENT
    */
    // GET: api/Orders
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreOrders)]
    public async Task<IActionResult> GetAllStoreOrders([FromQuery] Guid p_storeID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreOrders);
        return Ok(await _orderBL.GetAllOrdersByStoreID(p_storeID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrders);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Orders
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreOrdersFilter)]
    public async Task<IActionResult> GetAllStoreOrdersWithFilter([FromBody] Guid p_storeID, string p_filter)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreOrdersFilter);
        return Ok(await _orderBL.GetAllOrdersByStoreIDWithFilter(p_storeID, p_filter));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrdersFilter);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Order/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.StoreOrder)]
    public async Task<IActionResult> GetStoreOrderByID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreOrder);
        return Ok(await _orderBL.GetOrderByOrderID(p_orderID));
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
    public async Task<IActionResult> AcceptOrderByOrderID([FromQuery] Guid p_orderID)
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
    public async Task<IActionResult> RejectOrderByOrderID([FromQuery] Guid p_orderID)
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

    // PUT: api/Order/5
    [Authorize(Roles = "StoreManager")]
    [HttpPut(RouteConfigs.CompleteOrder)]
    public async Task<IActionResult> CompleteOrderByOrderID([FromQuery] Guid p_orderID)
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
        return StatusCode(406, e);
      }
    }

    // GET: api/Trackings
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Trackings)]
    public async Task<IActionResult> GetAllTrackingsByOrderID([FromQuery] Guid p_orderID)
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

    // GET: api/Tracking/5
    [Authorize(Roles = "StoreManager")]
    [HttpGet(RouteConfigs.Tracking)]
    public async Task<IActionResult> GetTrackingDetailByID([FromQuery] Guid p_trackingID)
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
    public async Task<IActionResult> UpdateTrackingNumber([FromBody] Tracking p_tracking)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Tracking);
        await _orderBL.UpdateTracking(p_tracking);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Tracking);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // POST: api/Tracking
    [Authorize(Roles = "StoreManager")]
    [HttpPost(RouteConfigs.Tracking)]
    public async Task<IActionResult> AddTrackingNumber([FromBody] Tracking p_tracking)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Tracking);
        await _orderBL.AddTrackingToOrder(p_tracking.OrderID, p_tracking);
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
