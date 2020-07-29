using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Compendium.Filter
{
  /// <summary>
  /// Validade Model State Attribute
  /// </summary>
  public class ValidateModelStateAttribute : ActionFilterAttribute
  {
    /// <summary>
    /// On Action Executing
    /// </summary>
    /// <param name="context">Action Executing Context</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      if (context != null && !context.ModelState.IsValid)
        context.Result = new ObjectResult(CustomReturn.ModelStateReturn(context.HttpContext.Request, context.ModelState));
    }
  }
}