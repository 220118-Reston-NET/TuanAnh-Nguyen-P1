using Model;

namespace DL.Interfaces
{
  public interface IStoreManagementDL
  {
    StoreFrontProfile AddNewStoreFrontProfile(StoreFrontProfile p_store);
    StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store);
    List<StoreFrontProfile> GetAllStoresProfile();
  }
}