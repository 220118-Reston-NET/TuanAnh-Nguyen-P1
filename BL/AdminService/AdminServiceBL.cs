using BL.Interfaces;
using DL.Interfaces;
using Model;

namespace BL.Implements
{
  public class AdminServiceBL : IAdminServiceBL
  {
    private readonly IAdminServiceDL _repo;
    public AdminServiceBL(IAdminServiceDL p_repo)
    {
      _repo = p_repo;
    }
    public async Task<Product> AddNewProduct(Product p_prod)
    {
      List<Product> _listOfProduct = await GetAllProducts();

      if (_listOfProduct.Any(p => p.Name.Equals(p_prod.Name)))
      {
        throw new Exception("Cannot add new product due to the name is already in the system!");
      }
      return await _repo.AddNewProduct(p_prod);
    }

    public async Task<List<Product>> GetAllProducts()
    {
      return await _repo.GetAllProducts();
    }

    public async Task<Product> GetProductByID(Guid p_prodID)
    {
      List<Product> _listOfProduct = await GetAllProducts();
      return _listOfProduct.Find(p => p.ProductID.Equals(p_prodID));
    }

    public async Task<List<Product>> SearchProductByName(string p_prodName)
    {
      List<Product> _listOfProduct = await GetAllProducts();
      return _listOfProduct.FindAll(p => p.Name.Contains(p_prodName));
    }

    public async Task<Product> UpdateProduct(Product p_prod)
    {
      List<Product> _listOfProduct = await GetAllProducts();
      List<Product> _listFilterProduct = _listOfProduct.FindAll(p => p.ProductID != p_prod.ProductID);
      if (_listFilterProduct.Any(p => p.Name.Equals(p_prod.Name)))
      {
        throw new Exception("Cannot update the product due to the name is already in the system!");
      }
      return await _repo.UpdateProduct(p_prod);
    }
  }
}