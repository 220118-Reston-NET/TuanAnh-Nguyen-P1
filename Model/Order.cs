namespace Model
{
  public class Order
  {
    public Guid OrderID { get; set; }
    public Guid CustomerID { get; set; }
    public Guid StoreID { get; set; }
    public List<LineItem> Cart { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public List<Tracking> Shipments { get; set; }
    public DateTime createdAt { get; set; }
    public Order()
    {
      OrderID = Guid.NewGuid();
      CustomerID = Guid.NewGuid();
      StoreID = Guid.NewGuid();
      Cart = new List<LineItem>() {
        new LineItem()
      };
      TotalPrice = 0;
      Status = "";
      Shipments = new List<Tracking>();
      createdAt = DateTime.UtcNow;
    }
  }
}