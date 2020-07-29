using Compendium.Models;
using Compendium.Repository.Model;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using XUnitTests.Extension;

namespace XUnitTests.Repository.Model
{
  [ExcludeFromCodeCoverage]
  public class ColumnEntityTest
  {
    private static ColumnEntity GetColumnEntity => new ColumnEntity()
    {
      Default = "Test",
      Description = "Test",
      Id = 1,
      IsIdentity = 'N',
      IsNull = 'Y',
      Length = 1,
      Name = "Test",
      Number = 1,
      SyncId = "Test",
      TableName = "Test",
      Type = "Test"
    };

    [Fact]
    public void Consts()
    {
      Assert.Equal("cid=@Id", ColumnEntity.keyQuery);
      Assert.Equal("cid as id, csyncid as syncid, ctable as tablename, cname as name, cnumber as number, ctype as type, clength as length, cdefault as default, cisnull as isnull, cisidentity as isidentity, cdescription as description", ColumnEntity.fieldsQuery);
      Assert.Equal("csyncid, ctable, cname, cnumber, ctype, clength, cdefault, cisnull, cisidentity, cdescription", ColumnEntity.insertQuery);
      Assert.Equal("@SyncId, @TableName, @Name, @Number, @Type, @Length, @Default, @IsNull, @IsIdentity, @Description", ColumnEntity.valuesQuery);
    }

    [Fact]
    public void ConstructorEmpty()
    {
      var model = new ColumnEntity();

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Null(model.Description);
      Assert.Equal(decimal.Zero, model.Id);
      Assert.Null(model.SyncId);
      Assert.Equal(string.Empty, model.TableName);
      Assert.Equal(string.Empty, model.Name);
      Assert.Equal(decimal.Zero, model.Number);
      Assert.Null(model.Type);
      Assert.Equal(decimal.Zero, model.Length);
      Assert.Null(model.Default);
      Assert.Equal(char.MinValue, model.IsNull);
      Assert.Equal(char.MinValue, model.IsIdentity);
    }

