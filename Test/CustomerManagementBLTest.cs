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
  public class CustomerManagementBLTest
  {
    [Fact]
    public async Task Should_Get_All_Customers()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      // Act
      List<CustomerProfile> _actualListOfCustomer = await _cusBL.GetAllCustomerProfile();

      // Assert
      Assert.Same(_expectedListOfCustomers, _actualListOfCustomer);
      Assert.Equal(_expectedListOfCustomers[0].LastName, _actualListOfCustomer[0].LastName);
      Assert.Equal(_expectedListOfCustomers[0].Email, _actualListOfCustomer[0].Email);
    }

    [Fact]
    public async Task Should_Get_Customer_Information_Matched_Id()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_listOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _expectedCustomer = _cus2;

      CustomerProfile _actualCustomer = new CustomerProfile();
      // Act
      _actualCustomer = await _cusBL.GetProfileByID(_cus2.CustomerID);

      // Assert
      Assert.Same(_expectedCustomer, _actualCustomer);
      Assert.Equal(_expectedCustomer.CustomerID, _actualCustomer.CustomerID);
      Assert.Equal(_expectedCustomer.Address, _actualCustomer.Address);
      Assert.Equal(_expectedCustomer.Email, _actualCustomer.Email);
      Assert.Equal(_expectedCustomer.PhoneNumber, _actualCustomer.PhoneNumber);
    }

    [Fact]
    public async Task Should_Not_Update_The_Customer()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _cus = new CustomerProfile();
      // Act & Assert
      // Change the name of the customer from Tester to Tester2 which is existing in the database
      await Assert.ThrowsAsync<System.Exception>(
        async () => _cus = await _cusBL.UpdateProfile(new CustomerProfile()
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
    public async Task Should_Not_Add_New_Customer()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _newCus = new CustomerProfile();
      // Act & Assert
      // Shouldn't add new customer due to the name is existing in the database
      await Assert.ThrowsAsync<System.Exception>(
        async () => _newCus = await _cusBL.AddNewCustomerProfile(new CustomerProfile()
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

    [Fact]
    public async Task Should_Not_Add_New_Customer_Due_To_Email()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _newCus = new CustomerProfile();
      // Act & Assert
      // Shouldn't add new customer due to the email is existing in the database
      await Assert.ThrowsAsync<System.Exception>(
        async () => _newCus = await _cusBL.AddNewCustomerProfile(new CustomerProfile()
        {
          CustomerID = Guid.NewGuid(),
          FirstName = "Teqwvqwvster",
          LastName = "TestLasttttt",
          Address = "Boston, MA",
          Email = "tester@gmail.com",
          PhoneNumber = "1804273655",
          DateOfBirth = new DateTime(1990 - 02 - 03),
        })
      );
    }

    [Fact]
    public async Task Should_Not_Add_New_Customer_Due_To_PhoneNumber()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _newCus = new CustomerProfile();
      // Act & Assert
      // Shouldn't add new customer due to the phone number is existing in the database
      await Assert.ThrowsAsync<System.Exception>(
        async () => _newCus = await _cusBL.AddNewCustomerProfile(new CustomerProfile()
        {
          CustomerID = Guid.NewGuid(),
          FirstName = "Teqwvqwvster",
          LastName = "TestLasttttt",
          Address = "Bosqton, MA",
          Email = "testeqwvqwr@gmail.com",
          PhoneNumber = "9018273645",
          DateOfBirth = new DateTime(1990 - 02 - 03),
        })
      );
    }

    [Fact]
    public async Task Should_Not_Update_Customer_Due_To_PhoneNumber()
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
      _mockRepo.Setup(repo => repo.GetAllCustomerProfile()).ReturnsAsync(_expectedListOfCustomers);
      ICustomerManagementBL _cusBL = new CustomerManagementBL(_mockRepo.Object);

      CustomerProfile _newCus = new CustomerProfile();
      // Act & Assert
      // Shouldn't update customer due to the phone number is existing in the database
      await Assert.ThrowsAsync<System.Exception>(
        async () => _newCus = await _cusBL.UpdateProfile(new CustomerProfile()
        {
          CustomerID = Guid.NewGuid(),
          FirstName = "Teqwvqwvster",
          LastName = "TestLasttttt",
          Address = "Bosqton, MA",
          Email = "testeqwvqwr@gmail.com",
          PhoneNumber = "9018273645",
          DateOfBirth = new DateTime(1990 - 02 - 03),
        })
      );
    }
  }
}