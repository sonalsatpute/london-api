using London.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public interface IOpeningService
  {
    Task<IEnumerable<Opening>> GetOpeningsAsync();

    Task<IEnumerable<BookingRange>> GetConflictingSlots(
        Guid roomId,
        DateTimeOffset start,
        DateTimeOffset end);
  }
}
