/*
  Tuan Anh Nguyen
  Project 1 - Revature
*/
global using Serilog;
using UI;

Log.Logger = new LoggerConfiguration()
                  .WriteTo.File("./logs/user.txt")
                  .CreateLogger();

bool repeat = true;
IMenu menu = new MainMenu();
FactoryMenu factory = new FactoryMenu();

while (repeat)
{
  Console.Clear();
  menu.Display();
  MenuType ans = menu.UserChoice();

  if (ans != MenuType.Exit)
  {
    menu = factory.CreateMenu(ans);
  }
  else
  {
    Log.Information("Exiting Application");
    repeat = false;
  }
}