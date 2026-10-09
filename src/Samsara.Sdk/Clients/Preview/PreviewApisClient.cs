namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Models.Drivers;
using Samsara.Sdk.Models.Preview;

/// <summary>
/// Preview / beta endpoints under <c>/preview/*</c> — subject to change.
/// </summary>
/// <remarks>
/// <para>
/// This client deliberately consolidates every <c>/preview</c> operation rather
/// than splitting them into per-domain clients: the whole surface is provisional
/// and graduates out of <c>/preview</c> as it stabilises, at which point each
/// operation moves to its permanent client (see
/// <c>CreateTachographFileUploadAsync</c> for the shim pattern used when
/// that happens). To keep the grab-bag navigable, each member's documentation
/// names the domain it belongs to, its spec path and its operationId.
/// </para>
/// <para>
/// Members are grouped as: vehicles · drivers · tachograph (graduated) ·
/// <b>orders</b> (<c>/preview/fleet/orders*</c>) · <b>warranties</b> and
/// <b>warranty claims</b> (graduated to <see cref="IMaintenanceClient"/>).
/// </para>
/// </remarks>
public interface IPreviewApisClient
{
    /// <summary>Lock a vehicle remotely (<c>PUT /preview/fleet/vehicles/{id}/lock</c>).</summary>
    Task LockVehicleAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Unlock a vehicle remotely (<c>DELETE /preview/fleet/vehicles/{id}/lock</c>).</summary>
    Task UnlockVehicleAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Create a driver auth token via the preview endpoint
    /// (<c>POST /preview/fleet/drivers/create-auth-token</c>).</summary>
    /// <remarks>
    /// The preview request body is <b>not</b> the same schema as the stable
    /// <c>POST /fleet/drivers/auth-token</c> body: it identifies the driver with
    /// <c>id</c> rather than <c>driverId</c>. That is why this method takes
    /// <see cref="PreviewCreateDriverAuthTokenRequest"/> and not
    /// <see cref="CreateDriverAuthTokenRequest"/>. The success payloads are
    /// identical, so both operations return
    /// <see cref="Samsara.Sdk.Models.Drivers.DriverAuthToken"/>.
    /// </remarks>
    Task<DriverAuthToken> CreateDriverAuthTokenAsync(PreviewCreateDriverAuthTokenRequest request, CancellationToken cancellationToken = default);

    /// <summary>Create a tachograph file upload — <b>moved</b>.</summary>
    /// <remarks>
    /// This operation graduated out of <c>/preview</c> in the Samsara spec: it is now
    /// <c>POST /fleet/tachograph/file-uploads</c> and lives on
    /// <see cref="ITachographClient.CreateFileUploadAsync"/>. This member forwards to
    /// the new location and will be removed in the next major release; prefer the
    /// tachograph client, which carries the <c>[Experimental("SAMSARA001")]</c>
    /// annotation recording that Samsara still tags the operation <c>[beta]</c>.
    /// </remarks>
    [Obsolete("Moved to ITachographClient.CreateFileUploadAsync — the endpoint graduated out of /preview. This member will be removed in the next major release.", error: false)]
    Task<TachographFileUpload> CreateTachographFileUploadAsync(CreateTachographFileUploadRequest request, CancellationToken cancellationToken = default);

    // ---------------------------------------------------------------- Orders
    // Domain: fleet orders (hub routing). Spec paths: /preview/fleet/orders*.

