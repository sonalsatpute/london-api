using AutoMapper;
using London.Api.Models;

namespace London.Api.Infrastructure
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<RoomEntity, Room>()
        .ForMember(destination => destination.Rate, option => option.MapFrom(src => src.Rate / 100.0m))
        .ForMember(destination => destination.Self, option => option.MapFrom(src =>
          Link.To(nameof(Controllers.RoomsController.GetRoomById), new { roomId = src.Id })
        ));
    }
  }
}
