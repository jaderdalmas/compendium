using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Compendium.Notify
{
  public interface INotification
  {
    /// <summary>
    /// Has Notification By Level
    /// </summary>
    /// <param name="levels">Levels</param>
    /// <returns>Notifications</returns>
    bool HasNotification(IEnumerable<LogLevel> levels = null);

    /// <summary>
    /// Get Notification By Level
    /// </summary>
    /// <param name="levels">Levels</param>
    /// <returns>Notifications</returns>
    IEnumerable<Notification> GetNotification(IEnumerable<LogLevel> levels = null);

    /// <summary>
    /// Get Notification By Level
    /// </summary>
    /// <param name="levels">Levels</param>
    /// <returns>Notifications</returns>
    string GetReturn(IEnumerable<LogLevel> levels = null);


    /// <summary>
    /// Set a Notification
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="name">Property</param>
    /// <param name="level">Log Level</param>
    void AddNotification(string message, string name = "", LogLevel level = LogLevel.Information);
  }
}
