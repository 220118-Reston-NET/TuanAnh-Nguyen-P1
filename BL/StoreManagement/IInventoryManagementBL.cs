using Model;

namespace BL.Interfaces
{
  public interface IInventoryManagementBL
  {
    /*
      STORE INVENTORY MANAGEMENT
      Inventory ImportNewProduct(Inventory p_inven);
      void ReplenishInventoryByID(string p_invenID, int p_quantity);
      List<Inventory> GetStoreInventoryByStoreID(string p_storeID);
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
    void ReplenishInventoryByID(string p_invenID, int p_quantity);

    /// <summary>
    /// Get all Inventory of the store by store ID
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    List<Inventory> GetStoreInventoryByStoreID(string p_storeID);
  }
}