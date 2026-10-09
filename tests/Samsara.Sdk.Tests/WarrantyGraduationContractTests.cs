namespace Samsara.Sdk.Tests;

using System.Net;
using System.Reflection;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Preview;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the warranty and warranty-claim operations, which Samsara removed from
/// <c>/preview/maintenance/*</c> and now serves under <c>/maintenance/*</c> (tag
/// <c>Maintenance</c>). The old paths return <c>404</c> from the live API.
/// <para>
/// They pin that <see cref="MaintenanceClient"/> calls the graduated paths, that every
/// <see cref="IPreviewApisClient"/> warranty member still works by forwarding there, and that
/// the new <c>GET /maintenance/warranties/assets</c> list binds its <c>{ id }</c> references.
/// </para>
/// </summary>
public sealed class WarrantyGraduationContractTests
{
    private static object WarrantyEnvelope() => new
    {
        data = new
        {
            id = "w-1",
            name = "Powertrain",
            warrantyType = "manufacturer",
            vendor = new { id = "v-9" },
            durationMonths = 36,
            odometerDistanceMeters = 160934,
            baseCoverage = new
            {
                name = "Base",
                isOdometerDistanceUnlimited = false,
                inclusions = new[] { new { id = "i-1", itemType = "vmrsCode", vmrsCode = "013" } },
            },
        },
    };

    private static object WarrantyPage() => new
    {
        data = new[] { new { id = "w-1", name = "Powertrain" }, new { id = "w-2", name = "Body" } },
        pagination = new { endCursor = "", hasNextPage = false },
    };

    private static object ClaimEnvelope() => new
    {
        data = new { id = "c-1", claimStatus = "submitted", linkedWarranty = new { id = "w-1" } },
    };

    private static object ClaimPage() => new
    {
        data = new[] { new { id = "c-1", claimStatus = "submitted" } },
        pagination = new { endCursor = "", hasNextPage = false },
    };

    private static object ReplaceEnvelope() => new
    {
        data = new
        {
            data = new[] { new { id = "w-1:a-1", warrantyId = "w-1", assetId = "a-1", startOdometerMeters = 1000 } },
        },
    };

    // ── MaintenanceClient: the graduated operations ─────────────────────────

    [Fact]
    public async Task ListWarrantiesAsync_GetsTheGraduatedPathWithFilters()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(WarrantyPage());
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var warranties = await CollectAsync(client.ListWarrantiesAsync(
            warrantyIds: "w-1,w-2", name: "Powertrain", includeExternalIds: true, limit: 50));

