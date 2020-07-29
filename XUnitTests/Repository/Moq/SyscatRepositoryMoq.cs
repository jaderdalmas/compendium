using Compendium.Properties;
using Compendium.Repository.Interfaces;
using Compendium.Repository.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace XUnitTests.Repository.Moq
{
  [ExcludeFromCodeCoverage]
  public class SyscatRepositoryMoq : ISyscatRepository
  {
    public Task<IEnumerable<SyscatColumn>> GetColumn()
        => Task.FromResult<IEnumerable<SyscatColumn>>(new List<SyscatColumn>());

    public Task<IEnumerable<SyscatTable>> GetTable()
        => Task.FromResult<IEnumerable<SyscatTable>>(new List<SyscatTable>());
  }

  [ExcludeFromCodeCoverage]
  public class SyscatRepositoryMoqNull : ISyscatRepository
  {
    public Task<IEnumerable<SyscatColumn>> GetColumn()
        => Task.FromResult<IEnumerable<SyscatColumn>>(null);

    public Task<IEnumerable<SyscatTable>> GetTable()
        => Task.FromResult<IEnumerable<SyscatTable>>(null);
  }

  [ExcludeFromCodeCoverage]
  public class SyscatRepositoryMoqException : ISyscatRepository
  {
    public Task<IEnumerable<SyscatColumn>> GetColumn()
        => throw new ExternalException(Resources.Test);

    public Task<IEnumerable<SyscatTable>> GetTable()
        => throw new ExternalException(Resources.Test);
  }
}
