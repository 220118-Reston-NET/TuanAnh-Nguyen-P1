using Model;

namespace DL.Interfaces
{
  public interface IStoreManagementDL
  {
    StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store);
    List<StoreFrontProfile> GetAllStoresProfile();
  }
}