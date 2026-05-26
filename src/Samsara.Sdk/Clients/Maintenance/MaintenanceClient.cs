namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;

internal sealed class MaintenanceClient : SamsaraServiceClientBase, IMaintenanceClient
{
    public MaintenanceClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<MaintenanceDvir> GetDvirsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceDvir>(QueryBuilder.WithTimeRange("dvirs/stream", startTime, endTime), cancellationToken: cancellationToken);

    public Task<MaintenanceDvir> GetDvirByIdAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MaintenanceDvir>($"dvirs/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<MaintenanceDvir> CreateDvirAsync(CreateDvirRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MaintenanceDvir>("fleet/dvirs", request, cancellationToken);

    public Task<MaintenanceDvir> UpdateDvirAsync(string id, UpdateDvirRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<MaintenanceDvir>($"fleet/dvirs/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<DefectRecord> GetDefectsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DefectRecord>(QueryBuilder.WithTimeRange("defects/stream", startTime, endTime), cancellationToken: cancellationToken);

    public Task<DefectRecord> GetDefectAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DefectRecord>($"defects/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<DefectRecord> UpdateDefectAsync(string id, UpdateDefectRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DefectRecord>($"fleet/defects/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<DefectType> ListDefectTypesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DefectType>("defect-types", cancellationToken: cancellationToken);

    /// <summary>Legacy v1 fleet maintenance list (<c>GET /v1/fleet/maintenance/list</c>).</summary>
    public IAsyncEnumerable<object> V1ListMaintenanceAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("v1/fleet/maintenance/list", cancellationToken: cancellationToken);

    /// <summary>List maintenance vendors (beta).</summary>
    public IAsyncEnumerable<object> ListVendorsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("fleet/maintenance/vendors", cancellationToken: cancellationToken);

    /// <summary>List maintenance vendor categories (beta).</summary>
    public IAsyncEnumerable<object> ListVendorCategoriesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("fleet/maintenance/vendor-categories", cancellationToken: cancellationToken);
}
