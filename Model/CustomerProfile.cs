﻿namespace Model
{
  public class CustomerProfile
  {
    public Guid CustomerID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public CustomerProfile()
    {
      CustomerID = Guid.NewGuid();
      FirstName = "";
      LastName = "";
      Address = "";
      Email = "";
      PhoneNumber = "";
      DateOfBirth = DateTime.UtcNow;
    }
    public override string ToString()
    {
      return $"Customer ID: {CustomerID}\nAddress: {Address}\nEmail: {Email}\nPhone Number: {PhoneNumber}\nDate Of Birth: {DateOfBirth}";
    }
  }
}

