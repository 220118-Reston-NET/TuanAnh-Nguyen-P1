using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class StoreManagementDL : IStoreManagementDL
  {
    private readonly string _connectionString;
    public StoreManagementDL(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public List<StoreFrontProfile> GetAllStoresProfile()
    {
      throw new NotImplementedException();
    }

    public StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store)
    {
      throw new NotImplementedException();
    }
  }
}