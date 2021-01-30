using Newtonsoft.Json;

namespace London.Api.Models
{
  public abstract class Resource : Link
  {
    [JsonIgnore]
    public Link Self { get; set; }
  }
}
