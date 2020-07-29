using Compendium;
using Compendium.Notify;
using Compendium.Repository.Interfaces;
using Compendium.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using XUnitTests.Repository.Moq;

namespace XUnitTests.Service
{
  [ExcludeFromCodeCoverage]
  public class BaseServiceTest : IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly WebApplicationFactory<Startup> _factory;
    private readonly IServiceScope _serviceScope;
    private IServiceProvider _serviceProvider => _serviceScope.ServiceProvider;

    public BaseServiceTest(TestWebApplicationFactory<Startup> factory, ETestType type = ETestType.Empty)
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
    public ISyscatSyncService GetSyscatSyncService => _serviceProvider.GetService<ISyscatSyncService>();
    public IColumnRepository GetColumnRepository => _serviceProvider.GetService<IColumnRepository>();
    public ITableRepository GetTableRepository => _serviceProvider.GetService<ITableRepository>();
  }
}