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
}
