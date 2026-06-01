namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Tests.Helpers;

public sealed class RoutesClientTests
{
    [Fact]
    public async Task ListAsync_ThreadsTimeRangeAndTagFilters()
    {
        var resp = new
        {
            data = new[] { new { id = "route-1", name = "Morning Run" } },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new RoutesClient(TestFactory.CreateHttpClient(handler));

        var routes = await CollectAsync(client.ListAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
            tagIds: new[] { "tag-1", "tag-2" }));

        routes.Should().HaveCount(1);
        routes[0].Id.Should().Be("route-1");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/routes");
        url.Should().Contain("startTime=");
        url.Should().Contain("endTime=");
        url.Should().Contain("tagIds=tag-1%2Ctag-2");
    }

    [Fact]
    public async Task GetAsync_CallsCorrectPathWithIncludeParam()
    {
        var resp = new { data = new { id = "route-1", name = "Morning Run" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new RoutesClient(TestFactory.CreateHttpClient(handler));

        var route = await client.GetAsync("route-1", include: new[] { "stops" });

        route.Id.Should().Be("route-1");
        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/routes/route-1");
        url.Should().Contain("include=stops");
    }

    [Fact]
    public async Task DeleteAsync_DeletesCorrectPath()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new RoutesClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteAsync("route-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/routes/route-1");
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
