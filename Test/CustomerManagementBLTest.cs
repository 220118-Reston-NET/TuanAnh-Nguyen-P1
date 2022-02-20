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
  public class CustomerManagementBLTest
  {
    [Fact]
    public void Should_Get_All_Customers()
    {
      List<CustomerProfile> _expectedListOfCustomers = new List<CustomerProfile>();
      _expectedListOfCustomers.Add(new CustomerProfile()
      {
        CustomerID = Guid.NewGuid(),
        FirstName = "Tester",
        LastName = "TestLast",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        DateOfBirth = new DateTime(1990 - 02 - 03),
      });

      Mock<ICustomerManagementDL> _mockRepo = new Mock<ICustomerManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).Returns(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      // Act
      List<CustomerProfile> _actualListOfCustomer = _cusBL.GetAllCustomerProfile();

      // Assert
      Assert.Same(_expectedListOfCustomers, _actualListOfCustomer);
      Assert.Equal(_expectedListOfCustomers[0].LastName, _actualListOfCustomer[0].LastName);
      Assert.Equal(_expectedListOfCustomers[0].Email, _actualListOfCustomer[0].Email);
    }

    [Fact]
    public void Should_Get_Customer_Information_Matched_Id()
    {
      // Arrange
      List<CustomerProfile> _listOfCustomers = new List<CustomerProfile>();
      CustomerProfile _cus1 = new CustomerProfile()
      {
        CustomerID = Guid.NewGuid(),
        FirstName = "Tester",
        LastName = "TestLast",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        DateOfBirth = new DateTime(1990 - 02 - 03),
      };
      CustomerProfile _cus2 = new CustomerProfile()
      {
        CustomerID = Guid.NewGuid(),
        FirstName = "Tester2",
        LastName = "TestLast2",
        Address = "Boston, MA",
        Email = "tester2@gmail.com",
        PhoneNumber = "9218273644",
        DateOfBirth = new DateTime(1990 - 02 - 06),
      };
      _listOfCustomers.Add(_cus1);
      _listOfCustomers.Add(_cus2);

      Mock<ICustomerManagementDL> _mockRepo = new Mock<ICustomerManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).Returns(_listOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _expectedCustomer = _cus2;

      CustomerProfile _actualCustomer = new CustomerProfile();
      // Act
      _actualCustomer = _cusBL.GetProfileByID(_cus2.CustomerID);

      // Assert
      Assert.Same(_expectedCustomer, _actualCustomer);
      Assert.Equal(_expectedCustomer.CustomerID, _actualCustomer.CustomerID);
      Assert.Equal(_expectedCustomer.Address, _actualCustomer.Address);
      Assert.Equal(_expectedCustomer.Email, _actualCustomer.Email);
      Assert.Equal(_expectedCustomer.PhoneNumber, _actualCustomer.PhoneNumber);
    }

    [Fact]
    public void Should_Not_Update_The_Customer()
    {
      // Arrange
      List<CustomerProfile> _expectedListOfCustomers = new List<CustomerProfile>();
      _expectedListOfCustomers.Add(new CustomerProfile()
      {
        CustomerID = Guid.NewGuid(),
        FirstName = "Tester",
        LastName = "TestLast",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        DateOfBirth = new DateTime(1990 - 02 - 03),
      });
      _expectedListOfCustomers.Add(new CustomerProfile()
      {
        CustomerID = Guid.NewGuid(),
        FirstName = "Tester2",
        LastName = "TestLast2",
        Address = "Boston, MA",
        Email = "tester2@gmail.com",
        PhoneNumber = "9218273644",
        DateOfBirth = new DateTime(1990 - 02 - 06),
      });

      Mock<ICustomerManagementDL> _mockRepo = new Mock<ICustomerManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).Returns(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _cus = new CustomerProfile();
      // Act & Assert
      // Change the name of the customer from Tester to Tester2 which is existing in the database
      Assert.Throws<System.Exception>(
        () => _cus = _cusBL.UpdateProfile(new CustomerProfile()
        {
          CustomerID = Guid.NewGuid(),
          FirstName = "Tester2",
          LastName = "TestLast",
          Address = "San Diego, CA",
          Email = "tester@gmail.com",
          PhoneNumber = "9018273645",
          DateOfBirth = new DateTime(1990 - 02 - 03),
        })
      );
    }

    [Fact]
    public void Should_Not_Add_New_Customer()
    {
      // Arrange
      List<CustomerProfile> _expectedListOfCustomers = new List<CustomerProfile>();
      _expectedListOfCustomers.Add(new CustomerProfile()
      {
        CustomerID = Guid.NewGuid(),
        FirstName = "Tester",
        LastName = "TestLast",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        DateOfBirth = new DateTime(1990 - 02 - 03),
      });

      Mock<ICustomerManagementDL> _mockRepo = new Mock<ICustomerManagementDL>();
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).Returns(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _newCus = new CustomerProfile();
      // Act & Assert
      // Shouldn't add new customer due to the name is existing in the database
      Assert.Throws<System.Exception>(
        () => _newCus = _cusBL.AddNewCustomerProfile(new CustomerProfile()
        {
          CustomerID = Guid.NewGuid(),
          FirstName = "Tester",
          LastName = "TestLasttttt",
          Address = "Boston, MA",
          Email = "tester@gmail.com",
          PhoneNumber = "1804273655",
          DateOfBirth = new DateTime(1990 - 02 - 03),
        })
      );
    }
  }
}