    /// <summary>
    /// <b>Orders</b> — get live orders by id
    /// (<c>GET /preview/fleet/orders</c>, <c>getOrders</c>).
    /// </summary>
    /// <remarks>
    /// This operation is <b>not</b> paginated: the spec returns a bare
    /// <c>data[]</c> with no <c>pagination</c> block, so the whole list is
    /// returned at once. Use <see cref="GetOrdersStreamAsync"/> for the
    /// paginated feed.
    /// </remarks>
    /// <param name="orderIds">Samsara order UUIDs or external ID tokens. Maximum 100. Required by the spec.</param>
    /// <param name="includeExternalIds">Include external IDs in the returned orders.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<IReadOnlyList<FleetOrder>> GetOrdersAsync(
        IReadOnlyList<string> orderIds,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Orders</b> — delete an order
    /// (<c>DELETE /preview/fleet/orders</c>, <c>deleteOrder</c>). Returns
    /// <c>204 No Content</c>.
    /// </summary>
    /// <param name="orderId">One Samsara order UUID or external ID token. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeleteOrderAsync(string orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Orders</b> — upsert up to 250 orders atomically
    /// (<c>POST /preview/fleet/orders/batch</c>, <c>postOrdersBatch</c>).
    /// </summary>
    /// <remarks>
    /// The success body is <b>not</b> <c>{ data: ... }</c>-enveloped: it is an
    /// <see cref="OrdersBatchResult"/> carrying one status-bearing entry per
    /// input order, in request order.
    /// </remarks>
    [Experimental("SAMSARA001")]
    Task<OrdersBatchResult> PostOrdersBatchAsync(
        OrdersBatchRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Orders</b> — poll order deletions
    /// (<c>GET /preview/fleet/orders/deletions</c>, <c>getOrderDeletions</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="startTime">Optional <c>deletedAtTime</c> lower bound (RFC 3339).</param>
    /// <param name="endTime">Optional <c>deletedAtTime</c> upper bound (RFC 3339).</param>
    /// <param name="limit">Maximum number of deletion markers per page.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<FleetOrderDeletionMarker> GetOrderDeletionsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Orders</b> — stream orders ordered by <c>updatedAtTime</c>
    /// (<c>GET /preview/fleet/orders/stream</c>, <c>getOrdersStream</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="startTime">Inclusive <c>updatedAtTime</c> lower bound (RFC 3339). Required by the spec.</param>
    /// <param name="endTime">Optional exclusive <c>updatedAtTime</c> upper bound (RFC 3339).</param>
    /// <param name="routeId">Optional route ID scope.</param>
    /// <param name="includeExternalIds">Include external IDs in the returned orders.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<FleetOrder> GetOrdersStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        string? routeId = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    // ----------------------------------------------------------- Warranties
    // Graduated: Samsara removed every /preview/maintenance/* operation and now serves them
    // under /maintenance/* (tag "Maintenance"). Each member below forwards to IMaintenanceClient.

    /// <summary><b>Warranties</b> — list warranties — <b>moved</b>.</summary>
    /// <remarks>
    /// This operation graduated out of <c>/preview</c>: it is now
    /// <c>GET /maintenance/warranties</c> and lives on
    /// <see cref="IMaintenanceClient.ListWarrantiesAsync"/>. This member forwards there (the old
    /// <c>/preview</c> path returns <c>404</c>) and will be removed in the next major release.
    /// </remarks>
    [Obsolete(WarrantyShimMessage.ListWarranties, error: false)]
    IAsyncEnumerable<Warranty> ListWarrantiesAsync(
        string? warrantyIds = null,
        string? name = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary><b>Warranties</b> — create a warranty — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.CreateWarrantyAsync"/>
    /// (<c>POST /maintenance/warranties</c>).</remarks>
    [Obsolete(WarrantyShimMessage.CreateWarranty, error: false)]
    Task<Warranty> CreateWarrantyAsync(CreateWarrantyRequest request, CancellationToken cancellationToken = default);

    /// <summary><b>Warranties</b> — update a warranty — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.UpdateWarrantyAsync"/>
    /// (<c>PATCH /maintenance/warranties</c>).</remarks>
    [Obsolete(WarrantyShimMessage.UpdateWarranty, error: false)]
    Task<Warranty> UpdateWarrantyAsync(string id, UpdateWarrantyRequest request, CancellationToken cancellationToken = default);

    /// <summary><b>Warranties</b> — delete a warranty — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.DeleteWarrantyAsync"/>
    /// (<c>DELETE /maintenance/warranties</c>).</remarks>
    [Obsolete(WarrantyShimMessage.DeleteWarranty, error: false)]
    Task DeleteWarrantyAsync(string id, CancellationToken cancellationToken = default);

    /// <summary><b>Warranties</b> — replace a warranty's asset assignments — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.ReplaceWarrantyAssetAssignmentsAsync"/>
    /// (<c>POST /maintenance/warranties/assets/replace</c>).</remarks>
    [Obsolete(WarrantyShimMessage.ReplaceWarrantyAssetAssignments, error: false)]
    Task<WarrantyAssetAssignmentReplaceResult> ReplaceWarrantyAssetAssignmentsAsync(
        ReplaceWarrantyAssetAssignmentsRequest request,
        string? warrantyId = null,
        CancellationToken cancellationToken = default);

    // ------------------------------------------------------ Warranty claims

    /// <summary><b>Warranty claims</b> — list warranty claims — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.ListWarrantyClaimsAsync"/>
    /// (<c>GET /maintenance/warranty-claims</c>).</remarks>
    [Obsolete(WarrantyShimMessage.ListWarrantyClaims, error: false)]
    IAsyncEnumerable<WarrantyClaim> ListWarrantyClaimsAsync(
        string? warrantyClaimIds = null,
        string? assetIds = null,
        string? claimStatus = null,
        string? warrantyIds = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary><b>Warranty claims</b> — create a warranty claim — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.CreateWarrantyClaimAsync"/>
    /// (<c>POST /maintenance/warranty-claims</c>).</remarks>
    [Obsolete(WarrantyShimMessage.CreateWarrantyClaim, error: false)]
    Task<WarrantyClaim> CreateWarrantyClaimAsync(
        CreateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default);

    /// <summary><b>Warranty claims</b> — update a warranty claim — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.UpdateWarrantyClaimAsync"/>
    /// (<c>PATCH /maintenance/warranty-claims</c>).</remarks>
    [Obsolete(WarrantyShimMessage.UpdateWarrantyClaim, error: false)]
    Task<WarrantyClaim> UpdateWarrantyClaimAsync(
        string id,
        UpdateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default);

    /// <summary><b>Warranty claims</b> — delete a warranty claim — <b>moved</b>.</summary>
    /// <remarks>Forwards to <see cref="IMaintenanceClient.DeleteWarrantyClaimAsync"/>
    /// (<c>DELETE /maintenance/warranty-claims</c>).</remarks>
    [Obsolete(WarrantyShimMessage.DeleteWarrantyClaim, error: false)]
    Task DeleteWarrantyClaimAsync(string id, CancellationToken cancellationToken = default);
}

internal sealed class PreviewApisClient : SamsaraServiceClientBase, IPreviewApisClient
{
    private const string OrdersPath = "preview/fleet/orders";

    private readonly MaintenanceClient _maintenance;

    public PreviewApisClient(SamsaraHttpClient httpClient) : base(httpClient)
    {
        _maintenance = new MaintenanceClient(httpClient);
    }

    public Task LockVehicleAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>($"preview/fleet/vehicles/{Uri.EscapeDataString(id)}/lock", new { }, cancellationToken);

    public Task UnlockVehicleAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"preview/fleet/vehicles/{Uri.EscapeDataString(id)}/lock", cancellationToken);

    public Task<DriverAuthToken> CreateDriverAuthTokenAsync(PreviewCreateDriverAuthTokenRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverAuthToken>("preview/fleet/drivers/create-auth-token", request, cancellationToken);

    // Forwarding shim. The path below is the CURRENT spec path, not the old /preview
    // one: the old path 404s. Callers keep compiling (with an obsoletion warning)
    // and keep working.
    [Obsolete("Moved to ITachographClient.CreateFileUploadAsync — see the interface for details.", error: false)]
    public Task<TachographFileUpload> CreateTachographFileUploadAsync(CreateTachographFileUploadRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<TachographFileUpload>("fleet/tachograph/file-uploads", request, cancellationToken);

    /// <summary>Orders — get live orders by id (<c>GET /preview/fleet/orders</c>). Not paginated.</summary>
    [Experimental("SAMSARA001")]
    public Task<IReadOnlyList<FleetOrder>> GetOrdersAsync(
        IReadOnlyList<string> orderIds,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<FleetOrder>>(
            QueryBuilder.WithParams(OrdersPath,
                ("orderIds", string.Join(",", orderIds)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    /// <summary>Orders — delete an order (<c>DELETE /preview/fleet/orders</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task DeleteOrderAsync(string orderId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(OrdersPath, ("orderId", orderId)), cancellationToken);

    /// <summary>Orders — upsert orders atomically (<c>POST /preview/fleet/orders/batch</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<OrdersBatchResult> PostOrdersBatchAsync(
        OrdersBatchRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<OrdersBatchResult>($"{OrdersPath}/batch", request, cancellationToken);

    /// <summary>Orders — poll order deletions (<c>GET /preview/fleet/orders/deletions</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<FleetOrderDeletionMarker> GetOrderDeletionsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<FleetOrderDeletionMarker>(
            QueryBuilder.WithTimeRange($"{OrdersPath}/deletions", startTime, endTime),
            limit,
            cancellationToken);

    /// <summary>Orders — stream orders by update time (<c>GET /preview/fleet/orders/stream</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<FleetOrder> GetOrdersStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        string? routeId = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<FleetOrder>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange($"{OrdersPath}/stream", startTime, endTime),
                ("routeId", routeId),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    // Forwarding shims. They delegate to MaintenanceClient rather than repeating the paths, so
    // the graduated operations have a single implementation and the old /preview paths (which
    // 404) are no longer reachable from the SDK.
    [Obsolete(WarrantyShimMessage.ListWarranties, error: false)]
    public IAsyncEnumerable<Warranty> ListWarrantiesAsync(
        string? warrantyIds = null,
        string? name = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => _maintenance.ListWarrantiesAsync(warrantyIds, name, includeExternalIds, limit, cancellationToken);

    [Obsolete(WarrantyShimMessage.CreateWarranty, error: false)]
    public Task<Warranty> CreateWarrantyAsync(CreateWarrantyRequest request, CancellationToken cancellationToken = default)
        => _maintenance.CreateWarrantyAsync(request, cancellationToken);

    [Obsolete(WarrantyShimMessage.UpdateWarranty, error: false)]
    public Task<Warranty> UpdateWarrantyAsync(string id, UpdateWarrantyRequest request, CancellationToken cancellationToken = default)
        => _maintenance.UpdateWarrantyAsync(id, request, cancellationToken);

    [Obsolete(WarrantyShimMessage.DeleteWarranty, error: false)]
    public Task DeleteWarrantyAsync(string id, CancellationToken cancellationToken = default)
        => _maintenance.DeleteWarrantyAsync(id, cancellationToken);

    [Obsolete(WarrantyShimMessage.ReplaceWarrantyAssetAssignments, error: false)]
    public Task<WarrantyAssetAssignmentReplaceResult> ReplaceWarrantyAssetAssignmentsAsync(
        ReplaceWarrantyAssetAssignmentsRequest request,
        string? warrantyId = null,
        CancellationToken cancellationToken = default)
        => _maintenance.ReplaceWarrantyAssetAssignmentsAsync(request, warrantyId, cancellationToken);

    [Obsolete(WarrantyShimMessage.ListWarrantyClaims, error: false)]
    public IAsyncEnumerable<WarrantyClaim> ListWarrantyClaimsAsync(
        string? warrantyClaimIds = null,
        string? assetIds = null,
        string? claimStatus = null,
        string? warrantyIds = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => _maintenance.ListWarrantyClaimsAsync(
            warrantyClaimIds, assetIds, claimStatus, warrantyIds, includeExternalIds, limit, cancellationToken);

    [Obsolete(WarrantyShimMessage.CreateWarrantyClaim, error: false)]
    public Task<WarrantyClaim> CreateWarrantyClaimAsync(
        CreateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default)
        => _maintenance.CreateWarrantyClaimAsync(request, cancellationToken);

    [Obsolete(WarrantyShimMessage.UpdateWarrantyClaim, error: false)]
    public Task<WarrantyClaim> UpdateWarrantyClaimAsync(
        string id,
        UpdateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default)
        => _maintenance.UpdateWarrantyClaimAsync(id, request, cancellationToken);

    [Obsolete(WarrantyShimMessage.DeleteWarrantyClaim, error: false)]
    public Task DeleteWarrantyClaimAsync(string id, CancellationToken cancellationToken = default)
        => _maintenance.DeleteWarrantyClaimAsync(id, cancellationToken);
}

/// <summary>
/// <c>[Obsolete]</c> messages for the warranty members of
/// <see cref="IPreviewApisClient"/> that graduated to <see cref="IMaintenanceClient"/>.
/// </summary>
internal static class WarrantyShimMessage
{
    private const string Suffix = " — the endpoint graduated out of /preview (the old path returns 404). This member will be removed in the next major release.";

    public const string ListWarranties = "Moved to IMaintenanceClient.ListWarrantiesAsync" + Suffix;
    public const string CreateWarranty = "Moved to IMaintenanceClient.CreateWarrantyAsync" + Suffix;
    public const string UpdateWarranty = "Moved to IMaintenanceClient.UpdateWarrantyAsync" + Suffix;
    public const string DeleteWarranty = "Moved to IMaintenanceClient.DeleteWarrantyAsync" + Suffix;
    public const string ReplaceWarrantyAssetAssignments = "Moved to IMaintenanceClient.ReplaceWarrantyAssetAssignmentsAsync" + Suffix;
    public const string ListWarrantyClaims = "Moved to IMaintenanceClient.ListWarrantyClaimsAsync" + Suffix;
    public const string CreateWarrantyClaim = "Moved to IMaintenanceClient.CreateWarrantyClaimAsync" + Suffix;
    public const string UpdateWarrantyClaim = "Moved to IMaintenanceClient.UpdateWarrantyClaimAsync" + Suffix;
    public const string DeleteWarrantyClaim = "Moved to IMaintenanceClient.DeleteWarrantyClaimAsync" + Suffix;
}
