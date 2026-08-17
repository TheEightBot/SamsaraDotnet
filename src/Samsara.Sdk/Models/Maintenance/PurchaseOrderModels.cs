namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>
/// A maintenance purchase order (beta). Mirrors the spec's
/// <c>EntityListPurchaseOrdersTypeResponseBody</c> and its byte-identical
/// create/update twins, so one record serves <c>GET</c>, <c>POST</c> and
/// <c>PATCH</c> <c>/maintenance/purchase-orders</c>.
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// Timestamps on these schemas are declared <c>type: string</c> with no
/// <c>format: date-time</c>, so they are modelled as <c>string</c>.
/// </remarks>
public sealed record PurchaseOrder
{
    /// <summary>Stable Samsara ID for the purchase order.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Customer-visible purchase order number.</summary>
    [JsonPropertyName("poNumber")]
    public string? PoNumber { get; init; }

    /// <summary>Optional prefix included in the purchase order number.</summary>
    [JsonPropertyName("poNumberPrefix")]
    public string? PoNumberPrefix { get; init; }

    /// <summary>Optional suffix included in the purchase order number.</summary>
    [JsonPropertyName("poNumberSuffix")]
    public string? PoNumberSuffix { get; init; }

    /// <summary>Current customer-visible status of the purchase order.</summary>
    [JsonPropertyName("orderStatus")]
    public string? OrderStatus { get; init; }

    /// <summary>Source that created the purchase order.</summary>
    [JsonPropertyName("creationSource")]
    public string? CreationSource { get; init; }

    /// <summary>The vendor supplying this purchase order.</summary>
    [JsonPropertyName("vendor")]
    public MaintenanceEntityRef? Vendor { get; init; }

    /// <summary>Parts ordered on the purchase order.</summary>
    [JsonPropertyName("parts")]
    public IReadOnlyList<PurchaseOrderPart>? Parts { get; init; }

    /// <summary>Costs on the purchase order not attributable to a part line.</summary>
    [JsonPropertyName("otherCost")]
    public MaintenanceMoney? OtherCost { get; init; }

    /// <summary>General ledger code associated with this purchase order.</summary>
    [JsonPropertyName("glCode")]
    public string? GlCode { get; init; }

    /// <summary>Vendor invoice number associated with this purchase order.</summary>
    [JsonPropertyName("invoiceNumber")]
    public string? InvoiceNumber { get; init; }

    /// <summary>Shipment tracking number for the purchase order.</summary>
    [JsonPropertyName("trackingNumber")]
    public string? TrackingNumber { get; init; }

    /// <summary>Free-text notes for the purchase order.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>IDs of media items attached to the purchase order.</summary>
    [JsonPropertyName("mediaItemIds")]
    public IReadOnlyList<string>? MediaItemIds { get; init; }

    /// <summary>RFC 3339 expected delivery time.</summary>
    [JsonPropertyName("deliveryAtTime")]
    public string? DeliveryAtTime { get; init; }

    /// <summary>RFC 3339 time when the purchase order was sent to its vendor.</summary>
    [JsonPropertyName("sentAtTime")]
    public string? SentAtTime { get; init; }

    /// <summary>RFC 3339 time when the first item was received.</summary>
    [JsonPropertyName("firstReceivedAtTime")]
    public string? FirstReceivedAtTime { get; init; }

    /// <summary>RFC 3339 time when all items were received.</summary>
    [JsonPropertyName("fullyReceivedAtTime")]
    public string? FullyReceivedAtTime { get; init; }

    /// <summary>RFC 3339 time when the purchase order was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>RFC 3339 time when the purchase order was last updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// One part line on a <see cref="PurchaseOrder"/>. Mirrors the spec's
/// <c>...PurchaseOrderPurchaseOrderPartTypeResponseBody</c> variants.
/// </summary>
public sealed record PurchaseOrderPart
{
    /// <summary>Stable identifier for the purchase order line.</summary>
    [JsonPropertyName("lineItemId")]
    public string? LineItemId { get; init; }

    /// <summary>The part definition ordered on this line.</summary>
    [JsonPropertyName("partSamsara")]
    public MaintenanceEntityRef? PartSamsara { get; init; }

    /// <summary>The place linked to the maintenance site holding this line's inventory.</summary>
    [JsonPropertyName("place")]
    public MaintenanceEntityRef? Place { get; init; }

    /// <summary>Description of the ordered part.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Batch or lot number for the ordered part.</summary>
    [JsonPropertyName("batchNumber")]
    public string? BatchNumber { get; init; }

    /// <summary>Quantity ordered on this line.</summary>
    [JsonPropertyName("quantityOrdered")]
    public double? QuantityOrdered { get; init; }

    /// <summary>Quantity received on this line.</summary>
    [JsonPropertyName("quantityReceived")]
    public double? QuantityReceived { get; init; }

    /// <summary>Unit of measure for quantities on this line.</summary>
    [JsonPropertyName("unitOfMeasureType")]
    public string? UnitOfMeasureType { get; init; }

    /// <summary>Per-unit cost for this line.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoney? UnitCost { get; init; }

