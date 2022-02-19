using System.Text;

namespace UI
{
  public class MainMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Displaying");
      Console.WriteLine("[1] - Password");
      Console.WriteLine("[0] - Exit");
    }

    public MenuType UserChoice()
    {
      string ans = Console.ReadLine();

      switch (ans)
      {
        case "0":
          return MenuType.Exit;
        case "1":
          Console.WriteLine("Enter password:");
          string password = Console.ReadLine();

          GeneratePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

          Console.WriteLine($"password: {password}\npasswordHash: {Encoding.UTF8.GetString(passwordHash)}\npasswordSalt: {Encoding.UTF8.GetString(passwordSalt)}");

          Console.WriteLine("Please press enter to continue");
          Console.ReadLine();
          return MenuType.MainMenu;
        default:
          Console.WriteLine("Please press enter to continue");
          Console.ReadLine();
          return MenuType.MainMenu;
      }

    }

    private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (string.IsNullOrWhiteSpace(password))
      {
        Console.WriteLine("Password Null or White Space only");
      }

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
      }
    }
  }
}