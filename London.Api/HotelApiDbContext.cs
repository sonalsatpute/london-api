using London.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace London.Api
{
  public class HotelApiDbContext : DbContext
  {
    public HotelApiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<RoomEntity> Rooms { get; set; } 
  }
}
