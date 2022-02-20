using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class CustomerManagementBL : ICustomerMangementBL
  {
    private ICustomerManagementDL _repo;
    public CustomerManagementBL(ICustomerManagementDL p_repo)
    {
      _repo = p_repo;
    }
    public List<CustomerProfile> GetAllCustomerProfile()
    {
      return _repo.GetAllCustomerProfile();
    }

    public CustomerProfile GetProfileByID(string p_customerID)
    {
      return GetAllCustomerProfile().Find(p => p.CustomerID.Equals(p_customerID));
    }

    public CustomerProfile UpdateProfile(CustomerProfile p_customer)
    {
      return _repo.UpdateProfile(p_customer);
    }
  }
}