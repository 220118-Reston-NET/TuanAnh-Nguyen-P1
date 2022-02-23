using Model;

namespace DL.Interfaces
{
  public interface IAdminServiceDL
  {
    Task<Product> AddNewProduct(Product p_prod);
    Task<List<Product>> GetAllProducts();
    Task<Product> UpdateProduct(Product p_prod);
  }
}