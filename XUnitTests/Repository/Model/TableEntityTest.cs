using Compendium.Models;
using Compendium.Repository.Model;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using XUnitTests.Extension;

namespace XUnitTests.Repository.Model
{
  [ExcludeFromCodeCoverage]
  public class TableEntityTest
  {
    private static TableEntity GetTableEntity => new TableEntity()
    {
      Description = "Test",
      Id = 1,
      SyncId = "Test",
      TableName = "Test",
      Type = 'T'
    };

    [Fact]
    public void Consts()
    {
      Assert.Equal("cid=@Id", TableEntity.keyQuery);
      Assert.Equal("cid as id, csyncid as syncid, cname as tablename, ctype as type, cdescription as description", TableEntity.fieldsQuery);
      Assert.Equal("csyncid, cname, ctype, cdescription", TableEntity.insertQuery);
      Assert.Equal("@SyncId, @TableName, @Type, @Description", TableEntity.valuesQuery);
    }

    [Fact]
    public void ConstructorEmpty()
    {
      var model = new TableEntity();

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Null(model.Description);
      Assert.Equal(decimal.Zero, model.Id);
      Assert.Null(model.SyncId);
      Assert.Equal(string.Empty, model.TableName);
      Assert.Equal(char.MinValue, model.Type);
    }

    [Fact]
    public void ConstructorSyscatTable()
    {
      var model = new TableEntity(new SyscatTable()
      {
        ColCount = 5,
        TabName = "Test",
        Type = 'T'
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Null(model.Description);
      Assert.Equal(decimal.Zero, model.Id);
      Assert.Equal("Test", model.SyncId);
      Assert.Equal("TEST", model.TableName);
      Assert.Equal('T', model.Type);
    }

    [Fact]
    public void ConstructorTableViewModel()
    {
      var model = new TableEntity(new TableViewModel()
      {
        Description = "Test",
        Id = 1,
        Name = "Test",
        Type = "T"
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal("Test", model.Description);
      Assert.Equal(1, model.Id);
      Assert.Null(model.SyncId);
      Assert.Equal("TEST", model.TableName);
      Assert.Equal('T', model.Type);
    }

    [Fact]
    public void ConstructorTableDescViewModel()
    {
      var model = new TableEntity(new TableDescViewModel()
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
      Assert.Equal(char.MinValue, model.Type);
    }

    [Fact]
    public void ToViewModel()
    {
      var model = GetTableEntity;

      var result = model.Validate();
      Assert.False(result.Any());

      var view = model.GetViewModel;
      Assert.Equal(model.Description, view.Description);
      Assert.Equal(model.Id, view.Id);
      Assert.Equal(model.TableName, view.Name);
      Assert.Equal(model.Type, view.Type.First());
    }

    [Fact]
    public void FillQuery()
    {
      var paramTest = "@Id|@Description";
      var model = GetTableEntity.FillQuery(paramTest);

      Assert.Equal("1|'Test'", model);
    }
  }

  public class TableExternalEntityTest
  {
    [Fact]
    public void Consts()
    {
      Assert.Equal("public.ttable", TableExternalEntity.tableQuery);
      Assert.Equal("Order By cname", TableExternalEntity.orderQuery);
      Assert.Equal("cname=@TableName, ctype=@Type", TableExternalEntity.updateQuery);
    }

    [Fact]
    public void ConstructorEmpty()
    {
      var model = new TableExternalEntity();

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Null(model.SyncId);
      Assert.Equal(string.Empty, model.TableName);
      Assert.Equal(char.MinValue, model.Type);
    }

    [Fact]
    public void ConstructorSyscatTable()
    {
      var model = new TableExternalEntity(new SyscatTable()
      {
        ColCount = 5,
        TabName = "Test",
        Type = 'T'
      });

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal("Test", model.SyncId);
      Assert.Equal("TEST", model.TableName);
      Assert.Equal('T', model.Type);
    }

    [Fact]
    public void FillQuery()
    {
      var paramTest = "@Type";
      var model = new TableExternalEntity()
      {
        Type = 'T'
      }.FillQuery(paramTest);

      Assert.Equal("'T'", model);
    }
  }
}
