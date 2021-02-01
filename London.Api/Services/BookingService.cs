using AutoMapper;
using London.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public class BookingService : IBookingService
  {
    private readonly HotelApiDbContext _context;
    private readonly IDateLogicService _dateLogicService;
    private readonly IMapper _mapper;

    public BookingService(HotelApiDbContext context, IDateLogicService dateLogicService, IMapper mapper)
    {
      _context = context;
      _dateLogicService = dateLogicService;
      _mapper = mapper;
    }

    public Task<Guid> CreateBookingAsync(Guid userId, Guid roomId, DateTimeOffset startAt, DateTimeOffset endAt)
    {
      throw new NotImplementedException();
    }

    public async Task<Booking> GetBookingAsync(Guid bookingId)
    {
      var entity = await _context.Bookings.SingleOrDefaultAsync(b => b.Id == bookingId);

      if (entity == null) return null;

      return _mapper.Map<Booking>(entity);
    }
  }
}