    [Fact]
    public void ConstructorSyscatColumn()
    {
      var model = new ColumnEntity(new SyscatColumn()
      {
        ColName = "Test",
        ColNo = 1,
        Default = "Test",
        Identity = 'N',
        Length = 1,
        Nulls = 'Y',
        TabName = "Test",
        TypeName = "Test"
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Null(model.Description);
      Assert.Equal(decimal.Zero, model.Id);
      Assert.Equal("Test.Test", model.SyncId);
      Assert.Equal("TEST", model.TableName);
      Assert.Equal("TEST", model.Name);
      Assert.Equal(1, model.Number);
      Assert.Equal("Test", model.Default);
      Assert.Equal('N', model.IsIdentity);
      Assert.Equal(1, model.Length);
      Assert.Equal('Y', model.IsNull);
      Assert.Equal("Test", model.Type);
    }

    [Fact]
    public void ConstructorColumnViewModel()
    {
      var model = new ColumnEntity(new ColumnViewModel()
      {
        Default = "Test",
        Description = "Test",
        Id = 1,
        IsIdentity = "N",
        IsNull = "Y",
        Length = 1,
        Name = "Test",
        Number = 1,
        TableName = "Test",
        Type = "Test"
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal("Test", model.Description);
      Assert.Equal(1, model.Id);
      Assert.Null(model.SyncId);
      Assert.Equal("TEST", model.TableName);
      Assert.Equal("TEST", model.Name);
      Assert.Equal(1, model.Number);
      Assert.Equal("Test", model.Default);
      Assert.Equal('N', model.IsIdentity);
      Assert.Equal(1, model.Length);
      Assert.Equal('Y', model.IsNull);
      Assert.Equal("Test", model.Type);
    }

    [Fact]
    public void ConstructorColumnDescViewModel()
    {
      var model = new ColumnEntity(new ColumnDescViewModel()
      {
        Description = "Test",
        Id = 1
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal("Test", model.Description);
      Assert.Equal(1, model.Id);
      Assert.Null(model.SyncId);
      Assert.Equal(string.Empty, model.TableName);
      Assert.Equal(string.Empty, model.Name);
      Assert.Equal(decimal.Zero, model.Number);
      Assert.Null(model.Type);
      Assert.Equal(decimal.Zero, model.Length);
      Assert.Null(model.Default);
      Assert.Equal(char.MinValue, model.IsNull);
      Assert.Equal(char.MinValue, model.IsIdentity);
    }

    [Fact]
    public void ToViewModel()
    {
      var model = GetColumnEntity;

      var result = model.Validate();
      Assert.False(result.Any());

      var view = model.GetViewModel;
      Assert.Equal(model.Description, view.Description);
      Assert.Equal(model.Id, view.Id);
      Assert.Equal(model.TableName, view.TableName);
      Assert.Equal(model.Name, view.Name);
      Assert.Equal(model.Number, view.Number);
      Assert.Equal(model.Type, view.Type);
      Assert.Equal(model.Length, view.Length);
      Assert.Equal(model.Default, view.Default);
      Assert.Equal(model.IsNull, view.IsNull.First());
      Assert.Equal(model.IsIdentity, view.IsIdentity.First());
    }

    [Fact]
    public void FillQuery()
    {
      var paramTest = "@Id|@Description";
      var model = GetColumnEntity.FillQuery(paramTest);

      Assert.Equal("1|'Test'", model);
    }
  }

  public class ColumnExternalEntityTest
  {
    [Fact]
    public void Consts()
    {
      Assert.Equal("public.tcolumn", ColumnExternalEntity.tableQuery);
      Assert.Equal("Order By ctable, cnumber", ColumnExternalEntity.orderQuery);
      Assert.Equal("ctable=@TableName, cname=@Name, cnumber=@Number, ctype=@Type, clength=@Length, cdefault=@Default, cisnull=@IsNull, cisidentity=@IsIdentity", ColumnExternalEntity.updateQuery);
    }

    [Fact]
    public void ConstructorEmpty()
    {
      var model = new ColumnExternalEntity();

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Null(model.SyncId);
      Assert.Equal(string.Empty, model.TableName);
      Assert.Equal(string.Empty, model.Name);
      Assert.Equal(decimal.Zero, model.Number);
      Assert.Null(model.Type);
      Assert.Equal(decimal.Zero, model.Length);
      Assert.Null(model.Default);
      Assert.Equal(char.MinValue, model.IsNull);
      Assert.Equal(char.MinValue, model.IsIdentity);
    }

    [Fact]
    public void ConstructorSyscatColumn()
    {
      var model = new ColumnExternalEntity(new SyscatColumn()
      {
        ColName = "Test",
        ColNo = 1,
        Default = "Test",
        Identity = 'N',
        Length = 1,
        Nulls = 'Y',
        TabName = "Test",
        TypeName = "Test"
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal("Test.Test", model.SyncId);
      Assert.Equal("TEST", model.TableName);
      Assert.Equal("TEST", model.Name);
      Assert.Equal(1, model.Number);
      Assert.Equal("Test", model.Type);
      Assert.Equal(1, model.Length);
      Assert.Equal("Test", model.Default);
      Assert.Equal('Y', model.IsNull);
      Assert.Equal('N', model.IsIdentity);
    }

    [Fact]
    public void FillQuery()
    {
      var paramTest = "@Name|@Number|@Type|@Length|@Default|@IsNull|@IsIdentity";
      var model = new ColumnExternalEntity()
      {
        Default = "Test",
        IsIdentity = 'N',
        IsNull = 'Y',
        Length = 1,
        Name = "Test",
        Number = 1,
        SyncId = "Test",
        TableName = "Test",
        Type = "Test"
      }.FillQuery(paramTest);

      Assert.Equal("'TEST'|1|'Test'|1|'Test'|'Y'|'N'", model);
    }
  }
}