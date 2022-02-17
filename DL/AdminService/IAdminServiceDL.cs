using Model;

namespace DL.Interfaces
{
  public interface IAdminServiceDL
  {
    Product AddNewProduct(Product p_prod);
    List<Product> GetAllProducts();
    Product UpdateProduct(Product p_prod);
    void DeleteProductByID(string p_prodID);
  }
}