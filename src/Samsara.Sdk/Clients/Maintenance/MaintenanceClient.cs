namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
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
    /// <remarks>
    /// The v1 body is a <c>{ vehicles: [...] }</c> object (spec
    /// <c>inline_response_200_4</c>) — it has neither a <c>data</c> array nor a
    /// <c>pagination</c> block, so it must NOT be paginated. This mirrors
    /// <c>TripsClient.ListAsync</c>: fetch the wrapper, return its array.
    /// </remarks>
    public async Task<IReadOnlyList<V1VehicleMaintenance>> V1ListMaintenanceAsync(CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync<V1MaintenanceListResponse>("v1/fleet/maintenance/list", cancellationToken)
            .ConfigureAwait(false);
        return response.Vehicles ?? [];
    }

    /// <summary>List maintenance vendors (beta).</summary>
    public IAsyncEnumerable<MaintenanceVendor> ListVendorsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceVendor>(
            QueryBuilder.WithParams("fleet/maintenance/vendors",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>List maintenance vendor categories (beta).</summary>
    public IAsyncEnumerable<MaintenanceVendorCategory> ListVendorCategoriesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceVendorCategory>("fleet/maintenance/vendor-categories", cancellationToken: cancellationToken);

    /// <summary>List preventive maintenance schedules (<c>GET /maintenance/preventive/schedules</c>) — beta.</summary>
    public IAsyncEnumerable<PreventiveMaintenanceSchedule> ListPreventiveMaintenanceSchedulesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<PreventiveMaintenanceSchedule>("maintenance/preventive/schedules", cancellationToken: cancellationToken);

    /// <summary>List upcoming preventive maintenance (<c>GET /maintenance/preventive/upcoming</c>) — beta.</summary>
    public IAsyncEnumerable<UpcomingPreventiveMaintenance> ListUpcomingPreventiveMaintenanceAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<UpcomingPreventiveMaintenance>("maintenance/preventive/upcoming", cancellationToken: cancellationToken);

    /// <summary>
    /// Stream technician time entries (<c>GET /maintenance/time-entries/stream</c>) — beta.
    /// </summary>
    /// <remarks>
    /// The response is the standard <c>{ data: [...], pagination: {...} }</c> envelope, so
    /// pagination is handled transparently. <paramref name="startTime"/> is REQUIRED by the
    /// spec. The feed also emits deletion tombstones — see <see cref="MaintenanceTimeEntry"/>.
    /// </remarks>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<MaintenanceTimeEntry> GetTimeEntriesStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceTimeEntry>(
            QueryBuilder.WithParams("maintenance/time-entries/stream",
                ("startTime", startTime.ToString("O")),
                ("endTime", endTime?.ToString("O"))),
            cancellationToken: cancellationToken);

    /// <summary>
    /// Update the open upcoming preventive-maintenance instance for an asset and schedule
    /// (<c>PATCH /maintenance/preventive/upcoming</c>) — beta.
    /// </summary>
    /// <remarks>
    /// Both identifiers travel in the <b>query string</b>, not the body. The response is a
    /// <c>{ data: T }</c> envelope whose payload is a strict superset of the list item — see
    /// <see cref="UpdatedUpcomingPreventiveMaintenance"/>.
    /// </remarks>
    [Experimental("SAMSARA001")]
    public Task<UpdatedUpcomingPreventiveMaintenance> UpdateUpcomingPreventiveMaintenanceAsync(
        string assetId,
        string scheduleId,
        UpdateUpcomingPreventiveMaintenanceRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<UpdatedUpcomingPreventiveMaintenance>(
            QueryBuilder.WithParams("maintenance/preventive/upcoming",
                ("assetId", assetId),
                ("scheduleId", scheduleId)),
            request,
            cancellationToken);

    /// <summary>
    /// Resolve the open preventive-maintenance instance for an asset and schedule
    /// (<c>POST /maintenance/preventive/resolve</c>) — beta. Samsara automatically creates the
    /// next due record from the schedule's intervals.
    /// </summary>
    /// <remarks>
    /// Both identifiers travel in the <b>query string</b>, not the body. The spec declares the
    /// success payload as
    /// <c>ResolvePreventiveMaintenanceResponseObjectTypeResponseBody</c>, a bare
    /// <c>{ type: object }</c> with no properties, so the <c>data</c> member is surfaced
    /// verbatim as a <see cref="JsonElement"/> rather than being modelled or discarded.
    /// </remarks>
    [Experimental("SAMSARA001")]
    public Task<JsonElement> ResolvePreventiveMaintenanceAsync(
        string assetId,
        string scheduleId,
        ResolvePreventiveMaintenanceRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<JsonElement>(
            QueryBuilder.WithParams("maintenance/preventive/resolve",
                ("assetId", assetId),
                ("scheduleId", scheduleId)),
            request,
            cancellationToken);
}
