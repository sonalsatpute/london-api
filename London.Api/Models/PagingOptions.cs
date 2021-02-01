using System.ComponentModel.DataAnnotations;

namespace London.Api.Models
{
  public class PagingOptions
  {
    [Range(1, int.MaxValue, ErrorMessage = "Offset must be greater than 0.")]
    public int? Offset { get; set; }

    [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
    public int? Limit { get; set; }

    public PagingOptions Replace(PagingOptions never)
    {
      return new PagingOptions
      {
        Offset = never.Offset ?? this.Offset,
        Limit = never.Limit ?? this.Limit
      };
    }
  }
}
