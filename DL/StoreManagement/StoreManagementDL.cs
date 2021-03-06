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

    public async Task<StoreFrontProfile> AddNewStoreFrontProfile(StoreFrontProfile p_store)
    {
      string _sqlQuery = @"INSERT INTO StoreFronts
                          (storeID, storeName, storeAddress)
                          VALUES(@storeID, @storeName, @storeAddress);";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@storeID", p_store.StoreID);
        command.Parameters.AddWithValue("@storeName", p_store.Name);
        command.Parameters.AddWithValue("@storeAddress", p_store.Address);

        await command.ExecuteNonQueryAsync();
      }

      return p_store;
    }

    public async Task<List<StoreFrontProfile>> GetAllStoresProfile()
    {
      string _sqlQuery = @"SELECT storeID, storeName, storeAddress
                          FROM StoreFronts;";

      List<StoreFrontProfile> _listStores = new List<StoreFrontProfile>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = await command.ExecuteReaderAsync();

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

    public async Task<StoreFrontProfile> UpdateStoreProfile(StoreFrontProfile p_store)
    {
      string _sqlQuery = @"UPDATE StoreFronts
                          SET storeName=@storeName, storeAddress=@storeAddress
                          WHERE storeID=@storeID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@storeName", p_store.Name);
        command.Parameters.AddWithValue("@storeAddress", p_store.Address);
        command.Parameters.AddWithValue("@storeID", p_store.StoreID);

        await command.ExecuteNonQueryAsync();
      }

      return p_store;
    }
  }
}