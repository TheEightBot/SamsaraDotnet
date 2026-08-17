namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>
/// A monetary amount on a maintenance resource. Mirrors the spec's
/// <c>...MoneyTypeResponseBody</c> family (part definitions, part inventory
/// locations, stock movements, purchase orders, warranty claims) — every
/// variant is the same two optional members, so one record serves them all.
/// </summary>
/// <remarks>
/// The request-side twin is <see cref="MaintenanceMoneyInput"/>: the spec marks
/// both members REQUIRED there, so the two directions cannot share a record.
/// </remarks>
public sealed record MaintenanceMoney
{
    /// <summary>Monetary amount as a decimal string in major currency units (e.g. <c>24.50</c>).</summary>
    [JsonPropertyName("amount")]
    public string? Amount { get; init; }

    /// <summary>ISO 4217 currency code, lowercased (e.g. <c>usd</c>).</summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; init; }
}

/// <summary>
/// A monetary amount supplied on a maintenance request body. Mirrors the spec's
/// <c>...MoneyInputTypeRequestBody</c> family, which marks both members REQUIRED.
/// </summary>
public sealed record MaintenanceMoneyInput
{
    /// <summary>Monetary amount as a decimal string in major currency units (e.g. <c>24.50</c>). Spec REQUIRED.</summary>
    [JsonPropertyName("amount")]
    public required string Amount { get; init; }

    /// <summary>ISO 4217 currency code, lowercased (e.g. <c>usd</c>). Spec REQUIRED.</summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
}

