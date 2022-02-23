using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class InventoryManagementBL : IInventoryManagementBL
  {
    private readonly IInventoryManagementDL _repo;
    public InventoryManagementBL(IInventoryManagementDL p_repo)
    {
      _repo = p_repo;
    }

    public async Task<List<Inventory>> GetAllInventory()
    {
      return await _repo.GetAllInventory();
    }

    public async Task<Inventory> GetInventory(Inventory p_inven)
    {
      List<Inventory> _listOfInventory = await GetAllInventory();
      return _listOfInventory.Find(p => p.ProductID.Equals(p_inven.ProductID) && p.StoreID.Equals(p_inven.StoreID));
    }

    public async Task<List<Inventory>> GetStoreInventoryByStoreID(Guid p_storeID)
    {
      List<Inventory> _listOfInventory = await GetAllInventory();
      return _listOfInventory.FindAll(p => p.StoreID.Equals(p_storeID));
    }

    public async Task<Inventory> ImportNewProduct(Inventory p_inven)
    {
      List<Inventory> _listOfInventory = await GetStoreInventoryByStoreID(p_inven.StoreID);
      if (_listOfInventory.Any(p => p.ProductID.Equals(p_inven.ProductID)))
      {
        throw new Exception("Cannot import this product due to it already in the store! Please check the inventory!");
      }
      return await _repo.ImportNewProduct(p_inven);
    }

    public async Task ReplenishInventoryByID(Inventory p_inven)
    {
      await _repo.ReplenishInventoryByID(p_inven);
    }
  }
}