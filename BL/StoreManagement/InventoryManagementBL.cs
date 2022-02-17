using BL.Interfaces;
using Model;

namespace BL.Implements
{
  public class InventoryManagementBL : IInventoryManagementBL
  {
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