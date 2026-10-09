namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Models.Preview;

/// <summary>
/// Client for Samsara maintenance (DVIRs, defects, diagnostics, vendors, warranties,
/// technician shifts and maintenance sites).
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

    /// <summary>List maintenance vendors (<c>GET /fleet/maintenance/vendors</c>) — beta.</summary>
    /// <param name="ids">Optional vendor IDs to filter by.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each vendor.</param>
    /// <param name="externalIds">Optional external IDs to filter by.</param>
    /// <param name="includeResolvedSettings">
    /// Include <see cref="MaintenanceVendor.ResolvedSettings"/>, the effective settings after
    /// vendor-group and system defaults are applied.
    /// </param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<MaintenanceVendor> ListVendorsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        IReadOnlyList<string>? externalIds = null,
        bool? includeResolvedSettings = null,
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

    // ---------------------------------------------------------------- Vendors

    /// <summary>
    /// Create a maintenance vendor (<c>POST /fleet/maintenance/vendors</c>,
    /// <c>createVendor</c>) — beta.
    /// </summary>
    /// <param name="request">The vendor to create.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in the response.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<MaintenanceVendorDetails> CreateVendorAsync(
        CreateMaintenanceVendorRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a maintenance vendor (<c>PATCH /fleet/maintenance/vendors</c>,
    /// <c>updateVendor</c>) — beta. Only the fields set on <paramref name="request"/> change.
    /// </summary>
    /// <param name="id">ID of the vendor (query parameter). Required by the spec.</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in the response.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<MaintenanceVendorDetails> UpdateVendorAsync(
        string id,
        UpdateMaintenanceVendorRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a maintenance vendor (<c>DELETE /fleet/maintenance/vendors</c>,
    /// <c>deleteVendor</c>) — beta. Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">ID of the vendor (query parameter). Required by the spec.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeleteVendorAsync(string id, CancellationToken cancellationToken = default);

    // ---------------------------------------------------------- Vendor groups

    /// <summary>
    /// List vendor groups (<c>GET /fleet/maintenance/vendor-groups</c>,
    /// <c>listVendorGroups</c>) — beta. Pagination is handled transparently.
    /// </summary>
    /// <param name="ids">Optional vendor group IDs to filter by.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each group.</param>
    /// <param name="limit">Page size.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<MaintenanceVendorGroup> ListVendorGroupsAsync(
        IReadOnlyList<string>? ids = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a vendor group (<c>POST /fleet/maintenance/vendor-groups</c>,
    /// <c>createVendorGroup</c>) — beta.
    /// </summary>
    /// <param name="request">The vendor group to create.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in the response.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<MaintenanceVendorGroup> CreateVendorGroupAsync(
        CreateMaintenanceVendorGroupRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a vendor group (<c>PATCH /fleet/maintenance/vendor-groups</c>,
    /// <c>updateVendorGroup</c>) — beta.
    /// </summary>
    /// <param name="id">ID of the vendor group (query parameter). Required by the spec.</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in the response.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<MaintenanceVendorGroup> UpdateVendorGroupAsync(
        string id,
        UpdateMaintenanceVendorGroupRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a vendor group (<c>DELETE /fleet/maintenance/vendor-groups</c>,
    /// <c>deleteVendorGroup</c>) — beta. Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">ID of the vendor group (query parameter). Required by the spec.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeleteVendorGroupAsync(string id, CancellationToken cancellationToken = default);

    // ----------------------------------------------------- Technician shifts

    /// <summary>
    /// List technician shifts (<c>GET /maintenance/technician-shifts</c>,
    /// <c>listTechnicianShifts</c>) — beta. Pagination is handled transparently.
    /// </summary>
    /// <param name="ids">Optional shift IDs to filter by.</param>
    /// <param name="userIds">Optional user IDs to filter by.</param>
    /// <param name="externalTechnicianIds">Optional external technician IDs to filter by.</param>
    /// <param name="startTime">Optional RFC 3339 lower bound.</param>
    /// <param name="endTime">Optional RFC 3339 upper bound.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each shift.</param>
    /// <param name="limit">Page size.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    /// <remarks>
    /// The spec declares the three ID filters <c>style=form, explode=true</c>, so each value is
    /// sent as its own repeated query parameter (<c>ids=a&amp;ids=b</c>).
    /// </remarks>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<TechnicianShift> ListTechnicianShiftsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? userIds = null,
        IReadOnlyList<string>? externalTechnicianIds = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a technician shift (<c>POST /maintenance/technician-shifts</c>,
    /// <c>createTechnicianShift</c>) — beta.
    /// </summary>
    /// <param name="request">The shift to create.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in the response.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<TechnicianShift> CreateTechnicianShiftAsync(
        CreateTechnicianShiftRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a technician shift (<c>PATCH /maintenance/technician-shifts</c>,
    /// <c>patchTechnicianShift</c>) — beta. Echo the shift's current
    /// <see cref="TechnicianShift.Version"/>; a stale version returns <c>409 Conflict</c>.
    /// </summary>
    /// <param name="id">ID of the shift (query parameter). Required by the spec.</param>
    /// <param name="request">The fields to change, plus the expected version.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in the response.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<TechnicianShift> UpdateTechnicianShiftAsync(
        string id,
        UpdateTechnicianShiftRequest request,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    // ------------------------------------------------------------ Warranties
    // Graduated out of /preview: the IPreviewApisClient members are now [Obsolete] forwarders.

    /// <summary>
    /// List warranties (<c>GET /maintenance/warranties</c>, <c>listWarranties</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="warrantyIds">Optional comma-separated list of warranty IDs.</param>
    /// <param name="name">Optional comma-separated list of names.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each response object.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<Warranty> ListWarrantiesAsync(
        string? warrantyIds = null,
        string? name = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a warranty (<c>POST /maintenance/warranties</c>, <c>createWarranty</c>).
    /// Responds <c>201 Created</c>.
    /// </summary>
    Task<Warranty> CreateWarrantyAsync(CreateWarrantyRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update a warranty (<c>PATCH /maintenance/warranties</c>, <c>updateWarranty</c>).</summary>
    /// <param name="id">Unique identifier for the warranty. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<Warranty> UpdateWarrantyAsync(string id, UpdateWarrantyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a warranty (<c>DELETE /maintenance/warranties</c>, <c>deleteWarranty</c>).
    /// Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">Unique identifier for the warranty. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task DeleteWarrantyAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List the assets covered by a warranty (<c>GET /maintenance/warranties/assets</c>,
    /// <c>listWarrantyAssetAssignments</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="warrantyId">ID of the warranty. Required by the spec (query param).</param>
    /// <param name="limit">Page size.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<WarrantyAssetAssignmentListItem> ListWarrantyAssetAssignmentsAsync(
        string warrantyId,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Replace a warranty's asset assignments (<c>POST /maintenance/warranties/assets/replace</c>,
    /// <c>replaceWarrantyAssetAssignments</c>). The supplied list becomes the warranty's entire
    /// asset set.
    /// </summary>
    /// <param name="request">The full desired asset set.</param>
    /// <param name="warrantyId">ID of the warranty whose asset set to replace (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<WarrantyAssetAssignmentReplaceResult> ReplaceWarrantyAssetAssignmentsAsync(
        ReplaceWarrantyAssetAssignmentsRequest request,
        string? warrantyId = null,
        CancellationToken cancellationToken = default);

    // ------------------------------------------------------- Warranty claims

    /// <summary>
    /// List warranty claims (<c>GET /maintenance/warranty-claims</c>, <c>listWarrantyClaims</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="warrantyClaimIds">Optional comma-separated list of claim IDs.</param>
    /// <param name="assetIds">Optional comma-separated list of asset IDs.</param>
    /// <param name="claimStatus">Optional comma-separated list of claim statuses.</param>
    /// <param name="warrantyIds">Optional comma-separated list of warranty IDs.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each response object.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<WarrantyClaim> ListWarrantyClaimsAsync(
        string? warrantyClaimIds = null,
        string? assetIds = null,
        string? claimStatus = null,
        string? warrantyIds = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a warranty claim (<c>POST /maintenance/warranty-claims</c>,
    /// <c>createWarrantyClaim</c>). Responds <c>201 Created</c>.
    /// </summary>
    Task<WarrantyClaim> CreateWarrantyClaimAsync(
        CreateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a warranty claim (<c>PATCH /maintenance/warranty-claims</c>,
    /// <c>updateWarrantyClaim</c>).
    /// </summary>
    /// <param name="id">Unique identifier for the claim. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<WarrantyClaim> UpdateWarrantyClaimAsync(
        string id,
        UpdateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a warranty claim (<c>DELETE /maintenance/warranty-claims</c>,
    /// <c>deleteWarrantyClaim</c>). Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">Unique identifier for the claim. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task DeleteWarrantyClaimAsync(string id, CancellationToken cancellationToken = default);

    // ----------------------------------------------------- Maintenance sites

    /// <summary>
    /// List maintenance sites (<c>GET /maintenance/sites</c>, <c>listMaintenanceSites</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="ids">Optional comma-separated list of site IDs.</param>
    /// <param name="placeIds">Optional comma-separated list of linked place IDs.</param>
    /// <param name="isArchived">Filter on archived (<c>true</c>) or active (<c>false</c>) sites.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each site.</param>
    /// <param name="limit">Page size.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<MaintenanceSite> ListSitesAsync(
        string? ids = null,
        string? placeIds = null,
        bool? isArchived = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a maintenance site (<c>POST /maintenance/sites</c>, <c>createMaintenanceSite</c>).
    /// Responds <c>201 Created</c>.
    /// </summary>
    Task<MaintenanceSite> CreateSiteAsync(CreateMaintenanceSiteRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a maintenance site (<c>PATCH /maintenance/sites</c>, <c>updateMaintenanceSite</c>).
    /// </summary>
    /// <param name="id">ID of the site (query parameter). Required by the spec.</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<MaintenanceSite> UpdateSiteAsync(string id, UpdateMaintenanceSiteRequest request, CancellationToken cancellationToken = default);
}
