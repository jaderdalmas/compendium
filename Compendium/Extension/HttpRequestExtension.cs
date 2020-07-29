using Microsoft.AspNetCore.Http;
using System;

namespace Compendium.Extension
{
  public static class HttpRequestExtension
  {
    private static string GetUrl(this HttpRequest request)
    {
      if (request is null
       || string.IsNullOrWhiteSpace(request.Scheme)
       || !request.Host.HasValue)
        return Consts.EmptyUri;

      return $"{request.Scheme}://{request.Host}{request.Path}";
    }

    public static Uri GetUri(this HttpRequest request)
    {
      if (request is null) return new Uri(Consts.EmptyUri);

      var referer = request.Headers["Referer"].ToString();
      if (!string.IsNullOrWhiteSpace(referer))
        try { return new Uri(referer); }
        catch (UriFormatException) { }

      return new Uri(GetUrl(request));
    }
  }
}
