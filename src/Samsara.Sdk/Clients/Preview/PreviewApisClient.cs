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
/// <b>warranty claims</b> (<c>/preview/maintenance/*</c>).
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
    // Domain: maintenance warranties. Spec paths: /preview/maintenance/warranties*.

    /// <summary>
    /// <b>Warranties</b> — list warranties
    /// (<c>GET /preview/maintenance/warranties</c>, <c>listWarranties</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="warrantyIds">Optional comma-separated list of warranty IDs.</param>
    /// <param name="name">Optional comma-separated list of names.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each response object.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<Warranty> ListWarrantiesAsync(
        string? warrantyIds = null,
        string? name = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranties</b> — create a warranty
    /// (<c>POST /preview/maintenance/warranties</c>, <c>createWarranty</c>).
    /// Responds <c>201 Created</c>.
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<Warranty> CreateWarrantyAsync(CreateWarrantyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranties</b> — update a warranty
    /// (<c>PATCH /preview/maintenance/warranties</c>, <c>updateWarranty</c>).
    /// </summary>
    /// <param name="id">Unique identifier for the warranty. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<Warranty> UpdateWarrantyAsync(string id, UpdateWarrantyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranties</b> — delete a warranty
    /// (<c>DELETE /preview/maintenance/warranties</c>, <c>deleteWarranty</c>).
    /// Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">Unique identifier for the warranty. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeleteWarrantyAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranties</b> — replace a warranty's asset assignments
    /// (<c>POST /preview/maintenance/warranties/assets/replace</c>,
    /// <c>replaceWarrantyAssetAssignments</c>). The supplied list becomes the
    /// warranty's entire asset set.
    /// </summary>
    /// <param name="request">The full desired asset set.</param>
    /// <param name="warrantyId">ID of the warranty whose asset set to replace (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<WarrantyAssetAssignmentReplaceResult> ReplaceWarrantyAssetAssignmentsAsync(
        ReplaceWarrantyAssetAssignmentsRequest request,
        string? warrantyId = null,
        CancellationToken cancellationToken = default);

    // ------------------------------------------------------ Warranty claims
    // Domain: maintenance warranty claims. Spec paths: /preview/maintenance/warranty-claims.

    /// <summary>
    /// <b>Warranty claims</b> — list warranty claims
    /// (<c>GET /preview/maintenance/warranty-claims</c>,
    /// <c>listWarrantyClaims</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="warrantyClaimIds">Optional comma-separated list of claim IDs.</param>
    /// <param name="assetIds">Optional comma-separated list of asset IDs.</param>
    /// <param name="claimStatus">Optional comma-separated list of claim statuses.</param>
    /// <param name="warrantyIds">Optional comma-separated list of warranty IDs.</param>
    /// <param name="includeExternalIds">Include <c>externalIds</c> in each response object.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<WarrantyClaim> ListWarrantyClaimsAsync(
        string? warrantyClaimIds = null,
        string? assetIds = null,
        string? claimStatus = null,
        string? warrantyIds = null,
        bool? includeExternalIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranty claims</b> — create a warranty claim
    /// (<c>POST /preview/maintenance/warranty-claims</c>,
    /// <c>createWarrantyClaim</c>). Responds <c>201 Created</c>.
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<WarrantyClaim> CreateWarrantyClaimAsync(
        CreateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranty claims</b> — update a warranty claim
    /// (<c>PATCH /preview/maintenance/warranty-claims</c>,
    /// <c>updateWarrantyClaim</c>).
    /// </summary>
    /// <param name="id">Unique identifier for the claim. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<WarrantyClaim> UpdateWarrantyClaimAsync(
        string id,
        UpdateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// <b>Warranty claims</b> — delete a warranty claim
    /// (<c>DELETE /preview/maintenance/warranty-claims</c>,
    /// <c>deleteWarrantyClaim</c>). Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">Unique identifier for the claim. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeleteWarrantyClaimAsync(string id, CancellationToken cancellationToken = default);
}

internal sealed class PreviewApisClient : SamsaraServiceClientBase, IPreviewApisClient
{
    private const string OrdersPath = "preview/fleet/orders";
    private const string WarrantiesPath = "preview/maintenance/warranties";
    private const string WarrantyClaimsPath = "preview/maintenance/warranty-claims";

    public PreviewApisClient(SamsaraHttpClient httpClient) : base(httpClient) { }

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

    /// <summary>Warranties — list warranties (<c>GET /preview/maintenance/warranties</c>).</summary>
    [Experimental("SAMSARA001")]
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

    /// <summary>Warranties — create a warranty (<c>POST /preview/maintenance/warranties</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<Warranty> CreateWarrantyAsync(CreateWarrantyRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Warranty>(WarrantiesPath, request, cancellationToken);

    /// <summary>Warranties — update a warranty (<c>PATCH /preview/maintenance/warranties</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<Warranty> UpdateWarrantyAsync(string id, UpdateWarrantyRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Warranty>(
            QueryBuilder.WithParams(WarrantiesPath, ("id", id)), request, cancellationToken);

    /// <summary>Warranties — delete a warranty (<c>DELETE /preview/maintenance/warranties</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task DeleteWarrantyAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(WarrantiesPath, ("id", id)), cancellationToken);

    /// <summary>
    /// Warranties — replace a warranty's asset assignments
    /// (<c>POST /preview/maintenance/warranties/assets/replace</c>).
    /// </summary>
    [Experimental("SAMSARA001")]
    public Task<WarrantyAssetAssignmentReplaceResult> ReplaceWarrantyAssetAssignmentsAsync(
        ReplaceWarrantyAssetAssignmentsRequest request,
        string? warrantyId = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<WarrantyAssetAssignmentReplaceResult>(
            QueryBuilder.WithParams($"{WarrantiesPath}/assets/replace", ("warrantyId", warrantyId)),
            request,
            cancellationToken);

    /// <summary>Warranty claims — list claims (<c>GET /preview/maintenance/warranty-claims</c>).</summary>
    [Experimental("SAMSARA001")]
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

    /// <summary>Warranty claims — create a claim (<c>POST /preview/maintenance/warranty-claims</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<WarrantyClaim> CreateWarrantyClaimAsync(
        CreateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<WarrantyClaim>(WarrantyClaimsPath, request, cancellationToken);

    /// <summary>Warranty claims — update a claim (<c>PATCH /preview/maintenance/warranty-claims</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<WarrantyClaim> UpdateWarrantyClaimAsync(
        string id,
        UpdateWarrantyClaimRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<WarrantyClaim>(
            QueryBuilder.WithParams(WarrantyClaimsPath, ("id", id)), request, cancellationToken);

    /// <summary>Warranty claims — delete a claim (<c>DELETE /preview/maintenance/warranty-claims</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task DeleteWarrantyClaimAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(WarrantyClaimsPath, ("id", id)), cancellationToken);
}
