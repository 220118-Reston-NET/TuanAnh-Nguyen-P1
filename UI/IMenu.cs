namespace UI
{
  public enum MenuType
  {
    MainMenu,
    SignUp,
    SignIn,
    Exit,
  }
  public interface IMenu
  {
    void Display();

    MenuType UserChoice();
  }
}