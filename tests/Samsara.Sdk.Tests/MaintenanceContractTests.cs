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
