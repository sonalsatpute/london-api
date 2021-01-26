using AutoMapper;
using London.Api.Models;

namespace London.Api.Infrastructure
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<RoomEntity, Room>().ForMember(destination => destination.Rate, option => option.MapFrom(src => src.Rate / 100.0m));
    }
  }
}
