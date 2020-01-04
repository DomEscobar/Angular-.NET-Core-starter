using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using PublicTimeAPI.Filters;
using PublicTimeAPI.Repository;
using PublicTimeAPI.Services;

namespace PublicTimeAPI
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
      services.AddMvc();

      services.AddResponseCompression();

      services.AddDbContext<ApplicationDbContext>(options =>
      {
        // options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
        options.UseInMemoryDatabase("RESTfulAPI");
      });

      // Add MVC Core
      services.AddMvcCore(
          options =>
          {
            options.Filters.Add(typeof(CustomExceptionFilterAttribute));
          }
         )
         .AddCors();

      services.AddHttpContextAccessor();
      services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

      services.TryAddSingleton<IEmailService, EmailService>();

      services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(o =>
      {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuerSigningKey = true,
          ValidateIssuer = false,
          ValidateAudience = false,
        };
      });
    }

    public void Configure(
      IApplicationBuilder app,
      IHostingEnvironment env)
    {

      app.UseResponseCompression();
      app.UseAuthentication();

      app.UseAuthentication();

      app.UseCors(builder =>
          builder.WithOrigins("*")
          .AllowAnyMethod()
          .AllowAnyHeader());

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
      });
    }
  }
}
