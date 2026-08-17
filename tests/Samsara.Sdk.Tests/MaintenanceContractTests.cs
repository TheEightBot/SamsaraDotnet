namespace Samsara.Sdk.Tests;

using System.Reflection;
using System.Text.Json.Serialization;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Maintenance domain. The DVIR and defect endpoints come
/// in two versions returning genuinely different objects, so each version has its
/// own record: <see cref="MaintenanceDvir"/>/<see cref="DefectRecord"/> for the v2
/// stream/get endpoints and <see cref="V1MaintenanceDvir"/>/<see cref="V1DefectRecord"/>
/// for the v1 create/update endpoints. These tests pin each record to its own
/// schema's wire shape so the split cannot silently regress into a union again.
/// See the 2026-08-17b design note in <c>docs/api-sync/30-maintenance.md</c>.
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

    // ── The four asset-reference shapes, one record each ────────────────────
    //
    // trailerTinyResponse            {id, name}               v1  V1MaintenanceTrailerRef
    // vehicleTinyResponse            {ExternalIds, id, name}  v1  V1MaintenanceVehicleRef  (capital E)
    // TrailerDvirObjectResponseBody  {externalIds, id}        v2  MaintenanceDvirAssetRef
    // VehicleDvirObjectResponseBody  {externalIds, id}        v2  MaintenanceDvirAssetRef
    //
    // These tests pin all four so the records cannot drift back into a union and
    // so the capital-E casing is not "corrected" unnoticed.

    /// <summary>
    /// <c>POST /fleet/dvirs</c> returns the v1 <c>Dvir</c> shape, whose
    /// <c>vehicle</c> is <c>vehicleTinyResponse</c> — <c>{ExternalIds, id, name}</c>,
    /// with the external-ID map spelled with a capital E, which the SDK mirrors
    /// verbatim on <see cref="V1MaintenanceVehicleRef"/>.
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
        // trailerTinyResponse carries no external IDs, so the record declares none.
        typeof(V1MaintenanceTrailerRef).GetProperties()
            .Select(p => p.Name)
            .Should().BeEquivalentTo(["Id", "Name"], "trailerTinyResponse is {id, name}");

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

        dvir.Trailer!.Id.Should().Be("494124");
        dvir.Trailer.ExternalIds!["payrollId"].Should().Be("ABFS18600");

        // Neither v2 schema defines a name, so the record declares none.
        typeof(MaintenanceDvirAssetRef).GetProperties()
            .Select(p => p.Name)
            .Should().BeEquivalentTo(["Id", "ExternalIds"], "the v2 asset refs are {externalIds, id}");
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

        defect.Vehicle!.Name.Should().Be("Midwest Truck #4");
        defect.Vehicle.ExternalIds!["maintenanceId"].Should().Be("250020");

        // v1-only fields that the v2 DefectRecord does not carry.
        defect.DefectType.Should().Be("Air Compressor");
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
    }

    // ── Per-version endpoint binding ────────────────────────────────────────

    /// <summary>
    /// The capital-E spelling on <see cref="V1MaintenanceVehicleRef"/> is copied
    /// from <c>vehicleTinyResponse</c> verbatim and is the only such spelling in
    /// the spec. Because deserialization is case-insensitive, "correcting" it
    /// would not break any wire test — this attribute assertion is the guard that
    /// would. See the remarks on the record.
    /// </summary>
    [Fact]
    public void V1MaintenanceVehicleRef_ExternalIds_MirrorsTheSpecCapitalESpelling()
    {
        JsonNameOf<V1MaintenanceVehicleRef>(nameof(V1MaintenanceVehicleRef.ExternalIds))
            .Should().Be(
                "ExternalIds",
                "vehicleTinyResponse spells it with a capital E; the SDK mirrors the spec verbatim");

        JsonNameOf<MaintenanceDvirAssetRef>(nameof(MaintenanceDvirAssetRef.ExternalIds))
            .Should().Be("externalIds", "the v2 asset schemas spell it lowercase");
    }

    /// <summary>
    /// <c>PATCH /fleet/defects/{id}</c> returns the v1 <c>Defect</c> shape, which
    /// carries <c>defectType</c> and <c>mechanicNotesUpdatedAtTime</c> — neither
    /// of which exists on the v2 <see cref="DefectRecord"/>.
    /// </summary>
    [Fact]
    public async Task UpdateDefectAsync_BindsTheV1OnlyFields()
    {
        var resp = new
        {
            data = new
            {
                id = "def-1",
                isResolved = true,
                comment = "Air Compressor not working",
                defectType = "Air Compressor",
                createdAtTime = "2024-01-01T08:00:00Z",
                mechanicNotes = "Replaced compressor",
                mechanicNotesUpdatedAtTime = "2024-01-02T09:00:00Z",
                resolvedAtTime = "2024-01-02T10:00:00Z",
                resolvedBy = new { id = "mech-1", name = "Bob Mechanic", type = "mechanic" },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var defect = await client.UpdateDefectAsync("def-1", new UpdateDefectRequest
        {
            IsResolved = true,
            ResolvedBy = new UpdateDefectResolvedBy { Id = "mech-1", Type = "mechanic" },
        });

        defect.Should().BeOfType<V1DefectRecord>();
        defect.DefectType.Should().Be("Air Compressor");
        defect.MechanicNotesUpdatedAtTime.Should().Be("2024-01-02T09:00:00Z");
        defect.ResolvedBy!.Name.Should().Be("Bob Mechanic");
    }

    /// <summary>
    /// <c>GET /defects/{id}</c> returns the v2
    /// <c>DvirDefectGetDefectResponseBody</c> shape, which carries
    /// <c>dvirId</c>, <c>defectTypeId</c>, <c>defectSafetyStatus</c> and
    /// <c>updatedAtTime</c> — none of which exist on the v1 record.
    /// </summary>
    [Fact]
    public async Task GetDefectAsync_BindsTheV2OnlyFields()
    {
        var resp = new
        {
            data = new
            {
                id = "def-1",
                dvirId = "dvir-9",
                comment = "Cracked windshield",
                isResolved = false,
                defectTypeId = "dt-3",
                defectSafetyStatus = "unsafe",
                updatedAtTime = "2024-01-02T09:00:00Z",
                vehicle = new
                {
                    id = "veh-1",
                    externalIds = new Dictionary<string, string> { ["maintenanceId"] = "250020" },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var defect = await client.GetDefectAsync("def-1");

        defect.Should().BeOfType<DefectRecord>();
        defect.DvirId.Should().Be("dvir-9");
        defect.DefectTypeId.Should().Be("dt-3");
        defect.DefectSafetyStatus.Should().Be("unsafe");
        defect.UpdatedAtTime.Should().Be("2024-01-02T09:00:00Z");
        defect.Vehicle!.ExternalIds!["maintenanceId"].Should().Be("250020");
    }

    /// <summary>
    /// <c>PATCH /fleet/dvirs/{id}</c> returns the v1 <c>Dvir</c> shape, which
    /// carries <c>startTime</c>, <c>endTime</c>, <c>licensePlate</c>,
    /// <c>location</c> and <c>trailerName</c> — none of which exist on the v2
    /// <see cref="MaintenanceDvir"/>.
    /// </summary>
    [Fact]
    public async Task UpdateDvirAsync_BindsTheV1OnlyFields()
    {
        var resp = new
        {
            data = new
            {
                id = "dvir-1",
                type = "mechanic",
                safetyStatus = "resolved",
                startTime = "2024-01-01T08:00:00Z",
                endTime = "2024-01-01T08:30:00Z",
                licensePlate = "8VZR291",
                location = "Bay 4, Chicago Yard",
                trailerName = "Midwest Trailer #5",
                odometerMeters = 123456,
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var dvir = await client.UpdateDvirAsync("dvir-1", new UpdateDvirRequest
        {
            AuthorId = "usr-1",
            IsResolved = true,
        });

        dvir.Should().BeOfType<V1MaintenanceDvir>();
        dvir.StartTime.Should().Be("2024-01-01T08:00:00Z");
        dvir.EndTime.Should().Be("2024-01-01T08:30:00Z");
        dvir.LicensePlate.Should().Be("8VZR291");
        dvir.Location.Should().Be("Bay 4, Chicago Yard");
        dvir.TrailerName.Should().Be("Midwest Trailer #5");
        dvir.OdometerMeters.Should().Be(123456);
    }

    /// <summary>
    /// <c>GET /dvirs/{id}</c> returns the v2 shape, which carries
    /// <c>dvirSubmissionBeginTime</c>, <c>dvirSubmissionTime</c>,
    /// <c>updatedAtTime</c>, <c>formattedAddress</c>, <c>defectIds</c> and
    /// <c>walkaroundPhotos</c> — none of which exist on the v1 record.
    /// </summary>
    [Fact]
    public async Task GetDvirByIdAsync_BindsTheV2OnlyFields()
    {
        var resp = new
        {
            data = new
            {
                id = "dvir-1",
                type = "mechanic",
                dvirSubmissionBeginTime = "2024-01-01T08:00:00Z",
                dvirSubmissionTime = "2024-01-01T08:10:00Z",
                updatedAtTime = "2024-01-01T08:15:00Z",
                formattedAddress = "350 Rhode Island St, San Francisco, CA",
                defectIds = new[] { "def-1", "def-2" },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var dvir = await client.GetDvirByIdAsync("dvir-1");

        dvir.Should().BeOfType<MaintenanceDvir>();
        dvir.DvirSubmissionBeginTime.Should().Be("2024-01-01T08:00:00Z");
        dvir.DvirSubmissionTime.Should().Be("2024-01-01T08:10:00Z");
        dvir.UpdatedAtTime.Should().Be("2024-01-01T08:15:00Z");
        dvir.FormattedAddress.Should().Be("350 Rhode Island St, San Francisco, CA");
        dvir.DefectIds.Should().ContainInOrder("def-1", "def-2");
    }

    /// <summary>
    /// The v1 and v2 signature objects nest different signatory users:
    /// <c>userTinyResponse</c> (<c>{id, name}</c>) on v1 versus
    /// <c>SignatoryUserObjectResponseBody</c> (<c>{externalIds, id}</c>) on v2.
    /// That single difference is what forced the signature records apart.
    /// </summary>
    [Fact]
    public async Task DvirSignatories_BindPerVersionShapes()
    {
        var v1Handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            data = new
            {
                id = "dvir-1",
                authorSignature = new
                {
                    signatoryUser = new { id = "usr-1", name = "Alice Driver" },
                    signedAtTime = "2024-01-01T08:00:00Z",
                    type = "driver",
                },
            },
        });
        var v1Dvir = await new MaintenanceClient(TestFactory.CreateHttpClient(v1Handler))
            .CreateDvirAsync(new CreateDvirRequest { AuthorId = "usr-1", SafetyStatus = "safe", Type = "mechanic" });

        v1Dvir.AuthorSignature!.SignatoryUser!.Id.Should().Be("usr-1");
        v1Dvir.AuthorSignature.SignatoryUser.Name.Should().Be("Alice Driver");
        typeof(V1MaintenanceSignatoryUser).GetProperties()
            .Select(p => p.Name)
            .Should().BeEquivalentTo(["Id", "Name"], "userTinyResponse is {id, name}");

        var v2Handler = MockHttpMessageHandler.WithJsonResponse(new
        {
            data = new
            {
                id = "dvir-1",
                authorSignature = new
                {
                    signatoryUser = new
                    {
                        id = "usr-1",
                        externalIds = new Dictionary<string, string> { ["payrollId"] = "ABFS18600" },
                    },
                    signedAtTime = "2024-01-01T08:00:00Z",
                    type = "driver",
                },
            },
        });
        var v2Dvir = await new MaintenanceClient(TestFactory.CreateHttpClient(v2Handler))
            .GetDvirByIdAsync("dvir-1");

        v2Dvir.AuthorSignature!.SignatoryUser!.Id.Should().Be("usr-1");
        v2Dvir.AuthorSignature.SignatoryUser.ExternalIds!["payrollId"].Should().Be("ABFS18600");
        typeof(MaintenanceSignatoryUser).GetProperties()
            .Select(p => p.Name)
            .Should().BeEquivalentTo(["Id", "ExternalIds"], "SignatoryUserObjectResponseBody is {externalIds, id}");
    }

    private static string? JsonNameOf<T>(string propertyName)
        => typeof(T).GetProperty(propertyName)!
            .GetCustomAttribute<JsonPropertyNameAttribute>()!.Name;

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
