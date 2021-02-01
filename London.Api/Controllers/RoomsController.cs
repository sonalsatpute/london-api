using London.Api.Models;
using London.Api.Services;
using Microsoft.AspNetCore.Mvc;
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

    public RoomsController(
        IRoomService roomService, IOpeningService openingService)
    {
      _roomService = roomService;
      _openingService = openingService;
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
    public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings()
    {
      var openings = await _openingService.GetOpeningsAsync();

      var collection = new Collection<Opening>()
      {
        Self = Link.ToCollection(nameof(GetAllRoomOpenings)),
        Value = openings.ToArray()
      };

      return collection;
    }

  }
}
