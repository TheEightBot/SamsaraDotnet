namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Models.Maintenance;

/// <summary>
/// Beta — maintenance parts inventory and purchase orders
/// (<c>/maintenance/parts*</c>, <c>/maintenance/purchase-orders</c>).
/// </summary>
/// <remarks>
/// Every operation on this client is tagged <c>[beta]</c> by Samsara and is
/// annotated <c>[Experimental("SAMSARA001")]</c>; suppress that diagnostic to
/// opt in. All of these operations identify their resource by <b>query string</b>
/// (<c>?id=</c>, <c>?partSamsaraId=&amp;placeId=</c>) rather than a path segment.
/// </remarks>
public interface IPartsClient
{
    /// <summary>
    /// List part definitions (<c>GET /maintenance/parts</c>, <c>listParts</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="idIn">Optional comma-separated list of part record IDs.</param>
    /// <param name="partIds">Optional comma-separated list of part IDs.</param>
    /// <param name="partStatus">Optional part-status filter.</param>
    /// <param name="includeDeleted">Whether to include deleted parts. Defaults to false.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<Part> ListPartsAsync(
        string? idIn = null,
        string? partIds = null,
        string? partStatus = null,
        bool? includeDeleted = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a part definition (<c>POST /maintenance/parts</c>, <c>createPart</c>). Responds <c>201 Created</c>.</summary>
    [Experimental("SAMSARA001")]
    Task<Part> CreatePartAsync(CreatePartRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update a part definition (<c>PATCH /maintenance/parts</c>, <c>updatePart</c>).</summary>
    /// <param name="id">Unique identifier for the part. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<Part> UpdatePartAsync(string id, UpdatePartRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete a part definition (<c>DELETE /maintenance/parts</c>, <c>deletePart</c>). Returns <c>204 No Content</c>.</summary>
    /// <param name="id">Unique identifier for the part. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeletePartAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List per-site part inventory levels
    /// (<c>GET /maintenance/parts/inventory-location</c>, <c>listPartInventory</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="placeIds">Optional comma-separated list of place IDs.</param>
    /// <param name="isLowStock">Filter to locations at or below their reorder threshold.</param>
    /// <param name="partSamsaraIds">Optional comma-separated list of part IDs.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<PartInventoryLocation> ListPartInventoryAsync(
        string? placeIds = null,
        bool? isLowStock = null,
        string? partSamsaraIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a part inventory location
    /// (<c>POST /maintenance/parts/inventory-location</c>,
    /// <c>createPartInventoryLocation</c>). Responds <c>201 Created</c>.
    /// </summary>
    /// <param name="request">The initial inventory settings.</param>
    /// <param name="partSamsaraId">The part definition these levels are tracked for (query param).</param>
    /// <param name="placeId">The place linked to the maintenance site holding this inventory (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<PartInventoryLocation> CreatePartInventoryLocationAsync(
        CreatePartInventoryLocationRequest request,
        string? partSamsaraId = null,
        string? placeId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a part inventory location
    /// (<c>PATCH /maintenance/parts/inventory-location</c>,
    /// <c>updatePartInventoryLocation</c>).
    /// </summary>
    /// <param name="request">The fields to change.</param>
    /// <param name="partSamsaraId">The part definition these levels are tracked for (query param).</param>
    /// <param name="placeId">The place linked to the maintenance site holding this inventory (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<PartInventoryLocation> UpdatePartInventoryLocationAsync(
        UpdatePartInventoryLocationRequest request,
        string? partSamsaraId = null,
        string? placeId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Record a stock movement (<c>POST /maintenance/parts/stock-movements</c>,
    /// <c>createStockMovement</c>). Returns the inventory levels at each end of
    /// the movement after it was applied.
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<StockMovementResult> CreateStockMovementAsync(
        CreateStockMovementRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List inventory transactions (<c>GET /maintenance/parts/transactions</c>,
    /// <c>listPartTransactions</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="happenedAtTimeStart">RFC 3339 lower bound on <c>happenedAtTime</c>. Required by the spec.</param>
    /// <param name="happenedAtTimeEnd">Optional RFC 3339 upper bound on <c>happenedAtTime</c>.</param>
    /// <param name="partSamsaraIds">Optional comma-separated list of part IDs.</param>
    /// <param name="placeIds">Optional comma-separated list of place IDs.</param>
    /// <param name="transactionTypeIn">Optional comma-separated list of transaction types.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<PartTransaction> ListPartTransactionsAsync(
        DateTimeOffset happenedAtTimeStart,
        DateTimeOffset? happenedAtTimeEnd = null,
        string? partSamsaraIds = null,
        string? placeIds = null,
        string? transactionTypeIn = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List purchase orders (<c>GET /maintenance/purchase-orders</c>,
    /// <c>listPurchaseOrders</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="startTime">Start of the updated-time range, inclusive. Required by the spec.</param>
    /// <param name="endTime">Optional end of the updated-time range, exclusive.</param>
    /// <param name="ids">Optional comma-separated list of purchase order IDs.</param>
    /// <param name="poNumbers">Optional comma-separated list of PO numbers.</param>
    /// <param name="vendorIds">Optional comma-separated list of vendor IDs.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<PurchaseOrder> ListPurchaseOrdersAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        string? ids = null,
        string? poNumbers = null,
        string? vendorIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a purchase order (<c>POST /maintenance/purchase-orders</c>,
    /// <c>createPurchaseOrder</c>). Responds <c>201 Created</c>.
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<PurchaseOrder> CreatePurchaseOrderAsync(
        CreatePurchaseOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a purchase order (<c>PATCH /maintenance/purchase-orders</c>,
    /// <c>updatePurchaseOrder</c>).
    /// </summary>
    /// <param name="id">Unique identifier for the purchase order. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<PurchaseOrder> UpdatePurchaseOrderAsync(
        string id,
        UpdatePurchaseOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a purchase order (<c>DELETE /maintenance/purchase-orders</c>,
    /// <c>deletePurchaseOrder</c>). Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">Unique identifier for the purchase order. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeletePurchaseOrderAsync(string id, CancellationToken cancellationToken = default);
}
