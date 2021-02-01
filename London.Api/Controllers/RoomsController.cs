using London.Api.Models;
using London.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace London.Api.Controllers
{
  [Route("/[controller]")]
  [ApiController]
  public class RoomsController : ControllerBase
  {
    private readonly IRoomService _roomService;
    private readonly IOpeningService _openingService;
    private readonly PagingOptions _defaultPagingOptions;

    public RoomsController(IRoomService roomService, IOpeningService openingService, IOptions<PagingOptions> defaultPagingOptionsWrapper)
    {
      _roomService = roomService;
      _openingService = openingService;
      _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
    }

    [HttpGet(Name = nameof(GetAllRooms))]
    [ProducesResponseType(200)]
    public async Task<ActionResult<Collection<Room>>> GetAllRooms()
    {
      var rooms = await _roomService.GetRoomsAsync();

      var collections = new Collection<Room>
      {
        Self = Link.ToCollection(nameof(GetAllRooms)),
        Value = rooms.ToArray()
      };

      return collections;
    }

    [HttpGet("{roomId}", Name = nameof(GetRoomById))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
    {
      Room room = await _roomService.GetRoomById(roomId);

      if (room == null) return NotFound();

      return room;
    }

    // GET /rooms/openings
    [HttpGet("openings", Name = nameof(GetAllRoomOpenings))]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings([FromQuery] PagingOptions pagingOptions = null)
    {
      pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
      pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

      PagedResult<Opening> openings = await _openingService.GetOpeningsAsync(pagingOptions);

      var collection = new PagedCollection<Opening>()
      {
        Self = Link.ToCollection(nameof(GetAllRoomOpenings)),
        Value = openings.Items.ToArray(),
        Size = openings.Total,
        Offset = pagingOptions.Offset,
        Limit = pagingOptions.Limit,
      };

      return collection;
    }

  }
}
