using Model;

namespace DL.Interfaces
{
  public interface ICustomerManagementDL
  {
    CustomerProfile UpdateProfile(CustomerProfile p_customer);
    List<CustomerProfile> GetAllCustomerProfile();
  }
}