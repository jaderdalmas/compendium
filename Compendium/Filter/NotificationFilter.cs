using Compendium.Controllers;
using Compendium.Extension;
using Compendium.Notify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Compendium.Filter
{
  public class NotificationFilter : IAsyncResultFilter
  {
    private readonly INotification _notification;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notification">Notification</param>
    public NotificationFilter(INotification notification)
    {
      _notification = notification;
    }

    /// <summary>
    /// On Notification Result
    /// </summary>
    /// <param name="context">Exception Context</param>
    /// <param name="next">Next Execution</param>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
      if (_notification.HasNotification(LogLevelExtension.ReturnLevel) && context != null
        && (string)context.HttpContext.Request.RouteValues["action"] != nameof(HomeController.Error))
        context.Result = new RedirectToActionResult(nameof(HomeController.Error), "Home", null);

      if (next != null) await next().ConfigureAwait(false);
    }
  }
}
