using Compendium.Extension;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnitTests.Extension
{
  [ExcludeFromCodeCoverage]
  public class LogLevelExtensionTest
  {
    [Fact]
    public void Valid()
    {
      Assert.Equal(LogLevelExtension.ErrorLevel, new List<LogLevel>() { LogLevel.Error, LogLevel.Critical });
      Assert.Equal(LogLevelExtension.AlertLevel, new List<LogLevel>() { LogLevel.Warning });
      Assert.Equal(LogLevelExtension.TestLevel, new List<LogLevel>() { LogLevel.Trace, LogLevel.Debug, LogLevel.Information });
      Assert.Equal(LogLevelExtension.ReturnLevel, new List<LogLevel>() { LogLevel.Warning, LogLevel.Error, LogLevel.Critical });
      Assert.Equal(LogLevelExtension.Levels, new List<LogLevel>() { LogLevel.Trace, LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical });
    }
  }
}
