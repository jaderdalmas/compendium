using Compendium.Notify;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Compendium.Filter
{
  public class CustomExceptionsFilter : IExceptionFilter
  {
    private readonly INotification _notification;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="notification">Notification</param>
    public CustomExceptionsFilter(INotification notification)
    {
      _notification = notification;
    }

    /// <summary>
    /// On Exception Pipeline
    /// </summary>
    /// <param name="context">Exception Context</param>
    public void OnException(ExceptionContext context)
    {
      if (context is null) return;

      _notification.AddNotification(context.Exception.StackTrace, context.Exception.Message, LogLevel.Error);
    }
  }
}
