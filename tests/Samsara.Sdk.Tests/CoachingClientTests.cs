namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

public sealed class CoachingClientTests
{
    [Fact]
    public async Task ListAssignmentsAsync_ThreadsDriverAndCoachFilters()
    {
        var resp = new
        {
            data = Array.Empty<object>(),
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new CoachingClient(TestFactory.CreateHttpClient(handler));

        _ = await CollectAsync(client.ListAssignmentsAsync(
            driverIds: new[] { "drv-1" },
            coachIds: new[] { "coach-1" },
            includeExternalIds: true));

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("coaching/driver-coach-assignments");
        url.Should().Contain("driverIds=drv-1");
        url.Should().Contain("coachIds=coach-1");
        url.Should().Contain("includeExternalIds=true");
    }

    [Fact]
    public async Task GetSessionsStreamAsync_ThreadsTimeRangeAndStatuses()
    {
        var resp = new
        {
            data = Array.Empty<object>(),
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new CoachingClient(TestFactory.CreateHttpClient(handler));

        _ = await CollectAsync(client.GetSessionsStreamAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
            sessionStatuses: new[] { "needsCoaching" }));

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("coaching/sessions/stream");
        url.Should().Contain("startTime=");
        url.Should().Contain("endTime=");
        url.Should().Contain("sessionStatuses=needsCoaching");
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
