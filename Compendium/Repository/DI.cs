using Compendium.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Compendium.Repository
{
  [ExcludeFromCodeCoverage]
  public static class DI
  {
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
      services.AddScoped(typeof(IColumnRepository), typeof(Postgre.ColumnRepository));
      services.AddScoped(typeof(ITableRepository), typeof(Postgre.TableRepository));

      services.AddScoped(typeof(ISyscatRepository), typeof(DB2.SyscatRepository));

      return services;
    }
  }
}
