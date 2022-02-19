using Model;

namespace BL.Interfaces
{
  public interface ICustomerMangementBL
  {
    /*
      CUSTOMER MANAGEMENT
      CustomerProfile GetProfileByID(string p_customerID);
      CustomerProfile UpdateProfile(CustomerProfile p_customer);
      List<CustomerProfile> GetAllCustomerProfile();
    */

    /// <summary>
    /// Get Customer Profile by ID
    /// </summary>
    /// <param name="p_customerID"></param>
    /// <returns></returns>
    CustomerProfile GetProfileByID(string p_customerID);

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