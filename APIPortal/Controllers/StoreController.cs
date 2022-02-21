using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPortal.Consts;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIPortal.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StoreController : ControllerBase
  {
    private IStoreManagementBL _storeBL;
    private IInventoryManagementBL _invenBL;
    private IOrderManagementBL _orderBL;
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
    [HttpGet(RouteConfigs.Stores)]
    public IActionResult GetAllStores()
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Stores);
        return Ok(_storeBL.GetAllStoresProfile());
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Stores);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Store/5
    [HttpGet(RouteConfigs.StoreProfile)]
    public IActionResult GetStoreProfileByStoreID([FromQuery] Guid p_storeID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreProfile);
        return Ok(_storeBL.GetStoreProfileByID(p_storeID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreProfile);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Store
    [HttpPut(RouteConfigs.StoreProfile)]
    public IActionResult UpdateStoreProfile([FromBody] StoreFrontProfile p_store)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreProfile);
        return Accepted(_storeBL.UpdateStoreProfile(p_store));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreProfile);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // POST: api/Store
    [HttpPost(RouteConfigs.StoreProfile)]
    public IActionResult AddNewStoreProfile([FromBody] StoreFrontProfile p_store)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreProfile);
        return Created("Successfully created new store front profile!", _storeBL.AddNewStoreFrontProfile(p_store));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreProfile);
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    /*
        STORE INVENTORY MANAGEMENT
    */
    // GET: api/Inventories
    [HttpGet(RouteConfigs.Inventories)]
    public IActionResult GetAllStoreInventories([FromQuery] Guid p_storeID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Inventories);
        return Ok(_invenBL.GetStoreInventoryByStoreID(p_storeID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventories);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Inventory/5
    [HttpGet(RouteConfigs.Inventory)]
    public IActionResult GetInventoryByID([FromBody] Inventory p_inven)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Inventory);
        return Ok(_invenBL.GetInventory(p_inven));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Inventory);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Inventory/5
    [HttpPut(RouteConfigs.Inventory)]
    public IActionResult ReplenishInventoryByID([FromBody] Inventory p_inven)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Inventory);
        _invenBL.ReplenishInventoryByID(p_inven);
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
    [HttpPost(RouteConfigs.Inventory)]
    public IActionResult ImportProduct([FromBody] Inventory p_inven)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Inventory);
        return Ok(_invenBL.ImportNewProduct(p_inven));
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
    [HttpGet(RouteConfigs.StoreOrders)]
    public IActionResult GetAllStoreOrders([FromQuery] Guid p_storeID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreOrders);
        return Ok(_orderBL.GetAllOrdersByStoreID(p_storeID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrders);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Orders
    [HttpGet(RouteConfigs.StoreOrdersFilter)]
    public IActionResult GetAllStoreOrdersWithFilter([FromBody] Guid p_storeID, string p_filter)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreOrdersFilter);
        return Ok(_orderBL.GetAllOrdersByStoreIDWithFilter(p_storeID, p_filter));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrdersFilter);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Order/5
    [HttpGet(RouteConfigs.StoreOrder)]
    public IActionResult GetStoreOrderByID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.StoreOrder);
        return Ok(_orderBL.GetOrderByOrderID(p_orderID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.StoreOrder);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Order/5
    [HttpPut(RouteConfigs.AcceptOrder)]
    public IActionResult AcceptOrderByOrderID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.AcceptOrder);
        _orderBL.AcceptOrderByOrderID(p_orderID);
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
    [HttpPut(RouteConfigs.RejectOrder)]
    public IActionResult RejectOrderByOrderID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.RejectOrder);
        _orderBL.RejectOrderByOrderID(p_orderID);
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
    [HttpPut(RouteConfigs.CompleteOrder)]
    public IActionResult CompleteOrderByOrderID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.CompleteOrder);
        _orderBL.CompleteOrderByOrderID(p_orderID);
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
    [HttpGet(RouteConfigs.Trackings)]
    public IActionResult GetAllTrackingsByOrderID([FromQuery] Guid p_orderID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Trackings);
        return Ok(_orderBL.GetAllTrackingByOrderID(p_orderID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Trackings);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // GET: api/Tracking/5
    [HttpGet(RouteConfigs.Tracking)]
    public IActionResult GetTrackingDetailByID([FromQuery] Guid p_trackingID)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Tracking);
        return Ok(_orderBL.GetTrackingNumberByID(p_trackingID));
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Tracking);
        Log.Warning(e.Message);
        return NotFound(e);
      }
    }

    // PUT: api/Tracking
    [HttpPut(RouteConfigs.Tracking)]
    public IActionResult UpdateTrackingNumber([FromBody] Tracking p_tracking)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Tracking);
        _orderBL.UpdateTracking(p_tracking);
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
    [HttpPost(RouteConfigs.Tracking)]
    public IActionResult AddTrackingNumber([FromBody] Tracking p_tracking)
    {
      try
      {
        Log.Information("Route: " + RouteConfigs.Tracking);
        _orderBL.AddTrackingToOrder(p_tracking.OrderID, p_tracking);
        return Ok();
      }
      catch (Exception e)
      {
        Log.Warning("Route: " + RouteConfigs.Tracking);
        Log.Warning(e.Message);
        return StatusCode(406, e);
      }
    }

    // // DELETE: api/Store/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
