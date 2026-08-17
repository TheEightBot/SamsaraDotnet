namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Samsara.Sdk.Models.Maintenance;

/// <summary>
/// Client for Samsara maintenance (DVIRs, defects, diagnostics).
/// </summary>
public interface IMaintenanceClient
{
    /// <summary>Stream DVIRs (<c>GET /dvirs/stream</c>).</summary>
    IAsyncEnumerable<MaintenanceDvir> GetDvirsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        IReadOnlyList<string>? safetyStatus = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get a single DVIR by ID (<c>GET /dvirs/{id}</c>).</summary>
    Task<MaintenanceDvir> GetDvirByIdAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a mechanic DVIR (<c>POST /fleet/dvirs</c>). Returns the v1
    /// <c>Dvir</c> shape — <see cref="V1MaintenanceDvir"/>, not the
    /// <see cref="MaintenanceDvir"/> returned by the v2 stream/get endpoints.
    /// </summary>
    Task<V1MaintenanceDvir> CreateDvirAsync(CreateDvirRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resolve a DVIR (<c>PATCH /fleet/dvirs/{id}</c>). Returns the v1
    /// <c>Dvir</c> shape — <see cref="V1MaintenanceDvir"/>.
    /// </summary>
    Task<V1MaintenanceDvir> UpdateDvirAsync(string id, UpdateDvirRequest request, CancellationToken cancellationToken = default);

    /// <summary>Stream DVIR defects (<c>GET /defects/stream</c>).</summary>
    IAsyncEnumerable<DefectRecord> GetDefectsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        bool? isResolved = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get a single DVIR defect by ID (<c>GET /defects/{id}</c>).</summary>
    Task<DefectRecord> GetDefectAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a defect (<c>PATCH /fleet/defects/{id}</c>). Returns the v1
    /// <c>Defect</c> shape — <see cref="V1DefectRecord"/>, not the
    /// <see cref="DefectRecord"/> returned by the v2 stream/get endpoints.
    /// </summary>
    Task<V1DefectRecord> UpdateDefectAsync(string id, UpdateDefectRequest request, CancellationToken cancellationToken = default);

    /// <summary>List DVIR defect types (<c>GET /defect-types</c>).</summary>
    IAsyncEnumerable<DefectType> ListDefectTypesAsync(
        IReadOnlyList<string>? ids = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Legacy v1 fleet maintenance list. The v1 body is a <c>{ vehicles: [...] }</c>
    /// object with no pagination, so the whole list is returned at once.
    /// </summary>
    Task<IReadOnlyList<V1VehicleMaintenance>> V1ListMaintenanceAsync(CancellationToken cancellationToken = default);

    /// <summary>List maintenance vendors (beta).</summary>
    IAsyncEnumerable<MaintenanceVendor> ListVendorsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>List maintenance vendor categories (beta).</summary>
    IAsyncEnumerable<MaintenanceVendorCategory> ListVendorCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>List preventive maintenance schedules (<c>GET /maintenance/preventive/schedules</c>) — beta.</summary>
    IAsyncEnumerable<PreventiveMaintenanceSchedule> ListPreventiveMaintenanceSchedulesAsync(CancellationToken cancellationToken = default);

    /// <summary>List upcoming preventive maintenance (<c>GET /maintenance/preventive/upcoming</c>) — beta.</summary>
    IAsyncEnumerable<UpcomingPreventiveMaintenance> ListUpcomingPreventiveMaintenanceAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream technician time entries (<c>GET /maintenance/time-entries/stream</c>,
    /// <c>listTimeEntries</c>) — beta. Pagination is handled transparently.
    /// </summary>
    /// <param name="startTime">RFC 3339 lower bound on <c>updatedAtTime</c>. REQUIRED by the spec.</param>
    /// <param name="endTime">Optional RFC 3339 upper bound on <c>updatedAtTime</c>.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    /// <remarks>
    /// The feed includes deletion tombstones: a deleted entry carries only its id,
    /// <c>deletedAtTime</c> and (when known) <c>deletedByUserId</c>.
    /// </remarks>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<MaintenanceTimeEntry> GetTimeEntriesStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update the open upcoming preventive-maintenance instance for an asset and schedule
    /// (<c>PATCH /maintenance/preventive/upcoming</c>,
    /// <c>updateUpcomingPreventiveMaintenance</c>) — beta. Only the fields set on
    /// <paramref name="request"/> are changed.
    /// </summary>
    /// <param name="assetId">Samsara ID for the asset (query parameter).</param>
    /// <param name="scheduleId">ID of the preventive-maintenance schedule (query parameter).</param>
    /// <param name="request">The due-target and last-resolved values to patch.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<UpdatedUpcomingPreventiveMaintenance> UpdateUpcomingPreventiveMaintenanceAsync(
        string assetId,
        string scheduleId,
        UpdateUpcomingPreventiveMaintenanceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Resolve the open preventive-maintenance instance for an asset and schedule
    /// (<c>POST /maintenance/preventive/resolve</c>, <c>resolvePreventiveMaintenance</c>) —
    /// beta. The next due record is created automatically from the schedule's intervals.
    /// </summary>
    /// <param name="assetId">Samsara ID of the asset the instance is resolved for (query parameter).</param>
    /// <param name="scheduleId">ID of the preventive-maintenance schedule to resolve (query parameter).</param>
    /// <param name="request">Resolution time and meter readings; all members are optional.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>
    /// The response's <c>data</c> member verbatim. The spec declares it as a bare
    /// <c>{ type: object }</c> with no properties, so there is no shape to model.
    /// </returns>
    [Experimental("SAMSARA001")]
    Task<JsonElement> ResolvePreventiveMaintenanceAsync(
        string assetId,
        string scheduleId,
        ResolvePreventiveMaintenanceRequest request,
        CancellationToken cancellationToken = default);
}
