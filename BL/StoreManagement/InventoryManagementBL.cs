using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class InventoryManagementBL : IInventoryManagementBL
  {
    private IInventoryManagementDL _repo;
    public InventoryManagementBL(IInventoryManagementDL p_repo)
    {
      _repo = p_repo;
    }

    public List<Inventory> GetAllInventory()
    {
      return _repo.GetAllInventory();
    }

    public Inventory GetInventory(Inventory p_inven)
    {
      return GetAllInventory().Find(p => p.ProductID.Equals(p_inven.ProductID) && p.StoreID.Equals(p_inven.StoreID));
    }

    public List<Inventory> GetStoreInventoryByStoreID(Guid p_storeID)
    {
      return GetAllInventory().FindAll(p => p.StoreID.Equals(p_storeID));
    }

    public Inventory ImportNewProduct(Inventory p_inven)
    {
      if (GetStoreInventoryByStoreID(p_inven.StoreID).Any(p => p.ProductID.Equals(p_inven.ProductID)))
      {
        throw new Exception("Cannot import this product due to it already in the store! Please check the inventory!");
      }
      return _repo.ImportNewProduct(p_inven);
    }

    public void ReplenishInventoryByID(Inventory p_inven)
    {
      _repo.ReplenishInventoryByID(p_inven);
    }
  }
}