using Model;

namespace BL.Interfaces
{
  public interface ICustomerManagementBL
  {
    /*
      CUSTOMER MANAGEMENT
      CustomerProfile AddNewCustomerProfile(CustomerProfile p_cus);
      CustomerProfile GetProfileByID(Guid p_customerID);
      CustomerProfile UpdateProfile(CustomerProfile p_customer);
      List<CustomerProfile> GetAllCustomerProfile();
    */

    /// <summary>
    /// Add new customer profile to system
    /// </summary>
    /// <param name="p_cus"></param>
    /// <returns></returns>
    CustomerProfile AddNewCustomerProfile(CustomerProfile p_cus);

    /// <summary>
    /// Get Customer Profile by ID
    /// </summary>
    /// <param name="p_customerID"></param>
    /// <returns></returns>
    CustomerProfile GetProfileByID(Guid p_customerID);

    /// <summary>
    /// Update the profile 
    /// </summary>
    /// <param name="p_customer"></param>
    /// <returns></returns>
    CustomerProfile UpdateProfile(CustomerProfile p_customer);

    /// <summary>
    /// Get All Customers in the system
    /// </summary>
    /// <returns></returns>
    List<CustomerProfile> GetAllCustomerProfile();
  }
}