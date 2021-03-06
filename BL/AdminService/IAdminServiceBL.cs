using Model;

namespace BL.Interfaces
{
  public interface IAdminServiceBL
  {
    /*
      ADMIN SERVICE
      Product AddNewProduct(Product p_prod);
      List<Product> GetAllProducts();
      Product GetProductByID(Guid p_prodID);
      Product UpdateProduct(Product p_prod);
      List<Product> SearchProductByName(string p_prodName);
    */

    /// <summary>
    /// Add a new product to the system
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Task<Product> AddNewProduct(Product p_prod);

    /// <summary>
    /// Get All Products in the system
    /// </summary>
    /// <returns>All Products</returns>
    Task<List<Product>> GetAllProducts();

    /// <summary>
    /// Get Product Detail By Product ID
    /// </summary>
    /// <param name="p_prodID"></param>
    /// <returns>Product Detail</returns>
    Task<Product> GetProductByID(Guid p_prodID);

    /// <summary>
    /// Update Product Detail
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Task<Product> UpdateProduct(Product p_prod);

    /// <summary>
    /// Search Products By Product Name
    /// </summary>
    /// <param name="p_prodName"></param>
    /// <returns></returns>
    Task<List<Product>> SearchProductByName(string p_prodName);
  }
}