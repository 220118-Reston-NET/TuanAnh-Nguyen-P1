using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class OrderManagementBL : IOrderManagementBL
  {
    private readonly IOrderManagementDL _repo;
    public OrderManagementBL(IOrderManagementDL p_repo)
    {
      _repo = p_repo;
    }
    public async Task AcceptOrderByOrderID(Guid p_orderID)
    {
      await _repo.AcceptOrderByOrderID(p_orderID);
    }

    public async Task<Tracking> AddTrackingToOrder(Guid p_orderID, Tracking p_tracking)
    {
      List<Tracking> _listOfTracking = await GetAllTrackingByOrderID(p_orderID);
      if (_listOfTracking.All(p => p.TrackingNumber != p_tracking.TrackingNumber))
      {
        return await _repo.AddTrackingToOrder(p_orderID, p_tracking);
      }
      else
      {
        throw new Exception("Cannot add this tracking number due to this tracking number is already added to this order!");
      }

    }

    public async Task CancelOrderByOrderID(Guid p_orderID)
    {
      Order _order = await GetOrderByOrderID(p_orderID);
      if (_order.Status != "Order Placed")
      {
        throw new Exception("Cannot cancel order due to the order is preparing to ship now! Please contact store for more detail!");
      }
      else
      {
        await _repo.CancelOrderByOrderID(p_orderID);
      }
    }

    public async Task CompleteOrderByOrderID(Guid p_orderID)
    {
      Order _order = await GetOrderByOrderID(p_orderID);
      if (_order.Shipments.Count == 0)
      {
        throw new Exception("Cannot complete the order! Please add shipment to the order before complete it!");
      }
      await _repo.CompleteOrderByOrderID(p_orderID);
    }

    public async Task<Order> CreateOrder(Order p_order)
    {
      return await _repo.CreateOrder(p_order);
    }

    public async Task<List<Order>> GetAllOrders()
    {
      return await _repo.GetAllOrders();
    }

    public async Task<List<Order>> GetAllOrdersByCustomerID(Guid p_customerID)
    {
      List<Order> _listOfOrder = await GetAllOrders();
      return _listOfOrder.FindAll(p => p.CustomerID.Equals(p_customerID));
    }

    public async Task<List<Order>> GetAllOrdersByCustomerIDWithFilter(Guid p_cusID, string p_filter)
    {
      List<Order> _listOfOrder = await GetAllOrdersByCustomerID(p_cusID);
      return _listOfOrder.FindAll(p => p.Status.Equals(p_filter));
    }

    public async Task<List<Order>> GetAllOrdersByStoreID(Guid p_storeID)
    {
      List<Order> _listOfOrder = await GetAllOrders();
      return _listOfOrder.FindAll(p => p.StoreID.Equals(p_storeID));
    }

    public async Task<List<Order>> GetAllOrdersByStoreIDWithFilter(Guid p_storeID, string p_filter)
    {
      List<Order> _listOfOrder = await GetAllOrdersByStoreID(p_storeID);
      return _listOfOrder.FindAll(p => p.Status.Equals(p_filter));
    }

    public async Task<List<Tracking>> GetAllTrackingByOrderID(Guid p_orderID)
    {
      List<Tracking> _listOfTracking = await GetTrackings();

      return _listOfTracking.FindAll(p => p.OrderID.Equals(p_orderID));
    }

    public async Task<Order> GetOrderByOrderID(Guid p_orderID)
    {
      List<Order> _listOfOrder = await GetAllOrders();
      return _listOfOrder.Find(p => p.OrderID.Equals(p_orderID));
    }

    public async Task<Tracking> GetTrackingNumberByID(Guid p_trackingID)
    {
      List<Tracking> _listOfTracking = await _repo.GetAllTrackings();
      return _listOfTracking.Find(p => p.TrackingID.Equals(p_trackingID));
    }

    public async Task<List<Tracking>> GetTrackings()
    {
      return await _repo.GetAllTrackings();
    }

    public async Task RejectOrderByOrderID(Guid p_orderID)
    {
      await _repo.RejectOrderByOrderID(p_orderID);
    }

    public async Task<Order> UpdateOrder(Order p_order)
    {
      Order _order = await GetOrderByOrderID(p_order.OrderID);
      if (_order.Status != "Order Placed")
      {
        throw new Exception("Cannot update order due to the order is preparing to ship now! Please contact store for more detail!");
      }
      else
      {
        return await _repo.UpdateOrder(p_order);
      }
    }

    public async Task UpdateTracking(Tracking p_tracking)
    {
      await _repo.UpdateTracking(p_tracking);
    }
  }
}