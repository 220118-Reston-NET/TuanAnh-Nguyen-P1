using Model;

namespace DL.Interfaces
{
  public interface IOrderManagementDL
  {
    Order CreateOrder(Order p_order);
    Order UpdateOrder(Order p_order);
    void CancelOrderByOrderID(string p_orderID);
    void RejectOrderByOrderID(string p_orderID);
    void AcceptOrderByOrderID(string p_orderID);
    Tracking AddTrackingToOrder(Tracking p_tracking);
    void UpdateTracking(Tracking p_tracking);
    List<Tracking> GetAllTrackingByOrderID(string p_orderID);
    List<Order> GetAllOrders();
  }
}