/*
  Tuan Anh Nguyen
  Project 1 - Revature
*/
global using Serilog;
using UI;
using BL;
using DL;
using Microsoft.Extensions.Configuration;

Log.Logger = new LoggerConfiguration()
                  .WriteTo.File("./logs/user.txt")
                  .CreateLogger();

var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
string _connectionString = configuration.GetConnectionString("Reference2DB");

bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
  Console.Clear();
  menu.Display();
  MenuType ans = menu.UserChoice();

  switch (ans)
  {
    case MenuType.MainMenu:
      menu = new MainMenu();
      break;
    default:
      Console.WriteLine("Page does not exist!");
      break;
  }
}