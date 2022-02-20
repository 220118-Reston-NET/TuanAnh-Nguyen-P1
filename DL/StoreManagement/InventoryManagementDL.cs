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
    public List<Inventory> GetAllInventory()
    {
      string _sqlQuery = @"SELECT storeID, productID, quantity
                          FROM Inventory;";
      List<Inventory> _listInventory = new List<Inventory>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

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

    public Inventory ImportNewProduct(Inventory p_inven)
    {
      string _sqlQuery = @"INSERT INTO Inventory
                          (storeID, productID, quantity)
                          VALUES(@storeID, @productID, @quantity);";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@storeID", p_inven.StoreID);
        command.Parameters.AddWithValue("@productID", p_inven.ProductID);
        command.Parameters.AddWithValue("@quantity", p_inven.Quantity);

        command.ExecuteNonQuery();
      }

      return p_inven;
    }

    public void ReplenishInventoryByID(Inventory p_inven)
    {
      string _sqlQuery = @"UPDATE Inventory
                          SET quantity=@quantity
                          WHERE storeID=@storeID, productID=@productID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@quantity", p_inven.Quantity);
        command.Parameters.AddWithValue("@storeID", p_inven.StoreID);
        command.Parameters.AddWithValue("@productID", p_inven.ProductID);

        command.ExecuteNonQuery();
      }
    }
  }
}