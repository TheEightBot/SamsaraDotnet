namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Tests.Helpers;

public sealed class HubsClientTests
{
    [Fact]
    public async Task ListHubsAsync_BindsHubAndThreadsFilters()
    {
        // Hub requires id/name/timeZone/createdAt/updatedAt — payload must supply them.
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "hub-1",
                    name = "West DC",
                    timeZone = "America/Los_Angeles",
                    createdAt = "2024-01-01T00:00:00Z",
                    updatedAt = "2024-01-02T00:00:00Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new HubsClient(TestFactory.CreateHttpClient(handler));

        var hubs = await CollectAsync(client.ListHubsAsync(hubIds: "hub-1"));

        hubs.Should().HaveCount(1);
        hubs[0].Id.Should().Be("hub-1");
        hubs[0].TimeZone.Should().Be("America/Los_Angeles");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("hubs");
        url.Should().Contain("hubIds=hub-1");
    }

    [Fact]
    public async Task ListPlanOrdersAsync_RequiresPlanIdInQuery()
    {
        var resp = new
        {
            data = Array.Empty<object>(),
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new HubsClient(TestFactory.CreateHttpClient(handler));

        _ = await CollectAsync(client.ListPlanOrdersAsync("plan-9", orderIds: "ord-1"));

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("hub/plan/orders");
        url.Should().Contain("planId=plan-9");
        url.Should().Contain("orderIds=ord-1");
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
