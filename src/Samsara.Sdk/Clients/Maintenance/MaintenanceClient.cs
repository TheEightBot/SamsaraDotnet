namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;

internal sealed class MaintenanceClient : SamsaraServiceClientBase, IMaintenanceClient
{
    public MaintenanceClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<MaintenanceDvir> GetDvirsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        IReadOnlyList<string>? safetyStatus = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceDvir>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("dvirs/stream", startTime, endTime),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("safetyStatus", safetyStatus is null ? null : string.Join(",", safetyStatus))),
            cancellationToken: cancellationToken);

    public Task<MaintenanceDvir> GetDvirByIdAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MaintenanceDvir>(
            QueryBuilder.WithParams(
                $"dvirs/{Uri.EscapeDataString(id)}",
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<V1MaintenanceDvir> CreateDvirAsync(CreateDvirRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<V1MaintenanceDvir>("fleet/dvirs", request, cancellationToken);

    public Task<V1MaintenanceDvir> UpdateDvirAsync(string id, UpdateDvirRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<V1MaintenanceDvir>($"fleet/dvirs/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<DefectRecord> GetDefectsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        bool? isResolved = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DefectRecord>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("defects/stream", startTime, endTime),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("isResolved", isResolved?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<DefectRecord> GetDefectAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DefectRecord>(
            QueryBuilder.WithParams(
                $"defects/{Uri.EscapeDataString(id)}",
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<V1DefectRecord> UpdateDefectAsync(string id, UpdateDefectRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<V1DefectRecord>($"fleet/defects/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<DefectType> ListDefectTypesAsync(
        IReadOnlyList<string>? ids = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DefectType>(
            QueryBuilder.WithParams(
                "defect-types",
                ("ids", ids is null ? null : string.Join(",", ids))),
            cancellationToken: cancellationToken);

    /// <summary>Legacy v1 fleet maintenance list (<c>GET /v1/fleet/maintenance/list</c>).</summary>
    public IAsyncEnumerable<object> V1ListMaintenanceAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("v1/fleet/maintenance/list", cancellationToken: cancellationToken);

    /// <summary>List maintenance vendors (beta).</summary>
    public IAsyncEnumerable<object> ListVendorsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams("fleet/maintenance/vendors",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>List maintenance vendor categories (beta).</summary>
    public IAsyncEnumerable<object> ListVendorCategoriesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("fleet/maintenance/vendor-categories", cancellationToken: cancellationToken);

    /// <summary>List preventive maintenance schedules (<c>GET /maintenance/preventive/schedules</c>) — beta.</summary>
    public IAsyncEnumerable<object> ListPreventiveMaintenanceSchedulesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("maintenance/preventive/schedules", cancellationToken: cancellationToken);

    /// <summary>List upcoming preventive maintenance (<c>GET /maintenance/preventive/upcoming</c>) — beta.</summary>
    public IAsyncEnumerable<object> ListUpcomingPreventiveMaintenanceAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("maintenance/preventive/upcoming", cancellationToken: cancellationToken);
}
