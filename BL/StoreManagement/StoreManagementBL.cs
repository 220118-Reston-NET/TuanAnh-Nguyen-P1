using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class StoreManagementBL : IStoreManagementBL
  {
    private readonly IStoreManagementDL _repo;
    public StoreManagementBL(IStoreManagementDL p_repo)
    {
      _repo = p_repo;
    }

    public StoreFrontProfile AddNewStoreFrontProfile(StoreFrontProfile p_store)
    {
      if (GetAllStoresProfile().Any(p => p.Name.Equals(p_store.Name)))
      {
        throw new Exception("Cannot add new store front profile due to the name is existing in the system!");
      }
      return _repo.AddNewStoreFrontProfile(p_store);
    }

    public List<StoreFrontProfile> GetAllStoresProfile()
    {
      return _repo.GetAllStoresProfile();
    }

    public StoreFrontProfile GetStoreProfileByID(Guid p_storeID)
    {
      return GetAllStoresProfile().Find(p => p.StoreID.Equals(p_storeID));
    }

    public List<StoreFrontProfile> SearchStoreByName(string p_storeName)
    {
      return GetAllStoresProfile().FindAll(p => p.Name.Contains(p_storeName));
    }

    public StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store)
    {
      List<StoreFrontProfile> _listFilteredStoreProfile = GetAllStoresProfile().FindAll(p => p.StoreID != p_store.StoreID);
      if (_listFilteredStoreProfile.Any(p => p.Name.Equals(p_store.Name)))
      {
        throw new Exception("Cannot update store front profile due to the name is existing in the system!");
      }
      return _repo.UpdateStoreProfile(p_store);
    }
  }
}