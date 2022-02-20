using Model;

namespace BL.Interfaces
{
  public interface IInventoryManagementBL
  {
    /*
      STORE INVENTORY MANAGEMENT
      Inventory ImportNewProduct(Inventory p_inven);
      void ReplenishInventoryByID(Inventory p_inven);
      List<Inventory> GetStoreInventoryByStoreID(Guid p_storeID);
      Inventory GetInventory(Inventory p_inven);
      List<Inventory> GetAllInventory();

    */

    /// <summary>
    /// Import a new product to the store's inventory
    /// </summary>
    /// <param name="p_inven"></param>
    /// <returns></returns>
    Inventory ImportNewProduct(Inventory p_inven);

    /// <summary>
    /// Replenish the inventory by ID
    /// </summary>
    /// <param name="p_invenID"></param>
    /// <param name="p_quantity"></param>
    /// <returns></returns>
    void ReplenishInventoryByID(Inventory p_inven);

    /// <summary>
    /// Get all Inventory of the store by store ID
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    List<Inventory> GetStoreInventoryByStoreID(Guid p_storeID);

    /// <summary>
    /// Get Inventory Detail
    /// </summary>
    /// <param name="p_invenID"></param>
    /// <returns></returns>
    Inventory GetInventory(Inventory p_inven);

    /// <summary>
    /// Get All Inventories
    /// </summary>
    /// <returns></returns>
    List<Inventory> GetAllInventory();
  }
}