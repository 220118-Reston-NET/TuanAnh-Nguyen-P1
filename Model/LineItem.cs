namespace Model
{
  public class LineItem
  {
    public Guid ProductID { get; set; }
    public Guid OrderID { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtCheckedOut { get; set; }
    public LineItem()
    {
      ProductID = Guid.NewGuid();
      OrderID = Guid.NewGuid();
      Quantity = 0;
      PriceAtCheckedOut = 0;
    }
  }
}