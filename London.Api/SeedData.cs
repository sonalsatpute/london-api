﻿using London.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace London.Api
{
  public static class SeedData
  {
    public static async Task InitializeAsync(IServiceProvider provider)
    {
      await AddTestData(provider.GetRequiredService<HotelApiDbContext>());
    }

    public static async Task AddTestData(HotelApiDbContext context)
    {
      if (context.Rooms.Any())
      {
        //All ready has data
        return;
      }

      context.Rooms.Add(new RoomEntity
      {
        Id = Guid.Parse("301df04d-8679-4b1b-ab92-0a586ae53d08"),
        Name = "Oxford Suite",
        Rate = 10119,
      });

      context.Rooms.Add(new RoomEntity
      {
        Id = Guid.Parse("ee2b83be-91db-4de5-8122-35a9e9195976"),
        Name = "Driscoll Suite",
        Rate = 23959
      });

      await context.SaveChangesAsync();
    }
  }
}
