using Model;

namespace DL.Interfaces
{
  public interface IOrderManagementDL
  {
    Order CreateOrder(Order p_order);
    Order UpdateOrder(Order p_order);
    void CancelOrderByOrderID(Guid p_orderID);
    void RejectOrderByOrderID(Guid p_orderID);
    void AcceptOrderByOrderID(Guid p_orderID);
    void CompleteOrderByOrderID(Guid p_orderID);
    List<LineItem> AddLineItemsToOrder(Order p_order);
    void SubstractInventoryAfterPlacedOrder(Order p_order);
    List<LineItem> GetLineItemsByOrderID(Guid p_orderID);
    Tracking AddTrackingToOrder(Guid p_orderID, Tracking p_tracking);
    void UpdateTracking(Tracking p_tracking);
    List<Tracking> GetAllTrackingByOrderID(Guid p_orderID);
    List<Tracking> GetAllTrackings();
    List<Order> GetAllOrders();
  }
}