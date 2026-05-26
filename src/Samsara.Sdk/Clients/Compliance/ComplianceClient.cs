namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class ComplianceClient : SamsaraServiceClientBase, IComplianceClient
{
    public ComplianceClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<HosLog> ListHosLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<HosLog>(QueryBuilder.WithTimeRange("fleet/hos/logs", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<HosViolation> ListHosViolationsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<HosViolation>(QueryBuilder.WithTimeRange("fleet/hos/violations", startTime, endTime), cancellationToken: cancellationToken);

    public Task<IReadOnlyList<HosClocksForDriver>> GetHosClocksAsync(IReadOnlyList<string> driverIds, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<HosClocksForDriver>>(
            QueryBuilder.WithParams("fleet/hos/clocks", ("driverIds", string.Join(",", driverIds))), cancellationToken);

    public IAsyncEnumerable<HosDailyLog> ListHosDailyLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<HosDailyLog>(QueryBuilder.WithTimeRange("fleet/hos/daily-logs", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<HosEldEvent> ListHosEldEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<HosEldEvent>(QueryBuilder.WithTimeRange("beta/fleet/hos/drivers/eld-events", startTime, endTime), cancellationToken: cancellationToken);

    /// <summary>HOS authentication logs (v1 legacy, <c>GET /v1/fleet/hos_authentication_logs</c>).</summary>
    public IAsyncEnumerable<object> V1ListHosAuthenticationLogsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("v1/fleet/hos_authentication_logs", startTime, endTime), cancellationToken: cancellationToken);

    /// <summary>Set a driver's current duty status (v1 legacy,
    /// <c>POST /v1/fleet/drivers/{driverId}/hos/duty_status</c>).</summary>
    public Task V1SetCurrentDutyStatusAsync(string driverId, object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync($"v1/fleet/drivers/{Uri.EscapeDataString(driverId)}/hos/duty_status", request, cancellationToken);

    /// <summary>Update shipping-doc metadata on HOS daily logs (beta,
    /// <c>PATCH /hos/daily-logs/log-meta-data</c>).</summary>
    public Task<object> UpdateShippingDocsAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>("hos/daily-logs/log-meta-data", request, cancellationToken);
}
