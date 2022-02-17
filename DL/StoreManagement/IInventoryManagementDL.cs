using Model;

namespace DL.Interfaces
{
  public interface IInventoryManagementDL
  {
    Inventory ImportNewProduct(Inventory p_inven);
    void ReplenishInventoryByID(string p_invenID, int p_quantity);
    List<Inventory> GetStoreInventoryByStoreID(string p_storeID);
  }
}