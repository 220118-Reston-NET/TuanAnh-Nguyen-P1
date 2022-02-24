namespace Model
{
  public class Product
  {
    public Guid ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int MinimumAge { get; set; }
    public DateTime createdAt { get; set; }
    public Product()
    {
      ProductID = Guid.NewGuid();
      Name = "";
      Price = 0;
      Description = "";
      MinimumAge = 0;
      createdAt = DateTime.UtcNow;
    }
  }
}