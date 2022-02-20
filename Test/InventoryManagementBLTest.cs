using System;
using System.Collections.Generic;
using DL.Interfaces;
using BL.Interfaces;
using BL.Implements;
using Model;
using Moq;
using Xunit;

namespace Test
{
  public class InventoryManagementBLTest
  {
    [Fact]
    public void Should_Get_All_Inventory_From_Store()
    {
      // Arrange
      List<Inventory> _expectedListOfInventory = new List<Inventory>();
      _expectedListOfInventory.Add(new Inventory()
      {
        ProductID = Guid.Parse("edc3d007-614e-40ed-b590-1826449518f3"),
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        Quantity = 5
      });

      Mock<IInventoryManagementDL> _mockRepo = new Mock<IInventoryManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllInventory()).Returns(_expectedListOfInventory);
      IInventoryManagementBL _invenBL = new InventoryManagementBL(_mockRepo.Object);

      // Act
      List<Inventory> _actualListOfInventory = _invenBL.GetStoreInventoryByStoreID(Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"));

      // Assert
      Assert.Equal(_expectedListOfInventory[0].StoreID, _actualListOfInventory[0].StoreID);
    }

    [Fact]
    public void Should_Get_All_Inventory()
    {
      // Arrange
      List<Inventory> _expectedListOfInventory = new List<Inventory>();
      _expectedListOfInventory.Add(new Inventory()
      {
        ProductID = Guid.Parse("edc3d007-614e-40ed-b590-1826449518f3"),
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        Quantity = 5
      });

      Mock<IInventoryManagementDL> _mockRepo = new Mock<IInventoryManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllInventory()).Returns(_expectedListOfInventory);
      IInventoryManagementBL _invenBL = new InventoryManagementBL(_mockRepo.Object);

      // Act
      List<Inventory> _actualListOfInventory = _invenBL.GetAllInventory();

      // Assert
      Assert.Same(_expectedListOfInventory, _actualListOfInventory);
    }

    [Fact]
    public void Should_Get_Inventory()
    {
      // Arrange
      List<Inventory> _listOfInventory = new List<Inventory>();
      Inventory _inven1 = new Inventory()
      {
        ProductID = Guid.Parse("edc3d007-614e-40ed-b590-1826449518f3"),
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        Quantity = 5
      };
      Inventory _inven2 = new Inventory()
      {
        ProductID = Guid.Parse("edc3d102-614e-55ed-b590-1826804518b6"),
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        Quantity = 20
      };
      _listOfInventory.Add(_inven1);
      _listOfInventory.Add(_inven2);

      Mock<IInventoryManagementDL> _mockRepo = new Mock<IInventoryManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllInventory()).Returns(_listOfInventory);
      IInventoryManagementBL _invenBL = new InventoryManagementBL(_mockRepo.Object);

      Inventory _expectedInventory = _inven2;
      // Act
      Inventory _actualInventory = _invenBL.GetInventory(_inven2);

      // Assert
      Assert.Same(_expectedInventory, _actualInventory);
    }

    [Fact]
    public void Should_Not_Import_New_Product_To_Store()
    {
      // Arrange
      List<Inventory> _expectedListOfInventory = new List<Inventory>();
      _expectedListOfInventory.Add(new Inventory()
      {
        ProductID = Guid.Parse("edc3d007-614e-40ed-b590-1826449518f3"),
        StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
        Quantity = 5
      });

      Mock<IInventoryManagementDL> _mockRepo = new Mock<IInventoryManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllInventory()).Returns(_expectedListOfInventory);
      IInventoryManagementBL _invenBL = new InventoryManagementBL(_mockRepo.Object);

      Inventory _newInventory = new Inventory();
      // Act & Assert
      // Shouldn't import this product to store due to the it is existing in the store
      Assert.Throws<System.Exception>(
        () => _newInventory = _invenBL.ImportNewProduct(new Inventory()
        {
          ProductID = Guid.Parse("edc3d007-614e-40ed-b590-1826449518f3"),
          StoreID = Guid.Parse("d270786b-3c63-4576-bca3-13b1be8ddc7b"),
          Quantity = 20
        })
      );
    }
  }
}