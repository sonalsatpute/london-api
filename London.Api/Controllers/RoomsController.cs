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
    [ProducesResponseType(400)]
    public async Task<ActionResult<Collection<Room>>> GetAllRooms([FromQuery] PagingOptions pagingOptions)
    {
      pagingOptions.Offset ??= _defaultPagingOptions.Offset;
      pagingOptions.Limit ??= _defaultPagingOptions.Limit;

      PagedResult<Room> pagedResult = await _roomService.GetRoomsAsync(pagingOptions);

      var collections = PagedCollection<Room>.Create(
        Link.ToCollection(nameof(GetAllRoomOpenings)),
        pagedResult.Items.ToArray(), pagedResult.Total, pagingOptions);

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

      var collection = PagedCollection<Opening>.Create(
        Link.ToCollection(nameof(GetAllRoomOpenings)),
        openings.Items.ToArray(), openings.Total, pagingOptions);

      return collection;
    }

  }
}
