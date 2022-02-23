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

    public async Task<CustomerProfile> AddNewCustomerProfile(CustomerProfile p_cus)
    {
      List<CustomerProfile> _listOfCustomerProfile = await GetAllCustomerProfile();
      if (_listOfCustomerProfile.Any(p => p.Email.Equals(p_cus.Email)))
      {
        throw new Exception("Cannot add new customer profile due to the email is existing in the system!");
      }
      if (_listOfCustomerProfile.Any(p => p.PhoneNumber.Equals(p_cus.PhoneNumber)))
      {
        throw new Exception("Cannot add new customer profile due to the phone number is existing in the system!");
      }
      return await _repo.AddNewCustomerProfile(p_cus);
    }

    public async Task<List<CustomerProfile>> GetAllCustomerProfile()
    {
      return await _repo.GetAllCustomerProfile();
    }

    public async Task<CustomerProfile> GetProfileByID(Guid p_customerID)
    {
      List<CustomerProfile> _listOfCustomerProfile = await GetAllCustomerProfile();
      return _listOfCustomerProfile.Find(p => p.CustomerID.Equals(p_customerID));
    }

    public async Task<CustomerProfile> UpdateProfile(CustomerProfile p_customer)
    {
      List<CustomerProfile> _listOfCustomerProfile = await GetAllCustomerProfile();
      List<CustomerProfile> _listFilterCustomerProfile = _listOfCustomerProfile.FindAll(p => p.CustomerID != p_customer.CustomerID);
      if (_listFilterCustomerProfile.Any(p => p.Email.Equals(p_customer.Email)))
      {
        throw new Exception("Cannot update the profile due to the email is existing in the database!");
      }
      if (_listFilterCustomerProfile.Any(p => p.PhoneNumber.Equals(p_customer.PhoneNumber)))
      {
        throw new Exception("Cannot update the profile due to the email is existing in the database!");
      }
      return await _repo.UpdateProfile(p_customer);
    }
  }
}