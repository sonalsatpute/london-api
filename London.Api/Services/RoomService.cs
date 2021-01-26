using AutoMapper;
using London.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public class RoomService : IRoomService
  {
    private readonly HotelApiDbContext _context;
    private readonly IMapper _mapper;

    public RoomService(HotelApiDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<Room> GetRoomById(Guid roomId)
    {
      RoomEntity entity = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == roomId);

      if (entity == null) return null;

      return _mapper.Map<Room>(entity);
    }
  }
}
