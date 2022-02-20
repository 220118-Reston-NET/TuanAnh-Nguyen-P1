using Microsoft.Extensions.Configuration;

namespace UI
{
  public class FactoryMenu : IFactory
  {
    public IMenu CreateMenu(MenuType p_menu)
    {
      var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
      string _connectionString = configuration.GetConnectionString("Reference2DB");

      switch (p_menu)
      {
        case MenuType.MainMenu:
          Log.Information("Displaying MainMenu");
          return new MainMenu();
        case MenuType.SignIn:
          Log.Information("Displaying SignInMenu");
          return new SignInMenu();
        case MenuType.SignUp:
          Log.Information("Displaying SignUpMenu");
          return new SignUpMenu();
        default:
          return new MainMenu();
      }
    }
  }
}