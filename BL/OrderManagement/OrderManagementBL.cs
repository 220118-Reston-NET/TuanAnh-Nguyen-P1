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
    public void AcceptOrderByOrderID(Guid p_orderID)
    {
      _repo.AcceptOrderByOrderID(p_orderID);
    }

    public Tracking AddTrackingToOrder(Guid p_orderID, Tracking p_tracking)
    {
      if (GetAllTrackingByOrderID(p_orderID).All(p => p.TrackingNumber != p_tracking.TrackingNumber))
      {
        return _repo.AddTrackingToOrder(p_orderID, p_tracking);
      }
      else
      {
        throw new Exception("Cannot add this tracking number due to this tracking number is already added to this order!");
      }

    }

    public void CancelOrderByOrderID(Guid p_orderID)
    {
      if (GetOrderByOrderID(p_orderID).Status != "Order Placed")
      {
        throw new Exception("Cannot cancel order due to the order is preparing to ship now! Please contact store for more detail!");
      }
      else
      {
        _repo.CancelOrderByOrderID(p_orderID);
      }
    }

    public void CompleteOrderByOrderID(Guid p_orderID)
    {
      _repo.CompleteOrderByOrderID(p_orderID);
    }

    public Order CreateOrder(Order p_order)
    {
      return _repo.CreateOrder(p_order);
    }

    public List<Order> GetAllOrders()
    {
      return _repo.GetAllOrders();
    }

    public List<Order> GetAllOrdersByCustomerID(Guid p_customerID)
    {
      return GetAllOrders().FindAll(p => p.CustomerID.Equals(p_customerID));
    }

    public List<Order> GetAllOrdersByCustomerIDWithFilter(Guid p_cusID, string p_filter)
    {
      return GetAllOrdersByCustomerID(p_cusID).FindAll(p => p.Status.Equals(p_filter));
    }

    public List<Order> GetAllOrdersByStoreID(Guid p_storeID)
    {
      return GetAllOrders().FindAll(p => p.StoreID.Equals(p_storeID));
    }

    public List<Order> GetAllOrdersByStoreIDWithFilter(Guid p_storeID, string p_filter)
    {
      return GetAllOrdersByStoreID(p_storeID).FindAll(p => p.Status.Equals(p_filter));
    }

    public List<Tracking> GetAllTrackingByOrderID(Guid p_orderID)
    {
      return _repo.GetAllTrackingByOrderID(p_orderID);
    }

    public Order GetOrderByOrderID(Guid p_orderID)
    {
      return GetAllOrders().Find(p => p.OrderID.Equals(p_orderID));
    }

    public Tracking GetTrackingNumberByID(Guid p_trackingID)
    {
      return _repo.GetAllTrackings().Find(p => p.TrackingID.Equals(p_trackingID));
    }

    public void RejectOrderByOrderID(Guid p_orderID)
    {
      _repo.RejectOrderByOrderID(p_orderID);
    }

    public Order UpdateOrder(Order p_order)
    {
      if (GetOrderByOrderID(p_order.OrderID).Status != "Order Placed")
      {
        throw new Exception("Cannot update order due to the order is preparing to ship now! Please contact store for more detail!");
      }
      else
      {
        return _repo.UpdateOrder(p_order);
      }
    }

    public void UpdateTracking(Tracking p_tracking)
    {
      _repo.UpdateTracking(p_tracking);
    }
  }
}