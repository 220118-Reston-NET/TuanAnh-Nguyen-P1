using Model;

namespace DL.Interfaces
{
  public interface IStoreManagementDL
  {
    Task<StoreFrontProfile> AddNewStoreFrontProfile(StoreFrontProfile p_store);
    Task<StoreFrontProfile> UpdateStoreProfile(StoreFrontProfile p_store);
    Task<List<StoreFrontProfile>> GetAllStoresProfile();
  }
}