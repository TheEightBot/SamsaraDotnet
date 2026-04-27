namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

public sealed class ComplianceClientTests
{
    [Fact]
    public async Task ListHosDailyLogsAsync_CallsCorrectPath()
    {
        var resp = new
        {
            data = new[] { new { id = "dl-1", driverId = "d-1", date = "2026-04-01" } },
            pagination = new { endCursor = (string?)null, hasNextPage = false }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var logs = new List<Samsara.Sdk.Models.Compliance.HosDailyLog>();
        await foreach (var log in client.ListHosDailyLogsAsync())
            logs.Add(log);

        logs.Should().HaveCount(1);
        logs[0].Id.Should().Be("dl-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/hos/daily-logs");
    }

    [Fact]
    public async Task ListHosEldEventsAsync_CallsCorrectPath()
    {
        var resp = new
        {
            data = new[] { new { id = "eld-1", driverId = "d-1", eventType = "DutyStatusChange" } },
            pagination = new { endCursor = (string?)null, hasNextPage = false }
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var events = new List<Samsara.Sdk.Models.Compliance.HosEldEvent>();
        await foreach (var e in client.ListHosEldEventsAsync())
            events.Add(e);

        events.Should().HaveCount(1);
        events[0].Id.Should().Be("eld-1");
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/hos/eld-events");
    }
}
