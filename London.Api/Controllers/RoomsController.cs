using London.Api.Models;
using London.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace London.Api.Controllers
{
  [Route("/[controller]")]
  [ApiController]
  public class RoomsController : ControllerBase
  {
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
      _roomService = roomService;
    }

    [HttpGet(Name = nameof(GetRooms))]
    public IActionResult GetRooms()
    {
      throw new NotImplementedException();
    }

    [HttpGet("{roomId}", Name = nameof(GetRoomById))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
    {
      Room room = await _roomService.GetRoomById(roomId);

      if (room == null) return NotFound();

      return room;
    }

  }
}
