using Model;

namespace APIPortal.Extensions
{
  public class OrderByAndFilterExtension
  {
    public List<Order> OrderByAndFilterExtensionForOrder(List<Order> p_listOrder, string orderby, string filter)
    {
      List<Order> _result = p_listOrder;
      if (orderby == null)
      {
        _result = p_listOrder;
      }
      else if (orderby.ToLower() == "createdat")
      {
        _result = p_listOrder.OrderBy(p => p.createdAt).ToList();
      }
      else if (orderby.ToLower() == "totalprice")
      {
        _result = p_listOrder.OrderBy(p => p.TotalPrice).ToList();
      }
      else if (orderby.ToLower() == "status")
      {
        _result = p_listOrder.OrderBy(p => p.Status).ToList();
      }

      if (filter == null)
      {
        return _result;
      }
      else if (filter.ToLower() == "orderplaced")
      {
        _result = _result.FindAll(p => p.Status.Equals("Order Placed"));
      }
      else if (filter.ToLower() == "processing")
      {
        _result = _result.FindAll(p => p.Status.Equals("Processing"));
      }
      else if (filter.ToLower() == "shipped")
      {
        _result = _result.FindAll(p => p.Status.Equals("Shipped"));
      }
      else if (filter.ToLower() == "cancelled")
      {
        _result = _result.FindAll(p => p.Status.Equals("Cancelled"));
      }

      return _result;
    }

    public List<Inventory> OrderByAndFilterExtensionForInventory(List<Inventory> p_listInventories, string orderby, int filter)
    {
      List<Inventory> _result = p_listInventories;
      if (orderby == null)
      {
        _result = p_listInventories;
      }
      else if (orderby.ToLower() == "quantity")
      {
        _result = p_listInventories.OrderBy(p => p.Quantity).ToList();
      }

      if (filter == 0)
      {
        return _result;
      }
      else
      {
        _result = _result.FindAll(p => p.Quantity > filter);
      }

      return _result;
    }
  }
}