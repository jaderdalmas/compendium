using System.Globalization;

namespace Compendium.Extension
{
  public static class StringExtension
  {
    public static string LikeParam(this string value)
    {
      return string.IsNullOrWhiteSpace(value) ? string.Empty : $"%{value.Trim().ToUpper(CultureInfo.InvariantCulture)}%";
    }
  }
}
