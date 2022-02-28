using APIPortal.DataTransferObject;

namespace APIPortal.Extensions
{
  public class PaggedExtensions<T>
  {
    public ResponePage<T> Pagged(List<T> p_list, int limit, int page)
    {
      if (page == 0)
      {
        page = 1;
      }
      int choosenPage = page;
      var pageResults = limit;
      var pageCount = Math.Ceiling(p_list.Count() / (double)pageResults);

      var pagedData = p_list.Skip((page - 1) * pageResults).Take(pageResults).ToList();

      ResponePage<T> result = new ResponePage<T>
      {
        TotalPages = (int)pageCount,
        CurrentPage = choosenPage,
        TotalRecords = p_list.Count(),
        Data = pagedData
      };
      return result;
    }
  }
}