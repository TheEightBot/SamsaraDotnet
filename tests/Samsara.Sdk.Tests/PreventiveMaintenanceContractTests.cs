namespace Samsara.Sdk.Tests;

using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the three beta maintenance operations added at the end of the
/// v0.5.0 spec-parity sweep: the technician time-entry feed and the two
/// preventive-maintenance actions.
/// <para>
/// They pin the two things the checkers cannot prove: that the identifiers the spec puts
/// in the <b>query string</b> actually go there (both actions would silently act on the
/// wrong record otherwise), and that each response binds to the typed record for the
/// schema the endpoint really returns.
/// </para>
/// </summary>
// SAMSARA001 is the [Experimental] diagnostic on beta-tagged operations. It is an ERROR by
// design, so consumers must consciously opt in; these tests are that opt-in.
#pragma warning disable SAMSARA001
public sealed class PreventiveMaintenanceContractTests
{
    // ── GET /maintenance/time-entries/stream ────────────────────────────────

    [Fact]
    public async Task GetTimeEntriesStreamAsync_BindsTypedLocationsAndHourlyRate()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "te-1",
                    userId = "usr-9",
                    workOrderId = "wo-4",
                    serviceTaskId = "st-7",
                    timeEntryStatus = "completed",
                    clockInAtTime = "2026-08-17T08:00:00Z",
                    clockInSource = "mobile",
                    clockInLocation = new { latitude = 37.7749, longitude = -122.4194 },
                    clockOutAtTime = "2026-08-17T11:30:00Z",
                    clockOutSource = "dashboard",
                    clockOutMethodType = "manual",
                    clockOutLocation = new { latitude = 37.7751, longitude = -122.4200 },
                    placeId = "place-3",
                    hourlyRate = new { amount = "24.50", currency = "usd" },
                    createdAtTime = "2026-08-17T08:00:01Z",
                    updatedAtTime = "2026-08-17T11:30:02Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var entries = await CollectAsync(
            client.GetTimeEntriesStreamAsync(DateTimeOffset.Parse("2026-08-17T00:00:00Z")));

        entries.Should().HaveCount(1);
        var entry = entries[0];
        entry.Id.Should().Be("te-1");
        entry.WorkOrderId.Should().Be("wo-4");
        entry.TimeEntryStatus.Should().Be("completed");
        entry.ClockInLocation!.Latitude.Should().Be(37.7749);
        entry.ClockOutLocation!.Longitude.Should().Be(-122.4200);
        entry.HourlyRate!.Amount.Should().Be("24.50");
        entry.HourlyRate.Currency.Should().Be("usd");

        var query = handler.LastRequest.RequestUri!.PathAndQuery;
        query.Should().Contain("maintenance/time-entries/stream");
        query.Should().Contain("startTime=");
    }

    /// <summary>
    /// The feed emits deletion tombstones carrying only the id, the deletion time and
    /// the deleting user — every other member must bind as null rather than throwing.
    /// </summary>
    [Fact]
    public async Task GetTimeEntriesStreamAsync_BindsDeletionTombstones()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "te-2",
                    deletedAtTime = "2026-08-17T12:00:00Z",
                    deletedByUserId = "usr-1",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var entries = await CollectAsync(
            client.GetTimeEntriesStreamAsync(DateTimeOffset.Parse("2026-08-17T00:00:00Z")));

        var tombstone = entries.Should().ContainSingle().Subject;
        tombstone.DeletedAtTime.Should().Be("2026-08-17T12:00:00Z");
        tombstone.DeletedByUserId.Should().Be("usr-1");
        tombstone.ClockInAtTime.Should().BeNull();
        tombstone.HourlyRate.Should().BeNull();
    }

    // ── PATCH /maintenance/preventive/upcoming ──────────────────────────────

    /// <summary>
    /// The asset and schedule are QUERY parameters on this operation, not body members:
    /// putting them in the body would patch whichever instance the API picked by default.
    /// </summary>
    [Fact]
    public async Task UpdateUpcomingPreventiveMaintenanceAsync_SendsIdentifiersAsQueryParams()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { status = "due" } });
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        await client.UpdateUpcomingPreventiveMaintenanceAsync(
            "asset-1",
            "sched-2",
            new UpdateUpcomingPreventiveMaintenanceRequest { NextOdometer = 500_000 });

        handler.LastRequest.Method.Method.Should().Be("PATCH");
        var query = handler.LastRequest.RequestUri!.PathAndQuery;
        query.Should().Contain("maintenance/preventive/upcoming");
        query.Should().Contain("assetId=asset-1");
        query.Should().Contain("scheduleId=sched-2");
        handler.LastRequestBody.Should().Contain("nextOdometer");
        handler.LastRequestBody.Should().NotContain("assetId");
    }

    /// <summary>
    /// The PATCH response is a strict superset of the GET list item — it adds the three
    /// miles-denominated fields and <c>priority</c> — which is why it has its own record.
    /// </summary>
    [Fact]
    public async Task UpdateUpcomingPreventiveMaintenanceAsync_BindsTheSupersetResponse()
    {
        var resp = new
        {
            data = new
            {
                asset = new { id = "asset-1" },
                schedule = new { id = "sched-2" },
                workOrder = new { id = "wo-3" },
                status = "due",
                currentEngineHours = 4_200,
                currentOdometer = 480_000,
                currentOdometerMiles = 298,
                dueInDays = 12,
                dueInEngineHours = 300,
                dueInOdometer = 20_000,
                dueInOdometerMiles = 12,
                lastResolvedAt = "2026-05-01T00:00:00Z",
                lastResolvedAtEngineHours = 3_900,
                lastResolvedAtOdometer = 460_000,
                nextEngineHours = 4_500,
                nextOdometer = 500_000,
                nextOdometerMiles = 310,
                nextTime = "2026-09-01T00:00:00Z",
                priority = 12,
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var updated = await client.UpdateUpcomingPreventiveMaintenanceAsync(
            "asset-1", "sched-2", new UpdateUpcomingPreventiveMaintenanceRequest());

        updated.Should().BeOfType<UpdatedUpcomingPreventiveMaintenance>();
        updated.Asset!.Id.Should().Be("asset-1");
        updated.Schedule!.Id.Should().Be("sched-2");
        updated.WorkOrder!.Id.Should().Be("wo-3");
        updated.CurrentOdometerMiles.Should().Be(298);
        updated.DueInOdometerMiles.Should().Be(12);
        updated.NextOdometerMiles.Should().Be(310);
        updated.Priority.Should().Be(12);

        // The list item deliberately does NOT carry the four superset fields.
        typeof(UpcomingPreventiveMaintenance).GetProperty("Priority").Should().BeNull(
            "EntityListUpcomingPreventiveMaintenanceTypeResponseBody has no priority");
    }

    // ── POST /maintenance/preventive/resolve ────────────────────────────────

    [Fact]
    public async Task ResolvePreventiveMaintenanceAsync_SendsIdentifiersAsQueryParamsAndBodyReadings()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { } });
        var client = new MaintenanceClient(TestFactory.CreateHttpClient(handler));

        var data = await client.ResolvePreventiveMaintenanceAsync(
            "asset-1",
            "sched-2",
            new ResolvePreventiveMaintenanceRequest
            {
                ResolvedAt = "2026-08-17T10:00:00Z",
                ResolvedAtEngineHours = 4_500,
                ResolvedAtOdometer = 500_000,
            });

        handler.LastRequest.Method.Method.Should().Be("POST");
        var query = handler.LastRequest.RequestUri!.PathAndQuery;
        query.Should().Contain("maintenance/preventive/resolve");
        query.Should().Contain("assetId=asset-1");
        query.Should().Contain("scheduleId=sched-2");
        handler.LastRequestBody.Should().Contain("resolvedAtOdometer");

        // The spec declares the payload as a bare { type: object } with no properties,
        // so the data member is surfaced verbatim rather than modelled.
        data.ValueKind.Should().Be(JsonValueKind.Object);
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
#pragma warning restore SAMSARA001
