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
  public class ColumnRepositoryMoq : IColumnRepository
  {
    public Task<bool> Delete(long id)
        => Task.FromResult(true);

    public Task<bool> Delete(IEnumerable<long> ids)
        => Task.FromResult(true);

    public Task<bool> Exists(long id)
        => Task.FromResult(true);

    public Task<IEnumerable<ColumnEntity>> GetAll(string tableName, string name)
        => Task.FromResult<IEnumerable<ColumnEntity>>(new List<ColumnEntity>());

    public Task<IEnumerable<ColumnEntity>> GetAll()
        => Task.FromResult<IEnumerable<ColumnEntity>>(new List<ColumnEntity>());

    public Task<ColumnEntity> GetOne(long id)
        => Task.FromResult(new ColumnEntity());

    public Task<long> Insert(ColumnEntity entity)
        => Task.FromResult<long>(1);

    public Task<bool> Insert(IEnumerable<ColumnEntity> entities)
        => Task.FromResult(true);

    public Task<bool> Update(ColumnEntity entity)
        => Task.FromResult(true);

    public Task<bool> Update(IEnumerable<ColumnExternalEntity> entities)
        => Task.FromResult(true);
  }

  [ExcludeFromCodeCoverage]
  public class ColumnRepositoryMoqNull : IColumnRepository
  {
    public Task<bool> Delete(long id)
        => Task.FromResult(false);

    public Task<bool> Delete(IEnumerable<long> ids)
        => Task.FromResult(false);

    public Task<bool> Exists(long id)
        => Task.FromResult(false);

    public Task<IEnumerable<ColumnEntity>> GetAll(string tableName, string name)
        => Task.FromResult<IEnumerable<ColumnEntity>>(null);

    public Task<IEnumerable<ColumnEntity>> GetAll()
        => Task.FromResult<IEnumerable<ColumnEntity>>(null);

    public Task<ColumnEntity> GetOne(long id)
        => Task.FromResult<ColumnEntity>(null);

    public Task<long> Insert(ColumnEntity entity)
        => Task.FromResult<long>(0);

    public Task<bool> Insert(IEnumerable<ColumnEntity> entities)
        => Task.FromResult(false);

    public Task<bool> Update(ColumnEntity entity)
        => Task.FromResult(false);

    public Task<bool> Update(IEnumerable<ColumnExternalEntity> entities)
        => Task.FromResult(false);
  }

  [ExcludeFromCodeCoverage]
  public class ColumnRepositoryMoqException : IColumnRepository
  {
    public Task<bool> Delete(long id)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Delete(IEnumerable<long> ids)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Exists(long id)
        => throw new ExternalException(Resources.Test);

    public Task<IEnumerable<ColumnEntity>> GetAll(string tableName, string name)
        => throw new ExternalException(Resources.Test);

    public Task<IEnumerable<ColumnEntity>> GetAll()
        => throw new ExternalException(Resources.Test);

    public Task<ColumnEntity> GetOne(long id)
        => throw new ExternalException(Resources.Test);

    public Task<long> Insert(ColumnEntity entity)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Insert(IEnumerable<ColumnEntity> entities)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Update(ColumnEntity entity)
        => throw new ExternalException(Resources.Test);

    public Task<bool> Update(IEnumerable<ColumnExternalEntity> entities)
        => throw new ExternalException(Resources.Test);
  }
}
