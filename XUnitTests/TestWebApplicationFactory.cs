using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics.CodeAnalysis;

namespace XUnitTests
{
  [ExcludeFromCodeCoverage]
  public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      /*if (builder is null) return;
      builder.ConfigureAppConfiguration((builderContext, config) =>
      {
          config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
      });*/

      // if you want to override Physical database with in-memory database
      //builder.ConfigureServices();
    }
  }
}
