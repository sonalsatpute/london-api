using London.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public class RoomService : IRoomService
  {
    private readonly HotelApiDbContext _context;

    public RoomService(HotelApiDbContext context)
    {
      _context = context;
    }

    public async Task<Room> GetRoomById(Guid roomId)
    {
      RoomEntity entity = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == roomId);

      if (entity == null) return null;

      return new Room
      {
        Href = null,
        Name = entity.Name,
        Rate = entity.Rate / 100
      };
    }
  }
}
