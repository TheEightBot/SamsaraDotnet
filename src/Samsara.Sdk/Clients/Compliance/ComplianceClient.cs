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

    public Task<HosClocks> GetHosClocksAsync(string driverId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<HosClocks>($"fleet/drivers/{Uri.EscapeDataString(driverId)}/hos/clocks", cancellationToken);

    public IAsyncEnumerable<DvirEntry> ListDvirsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DvirEntry>(QueryBuilder.WithTimeRange("fleet/dvirs", startTime, endTime), cancellationToken: cancellationToken);

    public Task<DvirEntry> GetDvirAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DvirEntry>($"fleet/dvirs/{Uri.EscapeDataString(id)}", cancellationToken);
}
