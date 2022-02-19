using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class InventoryManagementDL : IInventoryManagementDL
  {
    private readonly string _connectionString;
    public InventoryManagementDL(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public List<Inventory> GetStoreInventoryByStoreID(string p_storeID)
    {
      throw new NotImplementedException();
    }

    public Inventory ImportNewProduct(Inventory p_inven)
    {
      throw new NotImplementedException();
    }

    public void ReplenishInventoryByID(string p_invenID, int p_quantity)
    {
      throw new NotImplementedException();
    }
  }
}