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
  public class SyscatColumnTest
  {
    private static SyscatColumn GetSyscatColumn => new SyscatColumn()
    {
      ColName = "Test",
      ColNo = 1,
      Default = "Test",
      Identity = 'N',
      Length = 15,
      Nulls = 'Y',
      TabName = "Test",
      TypeName = "Test"
    };

    private static ColumnEntity GetColumnEntity => new ColumnEntity()
    {
      Id = 0,
      Description = string.Empty,
      SyncId = "Test.Test",
      Name = "Test",
      Number = 1,
      Default = "Test",
      IsIdentity = 'N',
      Length = 15,
      IsNull = 'Y',
      TableName = "Test",
      Type = "Test"
    };

    [Fact]
    public void ToInsert()
    {
      var model = GetSyscatColumn;

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntityInsert;
      Assert.Equal(model.Default, entity.Default);
      Assert.Null(entity.Description);
      Assert.Equal(model.Identity, entity.IsIdentity);
      Assert.Equal(model.Nulls, entity.IsNull);
      Assert.Equal(model.ColName.ToUpper(CultureInfo.InvariantCulture), entity.Name);
      Assert.Equal(model.ColNo, entity.Number);
      Assert.Equal(0, entity.Id);
      Assert.Equal(model.SyncId, entity.SyncId);
      Assert.Equal(model.TabName.ToUpper(CultureInfo.InvariantCulture), entity.TableName);
      Assert.Equal(model.TypeName, entity.Type);
    }

    [Fact]
    public void ToUpdate()
    {
      var model = GetSyscatColumn;

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntityUpdate;
      Assert.Equal(model.Default, entity.Default);
      Assert.Equal(model.Identity, entity.IsIdentity);
      Assert.Equal(model.Nulls, entity.IsNull);
      Assert.Equal(model.ColName.ToUpper(CultureInfo.InvariantCulture), entity.Name);
      Assert.Equal(model.ColNo, entity.Number);
      Assert.Equal(model.SyncId, entity.SyncId);
      Assert.Equal(model.TabName.ToUpper(CultureInfo.InvariantCulture), entity.TableName);
      Assert.Equal(model.TypeName, entity.Type);
    }

    [Fact]
    public void IsInsertTrue()
    {
      var model = GetSyscatColumn;
      model.ColName = "Other";

      IEnumerable<ColumnEntity> list = new List<ColumnEntity>() { GetColumnEntity };

      var result = model.IsInsert(list);
      Assert.True(result);
    }

    [Fact]
    public void IsInsertFalse()
    {
      var model = GetSyscatColumn;

      IEnumerable<ColumnEntity> list = new List<ColumnEntity>() { GetColumnEntity };

      var result = model.IsInsert(list);
      Assert.False(result);
    }

    [Fact]
    public void IsUpdateTrue()
    {
      var model = GetSyscatColumn;
      model.TypeName = "Other";

      IEnumerable<ColumnEntity> list = new List<ColumnEntity>() { GetColumnEntity };

      var result = model.IsUpdate(list);
      Assert.True(result);
    }

    [Fact]
    public void IsUpdateFalseNoExist()
    {
      var model = GetSyscatColumn;
      model.ColName = "Other";

      IEnumerable<ColumnEntity> list = new List<ColumnEntity>() { GetColumnEntity };

      var result = model.IsUpdate(list);
      Assert.False(result);
    }

    [Fact]
    public void IsUpdateFalseEquals()
    {
      var model = GetSyscatColumn;

      IEnumerable<ColumnEntity> list = new List<ColumnEntity>() { GetColumnEntity };

      var result = model.IsUpdate(list);
      Assert.False(result);
    }
  }
}