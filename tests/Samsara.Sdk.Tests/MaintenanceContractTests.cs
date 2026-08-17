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

    // ── MaintenanceDvirAssetRef: the v1 and v2 asset shapes bind independently ──
    //
    // MaintenanceDvir and DefectRecord each answer both a v1 and a v2 endpoint, so
    // their trailer/vehicle properties must bind four different spec schemas:
    //
    //   trailerTinyResponse            {id, name}              v1  no external IDs
    //   vehicleTinyResponse            {ExternalIds, id, name} v1  capital E (spec typo)
    //   TrailerDvirObjectResponseBody  {externalIds, id}       v2  no name
    //   VehicleDvirObjectResponseBody  {externalIds, id}       v2  no name
    //
    // These tests pin all four so a future per-schema split cannot silently drop a
    // field, and so the capital-E casing is not "corrected" without noticing that
    // the v1 payload stops binding.

    /// <summary>
    /// <c>POST /fleet/dvirs</c> returns the v1 <c>Dvir</c> shape, whose
    /// <c>vehicle</c> is <c>vehicleTinyResponse</c> — <c>{ExternalIds, id, name}</c>,
    /// with the external-ID map spelled with a capital E. The SDK declares the
    /// property as <c>externalIds</c>; it still populates because deserialization is
    /// case-insensitive.
    /// </summary>
    [Fact]
    public async Task CreateDvirAsync_V1VehicleWithCapitalExternalIds_PopulatesTheMap()
    {
        var resp = new
        {
            data = new
            {
                id = "dvir-1",
                safetyStatus = "safe",
                vehicle = new
                {
                    id = "veh-1",
                    name = "Midwest Truck #4",
                    ExternalIds = new Dictionary<string, string>
                    {
                        ["maintenanceId"] = "250020",
                        ["payrollId"] = "ABFS18600",
                    },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var dvir = await client.CreateDvirAsync(new CreateDvirRequest
        {
            AuthorId = "usr-1",
            SafetyStatus = "safe",
            Type = "mechanic",
        });

        dvir.Vehicle.Should().NotBeNull();
        dvir.Vehicle!.Id.Should().Be("veh-1");
        // Present only on the v1 schema.
        dvir.Vehicle.Name.Should().Be("Midwest Truck #4");
        // Capital-E payload binds the lowercase-declared property.
        dvir.Vehicle.ExternalIds.Should().NotBeNull();
        dvir.Vehicle.ExternalIds!["maintenanceId"].Should().Be("250020");
        dvir.Vehicle.ExternalIds["payrollId"].Should().Be("ABFS18600");
    }

    /// <summary>
    /// <c>PATCH /fleet/dvirs/{id}</c> also returns the v1 <c>Dvir</c> shape, whose
    /// <c>trailer</c> is <c>trailerTinyResponse</c> — <c>{id, name}</c> with no
    /// external-ID map at all.
    /// </summary>
    [Fact]
    public async Task UpdateDvirAsync_V1Trailer_BindsNameAndHasNoExternalIds()
    {
        var resp = new
        {
            data = new
            {
                id = "dvir-1",
                safetyStatus = "resolved",
                trailer = new { id = "trl-1", name = "Midwest Trailer #5" },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var dvir = await client.UpdateDvirAsync("dvir-1", new UpdateDvirRequest
        {
            AuthorId = "usr-1",
            IsResolved = true,
        });

        dvir.Trailer.Should().NotBeNull();
        dvir.Trailer!.Id.Should().Be("trl-1");
        dvir.Trailer.Name.Should().Be("Midwest Trailer #5");
        // trailerTinyResponse carries no external IDs — the property exists on the
        // shared record only to serve the v2 shape.
        dvir.Trailer.ExternalIds.Should().BeNull();

        handler.LastRequestBody.Should().Contain("\"authorId\":\"usr-1\"");
    }

    /// <summary>
    /// <c>GET /dvirs/{id}</c> returns the v2 <c>DvirStreamResponseDataResponseBody</c>,
    /// whose <c>trailer</c>/<c>vehicle</c> are <c>{externalIds, id}</c> — lowercase,
    /// and with no <c>name</c> to bind.
    /// </summary>
    [Fact]
    public async Task GetDvirByIdAsync_V2AssetRefs_BindLowercaseExternalIdsAndNoName()
    {
        var resp = new
        {
            data = new
            {
                id = "dvir-1",
                type = "mechanic",
                updatedAtTime = "2024-01-01T08:05:00Z",
                vehicle = new
                {
                    id = "494123",
                    externalIds = new Dictionary<string, string> { ["maintenanceId"] = "250020" },
                },
                trailer = new
                {
                    id = "494124",
                    externalIds = new Dictionary<string, string> { ["payrollId"] = "ABFS18600" },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var dvir = await client.GetDvirByIdAsync("dvir-1");

        dvir.Vehicle!.Id.Should().Be("494123");
        dvir.Vehicle.ExternalIds!["maintenanceId"].Should().Be("250020");
        dvir.Vehicle.Name.Should().BeNull("VehicleDvirObjectResponseBody has no name");

        dvir.Trailer!.Id.Should().Be("494124");
        dvir.Trailer.ExternalIds!["payrollId"].Should().Be("ABFS18600");
        dvir.Trailer.Name.Should().BeNull("TrailerDvirObjectResponseBody has no name");
    }

    /// <summary>
    /// <c>PATCH /fleet/defects/{id}</c> returns the v1 <c>Defect</c> shape, where
    /// <c>trailer</c> and <c>vehicle</c> disagree within a single object:
    /// <c>trailerTinyResponse</c> has no external IDs, while
    /// <c>vehicleTinyResponse</c> spells them with a capital E.
    /// </summary>
    [Fact]
    public async Task UpdateDefectAsync_V1AssetRefs_BindBothSpellingsInOneObject()
    {
        var resp = new
        {
            data = new
            {
                id = "def-1",
                isResolved = true,
                comment = "Air Compressor not working",
                defectType = "Air Compressor",
                trailer = new { id = "trl-1", name = "Midwest Trailer #5" },
                vehicle = new
                {
                    id = "veh-1",
                    name = "Midwest Truck #4",
                    ExternalIds = new Dictionary<string, string> { ["maintenanceId"] = "250020" },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var defect = await client.UpdateDefectAsync("def-1", new UpdateDefectRequest
        {
            IsResolved = true,
            ResolvedBy = new UpdateDefectResolvedBy { Id = "mech-1", Type = "mechanic" },
        });

        defect.Trailer!.Name.Should().Be("Midwest Trailer #5");
        defect.Trailer.ExternalIds.Should().BeNull();

        defect.Vehicle!.Name.Should().Be("Midwest Truck #4");
        defect.Vehicle.ExternalIds!["maintenanceId"].Should().Be("250020");
    }

    /// <summary>
    /// <c>GET /defects/stream</c> returns the v2 shape, whose <c>trailer</c> is
    /// <c>DefectTrailerResponseResponseBody</c> — <c>{externalIds, id}</c>, byte-identical
    /// to the v2 DVIR trailer and with no <c>name</c>.
    /// </summary>
    [Fact]
    public async Task GetDefectsStreamAsync_V2TrailerRef_BindsExternalIdsAndNoName()
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
                    trailer = new
                    {
                        id = "494123",
                        externalIds = new Dictionary<string, string> { ["maintenanceId"] = "250020" },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var defect = (await CollectAsync(client.GetDefectsStreamAsync(includeExternalIds: true)))[0];

        defect.Trailer!.Id.Should().Be("494123");
        defect.Trailer.ExternalIds!["maintenanceId"].Should().Be("250020");
        defect.Trailer.Name.Should().BeNull("DefectTrailerResponseResponseBody has no name");
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
