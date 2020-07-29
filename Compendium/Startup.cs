using Compendium.Filter;
using Compendium.Notify;
using Compendium.Repository;
using Compendium.Service;
using Compendium.Setting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Compendium
{
  public class Startup
  {
    private IConfiguration _configuration { get; }
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews(options =>
      {
        options.Filters.Add<CustomExceptionsFilter>();
        options.Filters.Add<NotificationFilter>();
      });

      services.AddNotification();
      services.AddService();
      services.AddRepository();

      services.Configure<DB2Setting>(_configuration.GetSection("DB2"));
      services.Configure<PostgreSetting>(_configuration.GetSection("Postgre"));
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
