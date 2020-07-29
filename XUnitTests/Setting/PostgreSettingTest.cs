using Compendium.Setting;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnitTests.Setting
{
  [ExcludeFromCodeCoverage]
  public class PostgreSettingTest
  {
    [Fact]
    public void Valid()
    {
      var model = new PostgreSetting()
      {
        UserID = "Test",
        Password = "Test",
        Host = "Test",
        Port = 1,
        Database = "Test"
      };

      Assert.NotNull(model.GetBuilder);
      Assert.NotNull(model.GetBuilder.ConnectionString);
    }

    [Fact]
    public void InvalidObject()
    {
      var model = new PostgreSetting();

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidUserId()
    {
      var model = new PostgreSetting()
      {
        UserID = "",
        Password = "Test",
        Host = "Test",
        Port = 1,
        Database = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidPassword()
    {
      var model = new PostgreSetting()
      {
        UserID = "Test",
        Password = "",
        Host = "Test",
        Port = 1,
        Database = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidHost()
    {
      var model = new PostgreSetting()
      {
        UserID = "Test",
        Password = "Test",
        Host = "",
        Port = 1,
        Database = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidPort()
    {
      var model = new PostgreSetting()
      {
        UserID = "Test",
        Password = "Test",
        Host = "Test",
        Port = 0,
        Database = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidDatabase()
    {
      var model = new PostgreSetting()
      {
        UserID = "Test",
        Password = "Test",
        Host = "Test",
        Port = 1,
        Database = ""
      };

      Assert.Null(model.GetBuilder);
    }
  }
}
