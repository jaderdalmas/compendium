using Compendium.Repository.Model;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using XUnitTests.Extension;

namespace XUnitTests.Repository.Model
{
  [ExcludeFromCodeCoverage]
  public class SyncEntityTest
  {
    [Fact]
    public void Consts()
    {
      Assert.Equal("csyncid=@SyncId", SyncEntity.syncQuery);
    }

    [Fact]
    public void Valid()
    {
      var model = new SyncEntity();

      var result = model.Validate();
      Assert.False(result.Any());
    }

    [Fact]
    public void FillQuery()
    {
      var paramTest = "@SyncId|@TableName";
      var model = new SyncEntity()
      {
        SyncId = "Test",
        TableName = "Test"
      }.FillQuery(paramTest);

      Assert.Equal("'Test'|'TEST'", model);
    }
  }
}
