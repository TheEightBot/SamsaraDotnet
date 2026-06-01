namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Safety;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Safety Events domain (Phase 3). <see cref="SafetyEvent"/>
/// binds typed nested records — <see cref="SafetyEventAsset"/>,
/// <see cref="SafetyEventDriver"/>, behavior labels, location — against the v2 wire
/// shape returned by <c>GET /safety-events</c> and <c>.../stream</c>.
/// </summary>
public sealed class SafetyEventContractTests
{
    private static object SafetyEventObj() => new
    {
        id = "evt-1",
        asset = new { id = "veh-1", name = "Truck 1", type = "vehicle", vin = "1FT" },
        driver = new { id = "drv-1", name = "Jane Doe" },
        behaviorLabels = new[] { new { label = "Braking", source = "automated" } },
        contextLabels = Array.Empty<object>(),
        createdAtTime = "2024-01-01T00:00:00Z",
        updatedAtTime = "2024-01-01T00:05:00Z",
        startMs = "2024-01-01T00:00:00Z",
        endMs = "2024-01-01T00:00:05Z",
        eventState = "needsReview",
        inboxEventUrl = "https://cloud.samsara.com/inbox/evt-1",
        incidentReportUrl = "https://cloud.samsara.com/incident/evt-1",
        location = new { latitude = 37.7749, longitude = -122.4194, headingDegrees = 90.0 },
        maxAccelerationGForce = 0.45,
    };

    // ── GET /safety-events ──────────────────────────────────────────────────
    [Fact]
    public async Task ListEventsAsync_BindsTypedNestedAssetDriverAndLocation()
    {
        var resp = new
        {
            data = new[] { SafetyEventObj() },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListEventsAsync(new[] { "evt-1" }, includeAsset: true, includeDriver: true));

        items.Should().HaveCount(1);
        var evt = items[0];
        evt.Id.Should().Be("evt-1");
        evt.EventState.Should().Be("needsReview");
        evt.MaxAccelerationGForce.Should().Be(0.45);
        // Typed nested asset.
        evt.Asset.Id.Should().Be("veh-1");
        evt.Asset.Vin.Should().Be("1FT");
        // Typed nested driver.
        evt.Driver.Id.Should().Be("drv-1");
        evt.Driver.Name.Should().Be("Jane Doe");
        // Typed nested behavior labels + location.
        evt.BehaviorLabels.Should().HaveCount(1);
        evt.BehaviorLabels[0].Label.Should().Be("Braking");
        evt.BehaviorLabels[0].Source.Should().Be("automated");
        evt.Location.Latitude.Should().Be(37.7749);

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("safety-events");
        url.Should().Contain("safetyEventIds=evt-1");
        url.Should().Contain("includeAsset=true");
        url.Should().Contain("includeDriver=true");
    }

    // ── GET /safety-events/stream ───────────────────────────────────────────
    [Fact]
    public async Task GetEventsStreamAsync_BindsEventAndThreadsTimeRange()
    {
        var resp = new
        {
            data = new[] { SafetyEventObj() },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new SafetyClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetEventsStreamAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero)));

        items.Should().HaveCount(1);
        items[0].Driver.Id.Should().Be("drv-1");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("safety-events/stream");
        url.Should().Contain("startTime=");
        url.Should().Contain("endTime=");
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
