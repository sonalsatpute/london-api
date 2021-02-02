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

    public async Task<PagedResult<Room>> GetRoomsAsync(PagingOptions pagingOptions)
    {
      IQueryable<RoomEntity> query = _context.Rooms;

      int size = await query.CountAsync();

      Room[] rooms = await query
        .Skip(pagingOptions.Offset.Value)
        .Take(pagingOptions.Limit.Value)
        .ProjectTo<Room>(_mappingConfigutation)
        .ToArrayAsync();

      return new PagedResult<Room>
      {
        Items = rooms,
        Total = size
      };
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
