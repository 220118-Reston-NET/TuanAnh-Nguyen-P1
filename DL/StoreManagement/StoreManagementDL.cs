using System.Data.SqlClient;
using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class StoreManagementDL : IStoreManagementDL
  {
    private readonly string _connectionString;
    public StoreManagementDL(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public List<StoreFrontProfile> GetAllStoresProfile()
    {
      string _sqlQuery = @"SELECT storeID, storeName, storeAddress
                          FROM StoreFronts;";

      List<StoreFrontProfile> _listStores = new List<StoreFrontProfile>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listStores.Add(new StoreFrontProfile()
          {
            StoreID = reader.GetGuid(0),
            Name = reader.GetString(1),
            Address = reader.GetString(2)
          });
        }
      }

      return _listStores;
    }

    public StoreFrontProfile UpdateStoreProfile(StoreFrontProfile p_store)
    {
      string _sqlQuery = @"UPDATE StoreFronts
                          SET storeName=@storeName, storeAddress=@storeAddress
                          WHERE storeID=@storeID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@storeName", p_store.Name);
        command.Parameters.AddWithValue("@storeAddress", p_store.Address);
        command.Parameters.AddWithValue("@storeID", p_store.StoreID);

        command.ExecuteNonQuery();
      }

      return p_store;
    }
  }
}