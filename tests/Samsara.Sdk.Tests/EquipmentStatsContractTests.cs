namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the equipment-stats snapshot vs. time-series split (Phase 3).
/// <c>GET /fleet/equipment/stats</c> returns singular typed <c>EquipmentStatValue</c>/
/// <c>EquipmentStatGps</c> objects; <c>.../stats/feed</c> and <c>.../stats/history</c>
/// return arrays (and the pluralized <c>engineStates</c> key).
/// </summary>
public sealed class EquipmentStatsContractTests
{
    // ── GET /fleet/equipment/stats (snapshot) ───────────────────────────────
    [Fact]
    public async Task GetStatsAsync_BindsSingularValueAndGps()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "eq-1",
                    name = "Excavator",
                    engineState = new { time = "2024-01-01T00:00:00Z", value = "On" },
                    engineRpm = new { time = "2024-01-01T00:00:00Z", value = 1500L },
                    gps = new
                    {
                        latitude = 34.0522,
                        longitude = -118.2437,
                        time = "2024-01-01T00:00:00Z",
                        headingDegrees = 270.0,
                        speedMilesPerHour = 0.0,
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsAsync("engineStates,engineRpm,gps", equipmentIds: new[] { "eq-1" }));

        items.Should().HaveCount(1);
        var stats = items[0];
        stats.Id.Should().Be("eq-1");
        stats.EngineState!.Value.Should().Be("On");
        stats.EngineRpm!.Value.Should().Be(1500L);
        stats.Gps.Should().NotBeNull();
        stats.Gps!.Latitude.Should().Be(34.0522);

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("fleet/equipment/stats");
        url.Should().Contain("equipmentIds=eq-1");
    }

    // ── GET /fleet/equipment/stats/feed (time-series) ───────────────────────
    [Fact]
    public async Task GetStatsFeedAsync_BindsEngineStatesAndGpsArrays()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "eq-1",
                    name = "Excavator",
                    engineStates = new[]
                    {
                        new { time = "2024-01-01T00:00:00Z", value = "Off" },
                        new { time = "2024-01-01T00:30:00Z", value = "On" },
                    },
                    gps = new[]
                    {
                        new { latitude = 34.0522, longitude = -118.2437, time = "2024-01-01T00:00:00Z" },
                    },
                    engineRpm = new[]
                    {
                        new { time = "2024-01-01T00:30:00Z", value = 1600L },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new EquipmentClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStatsFeedAsync("engineStates,gps,engineRpm", equipmentIds: new[] { "eq-1" }));

        items.Should().HaveCount(1);
        var sample = items[0];
        sample.EngineStates.Should().HaveCount(2);
        sample.EngineStates!.Select(s => s.Value).Should().ContainInOrder("Off", "On");
        sample.Gps.Should().HaveCount(1);
        sample.Gps![0].Longitude.Should().Be(-118.2437);
        sample.EngineRpm.Should().HaveCount(1);
        sample.EngineRpm![0].Value.Should().Be(1600L);

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/equipment/stats/feed");
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
