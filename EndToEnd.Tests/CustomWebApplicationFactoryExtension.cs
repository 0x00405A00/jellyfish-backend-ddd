using Microsoft.AspNetCore.Mvc.Testing;

namespace EndToEnd.Tests
{
    public static class CustomWebApplicationFactoryExtension
    {
        public static WebApplicationFactoryClientOptions Defaults { get; } = new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false,
        };

        public static HttpClient CreateHttpClient<TStartup>(this CustomWebApplicationFactory<TStartup> factory,string relativePathBaseAddr, WebApplicationFactoryClientOptions options = null)
            where TStartup : class
        {
            var _client = factory.CreateClient(options ?? Defaults);
            var baseAddr = _client.BaseAddress;
            _client.BaseAddress = new Uri(baseAddr, relativePathBaseAddr);
            return _client;
        }

    }
}
