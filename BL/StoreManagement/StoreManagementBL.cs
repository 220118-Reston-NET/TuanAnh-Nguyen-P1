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

    public async Task<StoreFrontProfile> AddNewStoreFrontProfile(StoreFrontProfile p_store)
    {
      List<StoreFrontProfile> _listOfStoreFrontProfile = await GetAllStoresProfile();
      if (_listOfStoreFrontProfile.Any(p => p.Name.Equals(p_store.Name)))
      {
        throw new Exception("Cannot add new store front profile due to the name is existing in the system!");
      }
      return await _repo.AddNewStoreFrontProfile(p_store);
    }

    public async Task<List<StoreFrontProfile>> GetAllStoresProfile()
    {
      return await _repo.GetAllStoresProfile();
    }

    public async Task<StoreFrontProfile> GetStoreProfileByID(Guid p_storeID)
    {
      List<StoreFrontProfile> _listOfStoreFrontProfile = await GetAllStoresProfile();
      return _listOfStoreFrontProfile.Find(p => p.StoreID.Equals(p_storeID));
    }

    public async Task<List<StoreFrontProfile>> SearchStoreByName(string p_storeName)
    {
      List<StoreFrontProfile> _listOfStoreFrontProfile = await GetAllStoresProfile();
      return _listOfStoreFrontProfile.FindAll(p => p.Name.Contains(p_storeName));
    }

    public async Task<StoreFrontProfile> UpdateStoreProfile(StoreFrontProfile p_store)
    {
      List<StoreFrontProfile> _listOfStoreFrontProfile = await GetAllStoresProfile();
      List<StoreFrontProfile> _listFilteredStoreProfile = _listOfStoreFrontProfile.FindAll(p => p.StoreID != p_store.StoreID);
      if (_listFilteredStoreProfile.Any(p => p.Name.Equals(p_store.Name)))
      {
        throw new Exception("Cannot update store front profile due to the name is existing in the system!");
      }
      return await _repo.UpdateStoreProfile(p_store);
    }
  }
}