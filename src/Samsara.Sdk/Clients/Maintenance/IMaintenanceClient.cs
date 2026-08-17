namespace Samsara.Sdk.Clients;

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

    /// <summary>Legacy v1 fleet maintenance list.</summary>
    IAsyncEnumerable<object> V1ListMaintenanceAsync(CancellationToken cancellationToken = default);

    /// <summary>List maintenance vendors (beta).</summary>
    IAsyncEnumerable<object> ListVendorsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>List maintenance vendor categories (beta).</summary>
    IAsyncEnumerable<object> ListVendorCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>List preventive maintenance schedules (<c>GET /maintenance/preventive/schedules</c>) — beta.</summary>
    IAsyncEnumerable<object> ListPreventiveMaintenanceSchedulesAsync(CancellationToken cancellationToken = default);

    /// <summary>List upcoming preventive maintenance (<c>GET /maintenance/preventive/upcoming</c>) — beta.</summary>
    IAsyncEnumerable<object> ListUpcomingPreventiveMaintenanceAsync(CancellationToken cancellationToken = default);
}
