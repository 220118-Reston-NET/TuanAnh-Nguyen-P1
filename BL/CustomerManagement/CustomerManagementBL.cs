using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class CustomerManagementBL : ICustomerManagementBL
  {
    private readonly ICustomerManagementDL _repo;
    public CustomerManagementBL(ICustomerManagementDL p_repo)
    {
      _repo = p_repo;
    }

    public CustomerProfile AddNewCustomerProfile(CustomerProfile p_cus)
    {
      if (GetAllCustomerProfile().Any(p => p.Email.Equals(p_cus.Email)))
      {
        throw new Exception("Cannot add new customer profile due to the email is existing in the system!");
      }
      if (GetAllCustomerProfile().Any(p => p.PhoneNumber.Equals(p_cus.PhoneNumber)))
      {
        throw new Exception("Cannot add new customer profile due to the phone number is existing in the system!");
      }
      return _repo.AddNewCustomerProfile(p_cus);
    }

    public List<CustomerProfile> GetAllCustomerProfile()
    {
      return _repo.GetAllCustomerProfile();
    }

    public CustomerProfile GetProfileByID(Guid p_customerID)
    {
      return GetAllCustomerProfile().Find(p => p.CustomerID.Equals(p_customerID));
    }

    public CustomerProfile UpdateProfile(CustomerProfile p_customer)
    {
      List<CustomerProfile> _listFilterCustomerProfile = GetAllCustomerProfile().FindAll(p => p.CustomerID != p_customer.CustomerID);
      if (_listFilterCustomerProfile.Any(p => p.Email.Equals(p_customer.Email)))
      {
        throw new Exception("Cannot update the profile due to the email is existing in the database!");
      }
      if (_listFilterCustomerProfile.Any(p => p.PhoneNumber.Equals(p_customer.PhoneNumber)))
      {
        throw new Exception("Cannot update the profile due to the email is existing in the database!");
      }
      return _repo.UpdateProfile(p_customer);
    }
  }
}