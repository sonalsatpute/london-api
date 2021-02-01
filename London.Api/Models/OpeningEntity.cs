using System;

namespace London.Api.Models
{
  class OpeningEntity : BookingEntity
  {
    public Guid RoomId { get; set; }
    public int Rate { get; set; }
  }
}
