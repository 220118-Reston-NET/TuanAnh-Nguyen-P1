namespace Model
{
  public class User
  {
    public Guid UserID { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public int? Role { get; set; }
    public bool IsActivated { get; set; }
    public DateTime createdAt { get; set; }
  }
}