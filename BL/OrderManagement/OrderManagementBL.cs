using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class OrderManagementBL : IOrderManagementBL
  {
    private IOrderManagementDL _repo;
    public OrderManagementBL(IOrderManagementDL p_repo)
    {
      _repo = p_repo;
    }
    public void AcceptOrderByOrderID(string p_orderID)
    {
      _repo.AcceptOrderByOrderID(p_orderID);
    }

    public Tracking AddTrackingToOrder(string p_orderID, Tracking p_tracking)
    {
      return _repo.AddTrackingToOrder(p_orderID, p_tracking);
    }

    public void CancelOrderByOrderID(string p_orderID)
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

    public void CompleteOrderByOrderID(string p_orderID)
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

    public List<Order> GetAllOrdersByCustomerID(string p_customerID)
    {
      return GetAllOrders().FindAll(p => p.CustomerID.Equals(p_customerID));
    }

    public List<Order> GetAllOrdersByStoreID(string p_storeID)
    {
      return GetAllOrders().FindAll(p => p.StoreID.Equals(p_storeID));
    }

    public List<Tracking> GetAllTrackingByOrderID(string p_orderID)
    {
      return _repo.GetAllTrackingByOrderID(p_orderID);
    }

    public Order GetOrderByOrderID(string p_orderID)
    {
      return GetAllOrders().Find(p => p.OrderID.ToString().Contains(p_orderID));
    }

    public Tracking GetTrackingNumberByID(string p_trackingID)
    {
      return GetTrackingNumberByID(p_trackingID);
    }

    public void RejectOrderByOrderID(string p_orderID)
    {
      _repo.RejectOrderByOrderID(p_orderID);
    }

    public Order UpdateOrder(Order p_order)
    {
      if (GetOrderByOrderID(p_order.OrderID.ToString()).Status != "Order Placed")
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