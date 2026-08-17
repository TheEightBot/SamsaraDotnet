namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Maintenance domain (Phase 3). <see cref="MaintenanceDvir"/>
/// binds typed nested signature/asset-reference records, and <see cref="DefectRecord"/>
/// binds the typed <c>resolvedBy</c>/asset references against the
/// <c>GET /dvirs/stream</c> and <c>GET /defects/stream</c> wire shapes.
/// </summary>
public sealed class MaintenanceContractTests
{
    // ── GET /dvirs/stream ───────────────────────────────────────────────────
    [Fact]
    public async Task GetDvirsStreamAsync_BindsTypedSignatureAndVehicle()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "dvir-1",
                    type = "preTrip",
                    safetyStatus = "safe",
                    dvirSubmissionTime = "2024-01-01T08:00:00Z",
                    updatedAtTime = "2024-01-01T08:05:00Z",
                    authorSignature = new
                    {
                        signatoryUser = new { id = "drv-1" },
                        signedAtTime = "2024-01-01T08:00:00Z",
                        type = "driver",
                    },
                    vehicle = new { id = "veh-1", externalIds = new Dictionary<string, string> { ["maintenanceId"] = "M-1" } },
                    defectIds = new[] { "def-1", "def-2" },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetDvirsStreamAsync());

        items.Should().HaveCount(1);
        var dvir = items[0];
        dvir.Id.Should().Be("dvir-1");
        dvir.Type.Should().Be("preTrip");
        dvir.SafetyStatus.Should().Be("safe");
        // Typed nested signature.
        dvir.AuthorSignature.Should().NotBeNull();
        dvir.AuthorSignature!.Type.Should().Be("driver");
        dvir.AuthorSignature.SignatoryUser!.Id.Should().Be("drv-1");
        // Typed nested vehicle reference with external ids.
        dvir.Vehicle.Should().NotBeNull();
        dvir.Vehicle!.Id.Should().Be("veh-1");
        dvir.Vehicle.ExternalIds!["maintenanceId"].Should().Be("M-1");
        dvir.DefectIds.Should().ContainInOrder("def-1", "def-2");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("dvirs/stream");
    }

    // ── GET /defects/stream ─────────────────────────────────────────────────
    [Fact]
    public async Task GetDefectsStreamAsync_BindsTypedResolvedByAndAssetRefs()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "def-1",
                    dvirId = "dvir-1",
                    comment = "Cracked windshield",
                    isResolved = true,
                    createdAtTime = "2024-01-01T08:00:00Z",
                    resolvedAtTime = "2024-01-02T10:00:00Z",
                    resolvedBy = new { id = "mech-1", name = "Bob Mechanic", type = "mechanic" },
                    vehicle = new { id = "veh-1" },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetDefectsStreamAsync(isResolved: true));

        items.Should().HaveCount(1);
        var defect = items[0];
        defect.Id.Should().Be("def-1");
        defect.IsResolved.Should().BeTrue();
        defect.Comment.Should().Be("Cracked windshield");
        // Typed nested resolvedBy.
        defect.ResolvedBy.Should().NotBeNull();
        defect.ResolvedBy!.Type.Should().Be("mechanic");
        defect.ResolvedBy.Name.Should().Be("Bob Mechanic");
        defect.Vehicle!.Id.Should().Be("veh-1");

        var url = handler.LastRequest.RequestUri!.PathAndQuery;
        url.Should().Contain("defects/stream");
        url.Should().Contain("isResolved=true");
    }

    // ── GET /dvirs/stream — walkaroundPhotos (was IReadOnlyList<JsonElement>) ──
    [Fact]
    public async Task GetDvirsStreamAsync_BindsTypedWalkaroundPhotos()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "dvir-1",
                    type = "preTrip",
                    walkaroundPhotos = new[]
                    {
                        new
                        {
                            createdAtTime = "2024-01-01T08:00:00Z",
                            name = "front-left.jpg",
                            url = "https://media.samsara.com/front-left.jpg",
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetDvirsStreamAsync());

        var photos = items[0].WalkaroundPhotos;
        photos.Should().HaveCount(1);
        photos![0].Name.Should().Be("front-left.jpg");
        photos[0].Url.Should().Be("https://media.samsara.com/front-left.jpg");
        photos[0].CreatedAtTime.Should().Be("2024-01-01T08:00:00Z");
    }

    // ── POST /fleet/dvirs — trailerDefects/vehicleDefects share one item schema ──
    [Fact]
    public async Task CreateDvirAsync_BindsTypedVehicleAndTrailerDefects()
    {
        var resp = new
        {
            data = new
            {
                id = "dvir-1",
                safetyStatus = "unsafe",
                trailerName = "Trailer 5",
                vehicleDefects = new[]
                {
                    new
                    {
                        id = "def-1",
                        isResolved = false,
                        comment = "Air compressor not working",
                        defectType = "Air Compressor",
                        createdAtTime = "2024-01-01T08:00:00Z",
                        vehicle = new
                        {
                            id = "veh-1",
                            name = "Truck 4",
                            ExternalIds = new Dictionary<string, string> { ["maintenanceId"] = "M-1" },
                        },
                    },
                },
                trailerDefects = new[]
                {
                    new
                    {
                        id = "def-2",
                        isResolved = true,
                        mechanicNotes = "Replaced light",
                        mechanicNotesUpdatedAtTime = "2024-01-02T09:00:00Z",
                        resolvedAtTime = "2024-01-02T10:00:00Z",
                        resolvedBy = new { id = "mech-1", name = "Bob Mechanic", type = "mechanic" },
                        trailer = new { id = "trl-1", name = "Trailer 5" },
                    },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var dvir = await client.CreateDvirAsync(new CreateDvirRequest
        {
            AuthorId = "usr-1",
            SafetyStatus = "unsafe",
            Type = "mechanic",
        });

        var vehicleDefect = dvir.VehicleDefects.Should().ContainSingle().Subject;
        vehicleDefect.Id.Should().Be("def-1");
        vehicleDefect.IsResolved.Should().BeFalse();
        vehicleDefect.DefectType.Should().Be("Air Compressor");
        vehicleDefect.Vehicle.Should().NotBeNull();
        vehicleDefect.Vehicle!.Name.Should().Be("Truck 4");
        // The spec spells this map with a capital E on this schema only.
        vehicleDefect.Vehicle.ExternalIds!["maintenanceId"].Should().Be("M-1");

        var trailerDefect = dvir.TrailerDefects.Should().ContainSingle().Subject;
        trailerDefect.Id.Should().Be("def-2");
        trailerDefect.IsResolved.Should().BeTrue();
        trailerDefect.ResolvedBy.Should().NotBeNull();
        trailerDefect.ResolvedBy!.Name.Should().Be("Bob Mechanic");
        trailerDefect.Trailer.Should().NotBeNull();
        trailerDefect.Trailer!.Name.Should().Be("Trailer 5");
    }

    // ── GET /defects/stream — defectPhotos + defectSafetyStatus ─────────────
    [Fact]
    public async Task GetDefectsStreamAsync_BindsTypedDefectPhotosAndSafetyStatus()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "def-1",
                    dvirId = "dvir-1",
                    comment = "Cracked windshield",
                    isResolved = false,
                    defectSafetyStatus = "unsafe",
                    defectPhotos = new[]
                    {
                        new
                        {
                            createdAtTime = "2024-01-01T08:00:00Z",
                            url = "https://media.samsara.com/defect-1.jpg",
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var defect = (await CollectAsync(client.GetDefectsStreamAsync()))[0];

        defect.DefectSafetyStatus.Should().Be("unsafe");
        var photo = defect.DefectPhotos.Should().ContainSingle().Subject;
        photo.Url.Should().Be("https://media.samsara.com/defect-1.jpg");
        photo.CreatedAtTime.Should().Be("2024-01-01T08:00:00Z");
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
