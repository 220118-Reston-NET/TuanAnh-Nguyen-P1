using System;
using System.Collections.Generic;
using DL.Interfaces;
using BL.Interfaces;
using BL.Implements;
using Model;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace Test
{
  public class IOrderManagementBLTest
  {
    [Fact]
    public async Task Should_Get_All_Orders()
    {
      // Arrange
      List<Order> _expectedListOfOrders = new List<Order>();
      _expectedListOfOrders.Add(new Order()
      {
        OrderID = Guid.NewGuid(),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      });

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_expectedListOfOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      // Act
      List<Order> _actualListOfOrders = await _orderBL.GetAllOrders();

      // Assert
      Assert.Same(_expectedListOfOrders, _actualListOfOrders);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
    }

    [Fact]
    public async Task Should_Get_All_Orders_By_Customer_ID()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order2 = new Order()
      {
        OrderID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        TotalPrice = 2100,
        StoreID = Guid.Parse("da1d56b8-2c10-4df2-b589-85d08246f74a"),
        CustomerID = Guid.Parse("2a72e7ef-1795-48d6-8faa-f4570b9eccaf"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order3 = new Order()
      {
        OrderID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        TotalPrice = 900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      List<Order> _expectedListOfOrders = new List<Order>();
      _expectedListOfOrders.Add(_order1);
      _expectedListOfOrders.Add(_order3);

      List<Order> _actualListOfOrders = new List<Order>();
      // Act
      _actualListOfOrders = await _orderBL.GetAllOrdersByCustomerID(_order1.CustomerID);

      // Assert
      Assert.Equal(_expectedListOfOrders.Count, _actualListOfOrders.Count);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
      Assert.Equal(_expectedListOfOrders[1].Status, _actualListOfOrders[1].Status);
    }

    [Fact]
    public async Task Should_Get_All_Orders_By_Customer_ID_With_Filter()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order2 = new Order()
      {
        OrderID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        TotalPrice = 2100,
        StoreID = Guid.Parse("da1d56b8-2c10-4df2-b589-85d08246f74a"),
        CustomerID = Guid.Parse("2a72e7ef-1795-48d6-8faa-f4570b9eccaf"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order3 = new Order()
      {
        OrderID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        TotalPrice = 900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      List<Order> _expectedListOfOrders = new List<Order>();
      _expectedListOfOrders.Add(_order3);

      List<Order> _actualListOfOrders = new List<Order>();
      // Act
      _actualListOfOrders = await _orderBL.GetAllOrdersByCustomerIDWithFilter(_order3.CustomerID, _order3.Status);

      // Assert
      Assert.Equal(_expectedListOfOrders.Count, _actualListOfOrders.Count);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
      Assert.Equal(_expectedListOfOrders[0].TotalPrice, _actualListOfOrders[0].TotalPrice);
    }

    [Fact]
    public async Task Should_Get_All_Orders_By_Store_ID()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order2 = new Order()
      {
        OrderID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        TotalPrice = 2100,
        StoreID = Guid.Parse("da1d56b8-2c10-4df2-b589-85d08246f74a"),
        CustomerID = Guid.Parse("2a72e7ef-1795-48d6-8faa-f4570b9eccaf"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order3 = new Order()
      {
        OrderID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        TotalPrice = 900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      List<Order> _expectedListOfOrders = new List<Order>();
      _expectedListOfOrders.Add(_order1);
      _expectedListOfOrders.Add(_order3);

      List<Order> _actualListOfOrders = new List<Order>();
      // Act
      _actualListOfOrders = await _orderBL.GetAllOrdersByStoreID(_order1.StoreID);

      // Assert
      Assert.Equal(_expectedListOfOrders.Count, _actualListOfOrders.Count);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
      Assert.Equal(_expectedListOfOrders[1].Status, _actualListOfOrders[1].Status);
    }

    [Fact]
    public async Task Should_Get_All_Orders_By_Store_ID_With_Filter()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order2 = new Order()
      {
        OrderID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        TotalPrice = 2100,
        StoreID = Guid.Parse("da1d56b8-2c10-4df2-b589-85d08246f74a"),
        CustomerID = Guid.Parse("2a72e7ef-1795-48d6-8faa-f4570b9eccaf"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order3 = new Order()
      {
        OrderID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        TotalPrice = 900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      List<Order> _expectedListOfOrders = new List<Order>();
      _expectedListOfOrders.Add(_order3);

      List<Order> _actualListOfOrders = new List<Order>();
      // Act
      _actualListOfOrders = await _orderBL.GetAllOrdersByStoreIDWithFilter(_order3.StoreID, _order3.Status);

      // Assert
      Assert.Equal(_expectedListOfOrders.Count, _actualListOfOrders.Count);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
      Assert.Equal(_expectedListOfOrders[0].TotalPrice, _actualListOfOrders[0].TotalPrice);
    }

    [Fact]
    public async Task Should_Get_Order_Detail_By_OrderID()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order2 = new Order()
      {
        OrderID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        TotalPrice = 2100,
        StoreID = Guid.Parse("da1d56b8-2c10-4df2-b589-85d08246f74a"),
        CustomerID = Guid.Parse("2a72e7ef-1795-48d6-8faa-f4570b9eccaf"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      Order _order3 = new Order()
      {
        OrderID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        TotalPrice = 900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      Order _expectedOrder = _order2;

      // Act
      Order _actualOrder = await _orderBL.GetOrderByOrderID(_order2.OrderID);

      // Assert
      Assert.Same(_expectedOrder, _actualOrder);
    }

    [Fact]
    public async Task Should_Get_All_Tracking()
    {
      // Arrange
      List<Tracking> _expectedListOfAllTracking = new List<Tracking>();
      Tracking _tracking1 = new Tracking()
      {
        TrackingID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 091238 819204 9014"
      };
      Tracking _tracking2 = new Tracking()
      {
        TrackingID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 894012 719024 7891"
      };
      _expectedListOfAllTracking.Add(_tracking1);
      _expectedListOfAllTracking.Add(_tracking2);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllTrackings()).ReturnsAsync(_expectedListOfAllTracking);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      // Act
      List<Tracking> _actualListOfTracking = await _orderBL.GetTrackings();

      // Assert
      Assert.Same(_expectedListOfAllTracking, _actualListOfTracking);
    }

    [Fact]
    public async Task Should_Get_Tracking_By_ID()
    {
      // Arrange
      List<Tracking> _listOfAllTracking = new List<Tracking>();
      Tracking _tracking1 = new Tracking()
      {
        TrackingID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 091238 819204 9014"
      };
      Tracking _tracking2 = new Tracking()
      {
        TrackingID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 894012 719024 7891"
      };
      _listOfAllTracking.Add(_tracking1);
      _listOfAllTracking.Add(_tracking2);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllTrackings()).ReturnsAsync(_listOfAllTracking);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      Tracking _expectedTracking = _tracking1;
      // Act
      Tracking _actualTracking = await _orderBL.GetTrackingNumberByID(_tracking1.TrackingID);

      // Assert
      Assert.Same(_expectedTracking, _actualTracking);
    }

    [Fact]
    public async Task Should_Get_All_Trackings_By_OrderID()
    {
      // Arrange
      List<Tracking> _expectedListOfAllTracking = new List<Tracking>();
      Tracking _tracking1 = new Tracking()
      {
        TrackingID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 091238 819204 9014"
      };
      Tracking _tracking2 = new Tracking()
      {
        TrackingID = Guid.Parse("6287cdb2-e83a-441b-a752-46f0e3c2ac75"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 894012 719024 7891"
      };
      _expectedListOfAllTracking.Add(_tracking1);
      _expectedListOfAllTracking.Add(_tracking2);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllTrackings()).ReturnsAsync(_expectedListOfAllTracking);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      // Act
      List<Tracking> _actualListOfTracking = await _orderBL.GetAllTrackingByOrderID(_tracking1.OrderID);

      // Assert
      Assert.Equal(_expectedListOfAllTracking.Count, _actualListOfTracking.Count);
    }

    [Fact]
    public async Task Should_Not_Cancel_Order_From_User_Due_To_Status_Preparing()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Preparing",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      Order _cancelOrder = new Order();
      // Act & Assert
      // Shouldn't cancel the order due to the order status is not "Order Placed"
      await Assert.ThrowsAsync<System.Exception>(
        async () => await _orderBL.CancelOrderByOrderID(_order1.OrderID)
      );
    }

    [Fact]
    public async Task Should_Not_Able_To_Update_Order_From_User_Due_To_Status_Preparing()
    {
      // Arrange
      List<Order> _listOfAllOrders = new List<Order>();
      Order _order1 = new Order()
      {
        OrderID = Guid.Parse("e5228c5d-5fe9-4147-82ca-54d202cca632"),
        TotalPrice = 2900,
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        CustomerID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Preparing",
        Cart = new List<LineItem>(),
        Shipments = new List<Tracking>()
      };
      _listOfAllOrders.Add(_order1);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).ReturnsAsync(_listOfAllOrders);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      Order _cancelOrder = new Order();
      // Act & Assert
      // Shouldn't cancel the order due to the order status is not "Order Placed"
      await Assert.ThrowsAsync<System.Exception>(
        async () => await _orderBL.UpdateOrder(_order1)
      );
    }

    [Fact]
    public async Task Should_Not_Add_Same_TrackingNumber()
    {
      // Arrange
      List<Tracking> _expectedListOfAllTracking = new List<Tracking>();
      Tracking _tracking1 = new Tracking()
      {
        TrackingID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 091238 819204 9014"
      };
      _expectedListOfAllTracking.Add(_tracking1);

      Mock<IOrderManagementDL> _mockRepo = new Mock<IOrderManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllTrackings()).ReturnsAsync(_expectedListOfAllTracking);
      IOrderManagementBL _orderBL = new OrderManagementBL(_mockRepo.Object);

      Tracking _tracking2 = new Tracking()
      {
        TrackingID = Guid.Parse("383884c3-bf20-412b-bc65-d70ca80ddf5b"),
        OrderID = Guid.Parse("9459e135-4de9-4566-88b2-85512b9e3bff"),
        TrackingNumber = "1Z 091238 819204 9014"
      };
      // Act & Assert
      // Shouldn't cancel the order due to the order status is not "Order Placed"
      await Assert.ThrowsAsync<System.Exception>(
        async () => await _orderBL.AddTrackingToOrder(_tracking2.OrderID, _tracking2)
      );
    }
  }
}
