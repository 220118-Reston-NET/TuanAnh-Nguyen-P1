namespace UI
{
  public interface IFactory
  {
    IMenu CreateMenu(MenuType p_menu);
  }
}