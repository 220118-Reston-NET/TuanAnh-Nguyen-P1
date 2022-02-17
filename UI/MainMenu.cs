namespace UI
{
  public class MainMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Displaying");
    }

    public MenuType UserChoice()
    {
      string ans = Console.ReadLine();

      switch (ans)
      {
        case "1":

          return MenuType.MainMenu;
        default:
          return MenuType.Exit;
      }

    }
  }
}