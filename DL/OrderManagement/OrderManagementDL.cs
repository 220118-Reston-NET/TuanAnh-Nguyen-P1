using System.Data.SqlClient;
using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class OrderManagementDL : IOrderManagementDL
  {
    private readonly string _connectionString;
    public OrderManagementDL(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public void AcceptOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Processing");

        command.ExecuteNonQuery();
      }
    }

    public List<LineItem> AddLineItemsToOrder(Order p_order)
    {
      string _sqlQuery = @"INSERT INTO LineItems
                          (productID, orderID, quantity, priceAtCheckedOut)
                          VALUES(@productID, @orderID, @quantity, @priceAtCheckedOut);";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand();

        foreach (var item in p_order.Cart)
        {
          command = new SqlCommand(_sqlQuery, conn);

          command.Parameters.AddWithValue("@productID", item.ProductID);
          command.Parameters.AddWithValue("@orderID", p_order.OrderID);
          command.Parameters.AddWithValue("@quantity", item.Quantity);
          command.Parameters.AddWithValue("@priceAtCheckedOut", item.PriceAtCheckedOut);

          command.ExecuteNonQuery();
        }
      }

      return p_order.Cart;
    }

    public Tracking AddTrackingToOrder(Guid p_orderID, Tracking p_tracking)
    {
      string _sqlQuery = @"INSERT INTO Tracking
                          (trackingID, orderID, trackingNumber)
                          VALUES(@trackingID, @orderID, @trackingNumber);";

      p_tracking.TrackingID = Guid.NewGuid();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@trackingID", p_tracking.TrackingID);
        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@trackingNumber", p_tracking.TrackingNumber);

        command.ExecuteNonQuery();
      }

      return p_tracking;
    }

    public void CancelOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Cancelled");

        command.ExecuteNonQuery();
      }
    }

    public void CompleteOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Shipped");

        command.ExecuteNonQuery();
      }
    }

    public Order CreateOrder(Order p_order)
    {
      string _sqlQuery = @"INSERT INTO Orders
                          (orderID, cusID, storeID, totalPrice, orderStatus, createdAt)
                          VALUES(@orderID, @cusID, @storeID, @totalPrice, @orderStatus, @createdAt);";

      p_order.OrderID = Guid.NewGuid();
      p_order.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_order.OrderID);
        command.Parameters.AddWithValue("@cusID", p_order.CustomerID);
        command.Parameters.AddWithValue("@storeID", p_order.StoreID);
        command.Parameters.AddWithValue("@totalPrice", p_order.TotalPrice);
        command.Parameters.AddWithValue("@orderStatus", "Order Placed");
        command.Parameters.AddWithValue("@createdAt", p_order.createdAt);

        command.ExecuteNonQuery();

        AddLineItemsToOrder(p_order);
        SubstractInventoryAfterPlacedOrder(p_order);
      }

      return p_order;
    }

    public List<Order> GetAllOrders()
    {
      string _sqlQuery = @"SELECT orderID, cusID, storeID, totalPrice, orderStatus, createdAt
                          FROM Orders;";
      List<Order> _listOfOrders = new List<Order>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listOfOrders.Add(new Order()
          {
            OrderID = reader.GetGuid(0),
            CustomerID = reader.GetGuid(1),
            StoreID = reader.GetGuid(2),
            TotalPrice = reader.GetDecimal(3),
            Status = reader.GetString(4),
            createdAt = reader.GetDateTime(5),
            Cart = GetLineItemsByOrderID(reader.GetGuid(0)),
            Shipments = GetAllTrackingByOrderID(reader.GetGuid(0))
          });
        }
      }

      return _listOfOrders;
    }

    public List<Tracking> GetAllTrackingByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"SELECT trackingID, trackingNumber
                            FROM Tracking
                            WHERE orderID = @orderID;";
      List<Tracking> _listOfTracking = new List<Tracking>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listOfTracking.Add(new Tracking()
          {
            TrackingID = reader.GetGuid(0),
            OrderID = p_orderID,
            TrackingNumber = reader.GetString(1)
          });
        }
      }

      return _listOfTracking;
    }

    public List<LineItem> GetLineItemsByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"SELECT productID, quantity, priceAtCheckedOut
                          FROM LineItems
                          WHERE orderID = @orderID;";
      List<LineItem> _cart = new List<LineItem>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _cart.Add(new LineItem()
          {
            OrderID = p_orderID,
            ProductID = reader.GetGuid(0),
            Quantity = reader.GetInt32(1),
            PriceAtCheckedOut = reader.GetDecimal(2)
          });
        }
      }

      return _cart;
    }

    public void RejectOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Rejected");

        command.ExecuteNonQuery();
      }
    }

    public void SubstractInventoryAfterPlacedOrder(Order p_order)
    {
      string _sqlQuery = @"UPDATE Inventory
                          SET quantity=quantity - @quantity
                          WHERE storeID=@storeID AND productID=@productID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand();

        foreach (var item in p_order.Cart)
        {
          command = new SqlCommand(_sqlQuery, conn);

          command.Parameters.AddWithValue("@quantity", item.Quantity);
          command.Parameters.AddWithValue("@storeID", p_order.StoreID);
          command.Parameters.AddWithValue("@productID", item.ProductID);

          command.ExecuteNonQuery();
        }
      }
    }

    public Order UpdateOrder(Order p_order)
    {
      string _sqlQuery = @"DELETE FROM LineItems
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_order.OrderID);

        command.ExecuteNonQuery();

        AddLineItemsToOrder(p_order);
      }

      return p_order;
    }

    public void UpdateTracking(Tracking p_tracking)
    {
      string _sqlQuery = @"UPDATE Tracking
                          SET trackingNumber=@trackingNumber
                          WHERE trackingID=@trackingID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@trackingNumber", p_tracking.TrackingNumber);
        command.Parameters.AddWithValue("@trackingID", p_tracking.TrackingID);

        command.ExecuteNonQuery();
      }
    }
  }
}