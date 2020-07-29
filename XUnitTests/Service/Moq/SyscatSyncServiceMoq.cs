using Compendium.Properties;
using Compendium.Service.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace XUnitTests.Service.Moq
{
  [ExcludeFromCodeCoverage]
  public class SyscatSyncServiceMoq : ISyscatSyncService
  {
    public Task<bool> ColumnSync()
        => Task.FromResult(true);

    public Task<bool> Sync()
        => Task.FromResult(true);

    public Task<bool> TableSync()
        => Task.FromResult(true);
  }

  [ExcludeFromCodeCoverage]
  public class SyscatSyncServiceMoqNull : ISyscatSyncService
  {
    public Task<bool> ColumnSync()
        => Task.FromResult(false);

    public Task<bool> Sync()
        => Task.FromResult(false);

    public Task<bool> TableSync()
        => Task.FromResult(false);
  }

  [ExcludeFromCodeCoverage]
  public class SyscatSyncServiceMoqException : ISyscatSyncService
  {
    public Task<bool> ColumnSync()
        => throw new ExternalException(Resources.Test);

    public Task<bool> Sync()
        => throw new ExternalException(Resources.Test);

    public Task<bool> TableSync()
        => throw new ExternalException(Resources.Test);
  }
}
