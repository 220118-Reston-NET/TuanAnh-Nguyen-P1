using Model;

namespace BL.Interfaces
{
  public interface IAdminServiceBL
  {
    /*
      ADMIN SERVICE
      Product AddNewProduct(Product p_prod);
      List<Product> GetAllProducts();
      Product GetProductByID(string p_prodID);
      Product UpdateProduct(Product p_prod);
      void DeleteProductByID(string p_prodID);
    */

    /// <summary>
    /// Add a new product to the system
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Product AddNewProduct(Product p_prod);

    /// <summary>
    /// Get All Products in the system
    /// </summary>
    /// <returns>All Products</returns>
    List<Product> GetAllProducts();

    /// <summary>
    /// Get Product Detail By Product ID
    /// </summary>
    /// <param name="p_prodID"></param>
    /// <returns>Product Detail</returns>
    Product GetProductByID(string p_prodID);

    /// <summary>
    /// Update Product Detail
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Product UpdateProduct(Product p_prod);

    /// <summary>
    /// Delete Product By Product ID
    /// </summary>
    /// <param name="p_prodID"></param>
    void DeleteProductByID(string p_prodID);
  }
}