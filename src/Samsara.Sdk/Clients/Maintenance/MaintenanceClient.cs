namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Models.Preview;

internal sealed class MaintenanceClient : SamsaraServiceClientBase, IMaintenanceClient
{
    private const string VendorsPath = "fleet/maintenance/vendors";
    private const string VendorGroupsPath = "fleet/maintenance/vendor-groups";
    private const string TechnicianShiftsPath = "maintenance/technician-shifts";
    private const string WarrantiesPath = "maintenance/warranties";
    private const string WarrantyClaimsPath = "maintenance/warranty-claims";
    private const string SitesPath = "maintenance/sites";

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
        IReadOnlyList<string>? externalIds = null,
        bool? includeResolvedSettings = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceVendor>(
            QueryBuilder.WithParams(VendorsPath,
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("externalIds", externalIds is null ? null : string.Join(",", externalIds)),
                ("includeResolvedSettings", includeResolvedSettings?.ToString().ToLowerInvariant()),
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

    // ---------------------------------------------------------------- Vendors

    [Experimental("SAMSARA001")]
    public Task<MaintenanceVendorDetails> CreateVendorAsync(
        CreateMaintenanceVendorRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MaintenanceVendorDetails>(
            QueryBuilder.WithParams(VendorsPath,
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            request,
            cancellationToken);

    [Experimental("SAMSARA001")]
    public Task<MaintenanceVendorDetails> UpdateVendorAsync(
        string id,
        UpdateMaintenanceVendorRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<MaintenanceVendorDetails>(
            QueryBuilder.WithParams(VendorsPath,
                ("id", id),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            request,
            cancellationToken);

    [Experimental("SAMSARA001")]
    public Task DeleteVendorAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(VendorsPath, ("id", id)), cancellationToken);

    // ---------------------------------------------------------- Vendor groups

    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<MaintenanceVendorGroup> ListVendorGroupsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceVendorGroup>(
            QueryBuilder.WithParams(VendorGroupsPath,
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            limit,
            cancellationToken);

    [Experimental("SAMSARA001")]
    public Task<MaintenanceVendorGroup> CreateVendorGroupAsync(
        CreateMaintenanceVendorGroupRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MaintenanceVendorGroup>(
            QueryBuilder.WithParams(VendorGroupsPath,
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            request,
            cancellationToken);

    [Experimental("SAMSARA001")]
    public Task<MaintenanceVendorGroup> UpdateVendorGroupAsync(
        string id,
        UpdateMaintenanceVendorGroupRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<MaintenanceVendorGroup>(
            QueryBuilder.WithParams(VendorGroupsPath,
                ("id", id),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            request,
            cancellationToken);

    [Experimental("SAMSARA001")]
    public Task DeleteVendorGroupAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(VendorGroupsPath, ("id", id)), cancellationToken);

    // ----------------------------------------------------- Technician shifts

    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<TechnicianShift> ListTechnicianShiftsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? userIds = null,
        IReadOnlyList<string>? externalTechnicianIds = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        var parameters = new List<(string Key, string? Value)>();
        AddExploded(parameters, "ids", ids);
        AddExploded(parameters, "userIds", userIds);
        AddExploded(parameters, "externalTechnicianIds", externalTechnicianIds);
        parameters.Add(("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()));

        return PaginateAsync<TechnicianShift>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(TechnicianShiftsPath, startTime, endTime),
                parameters.ToArray()),
            limit,
            cancellationToken);
    }

    [Experimental("SAMSARA001")]
    public Task<TechnicianShift> CreateTechnicianShiftAsync(
        CreateTechnicianShiftRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<TechnicianShift>(
            QueryBuilder.WithParams(TechnicianShiftsPath,
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            request,
            cancellationToken);

    [Experimental("SAMSARA001")]
    public Task<TechnicianShift> UpdateTechnicianShiftAsync(
        string id,
        UpdateTechnicianShiftRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<TechnicianShift>(
            QueryBuilder.WithParams(TechnicianShiftsPath,
                ("id", id),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            request,
            cancellationToken);

    // ------------------------------------------------------------ Warranties

    public IAsyncEnumerable<Warranty> ListWarrantiesAsync(
        string? warrantyIds = null,
        string? name = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Warranty>(
            QueryBuilder.WithParams(WarrantiesPath,
                ("warrantyIds", warrantyIds),
                ("name", name),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            limit,
            cancellationToken);

    public Task<Warranty> CreateWarrantyAsync(CreateWarrantyRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Warranty>(WarrantiesPath, request, cancellationToken);

    public Task<Warranty> UpdateWarrantyAsync(string id, UpdateWarrantyRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Warranty>(
            QueryBuilder.WithParams(WarrantiesPath, ("id", id)), request, cancellationToken);

    public Task DeleteWarrantyAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(WarrantiesPath, ("id", id)), cancellationToken);

    public IAsyncEnumerable<WarrantyAssetAssignmentListItem> ListWarrantyAssetAssignmentsAsync(
        string warrantyId,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<WarrantyAssetAssignmentListItem>(
            QueryBuilder.WithParams($"{WarrantiesPath}/assets", ("warrantyId", warrantyId)),
            limit,
            cancellationToken);

    public Task<WarrantyAssetAssignmentReplaceResult> ReplaceWarrantyAssetAssignmentsAsync(
        ReplaceWarrantyAssetAssignmentsRequest request,
        string? warrantyId = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<WarrantyAssetAssignmentReplaceResult>(
            QueryBuilder.WithParams($"{WarrantiesPath}/assets/replace", ("warrantyId", warrantyId)),
            request,
            cancellationToken);

    // ------------------------------------------------------- Warranty claims

    public IAsyncEnumerable<WarrantyClaim> ListWarrantyClaimsAsync(
        string? warrantyClaimIds = null,
        string? assetIds = null,
        string? claimStatus = null,
        string? warrantyIds = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<WarrantyClaim>(
            QueryBuilder.WithParams(WarrantyClaimsPath,
                ("warrantyClaimIds", warrantyClaimIds),
                ("assetIds", assetIds),
                ("claimStatus", claimStatus),
                ("warrantyIds", warrantyIds),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            limit,
            cancellationToken);

    public Task<WarrantyClaim> CreateWarrantyClaimAsync(
        CreateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<WarrantyClaim>(WarrantyClaimsPath, request, cancellationToken);

    public Task<WarrantyClaim> UpdateWarrantyClaimAsync(
        string id,
        UpdateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<WarrantyClaim>(
            QueryBuilder.WithParams(WarrantyClaimsPath, ("id", id)), request, cancellationToken);

    public Task DeleteWarrantyClaimAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(WarrantyClaimsPath, ("id", id)), cancellationToken);

    // ----------------------------------------------------- Maintenance sites

    public IAsyncEnumerable<MaintenanceSite> ListSitesAsync(
        string? ids = null,
        string? placeIds = null,
        bool? isArchived = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MaintenanceSite>(
            QueryBuilder.WithParams(SitesPath,
                ("ids", ids),
                ("placeIds", placeIds),
                ("isArchived", isArchived?.ToString().ToLowerInvariant()),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            limit,
            cancellationToken);

    public Task<MaintenanceSite> CreateSiteAsync(CreateMaintenanceSiteRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MaintenanceSite>(SitesPath, request, cancellationToken);

    public Task<MaintenanceSite> UpdateSiteAsync(string id, UpdateMaintenanceSiteRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<MaintenanceSite>(
            QueryBuilder.WithParams(SitesPath, ("id", id)), request, cancellationToken);

    private static void AddExploded(List<(string Key, string? Value)> parameters, string key, IReadOnlyList<string>? values)
    {
        if (values is null) return;
        foreach (var value in values)
        {
            parameters.Add((key, value));
        }
    }
}
