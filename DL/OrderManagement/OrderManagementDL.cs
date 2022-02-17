using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class OrderManagementDL : IOrderManagementDL
  {
    public void AcceptOrderByOrderID(string p_orderID)
    {
      throw new NotImplementedException();
    }

    public Tracking AddTrackingToOrder(Tracking p_tracking)
    {
      throw new NotImplementedException();
    }

    public void CancelOrderByOrderID(string p_orderID)
    {
      throw new NotImplementedException();
    }

    public Order CreateOrder(Order p_order)
    {
      throw new NotImplementedException();
    }

    public List<Order> GetAllOrders()
    {
      throw new NotImplementedException();
    }

    public List<Tracking> GetAllTrackingByOrderID(string p_orderID)
    {
      throw new NotImplementedException();
    }

    public void RejectOrderByOrderID(string p_orderID)
    {
      throw new NotImplementedException();
    }

    public Order UpdateOrder(Order p_order)
    {
      throw new NotImplementedException();
    }

    public void UpdateTrackingByTrackingID(Tracking p_trackingID)
    {
      throw new NotImplementedException();
    }
  }
}