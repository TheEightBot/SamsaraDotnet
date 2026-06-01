namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Hours-of-Service domain (Phase 3). The HOS list endpoints
/// nest per-driver objects: <see cref="HosLog"/> wraps a <c>hosLogs</c> array of
/// <see cref="HosLogEntry"/>, <see cref="HosViolation"/> wraps a <c>violations</c> array,
/// and <see cref="HosDailyLog"/> nests driver/distance/duty-status objects. Earlier SDK
/// versions modeled these as flat scalars; these tests lock in the nested binding.
/// </summary>
public sealed class HosContractTests
{
    // ── GET /fleet/hos/logs ─────────────────────────────────────────────────
    [Fact]
    public async Task ListHosLogsAsync_BindsNestedDriverAndLogEntries()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    driver = new { id = "drv-1", name = "Jane Doe" },
                    hosLogs = new object[]
                    {
                        new
                        {
                            hosStatusType = "driving",
                            logStartTime = "2024-01-01T08:00:00Z",
                            logEndTime = "2024-01-01T12:00:00Z",
                            logRecordedLocation = new { latitude = 37.7749, longitude = -122.4194 },
                            vehicle = new { id = "veh-1", name = "Truck 1" },
                        },
                        new
                        {
                            hosStatusType = "offDuty",
                            logStartTime = "2024-01-01T12:00:00Z",
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListHosLogsAsync());

        items.Should().HaveCount(1);
        var log = items[0];
        log.Driver.Should().NotBeNull();
        log.Driver!.Id.Should().Be("drv-1");
        log.HosLogs.Should().HaveCount(2);
        log.HosLogs![0].HosStatusType.Should().Be("driving");
        log.HosLogs[0].LogRecordedLocation!.Latitude.Should().Be(37.7749);
        log.HosLogs[0].Vehicle!.Id.Should().Be("veh-1");
        log.HosLogs[1].HosStatusType.Should().Be("offDuty");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/hos/logs");
    }

    // ── GET /fleet/hos/violations ───────────────────────────────────────────
    [Fact]
    public async Task ListHosViolationsAsync_BindsNestedViolationsArray()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    violations = new[]
                    {
                        new
                        {
                            type = "shiftDrivingHours",
                            description = "Exceeded 11-hour driving limit",
                            durationMs = 1800000L,
                            driver = new { id = "drv-1", name = "Jane Doe" },
                            day = new { startTime = "2024-01-01T00:00:00Z", endTime = "2024-01-02T00:00:00Z" },
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListHosViolationsAsync());

        items.Should().HaveCount(1);
        var violationGroup = items[0];
        violationGroup.Violations.Should().HaveCount(1);
        var v = violationGroup.Violations[0];
        v.Type.Should().Be("shiftDrivingHours");
        v.DurationMs.Should().Be(1800000L);
        v.Driver!.Id.Should().Be("drv-1");
        v.Day!.StartTime.Should().Be("2024-01-01T00:00:00Z");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/hos/violations");
    }

    // ── GET /fleet/hos/daily-logs (deep nested binding) ─────────────────────
    [Fact]
    public async Task ListHosDailyLogsAsync_BindsDistanceAndDutyStatusDurations()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    driver = new { id = "drv-1", name = "Jane Doe", timezone = "America/Los_Angeles" },
                    startTime = "2024-01-01T00:00:00-08:00",
                    endTime = "2024-01-02T00:00:00-08:00",
                    distanceTraveled = new { driveDistanceMeters = 320000L, yardMoveDistanceMeters = 1500L },
                    dutyStatusDurations = new { driveDurationMs = 39600000L, offDutyDurationMs = 36000000L },
                    logMetaData = new { isCertified = true, carrierName = "Acme Freight" },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListHosDailyLogsAsync());

        items.Should().HaveCount(1);
        var daily = items[0];
        daily.Driver.Id.Should().Be("drv-1");
        daily.Driver.Timezone.Should().Be("America/Los_Angeles");
        daily.DistanceTraveled!.DriveDistanceMeters.Should().Be(320000L);
        daily.DutyStatusDurations!.DriveDurationMs.Should().Be(39600000L);
        daily.LogMetaData!.IsCertified.Should().BeTrue();
        daily.LogMetaData.CarrierName.Should().Be("Acme Freight");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/hos/daily-logs");
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
