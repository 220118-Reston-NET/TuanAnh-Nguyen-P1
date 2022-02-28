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
    public async Task AcceptOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Processing");

        await command.ExecuteNonQueryAsync();
      }
    }

    public async Task<List<LineItem>> AddLineItemsToOrder(Order p_order)
    {
      string _sqlQuery = @"INSERT INTO LineItems
                          (productID, orderID, quantity, priceAtCheckedOut)
                          VALUES(@productID, @orderID, @quantity, @priceAtCheckedOut);";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        foreach (var item in p_order.Cart)
        {
          command = new SqlCommand(_sqlQuery, conn);
          command.Parameters.AddWithValue("@productID", item.ProductID);
          command.Parameters.AddWithValue("@orderID", p_order.OrderID);
          command.Parameters.AddWithValue("@quantity", item.Quantity);
          command.Parameters.AddWithValue("@priceAtCheckedOut", item.PriceAtCheckedOut);

          await command.ExecuteNonQueryAsync();
        }
      }

      return p_order.Cart;
    }

    public async Task<Tracking> AddTrackingToOrder(Guid p_orderID, Tracking p_tracking)
    {
      string _sqlQuery = @"INSERT INTO Tracking
                          (trackingID, orderID, trackingNumber)
                          VALUES(@trackingID, @orderID, @trackingNumber);";

      p_tracking.TrackingID = Guid.NewGuid();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@trackingID", p_tracking.TrackingID);
        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@trackingNumber", p_tracking.TrackingNumber);

        await command.ExecuteNonQueryAsync();
      }

      return p_tracking;
    }

    public async Task CancelOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Cancelled");

        await command.ExecuteNonQueryAsync();
      }
    }

    public async Task CompleteOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Shipped");

        await command.ExecuteNonQueryAsync();
      }
    }

    public async Task<Order> CreateOrder(Order p_order)
    {
      string _sqlQuery = @"INSERT INTO Orders
                          (orderID, cusID, storeID, totalPrice, orderStatus, createdAt)
                          VALUES(@orderID, @cusID, @storeID, @totalPrice, @orderStatus, @createdAt);";

      p_order.OrderID = Guid.NewGuid();
      p_order.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
      p_order.Status = "Order Placed";
      p_order.TotalPrice = CalTotalPrice(p_order);

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_order.OrderID);
        command.Parameters.AddWithValue("@cusID", p_order.CustomerID);
        command.Parameters.AddWithValue("@storeID", p_order.StoreID);
        command.Parameters.AddWithValue("@totalPrice", p_order.TotalPrice);
        command.Parameters.AddWithValue("@orderStatus", p_order.Status);
        command.Parameters.AddWithValue("@createdAt", p_order.createdAt);

        await command.ExecuteNonQueryAsync();

        await AddLineItemsToOrder(p_order);
        await SubstractInventoryAfterPlacedOrder(p_order);
      }

      return p_order;
    }

    public async Task<List<Order>> GetAllOrders()
    {
      string _sqlQuery = @"SELECT orderID, cusID, storeID, totalPrice, orderStatus, createdAt
                          FROM Orders;";
      List<Order> _listOfOrders = new List<Order>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = await command.ExecuteReaderAsync();

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
            Cart = await GetLineItemsByOrderID(reader.GetGuid(0)),
            Shipments = await GetAllTrackingByOrderID(reader.GetGuid(0))
          });
        }
      }

      return _listOfOrders;
    }

    public async Task<List<Tracking>> GetAllTrackingByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"SELECT trackingID, trackingNumber
                            FROM Tracking
                            WHERE orderID = @orderID;";
      List<Tracking> _listOfTracking = new List<Tracking>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);

        SqlDataReader reader = await command.ExecuteReaderAsync();

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

    public async Task<List<Tracking>> GetAllTrackings()
    {
      string _sqlQuery = @"SELECT trackingID, orderID, trackingNumber
                          FROM Tracking;";
      List<Tracking> _listOfTracking = new List<Tracking>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = await command.ExecuteReaderAsync();

        while (reader.Read())
        {
          _listOfTracking.Add(new Tracking()
          {
            TrackingID = reader.GetGuid(0),
            OrderID = reader.GetGuid(1),
            TrackingNumber = reader.GetString(2)
          });
        }
      }

      return _listOfTracking;
    }

    public async Task<List<LineItem>> GetLineItemsByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"SELECT productID, quantity, priceAtCheckedOut
                          FROM LineItems
                          WHERE orderID = @orderID;";
      List<LineItem> _cart = new List<LineItem>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);

        SqlDataReader reader = await command.ExecuteReaderAsync();

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

    public async Task RejectOrderByOrderID(Guid p_orderID)
    {
      string _sqlQuery = @"UPDATE Orders
                          SET orderStatus=@orderStatus
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@orderStatus", "Rejected");

        await command.ExecuteNonQueryAsync();
      }
    }

    public async Task SubstractInventoryAfterPlacedOrder(Order p_order)
    {
      string _sqlQuery = @"UPDATE Inventory
                          SET quantity=quantity - @quantity
                          WHERE storeID=@storeID AND productID=@productID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand();

        foreach (var item in p_order.Cart)
        {
          command = new SqlCommand(_sqlQuery, conn);

          command.Parameters.AddWithValue("@quantity", item.Quantity);
          command.Parameters.AddWithValue("@storeID", p_order.StoreID);
          command.Parameters.AddWithValue("@productID", item.ProductID);

          await command.ExecuteNonQueryAsync();
        }
      }
    }

    public async Task<Order> UpdateOrder(Order p_order)
    {
      string _sqlQuery = @"DELETE FROM LineItems
                          WHERE orderID=@orderID;";

      string _sqlQuery2 = @"UPDATE Orders
                          SET totalPrice=@totalPrice
                          WHERE orderID=@orderID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@orderID", p_order.OrderID);
        await command.ExecuteNonQueryAsync();

        p_order.TotalPrice = CalTotalPrice(p_order);
        p_order.Status = "Order Placed";
        command = new SqlCommand(_sqlQuery2, conn);
        command.Parameters.AddWithValue("@orderID", p_order.OrderID);
        command.Parameters.AddWithValue("@totalPrice", p_order.TotalPrice);
        await command.ExecuteNonQueryAsync();

        await AddLineItemsToOrder(p_order);
      }

      return p_order;
    }

    public async Task UpdateTracking(Tracking p_tracking)
    {
      string _sqlQuery = @"UPDATE Tracking
                          SET trackingNumber=@trackingNumber
                          WHERE trackingID=@trackingID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@trackingNumber", p_tracking.TrackingNumber);
        command.Parameters.AddWithValue("@trackingID", p_tracking.TrackingID);

        await command.ExecuteNonQueryAsync();
      }
    }

    protected decimal CalTotalPrice(Order p_order)
    {
      decimal _totalPrice = 0m;

      foreach (var item in p_order.Cart)
      {
        _totalPrice += item.Quantity * item.PriceAtCheckedOut;
      }

      return _totalPrice;
    }
  }
}