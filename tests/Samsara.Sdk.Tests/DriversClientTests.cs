namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

public sealed class DriversClientTests
{
    [Fact]
    public async Task ListAsync_ThreadsActivationStatusAndTagFilters()
    {
        var resp = new
        {
            data = new[] { new { id = "drv-1", name = "Jane Doe" } },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DriversClient(TestFactory.CreateHttpClient(handler));

        var drivers = await CollectAsync(client.ListAsync(
            driverActivationStatus: "active",
            tagIds: new[] { "tag-1" }));

        drivers.Should().HaveCount(1);
        drivers[0].Id.Should().Be("drv-1");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/drivers");
        url.Should().Contain("driverActivationStatus=active");
        url.Should().Contain("tagIds=tag-1");
    }

    [Fact]
    public async Task ListQrCodesAsync_RequiresDriverIdsInQuery()
    {
        var resp = new
        {
            data = Array.Empty<object>(),
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DriversClient(TestFactory.CreateHttpClient(handler));

        _ = await CollectAsync(client.ListQrCodesAsync(new[] { "drv-1", "drv-2" }));

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("drivers/qr-codes");
        url.Should().Contain("driverIds=drv-1%2Cdrv-2");
    }

    private static async Task<IReadOnlyList<T>> CollectAsync<T>(IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
        }

        return list;
    }
}
