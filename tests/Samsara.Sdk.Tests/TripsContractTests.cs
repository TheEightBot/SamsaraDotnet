namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the two distinct trip shapes (Phase 3): the v2 stream
/// (<c>GET /trips/stream</c>) binds the typed <c>Trip.asset</c>/<c>TripLocation</c>
/// records under the standard <c>{ data: [...] }</c> envelope, while the legacy v1
/// endpoint (<c>GET /v1/fleet/trips</c>) returns a non-standard <c>{ trips: [...] }</c>
/// wrapper bound to <see cref="V1Trip"/> via the fixed <c>ListAsync</c>.
/// </summary>
public sealed class TripsContractTests
{
    // ── GET /trips/stream (v2) ──────────────────────────────────────────────
    [Fact]
    public async Task GetStreamAsync_BindsTypedAssetAndLocations()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    asset = new { id = "veh-1", name = "Truck 1", type = "vehicle", vin = "1FT" },
                    completionStatus = "completed",
                    createdAtTime = "2024-01-01T00:00:00Z",
                    startLocation = new
                    {
                        latitude = 37.7749,
                        longitude = -122.4194,
                        headingDegrees = 90L,
                        // The v2 trip address is a postal address, not an {id,name}
                        // reference — see AddressResponseResponseBody in the spec.
                        address = new
                        {
                            streetNumber = "1",
                            street = "Market St",
                            city = "San Francisco",
                            state = "CA",
                            postalCode = "94105",
                            country = "US",
                            neighborhood = "Embarcadero",
                        },
                    },
                    endLocation = new { latitude = 37.8044, longitude = -122.2712, headingDegrees = 45L },
                    tripStartTime = "2024-01-01T08:00:00Z",
                    tripEndTime = "2024-01-01T09:30:00Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TripsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetStreamAsync(new[] { "veh-1" }, includeAsset: true));

        items.Should().HaveCount(1);
        var trip = items[0];
        trip.Asset.Should().NotBeNull();
        trip.Asset!.Id.Should().Be("veh-1");
        trip.Asset.Vin.Should().Be("1FT");
        trip.CompletionStatus.Should().Be("completed");
        trip.StartLocation.Should().NotBeNull();
        trip.StartLocation!.Latitude.Should().Be(37.7749);
        // Regression guard: this assertion previously read `.Name`, because
        // TripLocationAddress modelled {id, name} — a shape with ZERO overlap with
        // the spec, so every trip address deserialized all-null and the test still
        // passed against a fixture that encoded the same mistake.
        trip.StartLocation.Address!.City.Should().Be("San Francisco");
        trip.StartLocation.Address.Street.Should().Be("Market St");
        trip.StartLocation.Address.PostalCode.Should().Be("94105");
        trip.EndLocation!.Longitude.Should().Be(-122.2712);

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("trips/stream");
        url.Should().Contain("ids=veh-1");
        url.Should().Contain("includeAsset=true");
    }

    // ── GET /v1/fleet/trips (legacy) ────────────────────────────────────────
    [Fact]
    public async Task ListAsync_BindsV1TripsWrapper()
    {
        // The v1 endpoint responds with a bare { trips: [...] } wrapper, NOT the
        // standard { data: [...] } envelope. The fixed ListAsync must read `trips`.
        var resp = new
        {
            trips = new[]
            {
                new
                {
                    assetIds = new[] { 100L, 101L },
                    driverId = 55L,
                    distanceMeters = 42000L,
                    startMs = 1704096000000L,
                    endMs = 1704101400000L,
                    startLocation = "San Francisco, CA",
                    endLocation = "Oakland, CA",
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new TripsClient(TestFactory.CreateHttpClient(handler));

        var trips = await client.ListAsync("100", 1704096000000L, 1704101400000L);

        trips.Should().HaveCount(1);
        trips[0].DriverId.Should().Be(55L);
        trips[0].DistanceMeters.Should().Be(42000L);
        trips[0].AssetIds.Should().ContainInOrder(100L, 101L);
        trips[0].StartLocation.Should().Be("San Francisco, CA");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("v1/fleet/trips");
        url.Should().Contain("vehicleId=100");
        url.Should().Contain("startMs=1704096000000");
        url.Should().Contain("endMs=1704101400000");
    }

    [Fact]
    public async Task ListAsync_ReturnsEmptyList_WhenTripsOmitted()
    {
        // An empty response ({}) must yield an empty list, not throw.
        var handler = MockHttpMessageHandler.WithJsonResponse(new { });
        var client = new TripsClient(TestFactory.CreateHttpClient(handler));

        var trips = await client.ListAsync("100", 0L, 1L);

        trips.Should().NotBeNull();
        trips.Should().BeEmpty();
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
