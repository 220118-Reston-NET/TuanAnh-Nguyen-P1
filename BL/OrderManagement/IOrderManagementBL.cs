using Model;

namespace BL.Interfaces
{
  public interface IOrderManagementBL
  {
    /* 
      CUSTOMER MANAGEMENT:
      Order CreateOrder(Order p_order);
      Order UpdateOrder(Order p_order);
      void CancelOrderByOrderID(Guid p_orderID);
      List<Order> GetAllOrdersByCustomerID(Guid p_customerID);
      List<Order> GetAllOrdersByCustomerIDWithFilter(Guid p_cusID, string p_filter);
    */

    /// <summary>
    /// Creat a new order
    /// </summary>
    /// <param name="p_order"></param>
    /// <returns></returns>
    Order CreateOrder(Order p_order);

    /// <summary>
    /// Allow customer to update information of the order after placed, but before the store accepted it
    /// </summary>
    /// <param name="p_order"></param>
    /// <returns></returns>
    Order UpdateOrder(Order p_order);

    /// <summary>
    /// Allow customer to cancel the order after placed, but before the store accepted it
    /// </summary>
    /// <param name="p_orderID"></param>
    void CancelOrderByOrderID(Guid p_orderID);

    /// <summary>
    /// Get All the Orders of Customer
    /// </summary>
    /// <param name="p_customerID"></param>
    /// <returns>All Orders of Custoemr</returns>
    List<Order> GetAllOrdersByCustomerID(Guid p_customerID);

    /// <summary>
    /// Get All order of customer with filter
    /// </summary>
    /// <param name="p_cusID"></param>
    /// <param name="p_filter"></param>
    /// <returns></returns>
    List<Order> GetAllOrdersByCustomerIDWithFilter(Guid p_cusID, string p_filter);


    /* 
      STORE MANAGEMENT:
      void RejectOrderByOrderID(Guid p_orderID);
      void AcceptOrderByOrderID(Guid p_orderID);
      void CompleteOrderByOrderID(Guid p_orderID);
      Tracking AddTrackingToOrder(Guid p_orderID, Tracking p_tracking);
      Tracking GetTrackingNumberByID(Guid p_trackingID);
      void UpdateTracking(Tracking p_tracking);
      List<Order> GetAllOrdersByStoreID(Guid p_storeID);
      List<Order> GetAllOrdersByStoreIDWithFilter(Guid p_storeID, string p_filter);
      List<Tracking> GetAllTrackingByOrderID(Guid p_orderID);
    */

    /// <summary>
    /// Reject the order if something went wrong in the order
    /// </summary>
    /// <param name="p_orderID"></param>
    void RejectOrderByOrderID(Guid p_orderID);

    /// <summary>
    /// Accept the order to start fullfill order
    /// </summary>
    /// <param name="p_orderID"></param>
    void AcceptOrderByOrderID(Guid p_orderID);

    /// <summary>
    /// Complete Order After Added Tracking NUmber and ship
    /// </summary>
    /// <param name="p_orderID"></param>
    void CompleteOrderByOrderID(Guid p_orderID);

    /// <summary>
    /// Add a new tracking number to the order
    /// </summary>
    /// <param name="p_tracking"></param>
    /// <returns></returns>
    Tracking AddTrackingToOrder(Guid p_orderID, Tracking p_tracking);

    /// <summary>
    /// Get tracking number by tracking ID 
    /// </summary>
    /// <param name="p_trackingID"></param>
    /// <returns></returns>
    Tracking GetTrackingNumberByID(Guid p_trackingID);

    /// <summary>
    /// Update the tracking number
    /// </summary>
    /// <param name="p_trackingID"></param>
    void UpdateTracking(Tracking p_tracking);

    /// <summary>
    /// Get All the Order of Stores
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns>All the Orders of Stores</returns>
    List<Order> GetAllOrdersByStoreID(Guid p_storeID);

    /// <summary>
    /// Get All the Order of Store with Filter
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <param name="p_filter"></param>
    /// <returns></returns>
    List<Order> GetAllOrdersByStoreIDWithFilter(Guid p_storeID, string p_filter);

    /// <summary>
    /// Get all tracking number by order ID
    /// </summary>
    /// <param name="p_orderID"></param>
    /// <returns></returns>
    List<Tracking> GetAllTrackingByOrderID(Guid p_orderID);

    /*
      ALL:
      List<Order> GetAllOrders();
      Order GetOrderByOrderID(Guid p_orderID);
    */
    /// <summary>
    /// Get All Orders in the System
    /// </summary>
    /// <returns>All Orders</returns>
    List<Order> GetAllOrders();

    /// <summary>
    /// Get Order Detail by Order ID
    /// </summary>
    /// <param name="p_orderID"></param>
    /// <returns></returns>
    Order GetOrderByOrderID(Guid p_orderID);
  }
}