using Microsoft.Extensions.Logging;

namespace Compendium.Notify
{
  public class Notification
  {
    public Notification(string name, string message, LogLevel level = LogLevel.Information)
    {
      Name = name;
      Message = message;
      Level = level;
    }

    public string Name { get; private set; }
    public string Message { get; private set; }
    public LogLevel Level { get; private set; }

    public string GetText => string.IsNullOrWhiteSpace(Name) ? Message : $"{Name} - {Message}";
  }
}
