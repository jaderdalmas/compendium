using Compendium;
using Compendium.Extension;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Controller
{
  [ExcludeFromCodeCoverage]
  public class ColumnControllerTest : BaseControllerTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly string _controller = "Column";
    private readonly string _tableName = "AAA";
    public ColumnControllerTest(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Empty) { }

    [Fact]
    public async Task Index()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}?tableName={_tableName}");

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
    public async Task IndexFilter()
    {
      //Arrange
      var filterName = "AAA";
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}?tableName={_tableName}&name={filterName}");

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
    public async Task Details()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Details)}/{id}");

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
    public async Task Create()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Create)}?tableName={_tableName}");

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
    public async Task CreatePost()
    {
      //Arrange
      var link = $"/{_controller}/{nameof(Create)}";
      var request = new HttpRequestMessage(HttpMethod.Post, link);
      request.Content = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("Id", "0"),
        new KeyValuePair<string, string>("TableName", "Test"),
        new KeyValuePair<string, string>("Name", "Test"),
        new KeyValuePair<string, string>("Number", "0"),
        new KeyValuePair<string, string>("Type", "Test"),
        new KeyValuePair<string, string>("Length", "0"),
        new KeyValuePair<string, string>("Default", "Test"),
        new KeyValuePair<string, string>("IsNull", "Y"),
        new KeyValuePair<string, string>("IsIdentity", "N"),
        new KeyValuePair<string, string>("Description", "Test"),
        new KeyValuePair<string, string>("__RequestVerificationToken", EnsureAntiforgeryToken($"{link}?tableName={_tableName}").Result)
      });

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
    public async Task Edit()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Edit)}/{id}");

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
    public async Task EditPost()
    {
      //Arrange
      var id = 1;
      var link = $"/{_controller}/{nameof(Edit)}/{id}";
      var request = new HttpRequestMessage(HttpMethod.Post, link);
      request.Content = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("Id", "0"),
        new KeyValuePair<string, string>("Description", "Test"),
        new KeyValuePair<string, string>("__RequestVerificationToken", EnsureAntiforgeryToken(link).Result)
      });

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
    public async Task Delete()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Delete)}/{id}");

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
    public async Task DeletePost()
    {
      //Arrange
      var id = 1;
      var link = $"/{_controller}/{nameof(Delete)}/{id}";
      var request = new HttpRequestMessage(HttpMethod.Post, $"{link}?tableName={_tableName}");
      request.Content = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("__RequestVerificationToken", EnsureAntiforgeryToken(link).Result)
      });

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.Found, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }
  }

  [ExcludeFromCodeCoverage]
  public class ColumnControllerTestNull : BaseControllerTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly string _controller = "Column";
    private readonly string _tableName = "AAA";
    public ColumnControllerTestNull(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Null) { }

    [Fact]
    public async Task Index()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}?tableName={_tableName}");

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
    public async Task IndexFilter()
    {
      //Arrange
      var filterName = "AAA";
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}?tableName={_tableName}&name={filterName}");

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
    public async Task Details()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Details)}/{id}");

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
    public async Task Create()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Create)}?tableName={_tableName}");

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
    public async Task CreatePost()
    {
      //Arrange
      var link = $"/{_controller}/{nameof(Create)}";
      var request = new HttpRequestMessage(HttpMethod.Post, link);
      request.Content = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("Id", "0"),
        new KeyValuePair<string, string>("TableName", "Test"),
        new KeyValuePair<string, string>("Name", "Test"),
        new KeyValuePair<string, string>("Number", "0"),
        new KeyValuePair<string, string>("Type", "Test"),
        new KeyValuePair<string, string>("Length", "0"),
        new KeyValuePair<string, string>("Default", "Test"),
        new KeyValuePair<string, string>("IsNull", "Y"),
        new KeyValuePair<string, string>("IsIdentity", "N"),
        new KeyValuePair<string, string>("Description", "Test"),
        new KeyValuePair<string, string>("__RequestVerificationToken", EnsureAntiforgeryToken($"{link}?tableName={_tableName}").Result)
      });

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
    public async Task Edit()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Edit)}/{id}");

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
    public async Task Delete()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Delete)}/{id}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.Found, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }
  }

  [ExcludeFromCodeCoverage]
  public class ColumnControllerTestException : BaseControllerTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly string _controller = "Column";
    private readonly string _tableName = "AAA";
    public ColumnControllerTestException(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Exception) { }

    [Fact]
    public async Task Index()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}?tableName={_tableName}");

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
    public async Task IndexFilter()
    {
      //Arrange
      var filterName = "AAA";
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Index)}?tableName={_tableName}&name={filterName}");

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
    public async Task Details()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Details)}/{id}");

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
    public async Task Create()
    {
      //Arrange
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Create)}?tableName={_tableName}");

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
    public async Task CreatePost()
    {
      //Arrange
      var link = $"/{_controller}/{nameof(Create)}";
      var request = new HttpRequestMessage(HttpMethod.Post, link);
      request.Content = new FormUrlEncodedContent(new[]
      {
        new KeyValuePair<string, string>("Id", "0"),
        new KeyValuePair<string, string>("TableName", "Test"),
        new KeyValuePair<string, string>("Name", "Test"),
        new KeyValuePair<string, string>("Number", "0"),
        new KeyValuePair<string, string>("Type", "Test"),
        new KeyValuePair<string, string>("Length", "0"),
        new KeyValuePair<string, string>("Default", "Test"),
        new KeyValuePair<string, string>("IsNull", "Y"),
        new KeyValuePair<string, string>("IsIdentity", "N"),
        new KeyValuePair<string, string>("Description", "Test"),
        new KeyValuePair<string, string>("__RequestVerificationToken", EnsureAntiforgeryToken($"{link}?tableName={_tableName}").Result)
      });

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
    public async Task Edit()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Edit)}/{id}");

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
    public async Task Delete()
    {
      //Arrange
      var id = 1;
      var request = new HttpRequestMessage(HttpMethod.Get, $"/{_controller}/{nameof(Delete)}/{id}");

      //Act
      var response = await GetClient.SendAsync(request).ConfigureAwait(false);

      //Assert
      Assert.Equal(HttpStatusCode.Found, response.StatusCode);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      //Arrange
      response.Dispose();
      request.Dispose();
    }
  }
}
