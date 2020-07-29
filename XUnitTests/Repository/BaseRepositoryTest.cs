using Compendium;
using Compendium.Notify;
using Compendium.Repository.Interfaces;
using Compendium.Setting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using XUnitTests.Repository.Moq;

namespace XUnitTests.Repository
{
  [ExcludeFromCodeCoverage]
  public class BaseRepositoryTest : IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly WebApplicationFactory<Startup> _factory;
    private readonly IServiceScope _serviceScope;
    private IServiceProvider _serviceProvider => _serviceScope.ServiceProvider;

    public BaseRepositoryTest(TestWebApplicationFactory<Startup> factory, ETestType type = ETestType.Empty)
    {
      if (factory is null) return;
      _factory = factory.WithWebHostBuilder(builder =>
      {
        builder.ConfigureTestServices(services =>
        {
          services.AddMoqRepository(type);
        });
      });

      _serviceScope = _factory.Services.CreateScope();
    }

    public INotification GetNotification => _serviceProvider.GetService<INotification>();

    public IColumnRepository GetColumnRepository => _serviceProvider.GetService<IColumnRepository>();
    public ISyscatRepository GetSyscatRepository => _serviceProvider.GetService<ISyscatRepository>();
    public ITableRepository GetTableRepository => _serviceProvider.GetService<ITableRepository>();

    public IOptions<DB2Setting> GetDB2Settings => _serviceProvider.GetService<IOptions<DB2Setting>>();
    public IOptions<PostgreSetting> GetPostgreSettings => _serviceProvider.GetService<IOptions<PostgreSetting>>();
  }
}