using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Compendium.Filter
{
  /// <summary>
  /// Validate Parameters o Action
  /// </summary>
  public class ValidateActionParametersAttribute : ActionFilterAttribute
  {
    /// <summary>
    /// On Action Executing
    /// </summary>
    /// <param name="context">Action Executing Context</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      if (context != null && context.ActionDescriptor is ControllerActionDescriptor)
        foreach (var parameter in (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetParameters())
          if (context.ActionArguments.ContainsKey(parameter.Name))
            EvaluateValidationAttributes(parameter, context.ActionArguments[parameter.Name], context.ModelState);

      new ValidateModelStateAttribute().OnActionExecuting(context);
      base.OnActionExecuting(context);
    }

    /// <summary>
    /// Evaliate Atribute Validation
    /// </summary>
    /// <param name="parameter">Parameter Info</param>
    /// <param name="argument">Object</param>
    /// <param name="modelState">Model State</param>
    private void EvaluateValidationAttributes(ParameterInfo parameter, object argument, ModelStateDictionary modelState)
    {
      foreach (var attributeData in parameter.CustomAttributes)
      {
        var validationAttribute = CustomAttributeExtensions.GetCustomAttribute(parameter, attributeData.AttributeType) as ValidationAttribute;
        if (validationAttribute != null)
          if (!validationAttribute.IsValid(argument))
            modelState.AddModelError(parameter.Name, validationAttribute.FormatErrorMessage(parameter.Name));
      }
    }
  }
}