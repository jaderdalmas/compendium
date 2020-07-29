using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace XUnitTests.Extension
{
  [ExcludeFromCodeCoverage]
  public static class ValidateExtension
  {
    public static IEnumerable<ValidationResult> Validate(this object viewModel)
    {
      var context = new ValidationContext(viewModel, null, null);
      var results = new List<ValidationResult>();
      Validator.TryValidateObject(viewModel, context, results, true);

      return results;
    }
  }
}
