namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

public sealed class GatewaysClientTests
{
    [Fact]
    public async Task PairGatewaysAsync_PostsToGatewaysPair()
    {
        // POST /gateways/pair (beta) is the relocated home of gateway pairing — it replaced
        // the removed POST /preview/gateways/pair. Verify the verb + path the SDK calls.
        var resp = new { data = new { paired = true } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new GatewaysClient(TestFactory.CreateHttpClient(handler));

        var result = await client.PairGatewaysAsync(
            new { gateways = new[] { new { serial = "GABC1234", vin = "1HGCM82633A004352" } } });

        result.Should().NotBeNull();

        var lastRequest = handler.LastRequest;
        lastRequest.Method.Method.Should().Be("POST");
        lastRequest.RequestUri!.PathAndQuery.Should().Contain("gateways/pair");
    }
}
