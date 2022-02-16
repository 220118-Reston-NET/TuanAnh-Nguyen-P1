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
    public override string ToString()
    {
      return $"Store ID: {StoreID}\nProduct ID: {ProductID}\nQuantity: {Quantity}";
    }
  }
}