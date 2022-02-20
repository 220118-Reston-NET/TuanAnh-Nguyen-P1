using System.Data.SqlClient;
using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class CustomerManagementDL : ICustomerManagementDL
  {
    private readonly string _connectionString;
    public CustomerManagementDL(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }

    public CustomerProfile AddNewCustomerProfile(CustomerProfile p_cus)
    {
      string _sqlQuery = @"INSERT INTO Customers
                          (cusID, cusFName, cusLName, cusAddress, cusEmail, cusPhoneNo, dateOfBirth)
                          VALUES(@cusID, @cusFName, @cusLName, @cusAddress, @cusEmail, @cusPhoneNo, @dateOfBirth);";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@cusID", p_cus.CustomerID);
        command.Parameters.AddWithValue("@cusFName", p_cus.FirstName);
        command.Parameters.AddWithValue("@cusLName", p_cus.LastName);
        command.Parameters.AddWithValue("@cusAddress", p_cus.Address);
        command.Parameters.AddWithValue("@cusEmail", p_cus.Email);
        command.Parameters.AddWithValue("@cusPhoneNo", p_cus.PhoneNumber);
        command.Parameters.AddWithValue("@dateOfBirth", p_cus.DateOfBirth);

        command.ExecuteNonQuery();
      }

      return p_cus;
    }

    public List<CustomerProfile> GetAllCustomerProfile()
    {
      string _sqlQuery = @"SELECT cusID, cusFName, cusLName, cusAddress, cusEmail, cusPhoneNo, dateOfBirth
                          FROM Customers;";
      List<CustomerProfile> _listOfCustomers = new List<CustomerProfile>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listOfCustomers.Add(new CustomerProfile()
          {
            CustomerID = reader.GetGuid(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Address = reader.GetString(3),
            Email = reader.GetString(4),
            PhoneNumber = reader.GetString(5),
            DateOfBirth = reader.GetDateTime(6)
          });
        }
      }

      return _listOfCustomers;
    }

    public CustomerProfile UpdateProfile(CustomerProfile p_customer)
    {
      string _sqlQuery = @"UPDATE Customers
                          SET cusFName=@cusFName, cusLName=@cusLName, cusAddress=@cusAddress, cusEmail=@cusEmail, cusPhoneNo=@cusPhoneNo, dateOfBirth=@dateOfBirth
                          WHERE cusID=@cusID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@cusFName", p_customer.FirstName);
        command.Parameters.AddWithValue("@cusLName", p_customer.LastName);
        command.Parameters.AddWithValue("@cusAddress", p_customer.Address);
        command.Parameters.AddWithValue("@cusEmail", p_customer.Email);
        command.Parameters.AddWithValue("@cusPhoneNo", p_customer.PhoneNumber);
        command.Parameters.AddWithValue("@dateOfBirth", p_customer.DateOfBirth);
        command.Parameters.AddWithValue("@cusID", p_customer.CustomerID);

        command.ExecuteNonQuery();
      }

      return p_customer;
    }
  }
}