namespace Samsara.Sdk.Tests;

using System.Net;
using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the maintenance operations Samsara added in spec content
/// <c>2445b573294b</c>: vendor writes, vendor groups, technician shifts and maintenance sites,
/// plus the new <see cref="MaintenanceVendor"/> members and list filters.
/// <para>
/// They pin what the checkers cannot: that identifiers the spec puts in the <b>query string</b>
/// go there and not in the body, that request bodies serialize under the spec's names, and that
/// each response binds to the typed record for the schema the endpoint really returns.
/// </para>
/// </summary>
// SAMSARA001 is the [Experimental] diagnostic on beta-tagged operations; these tests opt in.
#pragma warning disable SAMSARA001
public sealed class MaintenanceSpecDriftContractTests
{
    // Opaque placeholders: the SDK passes email members through as plain strings.
    private const string PatEmail = "pat-email";
    private const string ShopEmail = "shop-email";
    private const string LeeEmail = "lee-email";

    // ── GET /fleet/maintenance/vendors (new filters and members) ────────────

    [Fact]
    public async Task ListVendorsAsync_SendsTheNewFiltersAndBindsResolvedSettings()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "ven-1",
                    categoryIds = new[] { "cat-1" },
                    name = "Acme Fleet Repair",
                    status = "active",
                    vendorGroupId = "grp-1",
                    isMobile = true,
                    defaultLaborRatePerHour = new { amount = "95.00", currency = "usd" },
                    assetAttributeSelections = new[] { new { attributeId = "attr-1", values = new[] { new { id = "val-1" } } } },
                    resolvedSettings = new
                    {
                        isActive = true,
                        isMobile = true,
                        isMobileSource = "vendorLocation",
                        isPreferred = false,
                        isPreferredSource = "system",
                        defaultLaborRatePerHour = new { amount = "95.00", currency = "usd" },
                        defaultLaborRatePerHourSource = "vendorGroup",
                        assetAttributeSelections = Array.Empty<object>(),
                        assetAttributeSelectionsSource = "system",
                    },
                },
            },
            pagination = new { endCursor = "", hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var vendors = await CollectAsync(client.ListVendorsAsync(
            ids: ["ven-1"], externalIds: ["erp:42"], includeResolvedSettings: true));

        var query = handler.LastRequest.RequestUri!.Query;
        query.Should().Contain("ids=ven-1")
            .And.Contain("externalIds=erp%3A42")
            .And.Contain("includeResolvedSettings=true");

        var vendor = vendors.Should().ContainSingle().Subject;
        vendor.Name.Should().Be("Acme Fleet Repair");
        vendor.Status.Should().Be("active");
        vendor.VendorGroupId.Should().Be("grp-1");
        vendor.IsMobile.Should().BeTrue();
        vendor.DefaultLaborRatePerHour!.Amount.Should().Be("95.00");
        vendor.AssetAttributeSelections![0].Values![0].Id.Should().Be("val-1");
        vendor.ResolvedSettings!.IsActive.Should().BeTrue();
        vendor.ResolvedSettings.DefaultLaborRatePerHourSource.Should().Be("vendorGroup");
        vendor.ResolvedSettings.AssetAttributeSelections.Should().BeEmpty();
    }

    [Fact]
    public async Task ListVendorsAsync_WithoutTheNewFilters_SendsNoExtraParameters()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(EmptyPage());
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        await CollectAsync(client.ListVendorsAsync());

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/fleet/maintenance/vendors");
    }

    // ── /fleet/maintenance/vendors writes ───────────────────────────────────

    [Fact]
    public async Task CreateVendorAsync_PostsTheBodyAndBindsTheWriteShape()
    {
        var resp = new
        {
            data = new
            {
                id = "ven-2",
                name = "Acme",
                contacts = new[] { new { name = "Pat", email = PatEmail, phoneNumber = "555-0100" } },
                externalIds = new[] { new { key = "erp", value = "42" } },
                placeId = new { id = "place-1" },
                emailAddresses = new[] { ShopEmail },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var vendor = await client.CreateVendorAsync(
            new CreateMaintenanceVendorRequest
            {
                Name = "Acme",
                Contacts = [new MaintenanceVendorContactInput { Name = "Pat", Email = PatEmail, PhoneNumber = "555-0100" }],
                DefaultLaborRatePerHour = new MaintenanceMoneyInput { Amount = "95.00", Currency = "usd" },
                AssetAttributeSelections =
                [
                    new VendorAssetAttributeSelectionInput
                    {
                        AttributeId = "attr-1",
                        Values = [new MaintenanceEntityRefInput { Id = "val-1" }],
                    },
                ],
            },
            includeExternalIds: true);

        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/fleet/maintenance/vendors?includeExternalIds=true");
        using (var body = JsonDocument.Parse(handler.LastRequestBody!))
        {
            body.RootElement.GetProperty("name").GetString().Should().Be("Acme");
            body.RootElement.GetProperty("contacts")[0].GetProperty("email").GetString().Should().Be(PatEmail);
            body.RootElement.GetProperty("defaultLaborRatePerHour").GetProperty("currency").GetString().Should().Be("usd");
            body.RootElement.GetProperty("assetAttributeSelections")[0].GetProperty("values")[0].GetProperty("id").GetString().Should().Be("val-1");
            body.RootElement.TryGetProperty("status", out _).Should().BeFalse("unset optional members must be omitted");
        }

        vendor.Id.Should().Be("ven-2");
        vendor.Contacts![0].Email.Should().Be(PatEmail);
        vendor.ExternalIds![0].Key.Should().Be("erp");
        vendor.PlaceId!.Id.Should().Be("place-1");
        vendor.EmailAddresses.Should().Equal(ShopEmail);
    }

    [Fact]
    public async Task UpdateAndDeleteVendor_CarryTheIdInTheQueryString()
    {
        var handler = new MockHttpMessageHandler((request, _) => Task.FromResult(
            request.Method == HttpMethod.Delete
                ? new HttpResponseMessage(HttpStatusCode.NoContent)
                : Json(new { data = new { id = "ven-2", status = "inactive" } })));
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var updated = await client.UpdateVendorAsync("ven-2", new UpdateMaintenanceVendorRequest { Status = "inactive" });
        await client.DeleteVendorAsync("ven-2");

        updated.Status.Should().Be("inactive");
        handler.Requests.Select(r => $"{r.Method} {r.RequestUri!.PathAndQuery}").Should().Equal(
            "PATCH /fleet/maintenance/vendors?id=ven-2",
            "DELETE /fleet/maintenance/vendors?id=ven-2");
        handler.RequestBodies[0].Should().Be("{\"status\":\"inactive\"}");
    }

    // ── /fleet/maintenance/vendor-groups ────────────────────────────────────

    [Fact]
    public async Task VendorGroups_RoundTripThroughTheirCrudOperations()
    {
        var group = new
        {
            id = "grp-1",
            name = "Acme Corp",
            status = "active",
            isPreferred = true,
            externalIds = new[] { new { key = "erp", value = "g-7" } },
            primaryCorporateContact = new { name = "Lee", email = LeeEmail },
            createdAtTime = "2026-08-01T00:00:00Z",
        };
        var handler = new MockHttpMessageHandler((request, _) => Task.FromResult(request.Method.Method switch
        {
            "GET" => Json(new { data = new[] { group }, pagination = new { endCursor = "", hasNextPage = false } }),
            "DELETE" => new HttpResponseMessage(HttpStatusCode.NoContent),
            _ => Json(new { data = group }),
        }));
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var listed = await CollectAsync(client.ListVendorGroupsAsync(ids: ["grp-1", "grp-2"], includeExternalIds: true, limit: 25));
        var created = await client.CreateVendorGroupAsync(new CreateMaintenanceVendorGroupRequest
        {
            Name = "Acme Corp",
            PrimaryCorporateContact = new MaintenanceVendorGroupContactInput { Name = "Lee" },
        });
        await client.UpdateVendorGroupAsync("grp-1", new UpdateMaintenanceVendorGroupRequest { IsPreferred = true }, includeExternalIds: true);
        await client.DeleteVendorGroupAsync("grp-1");

        listed.Should().ContainSingle();
        listed[0].PrimaryCorporateContact!.Email.Should().Be(LeeEmail);
        listed[0].ExternalIds![0].Value.Should().Be("g-7");
        created.IsPreferred.Should().BeTrue();
        handler.Requests.Select(r => $"{r.Method} {r.RequestUri!.PathAndQuery}").Should().Equal(
            "GET /fleet/maintenance/vendor-groups?ids=grp-1%2Cgrp-2&includeExternalIds=true&limit=25",
            "POST /fleet/maintenance/vendor-groups",
            "PATCH /fleet/maintenance/vendor-groups?id=grp-1&includeExternalIds=true",
            "DELETE /fleet/maintenance/vendor-groups?id=grp-1");
        handler.RequestBodies[1].Should().Contain("\"primaryCorporateContact\":{\"name\":\"Lee\"}");
    }

    // ── /maintenance/technician-shifts ──────────────────────────────────────

    [Fact]
    public async Task ListTechnicianShiftsAsync_ExplodesTheIdFiltersAndBindsTypedTimes()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "0b7e4d1c-5f0a-4b52-9c1e-3a8f2b6d9e01",
                    userId = "usr-1",
                    status = "completed",
                    clockInAtTime = "2026-08-17T08:00:00Z",
                    clockInSource = "mobile",
                    clockOutAtTime = "2026-08-17T16:30:00Z",
                    clockOutSource = "api",
                    externalTechnicianIds = new Dictionary<string, string> { ["hr"] = "T-9" },
                    version = 3,
                    createdAtTime = "2026-08-17T08:00:01Z",
                    updatedAtTime = "2026-08-17T16:30:01Z",
                },
            },
            pagination = new { endCursor = "", hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var shifts = await CollectAsync(client.ListTechnicianShiftsAsync(
            ids: ["s-1", "s-2"],
            userIds: ["usr-1"],
            startTime: DateTimeOffset.Parse("2026-08-17T00:00:00Z")));

        var query = handler.LastRequest.RequestUri!.Query;
        query.Should().Contain("ids=s-1&ids=s-2", "the spec declares ids as style=form, explode=true");
        query.Should().Contain("userIds=usr-1").And.Contain("startTime=");
        query.Should().NotContain("%2C", "exploded arrays repeat the key rather than comma-joining");

        var shift = shifts.Should().ContainSingle().Subject;
        shift.Status.Should().Be("completed");
        shift.ClockInAtTime.Should().Be(DateTimeOffset.Parse("2026-08-17T08:00:00Z"));
        shift.ClockOutAtTime.Should().Be(DateTimeOffset.Parse("2026-08-17T16:30:00Z"));
        shift.Version.Should().Be(3);
        shift.ExternalTechnicianIds!["hr"].Should().Be("T-9");
    }

    [Fact]
    public async Task CreateAndUpdateTechnicianShift_SendTheSpecBodies()
    {
        var shift = new { data = new { id = "s-1", userId = "usr-1", status = "inProgress", version = 4, clockInAtTime = "2026-08-17T08:00:00Z" } };
        var handler = new MockHttpMessageHandler((_, _) => Task.FromResult(Json(shift)));
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var created = await client.CreateTechnicianShiftAsync(new CreateTechnicianShiftRequest
        {
            UserId = "usr-1",
            ClockInAtTime = DateTimeOffset.Parse("2026-08-17T08:00:00Z"),
        });
        await client.UpdateTechnicianShiftAsync("s-1", new UpdateTechnicianShiftRequest
        {
            Version = 4,
            ClockOutAtTime = DateTimeOffset.Parse("2026-08-17T16:30:00Z"),
        });

        created.Status.Should().Be("inProgress");
        handler.Requests.Select(r => $"{r.Method} {r.RequestUri!.PathAndQuery}").Should().Equal(
            "POST /maintenance/technician-shifts",
            "PATCH /maintenance/technician-shifts?id=s-1");

        using var createBody = JsonDocument.Parse(handler.RequestBodies[0]!);
        createBody.RootElement.GetProperty("userId").GetString().Should().Be("usr-1");
        createBody.RootElement.GetProperty("clockInAtTime").GetDateTimeOffset()
            .Should().Be(DateTimeOffset.Parse("2026-08-17T08:00:00Z"));
        createBody.RootElement.TryGetProperty("clockOutAtTime", out _).Should().BeFalse();

        using var patchBody = JsonDocument.Parse(handler.RequestBodies[1]!);
        patchBody.RootElement.GetProperty("version").GetInt64().Should().Be(4, "the spec requires echoing the revision for optimistic concurrency");
        patchBody.RootElement.TryGetProperty("id", out _).Should().BeFalse("the shift id travels in the query string");
    }

    // ── /maintenance/sites ──────────────────────────────────────────────────

    [Fact]
    public async Task MaintenanceSites_ListCreateAndUpdate()
    {
        var site = new
        {
            id = "site-1",
            name = "North Yard",
            siteCode = "NYD",
            siteType = "YardOnsite",
            isArchived = false,
            customAddress = new { formattedAddress = "1 Main St", latitude = 37.5, longitude = -122.1 },
            places = Array.Empty<object>(),
            externalIds = new[] { new { key = "erp", value = "S-1" } },
        };
        var handler = new MockHttpMessageHandler((request, _) => Task.FromResult(request.Method.Method switch
        {
            "GET" => Json(new { data = new[] { site }, pagination = new { endCursor = "", hasNextPage = false } }),
            "POST" => Json(new { data = site }, HttpStatusCode.Created),
            _ => Json(new { data = site }),
        }));
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var sites = await CollectAsync(client.ListSitesAsync(isArchived: false, placeIds: "place-1"));
        var created = await client.CreateSiteAsync(new CreateMaintenanceSiteRequest
        {
            Name = "North Yard",
            SiteCode = "NYD",
            SiteType = "YardOnsite",
            CustomAddress = new MaintenanceSiteCustomAddressInput { FormattedAddress = "1 Main St", Latitude = 37.5, Longitude = -122.1 },
        });
        await client.UpdateSiteAsync("site-1", new UpdateMaintenanceSiteRequest { Description = "Overflow" });

        sites.Should().ContainSingle();
        sites[0].CustomAddress!.Latitude.Should().Be(37.5);
        sites[0].ExternalIds![0].Value.Should().Be("S-1");
        created.SiteCode.Should().Be("NYD");
        handler.Requests.Select(r => $"{r.Method} {r.RequestUri!.PathAndQuery}").Should().Equal(
            "GET /maintenance/sites?placeIds=place-1&isArchived=false",
            "POST /maintenance/sites",
            "PATCH /maintenance/sites?id=site-1");
        handler.RequestBodies[1].Should().Contain("\"siteType\":\"YardOnsite\"").And.Contain("\"formattedAddress\":\"1 Main St\"");
        handler.RequestBodies[2].Should().Be("{\"description\":\"Overflow\"}");
    }

    private static object EmptyPage() => new { data = Array.Empty<object>(), pagination = new { endCursor = "", hasNextPage = false } };

    private static HttpResponseMessage Json(object body, HttpStatusCode status = HttpStatusCode.OK)
        => new(status)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), System.Text.Encoding.UTF8, "application/json"),
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
#pragma warning restore SAMSARA001
