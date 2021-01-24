using Newtonsoft.Json;

namespace London.Api.Models
{
  public abstract class Resource
  {
    [JsonProperty(Order = -2)] //Order -2 to put the property on top
    public string Href { get; set; }
  }
}
