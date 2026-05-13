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

    public IAsyncEnumerable<MaintenanceDvir> GetDvirsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceDvir>(QueryBuilder.WithTimeRange("fleet/dvirs/stream", startTime, endTime), cancellationToken: cancellationToken);

    public Task<MaintenanceDvir> GetDvirByIdAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MaintenanceDvir>($"fleet/dvirs/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<MaintenanceDvir> CreateDvirAsync(CreateDvirRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MaintenanceDvir>("fleet/dvirs", request, cancellationToken);

    public Task<MaintenanceDvir> UpdateDvirAsync(string id, UpdateDvirRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<MaintenanceDvir>($"fleet/dvirs/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<DefectRecord> GetDefectsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DefectRecord>(QueryBuilder.WithTimeRange("fleet/defects/stream", startTime, endTime), cancellationToken: cancellationToken);

    public Task<DefectRecord> GetDefectAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DefectRecord>($"fleet/defects/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<DefectRecord> UpdateDefectAsync(string id, UpdateDefectRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DefectRecord>($"fleet/defects/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<DefectType> ListDefectTypesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DefectType>("fleet/defect-types", cancellationToken: cancellationToken);
}
