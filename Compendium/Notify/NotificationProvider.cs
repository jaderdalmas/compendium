using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Compendium.Notify
{
  public class NotificationProvider : INotification
  {
    private readonly ILogger<NotificationProvider> _log;
    public NotificationProvider(ILogger<NotificationProvider> log)
    {
      _log = log;
    }

    private List<Notification> Notification { get; set; } = new List<Notification>();

    public bool HasNotification(IEnumerable<LogLevel> levels = null) => Notification.Any(x => levels is null ? true : levels.Contains(x.Level));
    public IEnumerable<Notification> GetNotification(IEnumerable<LogLevel> levels = null) => Notification.Where(x => levels is null ? true : levels.Contains(x.Level));
    public string GetReturn(IEnumerable<LogLevel> levels = null) => string.Join(" | ", Notification.Where(x => levels.Contains(x.Level)).Select(x => x.GetText));

    public void AddNotification(string message, string name = "", LogLevel level = LogLevel.Information)
    {
      _log.Log(level, $"{name} {message}");

      if (!Notification.Any(x => x.Message == message))
        Notification.Add(new Notification(name, message, level));
    }
  }
}
