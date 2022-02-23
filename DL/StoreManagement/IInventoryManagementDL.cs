using Model;

namespace DL.Interfaces
{
  public interface IInventoryManagementDL
  {
    Task<Inventory> ImportNewProduct(Inventory p_inven);
    Task ReplenishInventoryByID(Inventory p_inven);
    Task<List<Inventory>> GetAllInventory();
  }
}