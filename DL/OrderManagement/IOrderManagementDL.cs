using Model;

namespace DL.Interfaces
{
  public interface IOrderManagementDL
  {
    Task<Order> CreateOrder(Order p_order);
    Task<Order> UpdateOrder(Order p_order);
    Task CancelOrderByOrderID(Guid p_orderID);
    Task RejectOrderByOrderID(Guid p_orderID);
    Task AcceptOrderByOrderID(Guid p_orderID);
    Task CompleteOrderByOrderID(Guid p_orderID);
    Task<List<LineItem>> AddLineItemsToOrder(Order p_order);
    Task SubstractInventoryAfterPlacedOrder(Order p_order);
    Task<List<LineItem>> GetLineItemsByOrderID(Guid p_orderID);
    Task<Tracking> AddTrackingToOrder(Guid p_orderID, Tracking p_tracking);
    Task UpdateTracking(Tracking p_tracking);
    Task<List<Tracking>> GetAllTrackingByOrderID(Guid p_orderID);
    Task<List<Tracking>> GetAllTrackings();
    Task<List<Order>> GetAllOrders();
  }
}