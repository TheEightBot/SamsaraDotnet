namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the vehicle-stats snapshot vs. time-series split (Phase 3).
/// The snapshot endpoint (<c>GET /fleet/vehicles/stats</c>) returns singular
/// <c>engineState</c>/<c>gps</c> objects, while the feed/history endpoints
/// (<c>.../stats/feed</c>, <c>.../stats/history</c>) return <c>engineStates</c>/<c>gps</c>
/// as ARRAYS of <c>{ time, value }</c> samples. Binding the time-series arrays was the
/// silent-data-loss fix; these tests lock in both wire shapes.
/// </summary>
public sealed class VehicleStatsContractTests
{
    // ── GET /fleet/vehicles/stats (snapshot) ────────────────────────────────
    [Fact]
    public async Task ListStatsAsync_BindsSnapshotSingularEngineStateAndGps()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "veh-1",
                    name = "Truck 1",
                    engineState = new { time = "2024-01-01T00:00:00Z", value = "On" },
                    gps = new
                    {
                        latitude = 37.7749,
                        longitude = -122.4194,
                        time = "2024-01-01T00:00:00Z",
                        headingDegrees = 90.0,
                        speedMilesPerHour = 35.5,
                        reverseGeo = new { formattedLocation = "San Francisco, CA" },
                    },
                    obdOdometerMeters = new { time = "2024-01-01T00:00:00Z", value = 123456L },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListStatsAsync("engineStates,gps,obdOdometerMeters", new[] { "veh-1" }));

        items.Should().HaveCount(1);
        var stats = items[0];
        stats.Id.Should().Be("veh-1");
        // Singular engineState binds to a single VehicleStatStringValue (not an array).
        stats.EngineState.Should().NotBeNull();
        stats.EngineState!.Value.Should().Be("On");
        // Singular gps binds to a single VehicleStatGps.
        stats.Gps.Should().NotBeNull();
        stats.Gps!.Latitude.Should().Be(37.7749);
        stats.Gps.SpeedMilesPerHour.Should().Be(35.5);
        stats.ObdOdometerMeters!.Value.Should().Be(123456L);

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/vehicles/stats");
        url.Should().Contain("types=engineStates%2Cgps%2CobdOdometerMeters");
        url.Should().Contain("vehicleIds=veh-1");
    }

    // ── GET /fleet/vehicles/stats/feed (time-series) ────────────────────────
    [Fact]
    public async Task GetStatsFeedAsync_BindsTimeSeriesEngineStatesAndGpsArrays()
    {
        // This is the silent-data-loss fix: the feed shape returns engineStates and
        // gps as ARRAYS. If they don't bind, the data is silently dropped.
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "veh-1",
                    name = "Truck 1",
                    engineStates = new[]
                    {
                        new { time = "2024-01-01T00:00:00Z", value = "Off" },
                        new { time = "2024-01-01T00:05:00Z", value = "On" },
                    },
                    gps = new[]
                    {
                        new { latitude = 37.7749, longitude = -122.4194, time = "2024-01-01T00:00:00Z" },
                        new { latitude = 37.7750, longitude = -122.4195, time = "2024-01-01T00:05:00Z" },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsFeedAsync("engineStates,gps", new[] { "veh-1" }));

        items.Should().HaveCount(1);
        var sample = items[0];
        sample.Id.Should().Be("veh-1");
        sample.EngineStates.Should().NotBeNull();
        sample.EngineStates.Should().HaveCount(2);
        sample.EngineStates!.Select(s => s.Value).Should().ContainInOrder("Off", "On");
        sample.Gps.Should().NotBeNull();
        sample.Gps.Should().HaveCount(2);
        sample.Gps![1].Longitude.Should().Be(-122.4195);

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/stats/feed");
    }

    // ── GET /fleet/vehicles/stats/history (time-series) ─────────────────────
    [Fact]
    public async Task GetStatsHistoryAsync_BindsFuelPercentsAndAuxInputArrays()
    {
        // The history shape uses the pluralized fuelPercents key and aux-input
        // arrays that carry a boolean value plus an optional name.
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "veh-1",
                    name = "Truck 1",
                    fuelPercents = new[]
                    {
                        new { time = "2024-01-01T00:00:00Z", value = 80L },
                        new { time = "2024-01-01T01:00:00Z", value = 75L },
                    },
                    auxInput1 = new[]
                    {
                        new { time = "2024-01-01T00:00:00Z", value = true, name = "Boom" },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsHistoryAsync(
            "fuelPercents,auxInput1",
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
            new[] { "veh-1" }));

        items.Should().HaveCount(1);
        var sample = items[0];
        sample.FuelPercents.Should().HaveCount(2);
        sample.FuelPercents!.Select(f => f.Value).Should().ContainInOrder(80L, 75L);
        sample.AuxInput1.Should().HaveCount(1);
        sample.AuxInput1![0].Value.Should().BeTrue();
        sample.AuxInput1[0].Name.Should().Be("Boom");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/vehicles/stats/history");
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
