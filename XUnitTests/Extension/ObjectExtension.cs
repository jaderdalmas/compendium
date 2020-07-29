using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;

namespace XUnitTests.Extension
{
  [ExcludeFromCodeCoverage]
  public static class ObjectExtension
  {
    public static StringContent GetContent(this object item, string contentType = "application/json")
    {
      var content = item is null ? "" : JsonConvert.SerializeObject(item);
      return new StringContent(content, Encoding.UTF8, contentType);
    }
  }
}