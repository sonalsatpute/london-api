using London.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace London.Api.Services
{
  public interface IRoomService
  {
    Task<Room> GetRoomById(Guid roomId);
    Task<IEnumerable<Room>> GetRoomsAsync();
  }
}
