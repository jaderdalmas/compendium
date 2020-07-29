using Compendium.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace XUnitTests.Service.Moq
{
  [ExcludeFromCodeCoverage]
  public static class DI
  {
    public static IServiceCollection AddMoqService(this IServiceCollection services, ETestType type = ETestType.Empty)
    {
      switch (type)
      {
        case ETestType.Null:
          services.AddScoped(typeof(ISyscatSyncService), typeof(SyscatSyncServiceMoqNull));
          break;
        case ETestType.Exception:
          services.AddScoped(typeof(ISyscatSyncService), typeof(SyscatSyncServiceMoqException));
          break;
        default:
          services.AddScoped(typeof(ISyscatSyncService), typeof(SyscatSyncServiceMoq));
          break;
      }
      return services;
    }
  }
}
