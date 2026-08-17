namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

public sealed class GatewaysClientTests
{
    [Fact]
    public async Task PairGatewaysAsync_PostsToGatewaysPair()
    {
        // POST /gateways/pair (beta) is the relocated home of gateway pairing — it replaced
        // the removed POST /preview/gateways/pair. Verify the verb + path the SDK calls.
        var resp = new
        {
            data = new[]
            {
                new
                {
                    gateway = new { id = "1234", model = "VG34", serial = "GABC1234" },
                    device = new { id = "5678", name = "Truck 1", serial = "GXYZ9876", type = "vehicle" },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new GatewaysClient(TestFactory.CreateHttpClient(handler));

        var result = await client.PairGatewaysAsync(
            new PairGatewaysRequest
            {
                Pairs = [new GatewayPairInput { GatewaySerial = "GABC1234", DeviceSerial = "GXYZ9876" }],
            });

        result.Should().HaveCount(1);
        result[0].Gateway!.Serial.Should().Be("GABC1234");
        result[0].Device!.Type.Should().Be("vehicle");

        var lastRequest = handler.LastRequest;
        lastRequest.Method.Method.Should().Be("POST");
        lastRequest.RequestUri!.PathAndQuery.Should().Contain("gateways/pair");
    }
}
