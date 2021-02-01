using System;

namespace London.Api.Models
{
  public class BookingRange
  {
    public DateTimeOffset StartAt { get; set; }
    public DateTimeOffset EndAt { get; set; }
  }
}
