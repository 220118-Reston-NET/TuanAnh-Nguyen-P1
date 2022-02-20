using Model;

namespace DL.Interfaces
{
  public interface ICustomerManagementDL
  {
    CustomerProfile AddNewCustomerProfile(CustomerProfile p_cus);
    CustomerProfile UpdateProfile(CustomerProfile p_customer);
    List<CustomerProfile> GetAllCustomerProfile();
  }
}