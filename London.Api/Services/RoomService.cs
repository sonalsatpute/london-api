using AutoMapper;
using AutoMapper.QueryableExtensions;
using London.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public class RoomService : IRoomService
  {
    private readonly HotelApiDbContext _context;
    private readonly IConfigurationProvider _mappingConfigutation;

    public RoomService(HotelApiDbContext context, IConfigurationProvider mappingConfigutation)
    {
      _context = context;
      _mappingConfigutation = mappingConfigutation;
    }

    public async Task<IEnumerable<Room>> GetRoomsAsync()
    {
      IQueryable<Room> query = _context.Rooms.ProjectTo<Room>(_mappingConfigutation);
      return await query.ToArrayAsync();
    }

    public async Task<Room> GetRoomById(Guid roomId)
    {
      RoomEntity entity = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == roomId);

      if (entity == null) return null;

      IMapper mapper = _mappingConfigutation.CreateMapper();

      return mapper.Map<Room>(entity);
    }
  }
}
