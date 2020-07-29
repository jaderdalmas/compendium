using Compendium.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace XUnitTests.Repository.Moq
{
  [ExcludeFromCodeCoverage]
  public static class DI
  {
    public static IServiceCollection AddMoqRepository(this IServiceCollection services, ETestType type = ETestType.Empty)
    {
      switch (type)
      {
        case ETestType.Null:
          services.AddScoped(typeof(IColumnRepository), typeof(ColumnRepositoryMoqNull));
          services.AddScoped(typeof(ITableRepository), typeof(TableRepositoryMoqNull));
          services.AddScoped(typeof(ISyscatRepository), typeof(SyscatRepositoryMoqNull));
          break;
        case ETestType.Exception:
          services.AddScoped(typeof(IColumnRepository), typeof(ColumnRepositoryMoqException));
          services.AddScoped(typeof(ITableRepository), typeof(TableRepositoryMoqException));
          services.AddScoped(typeof(ISyscatRepository), typeof(SyscatRepositoryMoqException));
          break;
        default:
          services.AddScoped(typeof(IColumnRepository), typeof(ColumnRepositoryMoq));
          services.AddScoped(typeof(ITableRepository), typeof(TableRepositoryMoq));
          services.AddScoped(typeof(ISyscatRepository), typeof(SyscatRepositoryMoq));
          break;
      }

      return services;
    }
  }
}
