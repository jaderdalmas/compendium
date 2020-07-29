using Compendium.Extension;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnitTests.Extension
{
  [ExcludeFromCodeCoverage]
  public class StringExtensionTest
  {
    [Theory]
    [InlineData("Name", "%NAME%")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void LikeParamValid(string param, string result)
    {
      Assert.Equal(result, param.LikeParam());
    }
  }
}
