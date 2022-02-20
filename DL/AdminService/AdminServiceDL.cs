using System.Data.SqlClient;
using DL.Interfaces;
using Model;

namespace DL.Implements
{
  public class AdminServiceDL : IAdminServiceDL
  {
    private readonly string _connectionString;
    public AdminServiceDL(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public Product AddNewProduct(Product p_prod)
    {
      string _sqlQuery = @"INSERT INTO Products
                          (productID, productName, productPrice, productDesc, minimumAge, createdAt)
                          VALUES(@productID, @productName, @productPrice, @productDesc, @minimumAge, @createdAt);";

      p_prod.ProductID = Guid.NewGuid();
      p_prod.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@productID", p_prod.ProductID);
        command.Parameters.AddWithValue("@productName", p_prod.Name);
        command.Parameters.AddWithValue("@productPrice", p_prod.Price);
        command.Parameters.AddWithValue("@productDesc", p_prod.Description);
        command.Parameters.AddWithValue("@minimumAge", p_prod.MinimumAge);
        command.Parameters.AddWithValue("@createdAt", p_prod.createdAt);

        command.ExecuteNonQuery();
      }

      return p_prod;
    }

    public List<Product> GetAllProducts()
    {
      string _sqlQuery = @"SELECT productID, productName, productPrice, productDesc, minimumAge, createdAt
                          FROM Products;";
      List<Product> _listOfProducts = new List<Product>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listOfProducts.Add(new Product()
          {
            ProductID = reader.GetGuid(0),
            Name = reader.GetString(1),
            Price = reader.GetDecimal(2),
            Description = reader.GetString(3),
            MinimumAge = reader.GetInt32(4),
            createdAt = reader.GetDateTime(5)
          });
        }
      }

      return _listOfProducts;
    }

    public Product UpdateProduct(Product p_prod)
    {
      string _sqlQuery = @"UPDATE Products
                          SET productName=@productName, productPrice=@productPrice, productDesc=@productDesc, minimumAge=@minimumAge, createdAt=@createdAt
                          WHERE productID=@productID;";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@productName", p_prod.Name);
        command.Parameters.AddWithValue("@productPrice", p_prod.Price);
        command.Parameters.AddWithValue("@productDesc", p_prod.Description);
        command.Parameters.AddWithValue("@minimumAge", p_prod.MinimumAge);
        command.Parameters.AddWithValue("@createdAt", p_prod.createdAt);
        command.Parameters.AddWithValue("@productID", p_prod.ProductID);

        command.ExecuteNonQuery();
      }

      return p_prod;
    }
  }
}