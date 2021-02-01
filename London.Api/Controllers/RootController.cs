using London.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace London.Api.Controllers
{
  [Route("/")]
  [ApiController]
  [ApiVersion("1.0")]
  public class RootController : ControllerBase
  {
    [HttpGet(Name = nameof(GetRoot))]
    [ProducesResponseType(200)]
    public IActionResult GetRoot() 
    {
      var response = new RootResponse
      {
        Self = Link.To(nameof(RootController.GetRoot)),
        Rooms = Link.ToCollection(nameof(RoomsController.GetAllRooms)),
        Info = Link.To(nameof(InfoController.GetInfo))
      };

      return Ok(response);
    }
  }
}
