using Model;

namespace BL.Interfaces
{
  public interface IStoreManagementBL
  {
    /*
      STORE MANAGEMENT
      StoreFrontProfile AddNewStoreFrontProfile(StoreFrontProfile p_store);
      StoreFrontProfile GetStoreProfileByID(string p_storeID);
      StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store);
      List<StoreFrontProfile> GetAllStoresProfile();
      List<StoreFrontProfile> SearchStoreByName(string p_storeName);
    */

    /// <summary>
    /// Add new store front profile
    /// </summary>
    /// <param name="p_store"></param>
    /// <returns></returns>
    Task<StoreFrontProfile> AddNewStoreFrontProfile(StoreFrontProfile p_store);

    /// <summary>
    /// Get Store Profile by store ID
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    Task<StoreFrontProfile> GetStoreProfileByID(Guid p_storeID);

    /// <summary>
    /// Update Store Profile
    /// </summary>
    /// <param name="p_store"></param>
    /// <returns></returns>
    Task<StoreFrontProfile> UpdateStoreProfile(StoreFrontProfile p_store);

    /// <summary>
    /// Get All Store Profiles in the system
    /// </summary>
    /// <returns></returns>
    Task<List<StoreFrontProfile>> GetAllStoresProfile();

    /// <summary>
    /// Search Stores By Store Name
    /// </summary>
    /// <param name="p_storeName"></param>
    /// <returns></returns>
    Task<List<StoreFrontProfile>> SearchStoreByName(string p_storeName);
  }
}