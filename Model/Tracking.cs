namespace Model
{
  public class Tracking
  {
    public Guid TrackingID { get; set; }
    public Guid OrderID { get; set; }
    public string TrackingNumber { get; set; }
    public Tracking()
    {
      TrackingID = Guid.NewGuid();
      OrderID = Guid.NewGuid();
      TrackingNumber = "";
    }
  }
}