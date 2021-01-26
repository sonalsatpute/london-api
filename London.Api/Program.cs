using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace London.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      IHost host = CreateHostBuilder(args).Build();
      InitializeDatabase(host);
      host.Run();
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });

    private static void InitializeDatabase(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        IServiceProvider serviceProvider = scope.ServiceProvider;

        try
        {
          SeedData.InitializeAsync(serviceProvider).Wait();
        }
        catch (Exception ex)
        {
          ILogger logger = serviceProvider.GetRequiredService<ILogger>();

          logger.LogError(ex, "An error occured seeding database.");
        }
      }
    }
  }
}
