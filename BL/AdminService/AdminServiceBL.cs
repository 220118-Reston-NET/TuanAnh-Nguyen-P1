using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class AdminServiceBL : IAdminServiceBL
  {
    private IAdminServiceDL _repo;
    public AdminServiceBL(IAdminServiceDL p_repo)
    {
      _repo = p_repo;
    }
    public Product AddNewProduct(Product p_prod)
    {
      return _repo.AddNewProduct(p_prod);
    }

    public List<Product> GetAllProducts()
    {
      return _repo.GetAllProducts();
    }

    public Product GetProductByID(string p_prodID)
    {
      return GetAllProducts().Find(p => p.ProductID.Equals(p_prodID));
    }

    public List<Product> SearchProductByName(string p_prodName)
    {
      return GetAllProducts().FindAll(p => p.Name.Contains(p_prodName));
    }

    public Product UpdateProduct(Product p_prod)
    {
      return _repo.UpdateProduct(p_prod);
    }
  }
}