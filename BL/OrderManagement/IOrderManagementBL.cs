using Model;

namespace BL.Interfaces
{
  public interface IOrderManagementBL
  {
    /* 
      CUSTOMER MANAGEMENT:
      Order CreateOrder(Order p_order);
      Order UpdateOrder(Order p_order);
      void CancelOrderByOrderID(string p_orderID);
      List<Order> GetAllOrdersByCustomerID(string p_customerID);
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
    void CancelOrderByOrderID(string p_orderID);

    /// <summary>
    /// Get All the Orders of Customer
    /// </summary>
    /// <param name="p_customerID"></param>
    /// <returns>All Orders of Custoemr</returns>
    List<Order> GetAllOrdersByCustomerID(string p_customerID);


    /* 
      STORE MANAGEMENT:
      void RejectOrderByOrderID(string p_orderID);
      void AcceptOrderByOrderID(string p_orderID);
      v
      Tracking AddTrackingToOrder(Tracking p_tracking);
      Tracking GetTrackingNumberByID(string p_trackingID);
      void UpdateTracking(Tracking p_tracking);
      List<Order> GetAllOrdersByStoreID(string p_storeID);
      List<Tracking> GetAllTrackingByOrderID(string p_orderID);
    */

    /// <summary>
    /// Reject the order if something went wrong in the order
    /// </summary>
    /// <param name="p_orderID"></param>
    void RejectOrderByOrderID(string p_orderID);

    /// <summary>
    /// Accept the order to start fullfill order
    /// </summary>
    /// <param name="p_orderID"></param>
    void AcceptOrderByOrderID(string p_orderID);

    /// <summary>
    /// Complete Order After Added Tracking NUmber and ship
    /// </summary>
    /// <param name="p_orderID"></param>
    void CompleteOrderByOrderID(string p_orderID);

    /// <summary>
    /// Add a new tracking number to the order
    /// </summary>
    /// <param name="p_tracking"></param>
    /// <returns></returns>
    Tracking AddTrackingToOrder(Tracking p_tracking);

    /// <summary>
    /// Get tracking number by tracking ID 
    /// </summary>
    /// <param name="p_trackingID"></param>
    /// <returns></returns>
    Tracking GetTrackingNumberByID(string p_trackingID);

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
    List<Order> GetAllOrdersByStoreID(string p_storeID);

    /// <summary>
    /// Get all tracking number by order ID
    /// </summary>
    /// <param name="p_orderID"></param>
    /// <returns></returns>
    List<Tracking> GetAllTrackingByOrderID(string p_orderID);

    /*
      ALL:
      List<Order> GetAllOrders();
      Order GetOrderByOrderID(string p_orderID);
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
    Order GetOrderByOrderID(string p_orderID);
  }
}