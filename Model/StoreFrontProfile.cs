namespace Model
{
  public class StoreFrontProfile
  {
    public Guid StoreID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public StoreFrontProfile()
    {
      StoreID = Guid.NewGuid();
      Name = "";
      Address = "";
    }
  }
}