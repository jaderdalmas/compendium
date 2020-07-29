using Compendium.Extension;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnitTests.Extension
{
  [ExcludeFromCodeCoverage]
  public class ConstsTest
  {
    [Fact]
    public void Valid()
    {
      Assert.Null(Consts.NullString);
    }
  }
}
