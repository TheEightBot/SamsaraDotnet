namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class ComplianceClient : SamsaraServiceClientBase, IComplianceClient
{
    public ComplianceClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<HosLog> ListHosLogsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HosLog>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/hos/logs", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds))),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<HosViolation> ListHosViolationsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        IReadOnlyList<string>? types = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HosViolation>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("fleet/hos/violations", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("types", types is null ? null : string.Join(",", types))),
            cancellationToken: cancellationToken);

    public Task<IReadOnlyList<HosClocksForDriver>> GetHosClocksAsync(
        IReadOnlyList<string> driverIds,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<HosClocksForDriver>>(
            QueryBuilder.WithParams("fleet/hos/clocks",
                ("driverIds", string.Join(",", driverIds)),
                ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
                ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    public IAsyncEnumerable<HosDailyLog> ListHosDailyLogsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? startDate = null,
        string? endDate = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        string? expand = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HosDailyLog>(
            QueryBuilder.WithParams(
                "fleet/hos/daily-logs",
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("startDate", startDate ?? startTime?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                ("endDate", endDate ?? endTime?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("driverActivationStatus", driverActivationStatus),
                ("expand", expand)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<HosEldEvent> ListHosEldEventsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        string? tagIds = null,
        string? parentTagIds = null,
        string? driverActivationStatus = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<HosEldEvent>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("beta/fleet/hos/drivers/eld-events", startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("driverActivationStatus", driverActivationStatus)),
            cancellationToken: cancellationToken);

    /// <summary>HOS authentication logs (v1 legacy, <c>GET /v1/fleet/hos_authentication_logs</c>).</summary>
    /// <remarks>
    /// The v1 body is an <c>{ authenticationLogs: [...] }</c> object (spec
    /// <c>V1HosAuthenticationLogsResponse</c>) — it has neither a <c>data</c> array nor a
    /// <c>pagination</c> block, so it must NOT be paginated. This mirrors
    /// <c>TripsClient.ListAsync</c>: fetch the wrapper, return its array.
    /// </remarks>
    public async Task<IReadOnlyList<V1HosAuthenticationLog>> V1ListHosAuthenticationLogsAsync(
        long driverId,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "v1/fleet/hos_authentication_logs",
            ("driverId", driverId.ToString(CultureInfo.InvariantCulture)),
            ("startMs", startTime?.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture)),
            ("endMs", endTime?.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture)));

        var response = await HttpClient.GetAsync<V1HosAuthenticationLogsResponse>(path, cancellationToken)
            .ConfigureAwait(false);
        return response.AuthenticationLogs ?? [];
    }

    /// <summary>Set a driver's current duty status (v1 legacy,
    /// <c>POST /v1/fleet/drivers/{driverId}/hos/duty_status</c>).</summary>
    public Task V1SetCurrentDutyStatusAsync(string driverId, V1SetDutyStatusRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync($"v1/fleet/drivers/{Uri.EscapeDataString(driverId)}/hos/duty_status", request, cancellationToken);

    /// <summary>
    /// Update shipping-doc metadata on HOS daily logs (beta, <c>PATCH /hos/daily-logs/log-meta-data</c>).
    /// Both <paramref name="driverID"/> and <paramref name="hosDate"/> are required query parameters.
    /// </summary>
    public Task<HosDailyLogMetaData> UpdateShippingDocsAsync(
        string driverID,
        string hosDate,
        UpdateShippingDocsRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<HosDailyLogMetaData>(
            QueryBuilder.WithParams("hos/daily-logs/log-meta-data",
                ("driverID", driverID),
                ("hosDate", hosDate)),
            request,
            cancellationToken);
}
