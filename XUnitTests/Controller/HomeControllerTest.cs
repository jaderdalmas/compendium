using Compendium;
using Compendium.Extension;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Controller
{
  [ExcludeFromCodeCoverage]
  public class HomeControllerTest : BaseControllerTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly string _controller = "Home";
    public HomeControllerTest(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Empty) { }

    [Fact]
    public async Task Default()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, "/");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Index()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Sync()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Sync)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.Found, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Privacy()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Privacy)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Error()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Error)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }
  }

  [ExcludeFromCodeCoverage]
  public class HomeControllerTestNull : BaseControllerTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly string _controller = "Home";
    public HomeControllerTestNull(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Null) { }

    [Fact]
    public async Task Default()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, "/");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Index()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Sync()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Sync)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.Found, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Privacy()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Privacy)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Error()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Error)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }
  }

  [ExcludeFromCodeCoverage]
  public class HomeControllerTestException : BaseControllerTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly string _controller = "Home";
    public HomeControllerTestException(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Exception) { }

    [Fact]
    public async Task Default()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, "/");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Index()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Sync()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Sync)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.Found, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Privacy()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Privacy)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }

    [Fact]
    public async Task Error()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Error)}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }
  }
}
