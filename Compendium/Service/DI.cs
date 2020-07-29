using Compendium.Service.Interfaces;
using Compendium.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Compendium.Service
{
  [ExcludeFromCodeCoverage]
  public static class DI
  {
    public static IServiceCollection AddService(this IServiceCollection services)
    {
      services.AddScoped(typeof(ISyscatSyncService), typeof(SyscatSyncService));

      return services;
    }
  }
}
