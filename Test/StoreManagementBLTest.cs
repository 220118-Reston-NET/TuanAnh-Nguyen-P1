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
  public class StoreManagementBLTest
  {
    [Fact]
    public void Should_Get_All_StoreFronts()
    {
      // Arrange
      List<StoreFrontProfile> _expectedListOfStoreFronts = new List<StoreFrontProfile>();
      _expectedListOfStoreFronts.Add(new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiTech",
        Address = "Dallas, TX"
      });

      Mock<IStoreManagementDL> _mockRepo = new Mock<IStoreManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllStoresProfile()).Returns(_expectedListOfStoreFronts);
      IStoreManagementBL _stofBL = new StoreManagementBL(_mockRepo.Object);

      // Act
      List<StoreFrontProfile> _actualListOfStoreFronts = _stofBL.GetAllStoresProfile();

      // Assert
      Assert.Same(_expectedListOfStoreFronts, _actualListOfStoreFronts);
      Assert.Equal(_expectedListOfStoreFronts[0].Name, _actualListOfStoreFronts[0].Name);
    }

    [Fact]
    public void Should_Not_Add_New_StoreFront()
    {
      // Arrange
      List<StoreFrontProfile> _expectedListOfStoreFronts = new List<StoreFrontProfile>();
      _expectedListOfStoreFronts.Add(new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiTech",
        Address = "Dallas, TX"
      });

      Mock<IStoreManagementDL> _mockRepo = new Mock<IStoreManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllStoresProfile()).Returns(_expectedListOfStoreFronts);
      IStoreManagementBL _stofBL = new StoreManagementBL(_mockRepo.Object);

      StoreFrontProfile _newStoreF = new StoreFrontProfile();

      // Act & Assert
      Assert.Throws<System.Exception>(
        () => _newStoreF = _stofBL.AddNewStoreFrontProfile(new StoreFrontProfile()
        {
          StoreID = Guid.NewGuid(),
          Name = "KiTech",
          Address = "Dorchester, MA"
        })
      );
    }

    [Fact]
    public void Should_Not_Update_The_StoreFront()
    {
      // Arrange
      List<StoreFrontProfile> _expectedListOfStoreFronts = new List<StoreFrontProfile>();
      StoreFrontProfile _store1 = new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiTech",
        Address = "Dallas, TX"
      };
      StoreFrontProfile _store2 = new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiStore",
        Address = "Mahattan, NY"
      };
      _expectedListOfStoreFronts.Add(_store1);
      _expectedListOfStoreFronts.Add(_store2);

      Mock<IStoreManagementDL> _mockRepo = new Mock<IStoreManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllStoresProfile()).Returns(_expectedListOfStoreFronts);
      IStoreManagementBL _stofBL = new StoreManagementBL(_mockRepo.Object);

      StoreFrontProfile _storeF = new StoreFrontProfile();

      // Act & Assert
      // Change the name of the KiTech to KiStore which is also existing in the database
      Assert.Throws<System.Exception>(
        () => _storeF = _stofBL.UpdateStoreProfile(new StoreFrontProfile()
        {
          StoreID = Guid.NewGuid(),
          Name = "KiStore",
          Address = "Dorchester, MA",
        })
      );
    }

    [Fact]
    public void Should_Get_StoreFront_Information_By_StoreID()
    {
      // Arrange
      List<StoreFrontProfile> _listOfStoreFronts = new List<StoreFrontProfile>();
      StoreFrontProfile _store1 = new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiTech",
        Address = "Dallas, TX"
      };
      StoreFrontProfile _store2 = new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiStore",
        Address = "Mahattan, NY"
      };
      _listOfStoreFronts.Add(_store1);
      _listOfStoreFronts.Add(_store2);

      Mock<IStoreManagementDL> _mockRepo = new Mock<IStoreManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllStoresProfile()).Returns(_listOfStoreFronts);
      IStoreManagementBL _stofBL = new StoreManagementBL(_mockRepo.Object);

      StoreFrontProfile _expectedStore = _store2;

      // Act
      StoreFrontProfile _actualStore = _stofBL.GetStoreProfileByID(_store2.StoreID);

      // Assert
      Assert.Same(_expectedStore, _actualStore);
    }

    [Fact]
    public void Should_Get_StoreFront_That_Have_Name_Matched()
    {
      // Arrange
      List<StoreFrontProfile> _expectedStore = new List<StoreFrontProfile>();
      StoreFrontProfile _store1 = new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiTech",
        Address = "Dallas, TX"
      };
      StoreFrontProfile _store2 = new StoreFrontProfile()
      {
        StoreID = Guid.NewGuid(),
        Name = "KiStore",
        Address = "Mahattan, NY"
      };
      _expectedStore.Add(_store1);
      _expectedStore.Add(_store2);

      Mock<IStoreManagementDL> _mockRepo = new Mock<IStoreManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllStoresProfile()).Returns(_expectedStore);
      IStoreManagementBL _stofBL = new StoreManagementBL(_mockRepo.Object);

      // Act
      List<StoreFrontProfile> _actualStore = _stofBL.SearchStoreByName("Ki");

      // Assert
      Assert.Equal(_expectedStore, _actualStore);
    }
  }
}