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
      throw new NotImplementedException();
    }

    public void DeleteProductByID(string p_prodID)
    {
      throw new NotImplementedException();
    }

    public List<Product> GetAllProducts()
    {
      throw new NotImplementedException();
    }

    public Product UpdateProduct(Product p_prod)
    {
      throw new NotImplementedException();
    }
  }
}