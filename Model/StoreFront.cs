namespace Model
{
  public class StoreFront
  {
    public Guid StoreID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime createdAt { get; set; }
    public StoreFront()
    {
      StoreID = Guid.NewGuid();
      Username = "";
      Password = "";
      Name = "";
      Address = "";
      createdAt = DateTime.UtcNow;
    }
    public override string ToString()
    {
      return $"Store ID: {StoreID}\nName: {Name}\nAddress: {Address}\nCreated At: {createdAt}";
    }
  }
}