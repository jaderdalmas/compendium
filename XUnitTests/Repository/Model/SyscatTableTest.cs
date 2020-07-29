using Compendium.Repository.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Xunit;
using XUnitTests.Extension;

namespace XUnitTests.Repository.Model
{
  [ExcludeFromCodeCoverage]
  public class SyscatTableTest
  {
    private static SyscatTable GetSyscatTable => new SyscatTable()
    {
      ColCount = 1,
      TabName = "Test",
      Type = 'T'
    };

    private static TableEntity GetTableEntity => new TableEntity()
    {
      Id = 0,
      Description = string.Empty,
      SyncId = "Test",
      TableName = "Test",
      Type = 'T'
    };

    [Fact]
    public void ToInsert()
    {
      var model = GetSyscatTable;

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntityInsert;
      Assert.Null(entity.Description);
      Assert.Equal(decimal.Zero, entity.Id);
      Assert.Equal(model.SyncId, entity.SyncId);
      Assert.Equal(model.TabName.ToUpper(CultureInfo.InvariantCulture), entity.TableName);
      Assert.Equal(model.Type, entity.Type);
    }

    [Fact]
    public void ToUpdate()
    {
      var model = GetSyscatTable;

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntityUpdate;
      Assert.Equal(model.SyncId, entity.SyncId);
      Assert.Equal(model.TabName.ToUpper(CultureInfo.InvariantCulture), entity.TableName);
      Assert.Equal(model.Type, entity.Type);
    }

    [Fact]
    public void IsInsertTrue()
    {
      var model = GetSyscatTable;
      model.TabName = "Other";

      IEnumerable<TableEntity> list = new List<TableEntity>() { GetTableEntity };

      var result = model.IsInsert(list);
      Assert.True(result);
    }

    [Fact]
    public void IsInsertFalse()
    {
      var model = GetSyscatTable;

      IEnumerable<TableEntity> list = new List<TableEntity>() { GetTableEntity };

      var result = model.IsInsert(list);
      Assert.False(result);
    }

    [Fact]
    public void IsUpdateTrue()
    {
      var model = GetSyscatTable;
      model.Type = 'O';

      IEnumerable<TableEntity> list = new List<TableEntity>() { GetTableEntity };

      var result = model.IsUpdate(list);
      Assert.True(result);
    }

    [Fact]
    public void IsUpdateFalseNoExist()
    {
      var model = GetSyscatTable;
      model.TabName = "Other";

      IEnumerable<TableEntity> list = new List<TableEntity>() { GetTableEntity };

      var result = model.IsUpdate(list);
      Assert.False(result);
    }

    [Fact]
    public void IsUpdateFalseEquals()
    {
      var model = GetSyscatTable;

      IEnumerable<TableEntity> list = new List<TableEntity>() { GetTableEntity };

      var result = model.IsUpdate(list);
      Assert.False(result);
    }
  }
}
