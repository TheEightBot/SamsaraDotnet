namespace Samsara.Sdk.Tests;

using System.Net;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Tests.Helpers;

public sealed class VehiclesClientTests
{
    [Fact]
    public async Task GetAsync_CallsCorrectPath()
    {
        // Spec marks createdAtTime as REQUIRED on the Vehicle response payload — the
        // mock payload must include it or System.Text.Json's `required` check throws
        // on deserialization.
        var resp = new { data = new { id = "v-1", name = "Truck 1", createdAtTime = "2024-01-01T00:00:00Z" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.GetAsync("v-1");

        vehicle.Id.Should().Be("v-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }

    [Fact]
    public async Task UpdateAsync_PatchesToCorrectPath()
    {
        var resp = new { data = new { id = "v-1", name = "Updated Truck", createdAtTime = "2024-01-01T00:00:00Z" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.UpdateAsync("v-1", new UpdateVehicleRequest { Name = "Updated Truck" });

        vehicle.Name.Should().Be("Updated Truck");
        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/vehicles/v-1");
    }

    // ── GET /v1/fleet/locations ─────────────────────────────────────────────

    /// <summary>
    /// The v1 body carries its items in a TOP-LEVEL <c>vehicles</c> array beside a
    /// top-level <c>pagination</c> block — not under <c>data</c>. Reading it with the v2
    /// list helper would find no items and yield an empty sequence, so this pins both the
    /// envelope and the flat v1 field shape.
    /// </summary>
    [Fact]
    public async Task V1GetFleetLocationsAsync_ReadsTheTopLevelVehiclesEnvelope()
    {
        var resp = new
        {
            pagination = new { hasNextPage = false },
            vehicles = new[]
            {
                new
                {
                    id = 112L,
                    name = "Truck A7",
                    vin = "JTNBB46KX73011966",
                    driverId = 1L,
                    latitude = 37.7749,
                    longitude = -122.4194,
                    location = "1 Main St, Dallas, TX",
                    heading = 246.42,
                    speed = 64.37,
                    odometerMeters = 71_774_705L,
                    odometerType = "GPS",
                    onTrip = true,
                    routeIds = new[] { 2_244_514L, 2_311_654L },
                    time = 1_462_881_998_034L,
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var locations = await CollectAsync(client.V1GetFleetLocationsAsync());

        var location = locations.Should().ContainSingle().Subject;
        location.Id.Should().Be(112);
        location.Name.Should().Be("Truck A7");
        location.DriverId.Should().Be(1);
        location.Latitude.Should().Be(37.7749);
        location.OdometerType.Should().Be("GPS");
        location.OnTrip.Should().BeTrue();
        location.RouteIds.Should().ContainInOrder(2_244_514L, 2_311_654L);
        location.Time.Should().Be(1_462_881_998_034L);

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("v1/fleet/locations");
    }

    /// <summary>The cursor also sits at the top level, so paging must thread it from there.</summary>
    [Fact]
    public async Task V1GetFleetLocationsAsync_PaginatesAcrossPagesAndPassesFilters()
    {
        var page1 = new
        {
            pagination = new { endCursor = "CURSOR2", hasNextPage = true },
            vehicles = new[] { new { id = 1L, name = "A" } },
        };
        var page2 = new
        {
            pagination = new { hasNextPage = false },
            vehicles = new[] { new { id = 2L, name = "B" } },
        };

        var handler = new MockHttpMessageHandler((req, _) =>
        {
            var body = req.RequestUri!.Query.Contains("after=") ? (object)page2 : page1;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(body),
                    System.Text.Encoding.UTF8,
                    "application/json"),
            });
        });
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var locations = await CollectAsync(
            client.V1GetFleetLocationsAsync(vehicleIds: ["1", "2"], tagIds: ["9"]));

        locations.Select(l => l.Id).Should().ContainInOrder(1L, 2L);
        handler.Requests.Should().HaveCount(2);
        handler.Requests[0].RequestUri!.Query.Should().Contain("vehicleIds=1%2C2");
        handler.Requests[0].RequestUri!.Query.Should().Contain("tagIds=9");
        handler.Requests[1].RequestUri!.Query.Should().Contain("after=CURSOR2");
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
