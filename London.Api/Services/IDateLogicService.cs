using London.Api.Models;
using System;
using System.Collections.Generic;

namespace London.Api.Services
{
  public interface IDateLogicService
  {
    DateTimeOffset AlignStartTime(DateTimeOffset data);

    TimeSpan GetMinimumStay();

    DateTimeOffset FurthestPossibleBooking(DateTimeOffset now);

    IEnumerable<BookingRange> GetAllSlots(DateTimeOffset start, DateTimeOffset? end = null);

    bool DoesConflict(BookingRange b, DateTimeOffset start, DateTimeOffset end);
  }
}
