namespace Samsara.Sdk.Tests.Helpers;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Samsara.Sdk.Http;

internal static class TestFactory
{
    public static SamsaraHttpClient CreateHttpClient(MockHttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.samsara.com/")
        };

        return new SamsaraHttpClient(httpClient, NullLogger<SamsaraHttpClient>.Instance);
    }
}
