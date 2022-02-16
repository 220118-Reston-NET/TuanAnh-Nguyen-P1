namespace Model
{
  public class Customer
  {
    public Guid CustomerID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime createdAt { get; set; }
    public Customer()
    {
      CustomerID = Guid.NewGuid();
      Username = "";
      Password = "";
      Name = "";
      Address = "";
      Email = "";
      PhoneNumber = "";
      DateOfBirth = DateTime.UtcNow;
      createdAt = DateTime.UtcNow;
    }
    public override string ToString()
    {
      return $"Customer ID: {CustomerID}\nUsername: {Username}\nName: {Name}\nAddress: {Address}\nEmail: {Email}\nPhone Number: {PhoneNumber}\nDate Of Birth: {DateOfBirth}\nCreated At: {createdAt}";
    }
  }
}

