namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

public sealed class TrailersClientTests
{
    [Fact]
    public async Task ListAsync_ThreadsTagFilters()
    {
        var resp = new
        {
            data = new[] { new { id = "trl-1", name = "Reefer 1" } },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TrailersClient(TestFactory.CreateHttpClient(handler));

        var trailers = await CollectAsync(client.ListAsync(parentTagIds: "p-1", tagIds: "t-1"));

        trailers.Should().HaveCount(1);
        trailers[0].Id.Should().Be("trl-1");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/trailers");
        url.Should().Contain("parentTagIds=p-1");
        url.Should().Contain("tagIds=t-1");
    }

    [Fact]
    public async Task GetAsync_CallsCorrectPath()
    {
        var resp = new { data = new { id = "trl-1", name = "Reefer 1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TrailersClient(TestFactory.CreateHttpClient(handler));

        var trailer = await client.GetAsync("trl-1");

        trailer.Id.Should().Be("trl-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/trailers/trl-1");
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
