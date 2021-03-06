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
  public class AdminServiceBLTest
  {
    [Fact]
    public async Task Should_Get_All_Products()
    {
      // Arrange
      List<Product> _expectedListOfProducts = new List<Product>();
      _expectedListOfProducts.Add(new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "Towel",
        Price = 20,
        Description = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      });

      Mock<IAdminServiceDL> _mockRepo = new Mock<IAdminServiceDL>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(_expectedListOfProducts);
      IAdminServiceBL _prodBL = new AdminServiceBL(_mockRepo.Object);

      // Act
      List<Product> _actualListOfProducts = await _prodBL.GetAllProducts();

      // Assert
      Assert.Same(_expectedListOfProducts, _actualListOfProducts);
      Assert.Equal(_expectedListOfProducts[0].Name, _actualListOfProducts[0].Name);
    }

    [Fact]
    public async Task Product_Should_Not_Added_To_Database()
    {
      // Arrange
      List<Product> _expectedListOfProducts = new List<Product>();
      _expectedListOfProducts.Add(new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "Towel",
        Price = 20,
        Description = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      });

      Mock<IAdminServiceDL> _mockRepo = new Mock<IAdminServiceDL>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(_expectedListOfProducts);
      IAdminServiceBL _prodBL = new AdminServiceBL(_mockRepo.Object);

      Product _newProd = new Product();

      // Act & Assert
      await Assert.ThrowsAsync<System.Exception>(
        async () => _newProd = await _prodBL.AddNewProduct(new Product()
        {
          ProductID = Guid.NewGuid(),
          Name = "Towel",
          Price = 30,
          Description = "Hard",
          MinimumAge = 2,
          createdAt = new DateTime(2022 - 02 - 19)
        })
      );
    }

    [Fact]
    public async Task Should_Not_Update_The_Product()
    {
      //Arrange
      List<Product> _expectedListOfProducts = new List<Product>();
      _expectedListOfProducts.Add(new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "Towel",
        Price = 20,
        Description = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      });
      _expectedListOfProducts.Add(new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "iPad",
        Price = 899,
        Description = "New Generation",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 02 - 19)
      });

      Mock<IAdminServiceDL> _mockRepo = new Mock<IAdminServiceDL>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(_expectedListOfProducts);
      IAdminServiceBL _prodBL = new AdminServiceBL(_mockRepo.Object);

      Product _prod = new Product();
      // Act & Assert
      // Test to change name of Towel to iPad
      await Assert.ThrowsAsync<System.Exception>(
        async () => _prod = await _prodBL.UpdateProduct(new Product()
        {
          ProductID = Guid.NewGuid(),
          Name = "iPad",
          Price = 20,
          Description = "Soft, Good",
          MinimumAge = 0,
          createdAt = new DateTime(2022 - 07 - 19)
        })
      );
    }

    [Fact]
    public async Task Should_Get_Product_Detail_By_ProductID()
    {
      //Arrange
      List<Product> _listOfProducts = new List<Product>();
      Product _prod1 = new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "Towel",
        Price = 20,
        Description = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      };
      Product _prod2 = new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "iPad",
        Price = 899,
        Description = "New Generation",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 02 - 19)
      };
      _listOfProducts.Add(_prod1);
      _listOfProducts.Add(_prod2);

      Mock<IAdminServiceDL> _mockRepo = new Mock<IAdminServiceDL>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(_listOfProducts);
      IAdminServiceBL _prodBL = new AdminServiceBL(_mockRepo.Object);

      Product _expectedProd = _prod1;

      // Act
      Product _actualProd = await _prodBL.GetProductByID(_prod1.ProductID);

      // Assert
      Assert.Same(_expectedProd, _actualProd);
    }

    [Fact]
    public async Task Should_Get_Product_Detail_That_Have_Matched_Name()
    {
      //Arrange
      List<Product> _expectedListOfProducts = new List<Product>();
      Product _prod1 = new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "iPhone",
        Price = 1899,
        Description = "New generation",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      };
      Product _prod2 = new Product()
      {
        ProductID = Guid.NewGuid(),
        Name = "iPad",
        Price = 899,
        Description = "New Generation",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 02 - 19)
      };
      _expectedListOfProducts.Add(_prod1);
      _expectedListOfProducts.Add(_prod2);

      Mock<IAdminServiceDL> _mockRepo = new Mock<IAdminServiceDL>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).ReturnsAsync(_expectedListOfProducts);
      IAdminServiceBL _prodBL = new AdminServiceBL(_mockRepo.Object);

      // Act
      List<Product> _actualListProd = await _prodBL.SearchProductByName("iP");

      // Assert
      Assert.Equal(_expectedListOfProducts, _actualListProd);
    }
  }
}