        warranties.Select(w => w.Id).Should().Equal("w-1", "w-2");
        handler.LastRequest.Method.Should().Be(HttpMethod.Get);
        var uri = handler.LastRequest.RequestUri!;
        uri.AbsolutePath.Should().Be("/maintenance/warranties");
        uri.Query.Should().Contain("warrantyIds=w-1%2Cw-2")
            .And.Contain("name=Powertrain")
            .And.Contain("includeExternalIds=true")
            .And.Contain("limit=50");
    }

    [Fact]
    public async Task CreateWarrantyAsync_PostsAndBindsTheTypedWarranty()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(WarrantyEnvelope(), HttpStatusCode.Created);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var warranty = await client.CreateWarrantyAsync(new CreateWarrantyRequest { Name = "Powertrain" });

        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.AbsolutePath.Should().Be("/maintenance/warranties");
        handler.LastRequestBody.Should().Contain("\"name\":\"Powertrain\"");
        warranty.Id.Should().Be("w-1");
        warranty.Vendor!.Id.Should().Be("v-9");
        warranty.DurationMonths.Should().Be(36);
        warranty.BaseCoverage!.Inclusions![0].VmrsCode.Should().Be("013");
    }

    [Fact]
    public async Task UpdateWarrantyAsync_PatchesWithTheIdInTheQueryString()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(WarrantyEnvelope());
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        await client.UpdateWarrantyAsync("w-1", new UpdateWarrantyRequest { Description = "extended" });

        handler.LastRequest.Method.Should().Be(HttpMethod.Patch);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/maintenance/warranties?id=w-1");
        handler.LastRequestBody.Should().Contain("extended").And.NotContain("\"id\"");
    }

    [Fact]
    public async Task DeleteWarrantyAsync_DeletesWithTheIdInTheQueryString()
    {
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        await client.DeleteWarrantyAsync("w 1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Delete);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/maintenance/warranties?id=w%201");
    }

    [Fact]
    public async Task ListWarrantyAssetAssignmentsAsync_BindsTheAssetAndWarrantyReferences()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "asg-1",
                    asset = new { id = "a-1" },
                    warranty = new { id = "w-1" },
                    startTime = "2026-01-01T00:00:00Z",
                    startEngineHours = 12,
                    startOdometerMeters = 1000,
                },
            },
            pagination = new { endCursor = "", hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var assignments = await CollectAsync(client.ListWarrantyAssetAssignmentsAsync("w-1", limit: 10));

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/maintenance/warranties/assets?warrantyId=w-1&limit=10");
        assignments.Should().ContainSingle();
        assignments[0].Asset!.Id.Should().Be("a-1");
        assignments[0].Warranty!.Id.Should().Be("w-1");
        assignments[0].StartEngineHours.Should().Be(12);
        assignments[0].StartOdometerMeters.Should().Be(1000);
    }

    [Fact]
    public async Task ReplaceWarrantyAssetAssignmentsAsync_PostsToTheGraduatedReplacePath()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(ReplaceEnvelope());
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var result = await client.ReplaceWarrantyAssetAssignmentsAsync(
            new ReplaceWarrantyAssetAssignmentsRequest
            {
                Assets = [new WarrantyAssetAssignmentInput { AssetId = "a-1", StartOdometerMeters = 1000 }],
            },
            warrantyId: "w-1");

        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/maintenance/warranties/assets/replace?warrantyId=w-1");
        handler.LastRequestBody.Should().Contain("\"assetId\":\"a-1\"");
        result.Data.Should().ContainSingle().Which.WarrantyId.Should().Be("w-1");
    }

    [Fact]
    public async Task ListWarrantyClaimsAsync_GetsTheGraduatedPathWithFilters()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(ClaimPage());
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var claims = await CollectAsync(client.ListWarrantyClaimsAsync(
            warrantyClaimIds: "c-1", assetIds: "a-1", claimStatus: "submitted", warrantyIds: "w-1"));

        claims.Should().ContainSingle().Which.ClaimStatus.Should().Be("submitted");
        var uri = handler.LastRequest.RequestUri!;
        uri.AbsolutePath.Should().Be("/maintenance/warranty-claims");
        uri.Query.Should().Contain("warrantyClaimIds=c-1")
            .And.Contain("assetIds=a-1")
            .And.Contain("claimStatus=submitted")
            .And.Contain("warrantyIds=w-1");
    }

    [Fact]
    public async Task CreateUpdateDeleteWarrantyClaim_UseTheGraduatedPath()
    {
        var handler = new MockHttpMessageHandler((request, _) => Task.FromResult(
            request.Method == HttpMethod.Delete
                ? new HttpResponseMessage(HttpStatusCode.NoContent)
                : JsonResponse(ClaimEnvelope())));
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var created = await client.CreateWarrantyClaimAsync(new CreateWarrantyClaimRequest
        {
            AssetId = "a-1",
            LinkedWarrantyId = "w-1",
            ClaimStatus = "submitted",
        });
        await client.UpdateWarrantyClaimAsync("c-1", new UpdateWarrantyClaimRequest { ClaimStatus = "approved" });
        await client.DeleteWarrantyClaimAsync("c-1");

        created.LinkedWarranty!.Id.Should().Be("w-1");
        handler.Requests.Select(r => $"{r.Method} {r.RequestUri!.PathAndQuery}").Should().Equal(
            "POST /maintenance/warranty-claims",
            "PATCH /maintenance/warranty-claims?id=c-1",
            "DELETE /maintenance/warranty-claims?id=c-1");
    }

    // ── IPreviewApisClient: obsolete forwarders ─────────────────────────────

    [Fact]
    public async Task ObsoletePreviewShims_ForwardEveryWarrantyCallToTheGraduatedPaths()
    {
        var handler = new MockHttpMessageHandler((request, _) => Task.FromResult(
            request.Method == HttpMethod.Delete
                ? new HttpResponseMessage(HttpStatusCode.NoContent)
                : JsonResponse(ResponseFor(request))));
        IPreviewApisClient client = new PreviewApisClient(TestFactory.CreateHttpClient(handler));

#pragma warning disable CS0618 // deliberately exercising the back-compat shims
        var warranties = await CollectAsync(client.ListWarrantiesAsync(name: "Powertrain", limit: 5));
        var created = await client.CreateWarrantyAsync(new CreateWarrantyRequest { Name = "Powertrain" });
        await client.UpdateWarrantyAsync("w-1", new UpdateWarrantyRequest { Name = "Renamed" });
        await client.DeleteWarrantyAsync("w-1");
        var replaced = await client.ReplaceWarrantyAssetAssignmentsAsync(
            new ReplaceWarrantyAssetAssignmentsRequest(), warrantyId: "w-1");
        var claims = await CollectAsync(client.ListWarrantyClaimsAsync(claimStatus: "submitted"));
        await client.CreateWarrantyClaimAsync(new CreateWarrantyClaimRequest
        {
            AssetId = "a-1",
            LinkedWarrantyId = "w-1",
            ClaimStatus = "submitted",
        });
        await client.UpdateWarrantyClaimAsync("c-1", new UpdateWarrantyClaimRequest());
        await client.DeleteWarrantyClaimAsync("c-1");
#pragma warning restore CS0618

        warranties.Should().HaveCount(2);
        created.Id.Should().Be("w-1");
        replaced.Data.Should().ContainSingle();
        claims.Should().ContainSingle();

        handler.Requests.Select(r => $"{r.Method} {r.RequestUri!.PathAndQuery}").Should().Equal(
            "GET /maintenance/warranties?name=Powertrain&limit=5",
            "POST /maintenance/warranties",
            "PATCH /maintenance/warranties?id=w-1",
            "DELETE /maintenance/warranties?id=w-1",
            "POST /maintenance/warranties/assets/replace?warrantyId=w-1",
            "GET /maintenance/warranty-claims?claimStatus=submitted",
            "POST /maintenance/warranty-claims",
            "PATCH /maintenance/warranty-claims?id=c-1",
            "DELETE /maintenance/warranty-claims?id=c-1");
        handler.Requests.Should().OnlyContain(
            r => !r.RequestUri!.AbsolutePath.StartsWith("/preview/"),
            "Samsara removed the /preview/maintenance/* operations and those paths now return 404");
    }

    [Fact]
    public void PreviewWarrantyMembers_AreObsoleteAndNameTheirReplacement()
    {
        var names = new[]
        {
            nameof(IPreviewApisClient.ListWarrantiesAsync),
            nameof(IPreviewApisClient.CreateWarrantyAsync),
            nameof(IPreviewApisClient.UpdateWarrantyAsync),
            nameof(IPreviewApisClient.DeleteWarrantyAsync),
            nameof(IPreviewApisClient.ReplaceWarrantyAssetAssignmentsAsync),
            nameof(IPreviewApisClient.ListWarrantyClaimsAsync),
            nameof(IPreviewApisClient.CreateWarrantyClaimAsync),
            nameof(IPreviewApisClient.UpdateWarrantyClaimAsync),
            nameof(IPreviewApisClient.DeleteWarrantyClaimAsync),
        };

        foreach (var name in names)
        {
            var member = typeof(IPreviewApisClient).GetMethod(name)!;
            var obsolete = member.GetCustomAttribute<ObsoleteAttribute>();
            obsolete.Should().NotBeNull($"{name} forwards to the graduated operation and should steer callers there");
            obsolete!.IsError.Should().BeFalse($"{name} must keep compiling for existing callers");
            obsolete.Message.Should().Contain($"IMaintenanceClient.{name}");
            typeof(IMaintenanceClient).GetMethod(name).Should().NotBeNull($"the replacement for {name} must exist");
        }
    }

    private static object ResponseFor(HttpRequestMessage request)
    {
        var path = request.RequestUri!.AbsolutePath;
        if (path.EndsWith("/assets/replace", StringComparison.Ordinal)) return ReplaceEnvelope();
        if (path.EndsWith("/warranty-claims", StringComparison.Ordinal))
            return request.Method == HttpMethod.Get ? ClaimPage() : ClaimEnvelope();
        return request.Method == HttpMethod.Get ? WarrantyPage() : WarrantyEnvelope();
    }

    private static HttpResponseMessage JsonResponse(object body)
        => new(HttpStatusCode.OK)
        {
            Content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(body),
                System.Text.Encoding.UTF8,
                "application/json"),
        };

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
