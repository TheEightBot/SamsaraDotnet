namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the trailer-stats snapshot vs. time-series split (Phase 3).
/// <c>GET /fleet/trailers/stats</c> returns singular <c>{ time, value }</c> objects,
/// while <c>.../stats/feed</c> and <c>.../stats/history</c> return arrays. These tests
/// lock in both wire shapes and the typed reefer-state record.
/// </summary>
public sealed class TrailerStatsContractTests
{
    // ── GET /fleet/trailers/stats (snapshot) ────────────────────────────────
    [Fact]
    public async Task GetStatsSnapshotAsync_BindsSingularGpsAndReeferState()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "trl-1",
                    name = "Reefer 1",
                    gps = new
                    {
                        latitude = 40.7128,
                        longitude = -74.0060,
                        time = "2024-01-01T00:00:00Z",
                        headingDegrees = 180L,
                        speedMilesPerHour = 22L,
                    },
                    reeferStateZone1 = new
                    {
                        time = "2024-01-01T00:00:00Z",
                        value = "On",
                        substateValue = "Defrost",
                    },
                    reeferFuelPercent = new { time = "2024-01-01T00:00:00Z", value = 60L },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TrailersClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsSnapshotAsync("gps,reeferStateZone1,reeferFuelPercent", trailerIds: "trl-1"));

        items.Should().HaveCount(1);
        var stats = items[0];
        stats.Id.Should().Be("trl-1");
        // Singular gps binds to a single TrailerStatGps (note: integer heading/speed).
        stats.Gps.Should().NotBeNull();
        stats.Gps!.Latitude.Should().Be(40.7128);
        stats.Gps.HeadingDegrees.Should().Be(180L);
        // Reefer state carries the optional substate.
        stats.ReeferStateZone1.Should().NotBeNull();
        stats.ReeferStateZone1!.Value.Should().Be("On");
        stats.ReeferStateZone1.SubstateValue.Should().Be("Defrost");
        stats.ReeferFuelPercent!.Value.Should().Be(60L);

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/trailers/stats");
        url.Should().Contain("trailerIds=trl-1");
    }

    // ── GET /fleet/trailers/stats/feed (time-series) ────────────────────────
    [Fact]
    public async Task GetStatsFeedAsync_BindsGpsAndReeferStateArrays()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "trl-1",
                    name = "Reefer 1",
                    gps = new[]
                    {
                        new { latitude = 40.7128, longitude = -74.0060, time = "2024-01-01T00:00:00Z" },
                        new { latitude = 40.7130, longitude = -74.0062, time = "2024-01-01T00:10:00Z" },
                    },
                    reeferStateZone1 = new[]
                    {
                        new { time = "2024-01-01T00:00:00Z", value = "Off" },
                        new { time = "2024-01-01T00:10:00Z", value = "On" },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TrailersClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsFeedAsync("gps,reeferStateZone1", trailerIds: "trl-1"));

        items.Should().HaveCount(1);
        var sample = items[0];
        sample.Gps.Should().HaveCount(2);
        sample.Gps![1].Longitude.Should().Be(-74.0062);
        sample.ReeferStateZone1.Should().HaveCount(2);
        sample.ReeferStateZone1!.Select(s => s.Value).Should().ContainInOrder("Off", "On");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/trailers/stats/feed");
    }

    // ── GET /fleet/trailers/stats/feed — reefer alarms nested array ─────────
    [Fact]
    public async Task GetStatsFeedAsync_BindsReeferAlarmsNestedArray()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "trl-1",
                    name = "Reefer 1",
                    reeferAlarms = new[]
                    {
                        new
                        {
                            time = "2024-01-01T00:00:00Z",
                            alarms = new[]
                            {
                                new
                                {
                                    alarmCode = "A1",
                                    description = "Low refrigerant",
                                    operatorAction = "Check unit",
                                    severity = 3L,
                                },
                            },
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TrailersClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsFeedAsync("reeferAlarms", trailerIds: "trl-1"));

        items.Should().HaveCount(1);
        var alarms = items[0].ReeferAlarms;
        alarms.Should().HaveCount(1);
        alarms![0].Alarms.Should().HaveCount(1);
        alarms[0].Alarms[0].AlarmCode.Should().Be("A1");
        alarms[0].Alarms[0].Severity.Should().Be(3L);
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
