using Compendium;
using Compendium.Notify;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using XUnitTests.Repository.Moq;
using XUnitTests.Service.Moq;

namespace XUnitTests.Controller
{
  [ExcludeFromCodeCoverage]
  public class BaseControllerTest : IClassFixture<TestWebApplicationFactory<Startup>>
  {
    private readonly WebApplicationFactory<Startup> _factory;
    private readonly IServiceScope _serviceScope;
    protected HttpClient GetClient { get; private set; }

    private IServiceProvider _serviceProvider => _serviceScope.ServiceProvider;

    public BaseControllerTest(TestWebApplicationFactory<Startup> factory, ETestType type = ETestType.Empty)
    {
      if (factory is null) return;
      _factory = factory.WithWebHostBuilder(builder =>
      {
        builder.ConfigureTestServices(services =>
        {
          services.AddMoqRepository(type);
          services.AddMoqService(type);
        });
      });

      GetClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
      { AllowAutoRedirect = false, });

      _serviceScope = _factory.Services.CreateScope();
    }

    public INotification GetNotification => _serviceProvider.GetService<INotification>();

    private readonly Regex _antiforgeryFormFieldRegex = new Regex(@"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
    protected async Task<string> EnsureAntiforgeryToken(string link)
    {
      HttpResponseMessage response;
      using (var request = new HttpRequestMessage(HttpMethod.Get, link))
      {
        response = await GetClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
      }

      var responseHtml = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      var match = _antiforgeryFormFieldRegex.Match(responseHtml);
      var antiforgeryToken = match.Success ? match.Groups[1].Captures[0].Value : null;
      Assert.NotNull(antiforgeryToken);

      return antiforgeryToken;
    }
  }
}