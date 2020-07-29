using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Compendium.Notify
{
  [ExcludeFromCodeCoverage]
  public static class DI
  {
    public static IServiceCollection AddNotification(this IServiceCollection services)
    {
      services.AddScoped<INotification, NotificationProvider>();

      return services;
    }
  }
}
