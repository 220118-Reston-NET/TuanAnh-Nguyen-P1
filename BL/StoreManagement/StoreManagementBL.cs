using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class StoreManagementBL : IStoreManagementBL
  {
    private IStoreManagementDL _repo;
    public StoreManagementBL(IStoreManagementDL p_repo)
    {
      _repo = p_repo;
    }
    public List<StoreFrontProfile> GetAllStoresProfile()
    {
      return _repo.GetAllStoresProfile();
    }

    public StoreFrontProfile GetStoreProfileByID(string p_storeID)
    {
      return GetAllStoresProfile().Find(p => p.StoreID.Equals(p_storeID));
    }

    public List<StoreFrontProfile> SearchStoreByName(string p_storeName)
    {
      return GetAllStoresProfile().FindAll(p => p.Name.Contains(p_storeName));
    }

    public StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store)
    {
      return _repo.UpdateStoreProfile(p_store);
    }
  }
}