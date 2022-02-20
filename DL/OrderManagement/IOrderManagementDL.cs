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
    void CompleteOrderByOrderID(string p_orderID);
    List<LineItem> AddLineItemsToOrder(Order p_order);
    void SubstractInventoryAfterPlacedOrder(Order p_order);
    List<LineItem> GetLineItemsByOrderID(string p_orderID);
    Tracking AddTrackingToOrder(string p_orderID, Tracking p_tracking);
    void UpdateTracking(Tracking p_tracking);
    List<Tracking> GetAllTrackingByOrderID(string p_orderID);
    List<Order> GetAllOrders();
  }
}