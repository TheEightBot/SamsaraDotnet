namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;

internal sealed class MaintenanceClient : SamsaraServiceClientBase, IMaintenanceClient
{
    public MaintenanceClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<MaintenanceDvir> ListDvirsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceDvir>(QueryBuilder.WithTimeRange("fleet/maintenance/dvirs", startTime, endTime), cancellationToken: cancellationToken);

    public Task<MaintenanceDvir> GetDvirAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MaintenanceDvir>($"fleet/maintenance/dvirs/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<DiagnosticTroubleCode> ListDtcsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DiagnosticTroubleCode>(QueryBuilder.WithTimeRange("fleet/vehicles/diagnostics", startTime, endTime), cancellationToken: cancellationToken);
}
