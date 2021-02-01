using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace London.Api.Models
{
  class BookingRangeComparer : IEqualityComparer<BookingRange>
  {
    public bool Equals(BookingRange x, BookingRange y) => x.StartAt == y.StartAt && x.EndAt == y.EndAt;

    public int GetHashCode([DisallowNull] BookingRange obj) => obj.StartAt.GetHashCode() ^ obj.EndAt.GetHashCode();
  }
}
