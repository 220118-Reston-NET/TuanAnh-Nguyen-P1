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
    public List<CustomerProfile> GetAllCustomerProfile()
    {
      throw new NotImplementedException();
    }

    public CustomerProfile UpdateProfile(CustomerProfile p_customer)
    {
      throw new NotImplementedException();
    }
  }
}