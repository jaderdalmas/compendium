using Compendium.Extension;
using Compendium.Notify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Data.Common;
using System.Net;

namespace Compendium.Filter
{
  public static class CustomReturn
  {
    private const string Title = "One or more validation errors have occurred";

    /// <summary>
    /// Exception Return
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="exception">Exception</param>
    /// <returns>Problem Details</returns>
    public static ProblemDetails ExceptionReturn(HttpRequest request, Exception exception)
    {
      if (exception is null) throw new ArgumentNullException(nameof(exception));

      var result = new ProblemDetails()
      { //Please refer to the errors property for additional details
        Status = (int)HttpStatusCode.InternalServerError,
        Instance = request.GetUri().AbsolutePath,
        Title = Title,
        Detail = $"Message: {exception.Message}\r\n StackTrace: {exception.StackTrace}"
      };

      if (exception is DbException) //DB2Exception | NpgsqlException
        result.Status = (int)HttpStatusCode.PreconditionFailed;

      return result;
    }

    /// <summary>
    /// ModelState Return
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="modelState">ModelState Dictionary</param>
    /// <returns>Problem Details</returns>
    public static ValidationProblemDetails ModelStateReturn(HttpRequest request, ModelStateDictionary modelState)
    {
      var result = new ValidationProblemDetails(modelState)
      { //Please refer to the errors property for additional details
        Status = (int)HttpStatusCode.BadRequest,
        Instance = request.GetUri().AbsolutePath,
        Title = Title,
        Detail = "Please note the properties of errors for more details"
      };

      return result;
    }

    /// <summary>
    /// Notification Return
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="notification">Notification</param>
    /// <returns>Problem Details</returns>
    public static ProblemDetails NotificationReturn(HttpRequest request, INotification notification)
    {
      if (notification is null) throw new ArgumentNullException(nameof(notification));

      var result = new ProblemDetails()
      { //Please refer to the errors property for additional details
        Status = (int)HttpStatusCode.NoContent,
        Instance = request.GetUri().AbsolutePath,
        Title = Title,
        Detail = notification.GetReturn(LogLevelExtension.ReturnLevel)
      };

      if (notification.HasNotification(LogLevelExtension.ErrorLevel))
        result.Status = (int)HttpStatusCode.BadRequest;
      else if (notification.HasNotification(LogLevelExtension.AlertLevel))
        result.Status = (int)HttpStatusCode.PreconditionFailed;

      return result;
    }
  }
}
