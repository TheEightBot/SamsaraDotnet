namespace Samsara.Sdk.Tests;

using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Samsara.Sdk.Authentication;
using Samsara.Sdk.Configuration;
using Samsara.Sdk.Tests.Helpers;

public sealed class SamsaraAuthenticationHandlerTests
{
    [Fact]
    public async Task SendAsync_AddsStaticBearerToken()
    {
        var options = Options.Create(new SamsaraClientOptions { ApiToken = "samsara_api_mytoken" });
        var monitor = new TestOptionsMonitor(options.Value);

        var innerHandler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK));
        var authHandler = new SamsaraAuthenticationHandler(monitor)
        {
            InnerHandler = innerHandler
        };

        using var invoker = new HttpMessageInvoker(authHandler);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samsara.com/fleet/tags");

        await invoker.SendAsync(request, CancellationToken.None);

        request.Headers.Authorization.Should().NotBeNull();
        request.Headers.Authorization!.Scheme.Should().Be("Bearer");
        request.Headers.Authorization.Parameter.Should().Be("samsara_api_mytoken");
    }

    [Fact]
    public async Task SendAsync_UsesDynamicTokenProvider_WhenSet()
    {
        var options = new SamsaraClientOptions
        {
            TokenProvider = _ => ValueTask.FromResult("dynamic-token-123")
        };
        var monitor = new TestOptionsMonitor(options);

        var innerHandler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK));
        var authHandler = new SamsaraAuthenticationHandler(monitor)
        {
            InnerHandler = innerHandler
        };

        using var invoker = new HttpMessageInvoker(authHandler);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samsara.com/fleet/tags");

        await invoker.SendAsync(request, CancellationToken.None);

        request.Headers.Authorization!.Parameter.Should().Be("dynamic-token-123");
    }

    [Fact]
    public async Task SendAsync_ThrowsInvalidOperation_WhenNoTokenConfigured()
    {
        var options = new SamsaraClientOptions(); // no token, no provider
        var monitor = new TestOptionsMonitor(options);

        var innerHandler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK));
        var authHandler = new SamsaraAuthenticationHandler(monitor)
        {
            InnerHandler = innerHandler
        };

        using var invoker = new HttpMessageInvoker(authHandler);
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samsara.com/fleet/tags");

        var act = () => invoker.SendAsync(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*API token*token provider*");
    }

    /// <summary>
    /// Minimal IOptionsMonitor implementation for testing.
    /// </summary>
    private sealed class TestOptionsMonitor : IOptionsMonitor<SamsaraClientOptions>
    {
        public TestOptionsMonitor(SamsaraClientOptions value) => CurrentValue = value;
        public SamsaraClientOptions CurrentValue { get; }
        public SamsaraClientOptions Get(string? name) => CurrentValue;
        public IDisposable? OnChange(Action<SamsaraClientOptions, string?> listener) => null;
    }
}
