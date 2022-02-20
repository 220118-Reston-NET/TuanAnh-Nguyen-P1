using Model;

namespace DL.Interfaces
{
  public interface IInventoryManagementDL
  {
    Inventory ImportNewProduct(Inventory p_inven);
    void ReplenishInventoryByID(Inventory p_inven);
    List<Inventory> GetAllInventory();
  }
}