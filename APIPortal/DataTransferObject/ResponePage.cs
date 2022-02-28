namespace APIPortal.DataTransferObject
{
  public class ResponePage<T>
  {
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public List<T> Data { get; set; } = new List<T>();
  }
}