    /// <summary>The returnable-core charge attached to this line, when any.</summary>
    [JsonPropertyName("coreCharge")]
    public PurchaseOrderCoreCharge? CoreCharge { get; init; }
}

/// <summary>
/// The returnable-core charge on a <see cref="PurchaseOrderPart"/>. Mirrors the
/// spec's <c>...PurchaseOrderPurchaseOrderCoreChargeTypeResponseBody</c>.
/// </summary>
public sealed record PurchaseOrderCoreCharge
{
    /// <summary>Whether the core charge is active, removed or disabled.</summary>
    [JsonPropertyName("coreChargeStatus")]
    public string? CoreChargeStatus { get; init; }

    /// <summary>The returnable core part.</summary>
    [JsonPropertyName("corePartSamsara")]
    public MaintenanceEntityRef? CorePartSamsara { get; init; }

    /// <summary>The vendor that receives returned cores.</summary>
    [JsonPropertyName("returnRecipientVendor")]
    public MaintenanceEntityRef? ReturnRecipientVendor { get; init; }

    /// <summary>Per-unit core amount charged.</summary>
    [JsonPropertyName("unitCoreAmount")]
    public MaintenanceMoney? UnitCoreAmount { get; init; }

    /// <summary>How long the core remains recoverable.</summary>
    [JsonPropertyName("recoverabilityPolicy")]
    public PurchaseOrderCoreRecoverabilityPolicy? RecoverabilityPolicy { get; init; }
}

/// <summary>
/// The recoverability window for a returnable core. Mirrors the spec's
/// <c>...PurchaseOrderCoreRecoverabilityPolicyTypeResponseBody</c>.
/// </summary>
public sealed record PurchaseOrderCoreRecoverabilityPolicy
{
    /// <summary>Recoverability policy type.</summary>
    [JsonPropertyName("policyType")]
    public string? PolicyType { get; init; }

    /// <summary>Absolute deadline for core return.</summary>
    [JsonPropertyName("fixedRecoverableUntilTime")]
    public string? FixedRecoverableUntilTime { get; init; }

    /// <summary>
    /// Duration after receipt when the core must be returned, in milliseconds.
    /// </summary>
    [JsonPropertyName("relativeWindowDuration")]
    public long? RelativeWindowDuration { get; init; }
}

/// <summary>
/// The recoverability window supplied on a purchase-order request body. Mirrors
/// the spec's <c>...CoreRecoverabilityPolicyInputTypeRequestBody</c>, which
/// marks <c>policyType</c> REQUIRED.
/// </summary>
public sealed record PurchaseOrderCoreRecoverabilityPolicyInput
{
    /// <summary>Recoverability policy type. Spec REQUIRED.</summary>
    [JsonPropertyName("policyType")]
    public required string PolicyType { get; init; }

    /// <summary>Absolute deadline for core return.</summary>
    [JsonPropertyName("fixedRecoverableUntilTime")]
    public string? FixedRecoverableUntilTime { get; init; }

    /// <summary>
    /// Duration after receipt when the core must be returned, in milliseconds.
    /// </summary>
    [JsonPropertyName("relativeWindowDuration")]
    public long? RelativeWindowDuration { get; init; }
}

/// <summary>
/// The returnable-core charge supplied on a purchase-order request body.
/// Mirrors the spec's <c>...PurchaseOrderCoreChargeInputTypeRequestBody</c>,
/// which identifies the core part and return vendor by bare ID and marks
/// <c>coreChargeStatus</c> REQUIRED.
/// </summary>
public sealed record PurchaseOrderCoreChargeInput
{
    /// <summary>Whether the core charge is active, removed or disabled. Spec REQUIRED.</summary>
    [JsonPropertyName("coreChargeStatus")]
    public required string CoreChargeStatus { get; init; }

    /// <summary>ID of the returnable core part.</summary>
    [JsonPropertyName("corePartSamsaraId")]
    public string? CorePartSamsaraId { get; init; }

    /// <summary>ID of the vendor that receives returned cores.</summary>
    [JsonPropertyName("returnRecipientVendorId")]
    public string? ReturnRecipientVendorId { get; init; }

    /// <summary>Per-unit core amount charged.</summary>
    [JsonPropertyName("unitCoreAmount")]
    public MaintenanceMoneyInput? UnitCoreAmount { get; init; }

    /// <summary>How long the core remains recoverable.</summary>
    [JsonPropertyName("recoverabilityPolicy")]
    public PurchaseOrderCoreRecoverabilityPolicyInput? RecoverabilityPolicy { get; init; }
}

/// <summary>
/// One part line supplied on a purchase-order request body. Mirrors the spec's
/// <c>...PurchaseOrderPurchaseOrderPartInput...TypeRequestBody</c> variants,
/// which identify the part and place by bare ID and mark four members REQUIRED.
/// </summary>
public sealed record PurchaseOrderPartInput
{
    /// <summary>ID of the part definition ordered on this line. Spec REQUIRED.</summary>
    [JsonPropertyName("partSamsaraId")]
    public required string PartSamsaraId { get; init; }

