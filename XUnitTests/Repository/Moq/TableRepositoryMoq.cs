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
  public class TableRepositoryMoq : ITableRepository
  {
    public Task<bool> Delete(long id)
        => Task.FromResult(true);

    public Task<bool> Delete(IEnumerable<long> ids)
        => Task.FromResult(true);

    public Task<bool> Exists(long id)
        => Task.FromResult(true);

    public Task<IEnumerable<TableEntity>> GetAll(string name)
        => Task.FromResult<IEnumerable<TableEntity>>(new List<TableEntity>());

    public Task<IEnumerable<TableEntity>> GetAll()
        => Task.FromResult<IEnumerable<TableEntity>>(new List<TableEntity>());

    public Task<TableEntity> GetOne(long id)
        => Task.FromResult(new TableEntity());

    public Task<long> Insert(TableEntity entity)
        => Task.FromResult<long>(1);

    public Task<bool> Insert(IEnumerable<TableEntity> entities)
        => Task.FromResult(true);

    public Task<bool> Update(TableEntity entity)
        => Task.FromResult(true);

    public Task<bool> Update(IEnumerable<TableExternalEntity> entities)
        => Task.FromResult(true);
  }

  [ExcludeFromCodeCoverage]
  public class TableRepositoryMoqNull : ITableRepository
  {
    public Task<bool> Delete(long id)
        => Task.FromResult(false);

    public Task<bool> Delete(IEnumerable<long> ids)
        => Task.FromResult(false);

    public Task<bool> Exists(long id)
        => Task.FromResult(false);

    public Task<IEnumerable<TableEntity>> GetAll(string name)
        => Task.FromResult<IEnumerable<TableEntity>>(null);

    public Task<IEnumerable<TableEntity>> GetAll()
        => Task.FromResult<IEnumerable<TableEntity>>(null);

    public Task<TableEntity> GetOne(long id)
        => Task.FromResult<TableEntity>(null);

    public Task<long> Insert(TableEntity entity)
        => Task.FromResult<long>(0);

    public Task<bool> Insert(IEnumerable<TableEntity> entities)
        => Task.FromResult(false);

    public Task<bool> Update(TableEntity entity)
        => Task.FromResult(false);

    public Task<bool> Update(IEnumerable<TableExternalEntity> entities)
        => Task.FromResult(false);
  }

  [ExcludeFromCodeCoverage]
  public class TableRepositoryMoqException : ITableRepository
  {
    public Task<bool> Delete(long id)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Delete(IEnumerable<long> ids)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Exists(long id)
        => throw new ExternalException(Resources.Test);

    public Task<IEnumerable<TableEntity>> GetAll(string name)
        => throw new ExternalException(Resources.Test);

    public Task<IEnumerable<TableEntity>> GetAll()
        => throw new ExternalException(Resources.Test);

    public Task<TableEntity> GetOne(long id)
        => throw new ExternalException(Resources.Test);

    public Task<long> Insert(TableEntity entity)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Insert(IEnumerable<TableEntity> entities)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Update(TableEntity entity)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Update(IEnumerable<TableExternalEntity> entities)
        => throw new ExternalException(Resources.Test);
  }
}
