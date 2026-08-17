namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Maintenance;

internal sealed class PartsClient : SamsaraServiceClientBase, IPartsClient
{
    private const string BasePath = "maintenance/parts";
    private const string InventoryLocationPath = "maintenance/parts/inventory-location";
    private const string StockMovementsPath = "maintenance/parts/stock-movements";
    private const string TransactionsPath = "maintenance/parts/transactions";
    private const string PurchaseOrdersPath = "maintenance/purchase-orders";

    public PartsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>List part definitions (<c>GET /maintenance/parts</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<Part> ListPartsAsync(
        string? idIn = null,
        string? partIds = null,
        string? partStatus = null,
        bool? includeDeleted = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Part>(
            QueryBuilder.WithParams(BasePath,
                ("idIn", idIn),
                ("partIds", partIds),
                ("partStatus", partStatus),
                ("includeDeleted", includeDeleted?.ToString().ToLowerInvariant())),
            limit,
            cancellationToken);

    /// <summary>Create a part definition (<c>POST /maintenance/parts</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<Part> CreatePartAsync(CreatePartRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Part>(BasePath, request, cancellationToken);

    /// <summary>Update a part definition (<c>PATCH /maintenance/parts</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<Part> UpdatePartAsync(string id, UpdatePartRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Part>(QueryBuilder.WithParams(BasePath, ("id", id)), request, cancellationToken);

    /// <summary>Delete a part definition (<c>DELETE /maintenance/parts</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task DeletePartAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);

    /// <summary>List per-site part inventory levels (<c>GET /maintenance/parts/inventory-location</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<PartInventoryLocation> ListPartInventoryAsync(
        string? placeIds = null,
        bool? isLowStock = null,
        string? partSamsaraIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<PartInventoryLocation>(
            QueryBuilder.WithParams(InventoryLocationPath,
                ("placeIds", placeIds),
                ("isLowStock", isLowStock?.ToString().ToLowerInvariant()),
                ("partSamsaraIds", partSamsaraIds)),
            limit,
            cancellationToken);

    /// <summary>Create a part inventory location (<c>POST /maintenance/parts/inventory-location</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<PartInventoryLocation> CreatePartInventoryLocationAsync(
        CreatePartInventoryLocationRequest request,
        string? partSamsaraId = null,
        string? placeId = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<PartInventoryLocation>(
            QueryBuilder.WithParams(InventoryLocationPath,
                ("partSamsaraId", partSamsaraId),
                ("placeId", placeId)),
            request,
            cancellationToken);

    /// <summary>Update a part inventory location (<c>PATCH /maintenance/parts/inventory-location</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<PartInventoryLocation> UpdatePartInventoryLocationAsync(
        UpdatePartInventoryLocationRequest request,
        string? partSamsaraId = null,
        string? placeId = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<PartInventoryLocation>(
            QueryBuilder.WithParams(InventoryLocationPath,
                ("partSamsaraId", partSamsaraId),
                ("placeId", placeId)),
            request,
            cancellationToken);

    /// <summary>Record a stock movement (<c>POST /maintenance/parts/stock-movements</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<StockMovementResult> CreateStockMovementAsync(
        CreateStockMovementRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<StockMovementResult>(StockMovementsPath, request, cancellationToken);

    /// <summary>List inventory transactions (<c>GET /maintenance/parts/transactions</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<PartTransaction> ListPartTransactionsAsync(
        DateTimeOffset happenedAtTimeStart,
        DateTimeOffset? happenedAtTimeEnd = null,
        string? partSamsaraIds = null,
        string? placeIds = null,
        string? transactionTypeIn = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<PartTransaction>(
            QueryBuilder.WithParams(TransactionsPath,
                ("happenedAtTimeStart", happenedAtTimeStart.ToString("O", CultureInfo.InvariantCulture)),
                ("happenedAtTimeEnd", happenedAtTimeEnd?.ToString("O", CultureInfo.InvariantCulture)),
                ("partSamsaraIds", partSamsaraIds),
                ("placeIds", placeIds),
                ("transactionTypeIn", transactionTypeIn)),
            limit,
            cancellationToken);

    /// <summary>List purchase orders (<c>GET /maintenance/purchase-orders</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<PurchaseOrder> ListPurchaseOrdersAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        string? ids = null,
        string? poNumbers = null,
        string? vendorIds = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<PurchaseOrder>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(PurchaseOrdersPath, startTime, endTime),
                ("ids", ids),
                ("poNumbers", poNumbers),
                ("vendorIds", vendorIds)),
            limit,
            cancellationToken);

    /// <summary>Create a purchase order (<c>POST /maintenance/purchase-orders</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<PurchaseOrder> CreatePurchaseOrderAsync(
        CreatePurchaseOrderRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<PurchaseOrder>(PurchaseOrdersPath, request, cancellationToken);

    /// <summary>Update a purchase order (<c>PATCH /maintenance/purchase-orders</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<PurchaseOrder> UpdatePurchaseOrderAsync(
        string id,
        UpdatePurchaseOrderRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<PurchaseOrder>(
            QueryBuilder.WithParams(PurchaseOrdersPath, ("id", id)), request, cancellationToken);

    /// <summary>Delete a purchase order (<c>DELETE /maintenance/purchase-orders</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task DeletePurchaseOrderAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(PurchaseOrdersPath, ("id", id)), cancellationToken);
}
