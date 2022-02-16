namespace UI
{
  public enum MenuType
  {
    MainMenu,
    Exit,
  }
  public interface IMenu
  {
    void Display();

    MenuType UserChoice();
  }
}