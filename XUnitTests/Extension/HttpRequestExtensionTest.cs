using Compendium.Extension;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace XUnitTests.Extension
{
  [ExcludeFromCodeCoverage]
  public class HttpRequestExtensionTest
  {
    [Theory]
    [InlineData("http", "localhost:1234", "")]
    [InlineData("https", "www.google.com", "/Search")]
    public void GetUriByReferer(string scheme, string host, string path)
    {
      var url = $"{scheme}://{host}{path}";

      var moqRequest = new Mock<HttpRequest>();
      moqRequest.Setup(c => c.Headers["Referer"]).Returns(url);
      moqRequest.Setup(c => c.Scheme).Returns(scheme);
      moqRequest.Setup(c => c.Host).Returns(new HostString(host));
      moqRequest.Setup(c => c.Path).Returns(new PathString(path));

      var request = moqRequest.Object;
      Assert.Equal(new Uri(url), request.GetUri());
    }

    [Theory]
    [InlineData("http", "localhost:1234", "")]
    [InlineData("https", "www.google.com", "/Search")]
    public void GetUriByRequest(string scheme, string host, string path)
    {
      var url = $"{scheme}://{host}{path}";

      var moqRequest = new Mock<HttpRequest>();
      moqRequest.Setup(c => c.Headers["Referer"]).Returns("teste");
      moqRequest.Setup(c => c.Scheme).Returns(scheme);
      moqRequest.Setup(c => c.Host).Returns(new HostString(host));
      moqRequest.Setup(c => c.Path).Returns(new PathString(path));

      var request = moqRequest.Object;
      Assert.Equal(new Uri(url), request.GetUri());
    }

    [Fact]
    public void GetUriEmpty()
    {
      var moqRequest = new Mock<HttpRequest>();
      moqRequest.Setup(c => c.Headers["Referer"]).Returns("test");
      moqRequest.Setup(c => c.Scheme).Returns(string.Empty);
      moqRequest.Setup(c => c.Host).Returns(new HostString(string.Empty));
      moqRequest.Setup(c => c.Path).Returns(new PathString(string.Empty));

      var request = moqRequest.Object;
      Assert.Equal(new Uri(Consts.EmptyUri), request.GetUri());
    }

    [Fact]
    public void GetUriNull()
    {
      HttpRequest request = null;
      Assert.Equal(new Uri(Consts.EmptyUri), request.GetUri());
    }
  }
}
