using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Compendium.Extension
{
  public static class LogLevelExtension
  {
    /// <summary>
    /// LogLevel for Levels
    /// </summary>
    public static IEnumerable<LogLevel> Levels => TestLevel.Concat(AlertLevel.Concat(ErrorLevel));

    /// <summary>
    /// LogLevel for Return Level
    /// </summary>
    public static IEnumerable<LogLevel> ReturnLevel => AlertLevel.Concat(ErrorLevel);

    /// <summary>
    /// LogLevel for Test Level
    /// </summary>
    public static IEnumerable<LogLevel> TestLevel => new List<LogLevel>() { LogLevel.Trace, LogLevel.Debug, LogLevel.Information };

    /// <summary>
    /// LogLevel for Alert Level
    /// </summary>
    public static IEnumerable<LogLevel> AlertLevel => new List<LogLevel>() { LogLevel.Warning };

    /// <summary>
    /// LogLevel for Error Level
    /// </summary>
    public static IEnumerable<LogLevel> ErrorLevel => new List<LogLevel>() { LogLevel.Error, LogLevel.Critical };
  }
}