/// <summary>
/// A <c>{ id }</c> reference to another maintenance-domain entity. Mirrors the
/// spec's structurally identical <c>...PartDefinitionRefType</c>,
/// <c>...PlaceRefType</c>, <c>...VendorRefType</c>, <c>...WorkOrderRefType</c>,
/// <c>...AssetRefType</c> and <c>...WarrantyRefType</c> response schemas.
/// </summary>
public sealed record MaintenanceEntityRef
{
    /// <summary>The ID of the referenced entity. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// A part definition (beta). Mirrors the spec's
/// <c>EntityListPartsTypeResponseBody</c> and its byte-identical create/update
/// twins, so one record serves <c>GET</c>, <c>POST</c> and <c>PATCH</c>
/// <c>/maintenance/parts</c>.
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record Part
{
    /// <summary>Unique identifier for the part.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the part definition.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Customer-visible part number for the part.</summary>
    [JsonPropertyName("partNumber")]
    public string? PartNumber { get; init; }

    /// <summary>Description of the part definition.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Customer-supplied external identifier for the part.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Status of the part.</summary>
    [JsonPropertyName("partStatus")]
    public string? PartStatus { get; init; }

    /// <summary>Category of the part definition.</summary>
    [JsonPropertyName("category")]
    public string? Category { get; init; }

    /// <summary>Subcategory of the part definition.</summary>
    [JsonPropertyName("subcategory")]
    public string? Subcategory { get; init; }

    /// <summary>Name of the manufacturer for the part definition.</summary>
    [JsonPropertyName("manufacturerName")]
    public string? ManufacturerName { get; init; }

    /// <summary>Manufacturer-supplied part number.</summary>
    [JsonPropertyName("manufacturerPartNumber")]
    public string? ManufacturerPartNumber { get; init; }

    /// <summary>Barcode associated with the part definition.</summary>
    [JsonPropertyName("barcodeString")]
    public string? BarcodeString { get; init; }

    /// <summary>Type of barcode associated with the part definition.</summary>
    [JsonPropertyName("barcodeType")]
    public string? BarcodeType { get; init; }

    /// <summary>VMRS code associated with the part definition.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }

    /// <summary>Whether inventory tracking is enabled for this part.</summary>
    [JsonPropertyName("isInventoryTracked")]
    public bool? IsInventoryTracked { get; init; }

    /// <summary>Unit of measure for the part.</summary>
    [JsonPropertyName("unitOfMeasureType")]
    public string? UnitOfMeasureType { get; init; }

    /// <summary>Default unit cost for the part.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoney? UnitCost { get; init; }

    /// <summary>Time when the part was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>Time when the part was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }

    /// <summary>Time when the part was archived.</summary>
    [JsonPropertyName("archivedAtTime")]
    public string? ArchivedAtTime { get; init; }

    /// <summary>Time when the part was deleted.</summary>
    [JsonPropertyName("deletedAtTime")]
    public string? DeletedAtTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /maintenance/parts</c> (<c>createPart</c>, beta).
/// Mirrors the spec's <c>EntityPartDefinitionsServiceCreatePartRequestBody</c>.
/// </summary>
public sealed record CreatePartRequest
{
    /// <summary>Customer-visible part number for the part. Spec REQUIRED.</summary>
    [JsonPropertyName("partNumber")]
    public required string PartNumber { get; init; }

    /// <summary>Name of the part definition.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Description of the part definition.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Customer-supplied external identifier for the part.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Name of the manufacturer for the part definition.</summary>
    [JsonPropertyName("manufacturerName")]
    public string? ManufacturerName { get; init; }

    /// <summary>Manufacturer-supplied part number.</summary>
    [JsonPropertyName("manufacturerPartNumber")]
    public string? ManufacturerPartNumber { get; init; }

    /// <summary>Barcode associated with the part definition.</summary>
    [JsonPropertyName("barcodeString")]
    public string? BarcodeString { get; init; }

    /// <summary>Type of barcode associated with the part definition.</summary>
    [JsonPropertyName("barcodeType")]
    public string? BarcodeType { get; init; }

    /// <summary>VMRS code associated with the part definition.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }

    /// <summary>Whether inventory tracking is enabled for this part.</summary>
    [JsonPropertyName("isInventoryTracked")]
    public bool? IsInventoryTracked { get; init; }

    /// <summary>Default unit cost for the part.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoneyInput? UnitCost { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /maintenance/parts</c> (<c>updatePart</c>, beta).
/// Mirrors the spec's <c>EntityPartDefinitionsServiceUpdatePartRequestBody</c>,
/// which — unlike the create body — marks nothing required.
/// </summary>
public sealed record UpdatePartRequest
{
    /// <summary>Customer-visible part number for the part.</summary>
    [JsonPropertyName("partNumber")]
    public string? PartNumber { get; init; }

    /// <summary>Name of the part definition.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Description of the part definition.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Customer-supplied external identifier for the part.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Name of the manufacturer for the part definition.</summary>
    [JsonPropertyName("manufacturerName")]
    public string? ManufacturerName { get; init; }

    /// <summary>Manufacturer-supplied part number.</summary>
    [JsonPropertyName("manufacturerPartNumber")]
    public string? ManufacturerPartNumber { get; init; }

    /// <summary>Barcode associated with the part definition.</summary>
    [JsonPropertyName("barcodeString")]
    public string? BarcodeString { get; init; }

    /// <summary>Type of barcode associated with the part definition.</summary>
    [JsonPropertyName("barcodeType")]
    public string? BarcodeType { get; init; }

    /// <summary>VMRS code associated with the part definition.</summary>
    [JsonPropertyName("vmrsCode")]
    public string? VmrsCode { get; init; }

    /// <summary>Whether inventory tracking is enabled for this part.</summary>
    [JsonPropertyName("isInventoryTracked")]
    public bool? IsInventoryTracked { get; init; }

    /// <summary>Default unit cost for the part.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoneyInput? UnitCost { get; init; }
}

/// <summary>
/// Inventory levels for one part at one maintenance site (beta). Mirrors the
/// spec's <c>EntityListPartInventoryTypeResponseBody</c> and its create/update
/// twins; the create and update responses additionally carry <c>id</c>,
/// which the list shape omits.
/// </summary>
public sealed record PartInventoryLocation
{
    /// <summary>
    /// Unique identifier for the part inventory level record. Returned by the
    /// create and update operations; the list shape omits it.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The part definition these inventory levels are tracked for.</summary>
    [JsonPropertyName("partSamsara")]
    public MaintenanceEntityRef? PartSamsara { get; init; }

    /// <summary>The place linked to the maintenance site holding this inventory.</summary>
    [JsonPropertyName("place")]
    public MaintenanceEntityRef? Place { get; init; }

    /// <summary>Aisle within the location where the part is stored.</summary>
    [JsonPropertyName("aisle")]
    public string? Aisle { get; init; }

    /// <summary>Row within the location where the part is stored.</summary>
    [JsonPropertyName("row")]
    public string? Row { get; init; }

    /// <summary>Bin within the location where the part is stored.</summary>
    [JsonPropertyName("bin")]
    public string? Bin { get; init; }

    /// <summary>Total physical quantity on hand at this location.</summary>
    [JsonPropertyName("currentQuantity")]
    public double? CurrentQuantity { get; init; }

    /// <summary>Quantity available to be consumed at this location (current minus reserved). Read-only.</summary>
    [JsonPropertyName("availableQuantity")]
    public double? AvailableQuantity { get; init; }

    /// <summary>Quantity reserved against work orders at this location. Read-only.</summary>
    [JsonPropertyName("reservedQuantity")]
    public double? ReservedQuantity { get; init; }

    /// <summary>Minimum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("minStockLevel")]
    public double? MinStockLevel { get; init; }

    /// <summary>Maximum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("maxStockLevel")]
    public double? MaxStockLevel { get; init; }

    /// <summary>Available quantity at or below which the part should be reordered.</summary>
    [JsonPropertyName("reorderThreshold")]
    public double? ReorderThreshold { get; init; }

    /// <summary>Quantity to reorder when stock reaches the reorder threshold.</summary>
    [JsonPropertyName("reorderQuantity")]
    public double? ReorderQuantity { get; init; }

    /// <summary>Whether the available quantity is greater than zero and at or below the reorder threshold.</summary>
    [JsonPropertyName("isLowStock")]
    public bool? IsLowStock { get; init; }

    /// <summary>Whether costing is tracked at this location.</summary>
    [JsonPropertyName("isCostTracked")]
    public bool? IsCostTracked { get; init; }

    /// <summary>Unit cost recorded for the part at this location.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoney? UnitCost { get; init; }

    /// <summary>Unit of measure the quantity fields on this record are expressed in.</summary>
    [JsonPropertyName("unitOfMeasureType")]
    public string? UnitOfMeasureType { get; init; }

    /// <summary>Time when the inventory level record was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>Time when the inventory level record was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /maintenance/parts/inventory-location</c>
/// (<c>createPartInventoryLocation</c>, beta). Mirrors the spec's
/// <c>EntityPartInventoryLocationsServiceCreatePartInventoryLocationRequestBody</c>.
/// The part and place are identified by the <c>partSamsaraId</c> /
/// <c>placeId</c> query parameters, not by the body.
/// </summary>
public sealed record CreatePartInventoryLocationRequest
{
    /// <summary>Aisle within the location where the part is stored.</summary>
    [JsonPropertyName("aisle")]
    public string? Aisle { get; init; }

    /// <summary>Row within the location where the part is stored.</summary>
    [JsonPropertyName("row")]
    public string? Row { get; init; }

    /// <summary>Bin within the location where the part is stored.</summary>
    [JsonPropertyName("bin")]
    public string? Bin { get; init; }

    /// <summary>Total physical quantity on hand at this location.</summary>
    [JsonPropertyName("currentQuantity")]
    public double? CurrentQuantity { get; init; }

    /// <summary>Minimum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("minStockLevel")]
    public double? MinStockLevel { get; init; }

    /// <summary>Maximum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("maxStockLevel")]
    public double? MaxStockLevel { get; init; }

    /// <summary>Available quantity at or below which the part should be reordered.</summary>
    [JsonPropertyName("reorderThreshold")]
    public double? ReorderThreshold { get; init; }

    /// <summary>Quantity to reorder when stock reaches the reorder threshold.</summary>
    [JsonPropertyName("reorderQuantity")]
    public double? ReorderQuantity { get; init; }

    /// <summary>Whether costing is tracked at this location. Defaults to false.</summary>
    [JsonPropertyName("isCostTracked")]
    public bool? IsCostTracked { get; init; }

    /// <summary>Unit cost recorded for the part at this location.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoneyInput? UnitCost { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /maintenance/parts/inventory-location</c>
/// (<c>updatePartInventoryLocation</c>, beta). Mirrors the spec's
/// <c>EntityPartInventoryLocationsServiceUpdatePartInventoryLocationRequestBody</c>,
/// which — unlike the create body — has no <c>currentQuantity</c> member
/// (stock levels change only through stock movements).
/// </summary>
public sealed record UpdatePartInventoryLocationRequest
{
    /// <summary>Aisle within the location where the part is stored.</summary>
    [JsonPropertyName("aisle")]
    public string? Aisle { get; init; }

    /// <summary>Row within the location where the part is stored.</summary>
    [JsonPropertyName("row")]
    public string? Row { get; init; }

    /// <summary>Bin within the location where the part is stored.</summary>
    [JsonPropertyName("bin")]
    public string? Bin { get; init; }

    /// <summary>Minimum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("minStockLevel")]
    public double? MinStockLevel { get; init; }

    /// <summary>Maximum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("maxStockLevel")]
    public double? MaxStockLevel { get; init; }

    /// <summary>Available quantity at or below which the part should be reordered.</summary>
    [JsonPropertyName("reorderThreshold")]
    public double? ReorderThreshold { get; init; }

    /// <summary>Quantity to reorder when stock reaches the reorder threshold.</summary>
    [JsonPropertyName("reorderQuantity")]
    public double? ReorderQuantity { get; init; }

    /// <summary>Whether costing is tracked at this location.</summary>
    [JsonPropertyName("isCostTracked")]
    public bool? IsCostTracked { get; init; }

    /// <summary>Unit cost recorded for the part at this location.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoneyInput? UnitCost { get; init; }
}

/// <summary>
/// Request body for <c>POST /maintenance/parts/stock-movements</c>
/// (<c>createStockMovement</c>, beta). Mirrors the spec's
/// <c>CreateStockMovementActionServiceCreateStockMovementRequestBody</c>.
/// </summary>
public sealed record CreateStockMovementRequest
{
    /// <summary>Unique identifier of the part definition the movement applies to. Spec REQUIRED.</summary>
    [JsonPropertyName("partSamsaraId")]
    public required string PartSamsaraId { get; init; }

    /// <summary>
    /// Type of stock movement to record. Must be one of <c>Receive</c>,
    /// <c>Transfer</c>, <c>Scrap</c> or <c>Adjust</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("movementType")]
    public required string MovementType { get; init; }

    /// <summary>
    /// Quantity moved, in the part's unit of measure. Positive magnitude for a
    /// receive, and so on per movement type. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("quantity")]
    public required double Quantity { get; init; }

    /// <summary>Unique identifier of the place the movement targets.</summary>
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    /// <summary>Transfer only — unique identifier of the source place.</summary>
    [JsonPropertyName("fromPlaceId")]
    public string? FromPlaceId { get; init; }

    /// <summary>Transfer only — unique identifier of the destination place.</summary>
    [JsonPropertyName("toPlaceId")]
    public string? ToPlaceId { get; init; }

    /// <summary>Batch or lot identifier the movement applies to, if the part is batch-tracked.</summary>
    [JsonPropertyName("batch")]
    public string? Batch { get; init; }

    /// <summary>Time when the movement occurred. Defaults to the current time if not provided.</summary>
    [JsonPropertyName("happenedAtTime")]
    public string? HappenedAtTime { get; init; }

    /// <summary>Notes explaining the movement. Scrap and adjust only.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Purchase order reference for the received inventory. Receive only.</summary>
    [JsonPropertyName("purchaseOrder")]
    public string? PurchaseOrder { get; init; }

    /// <summary>Unique identifier of the vendor the inventory was received from. Receive only.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>Per-unit cost recorded with the movement.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoneyInput? UnitCost { get; init; }
}

/// <summary>
/// The inventory levels at one end of a stock movement, after the movement was
/// applied. Mirrors the spec's
/// <c>EntityCreateStockMovementStockMovementLocationTypeResponseBody</c>.
/// </summary>
/// <remarks>
/// This is deliberately <b>not</b> <see cref="PartInventoryLocation"/>: on this
/// schema <c>partSamsara</c> and <c>place</c> are bare ID strings rather than
/// <c>{ id }</c> reference objects.
/// </remarks>
public sealed record StockMovementLocation
{
    /// <summary>Unique identifier for the part inventory level record.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Unique identifier for the part definition these inventory levels are tracked for.</summary>
    [JsonPropertyName("partSamsara")]
    public string? PartSamsara { get; init; }

    /// <summary>Unique identifier for the place linked to the maintenance site holding this inventory.</summary>
    [JsonPropertyName("place")]
    public string? Place { get; init; }

    /// <summary>Aisle within the location where the part is stored.</summary>
    [JsonPropertyName("aisle")]
    public string? Aisle { get; init; }

    /// <summary>Row within the location where the part is stored.</summary>
    [JsonPropertyName("row")]
    public string? Row { get; init; }

    /// <summary>Bin within the location where the part is stored.</summary>
    [JsonPropertyName("bin")]
    public string? Bin { get; init; }

    /// <summary>Total physical quantity on hand at this location after the movement.</summary>
    [JsonPropertyName("currentQuantity")]
    public double? CurrentQuantity { get; init; }

    /// <summary>Quantity available to be consumed at this location after the movement.</summary>
    [JsonPropertyName("availableQuantity")]
    public double? AvailableQuantity { get; init; }

    /// <summary>Quantity reserved against work orders at this location.</summary>
    [JsonPropertyName("reservedQuantity")]
    public double? ReservedQuantity { get; init; }

    /// <summary>Minimum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("minStockLevel")]
    public double? MinStockLevel { get; init; }

    /// <summary>Maximum quantity to keep in stock at this location.</summary>
    [JsonPropertyName("maxStockLevel")]
    public double? MaxStockLevel { get; init; }

    /// <summary>Available quantity at or below which the part should be reordered.</summary>
    [JsonPropertyName("reorderThreshold")]
    public double? ReorderThreshold { get; init; }

    /// <summary>Quantity to reorder when stock reaches the reorder threshold.</summary>
    [JsonPropertyName("reorderQuantity")]
    public double? ReorderQuantity { get; init; }

    /// <summary>Whether the available quantity is greater than zero and at or below the reorder threshold.</summary>
    [JsonPropertyName("isLowStock")]
    public bool? IsLowStock { get; init; }

    /// <summary>Whether costing is tracked at this location.</summary>
    [JsonPropertyName("isCostTracked")]
    public bool? IsCostTracked { get; init; }

    /// <summary>Unit cost recorded for the part at this location.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoney? UnitCost { get; init; }

    /// <summary>Unit of measure the quantity fields on this record are expressed in.</summary>
    [JsonPropertyName("unitOfMeasureType")]
    public string? UnitOfMeasureType { get; init; }

    /// <summary>Time when the inventory level record was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>Time when the inventory level record was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// The result of <c>POST /maintenance/parts/stock-movements</c>
/// (<c>createStockMovement</c>, beta): the inventory levels at each end of the
/// movement after it was applied. Mirrors the spec's
/// <c>CreateStockMovementResponseObjectTypeResponseBody</c>.
/// </summary>
public sealed record StockMovementResult
{
    /// <summary>Inventory levels at the source location, for transfers and outbound movements.</summary>
    [JsonPropertyName("sourceLocation")]
    public StockMovementLocation? SourceLocation { get; init; }

    /// <summary>Inventory levels at the destination location, for transfers and inbound movements.</summary>
    [JsonPropertyName("destinationLocation")]
    public StockMovementLocation? DestinationLocation { get; init; }
}

/// <summary>
/// An inventory transaction (beta) returned by
/// <c>GET /maintenance/parts/transactions</c> (<c>listPartTransactions</c>).
/// Mirrors the spec's <c>EntityListPartTransactionsTypeResponseBody</c>.
/// </summary>
public sealed record PartTransaction
{
    /// <summary>Stable unique identifier for the inventory transaction.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The part definition this transaction applies to.</summary>
    [JsonPropertyName("part")]
    public MaintenanceEntityRef? Part { get; init; }

    /// <summary>The work order this transaction was recorded against, when applicable.</summary>
    [JsonPropertyName("workOrder")]
    public MaintenanceEntityRef? WorkOrder { get; init; }

    /// <summary>The kind of inventory movement this record represents.</summary>
    [JsonPropertyName("transactionType")]
    public string? TransactionType { get; init; }

    /// <summary>
    /// Signed net effect on inventory in the part's unit of measure. Positive
    /// adds stock.
    /// </summary>
    [JsonPropertyName("quantity")]
    public double? Quantity { get; init; }

    /// <summary>Absolute on-hand quantity at the place after this transaction was applied.</summary>
    [JsonPropertyName("resultingQuantity")]
    public double? ResultingQuantity { get; init; }

    /// <summary>
    /// Per-unit cost recorded with the transaction, as a bare number (not a
    /// money object). Present on receive transactions.
    /// </summary>
    [JsonPropertyName("unitCost")]
    public double? UnitCost { get; init; }

    /// <summary>Maintenance site (linked place ID) where this transaction occurred.</summary>
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    /// <summary>Transfer only — source maintenance site (place ID).</summary>
    [JsonPropertyName("fromPlaceId")]
    public string? FromPlaceId { get; init; }

    /// <summary>Transfer only — destination maintenance site (place ID).</summary>
    [JsonPropertyName("toPlaceId")]
    public string? ToPlaceId { get; init; }

    /// <summary>Batch or lot identifier, when the part is batch-tracked.</summary>
    [JsonPropertyName("batch")]
    public string? Batch { get; init; }

    /// <summary>Purchase order reference. Present on receive transactions.</summary>
    [JsonPropertyName("purchaseOrder")]
    public string? PurchaseOrder { get; init; }

    /// <summary>Vendor the part was received from. Present on receive transactions.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>Free-text notes. Present on scrap and adjust transactions.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>
    /// User-supplied time the transaction occurred. The window filter and result
    /// ordering use this field.
    /// </summary>
    [JsonPropertyName("happenedAtTime")]
    public string? HappenedAtTime { get; init; }

    /// <summary>Server ingestion timestamp. Not used for filtering or ordering.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>
    /// ID of the user who performed the transaction. Absent on work-order-backed
    /// and system-generated transactions.
    /// </summary>
    [JsonPropertyName("createdByUserId")]
    public string? CreatedByUserId { get; init; }
}
