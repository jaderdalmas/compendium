using Compendium.Setting;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnitTests.Setting
{
  [ExcludeFromCodeCoverage]
  public class DB2SettingTest
  {
    [Fact]
    public void Valid()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.NotNull(model.GetBuilder);
      Assert.NotNull(model.GetBuilder.ConnectionString);
    }

    [Fact]
    public void InvalidObject()
    {
      var model = new DB2Setting();

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidServer()
    {
      var model = new DB2Setting()
      {
        Server = "",
        Database = "Test",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidDatabase()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidUserID()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidPassword()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "Test",
        Password = "",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidCurrentSchema()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidCurrentFunctionPath()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "",
        IsolationLevel = "Test",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidIsolationLevel()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "",
        ClientAcctStr = "Test"
      };

      Assert.Null(model.GetBuilder);
    }

    [Fact]
    public void InvalidClientAcctStr()
    {
      var model = new DB2Setting()
      {
        Server = "Test",
        Database = "Test",
        UserID = "Test",
        Password = "Test",
        CurrentSchema = "Test",
        CurrentFunctionPath = "Test",
        IsolationLevel = "Test",
        ClientAcctStr = ""
      };

      Assert.Null(model.GetBuilder);
    }
  }
}
