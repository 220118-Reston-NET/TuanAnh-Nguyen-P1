using Model;

namespace DL.Interfaces
{
  public interface ICustomerManagementDL
  {
    Task<CustomerProfile> AddNewCustomerProfile(CustomerProfile p_cus);
    Task<CustomerProfile> UpdateProfile(CustomerProfile p_customer);
    Task<List<CustomerProfile>> GetAllCustomerProfile();
  }
}