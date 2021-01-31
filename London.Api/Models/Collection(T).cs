namespace London.Api.Models
{
  public class Collection<T> : Resource
  {
    public T[] Value { get; set; }
  }
}
