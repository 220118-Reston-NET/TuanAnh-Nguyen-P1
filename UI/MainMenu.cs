using System.Text;

namespace UI
{
  public class MainMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Welcome to my Project 1!");
      Console.WriteLine("Please use these options below to start:");
      Console.WriteLine("[1] - Sign Up");
      Console.WriteLine("[2] - Sign In");
      Console.WriteLine("-----------------------------------------");
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
          return MenuType.SignUp;
        case "2":
          return MenuType.SignIn;
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