using Model;

namespace BL.Interfaces
{
  public interface IStoreManagementBL
  {
    /*
      STORE MANAGEMENT
      StoreFrontProfile GetStoreProfileByID(string p_storeID);
      StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store);
      List<StoreFrontProfile> GetAllStoresProfile();
    */

    /// <summary>
    /// Get Store Profile by store ID
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    StoreFrontProfile GetStoreProfileByID(string p_storeID);

    /// <summary>
    /// Update Store Profile
    /// </summary>
    /// <param name="p_store"></param>
    /// <returns></returns>
    StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store);

    /// <summary>
    /// Get All Store Profiles in the system
    /// </summary>
    /// <returns></returns>
    List<StoreFrontProfile> GetAllStoresProfile();
  }
}