using System.Data.SqlClient;
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
    public async Task<List<Inventory>> GetAllInventory()
    {
      string _sqlQuery = @"SELECT storeID, productID, quantity
                          FROM Inventory;";
      List<Inventory> _listInventory = new List<Inventory>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = await command.ExecuteReaderAsync();

        while (reader.Read())
        {
          _listInventory.Add(new Inventory()
          {
            StoreID = reader.GetGuid(0),
            ProductID = reader.GetGuid(1),
            Quantity = reader.GetInt32(2)
          });
        }
      }

      return _listInventory;
    }

    public async Task<Inventory> ImportNewProduct(Inventory p_inven)
    {
      string _sqlQuery = @"INSERT INTO Inventory
                          (storeID, productID, quantity)
                          VALUES(@storeID, @productID, @quantity);";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@storeID", p_inven.StoreID);
        command.Parameters.AddWithValue("@productID", p_inven.ProductID);
        command.Parameters.AddWithValue("@quantity", p_inven.Quantity);

        await command.ExecuteNonQueryAsync();
      }

      return p_inven;
    }

    public async Task ReplenishInventoryByID(Inventory p_inven)
    {
      string _sqlQuery = @"UPDATE Inventory
                          SET quantity= quantity + @quantity
                          WHERE storeID=@storeID AND productID=@productID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@quantity", p_inven.Quantity);
        command.Parameters.AddWithValue("@storeID", p_inven.StoreID);
        command.Parameters.AddWithValue("@productID", p_inven.ProductID);

        await command.ExecuteNonQueryAsync();
      }
    }
  }
}