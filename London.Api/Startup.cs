using London.Api.Filters;
using London.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using London.Api.Services;
using AutoMapper;
using London.Api.Infrastructure;
using Landon.Api.Filters;

namespace London.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<HotelInfo>(Configuration.GetSection("Info"));

      services.Configure<HotelOptions>(Configuration);

      services.AddScoped<IRoomService, RoomService>();
      services.AddScoped<IOpeningService, OpeningService>();
      services.AddScoped<IBookingService, BookingService>();
      services.AddScoped<IDateLogicService, DateLogicService>();


      //Use in-memory databata base for quick dev and testing.
      services.AddDbContext<HotelApiDbContext>(options => options.UseInMemoryDatabase("londondb"));

      services.AddRouting(options => options.LowercaseUrls = true);

      services.AddControllers(options =>
      {
        options.Filters.Add<JsonExceptionFilter>();
        options.Filters.Add<RequireHttpsOrCloseAttribute>();
        options.Filters.Add<LinkRewritingFilter>();
      }).AddNewtonsoftJson();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "London.Api", Version = "v1" });
      });

      services.AddApiVersioning(options =>
      {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ApiVersionReader = new MediaTypeApiVersionReader();
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
        options.ReportApiVersions = true;
      });

      // policy => policy.WithOrigins("https://example.com") // set this with origin, you need to use in production
      services.AddCors(options =>
      {
        options.AddPolicy("allowed-app-name-policy", policy => policy.AllowAnyOrigin()); // remove all AllowAny* in production 
      });

      services.AddAutoMapper(options => options.AddProfile<MappingProfile>());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "London.Api v1"));
      }

      //app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseCors("allowed-app-name-policy");
    }
  }
}
