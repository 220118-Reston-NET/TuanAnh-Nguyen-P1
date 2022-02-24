namespace Model
{
  public class Inventory
  {
    public Guid StoreID { get; set; }
    public Guid ProductID { get; set; }
    public int Quantity { get; set; }
    public Inventory()
    {
      StoreID = Guid.NewGuid();
      ProductID = Guid.NewGuid();
      Quantity = 0;
    }
  }
}