    /// <summary>Quantity ordered on this line. Spec REQUIRED.</summary>
    [JsonPropertyName("quantityOrdered")]
    public required double QuantityOrdered { get; init; }

    /// <summary>Quantity received on this line. Spec REQUIRED.</summary>
    [JsonPropertyName("quantityReceived")]
    public required double QuantityReceived { get; init; }

    /// <summary>Unit of measure for quantities on this line. Spec REQUIRED.</summary>
    [JsonPropertyName("unitOfMeasureType")]
    public required string UnitOfMeasureType { get; init; }

    /// <summary>Stable identifier for the purchase order line. Omit to add a new line.</summary>
    [JsonPropertyName("lineItemId")]
    public string? LineItemId { get; init; }

    /// <summary>
    /// Place linked to the maintenance site holding this line's inventory.
    /// </summary>
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    /// <summary>Description of the ordered part.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Batch or lot number for the ordered part.</summary>
    [JsonPropertyName("batchNumber")]
    public string? BatchNumber { get; init; }

    /// <summary>Per-unit cost for this line.</summary>
    [JsonPropertyName("unitCost")]
    public MaintenanceMoneyInput? UnitCost { get; init; }

    /// <summary>The returnable-core charge attached to this line, when any.</summary>
    [JsonPropertyName("coreCharge")]
    public PurchaseOrderCoreChargeInput? CoreCharge { get; init; }
}

/// <summary>
/// Request body for <c>POST /maintenance/purchase-orders</c>
/// (<c>createPurchaseOrder</c>, beta). Mirrors the spec's
/// <c>EntityPurchaseOrdersServiceCreatePurchaseOrderRequestBody</c>.
/// </summary>
public sealed record CreatePurchaseOrderRequest
{
    /// <summary>Current customer-visible status of the purchase order. Spec REQUIRED.</summary>
    [JsonPropertyName("orderStatus")]
    public required string OrderStatus { get; init; }

    /// <summary>ID of the vendor supplying this purchase order. Spec REQUIRED.</summary>
    [JsonPropertyName("vendorId")]
    public required string VendorId { get; init; }

    /// <summary>Optional prefix included in the purchase order number.</summary>
    [JsonPropertyName("poNumberPrefix")]
    public string? PoNumberPrefix { get; init; }

    /// <summary>Optional suffix included in the purchase order number.</summary>
    [JsonPropertyName("poNumberSuffix")]
    public string? PoNumberSuffix { get; init; }

    /// <summary>Parts ordered on the purchase order.</summary>
    [JsonPropertyName("parts")]
    public IReadOnlyList<PurchaseOrderPartInput>? Parts { get; init; }

    /// <summary>Costs on the purchase order not attributable to a part line.</summary>
    [JsonPropertyName("otherCost")]
    public MaintenanceMoneyInput? OtherCost { get; init; }

    /// <summary>General ledger code associated with this purchase order.</summary>
    [JsonPropertyName("glCode")]
    public string? GlCode { get; init; }

    /// <summary>Vendor invoice number associated with this purchase order.</summary>
    [JsonPropertyName("invoiceNumber")]
    public string? InvoiceNumber { get; init; }

    /// <summary>Shipment tracking number for the purchase order.</summary>
    [JsonPropertyName("trackingNumber")]
    public string? TrackingNumber { get; init; }

    /// <summary>Free-text notes for the purchase order.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /maintenance/purchase-orders</c>
/// (<c>updatePurchaseOrder</c>, beta). Mirrors the spec's
/// <c>EntityPurchaseOrdersServiceUpdatePurchaseOrderRequestBody</c>, which marks
/// nothing required and drops the <c>poNumberPrefix</c> / <c>poNumberSuffix</c>
/// members the create body accepts.
/// </summary>
public sealed record UpdatePurchaseOrderRequest
{
    /// <summary>Current customer-visible status of the purchase order.</summary>
    [JsonPropertyName("orderStatus")]
    public string? OrderStatus { get; init; }

    /// <summary>ID of the vendor supplying this purchase order.</summary>
    [JsonPropertyName("vendorId")]
    public string? VendorId { get; init; }

    /// <summary>Parts ordered on the purchase order.</summary>
    [JsonPropertyName("parts")]
    public IReadOnlyList<PurchaseOrderPartInput>? Parts { get; init; }

    /// <summary>Costs on the purchase order not attributable to a part line.</summary>
    [JsonPropertyName("otherCost")]
    public MaintenanceMoneyInput? OtherCost { get; init; }

    /// <summary>General ledger code associated with this purchase order.</summary>
    [JsonPropertyName("glCode")]
    public string? GlCode { get; init; }

    /// <summary>Vendor invoice number associated with this purchase order.</summary>
    [JsonPropertyName("invoiceNumber")]
    public string? InvoiceNumber { get; init; }

    /// <summary>Shipment tracking number for the purchase order.</summary>
    [JsonPropertyName("trackingNumber")]
    public string? TrackingNumber { get; init; }

    /// <summary>Free-text notes for the purchase order.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}
