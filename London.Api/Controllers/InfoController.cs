using London.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace London.Api.Controllers
{
  [Route("/[controller]")]
  [ApiController]
  public class InfoController : ControllerBase
  {
    private readonly HotelInfo _hotelInfo;

    public InfoController(IOptions<HotelInfo> hotelInfoWrapper)
    {
      _hotelInfo = hotelInfoWrapper.Value;
    }

    [HttpGet(Name = nameof(GetInfo))]
    [ProducesResponseType(200)]
    public ActionResult<HotelInfo> GetInfo()
    {
      _hotelInfo.Href = Url.Link(nameof(GetInfo), null);

      return Ok(_hotelInfo);
    }
  }
}
