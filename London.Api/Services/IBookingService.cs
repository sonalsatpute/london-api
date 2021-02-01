using London.Api.Models;
using System;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public interface IBookingService
  {
    Task<Booking> GetBookingAsync(Guid bookingId);

    Task<Guid> CreateBookingAsync(Guid userId, Guid roomId, DateTimeOffset startAt, DateTimeOffset endAt);
  }
}
