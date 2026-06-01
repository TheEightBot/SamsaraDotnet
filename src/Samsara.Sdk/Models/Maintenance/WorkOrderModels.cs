namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>Represents a maintenance work order.</summary>
public sealed record WorkOrder
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("assetId")] public required string AssetId { get; init; }
    [JsonPropertyName("status")] public required string Status { get; init; }
    [JsonPropertyName("createdAtTime")] public required DateTimeOffset CreatedAtTime { get; init; }
    [JsonPropertyName("updatedAtTime")] public required DateTimeOffset UpdatedAtTime { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("archivedAtTime")] public DateTimeOffset? ArchivedAtTime { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("closingNotes")] public string? ClosingNotes { get; init; }
    [JsonPropertyName("completedAtTime")] public DateTimeOffset? CompletedAtTime { get; init; }
    [JsonPropertyName("createdByUserId")] public string? CreatedByUserId { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("discount")] public System.Text.Json.JsonElement? Discount { get; init; }
    [JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }
    [JsonPropertyName("engineHours")] public long? EngineHours { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }
    [JsonPropertyName("items")] public IReadOnlyList<System.Text.Json.JsonElement>? Items { get; init; }
    [JsonPropertyName("maintenanceSite")] public WorkOrderMaintenanceSite? MaintenanceSite { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("poNumber")] public string? PoNumber { get; init; }
    [JsonPropertyName("priority")] public string? Priority { get; init; }
    [JsonPropertyName("serviceTaskInstances")] public IReadOnlyList<System.Text.Json.JsonElement>? ServiceTaskInstances { get; init; }
    [JsonPropertyName("tax")] public System.Text.Json.JsonElement? Tax { get; init; }
    [JsonPropertyName("unallocatedLabor")] public System.Text.Json.JsonElement? UnallocatedLabor { get; init; }
    [JsonPropertyName("vendorUuid")] public string? VendorUuid { get; init; }
    [JsonPropertyName("attachments")] public IReadOnlyList<System.Text.Json.JsonElement>? Attachments { get; init; }
}

/// <summary>Request body for creating a work order.</summary>
public sealed record CreateWorkOrderRequest
{
    [JsonPropertyName("assetId")] public required string AssetId { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("discount")] public System.Text.Json.JsonElement? Discount { get; init; }
    [JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }
    [JsonPropertyName("engineHours")] public long? EngineHours { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }
    [JsonPropertyName("items")] public IReadOnlyList<System.Text.Json.JsonElement>? Items { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("placeExternalId")] public string? PlaceExternalId { get; init; }
    [JsonPropertyName("placeId")] public string? PlaceId { get; init; }
    [JsonPropertyName("poNumber")] public string? PoNumber { get; init; }
    [JsonPropertyName("priority")] public string? Priority { get; init; }
    [JsonPropertyName("serviceTaskInstances")] public IReadOnlyList<System.Text.Json.JsonElement>? ServiceTaskInstances { get; init; }
    [JsonPropertyName("tax")] public System.Text.Json.JsonElement? Tax { get; init; }
    [JsonPropertyName("vendorUuid")] public string? VendorUuid { get; init; }
}

/// <summary>Request body for updating a work order.</summary>
public sealed record UpdateWorkOrderRequest
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("closingNotes")] public string? ClosingNotes { get; init; }
    [JsonPropertyName("completedAtTime")] public DateTimeOffset? CompletedAtTime { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("discount")] public System.Text.Json.JsonElement? Discount { get; init; }
    [JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }
    [JsonPropertyName("engineHours")] public long? EngineHours { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }
    [JsonPropertyName("items")] public IReadOnlyList<System.Text.Json.JsonElement>? Items { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("placeExternalId")] public string? PlaceExternalId { get; init; }
    [JsonPropertyName("placeId")] public string? PlaceId { get; init; }
    [JsonPropertyName("poNumber")] public string? PoNumber { get; init; }
    [JsonPropertyName("priority")] public string? Priority { get; init; }
    [JsonPropertyName("serviceTaskInstances")] public IReadOnlyList<System.Text.Json.JsonElement>? ServiceTaskInstances { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("tax")] public System.Text.Json.JsonElement? Tax { get; init; }
    [JsonPropertyName("vendorUuid")] public string? VendorUuid { get; init; }
}

/// <summary>Request body for deleting work orders.</summary>
public sealed record DeleteWorkOrdersRequest
{
    [JsonPropertyName("ids")] public required IReadOnlyList<string> Ids { get; init; }
}

/// <summary>Represents a maintenance service task.</summary>
public sealed record ServiceTask
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("estimatedLaborTimeMinutes")] public int? EstimatedLaborTimeMinutes { get; init; }
    [JsonPropertyName("estimatedPartsCost")] public WorkOrderMoney? EstimatedPartsCost { get; init; }
    [JsonPropertyName("subcategory")] public string? Subcategory { get; init; }
}

/// <summary>Represents an invoice scan job. Mirrors the spec's
/// <c>PostInvoiceScanResponseDataResponseBody</c>.</summary>
public sealed record InvoiceScan
{
    [JsonPropertyName("workOrderId")] public required string WorkOrderId { get; init; }
}

/// <summary>Request body for posting an invoice scan.</summary>
public sealed record PostInvoiceScanRequest
{
    [JsonPropertyName("file")] public required InvoiceScanFile File { get; init; }
    [JsonPropertyName("assetId")] public string? AssetId { get; init; }
    [JsonPropertyName("workOrderId")] public string? WorkOrderId { get; init; }
}

/// <summary>Invoice file payload for <c>POST /maintenance/invoice-scans</c>. Mirrors the spec's
/// <c>InvoiceScanFileRequestBody</c>.</summary>
public sealed record InvoiceScanFile
{
    /// <summary>Base64-encoded file content (maximum decoded size 10MB). Spec-required.</summary>
    [JsonPropertyName("base64Content")] public required string Base64Content { get; init; }

    /// <summary>MIME type of the file. Supported: <c>application/pdf</c>, <c>image/jpeg</c>,
    /// <c>image/png</c>. Spec-required.</summary>
    [JsonPropertyName("contentType")] public required string ContentType { get; init; }
}

/// <summary>The maintenance site (inventory location) where work is performed. Mirrors the spec's
/// <c>WorkOrderMaintenanceSiteObjectResponseBody</c>.</summary>
public sealed record WorkOrderMaintenanceSite
{
    /// <summary>Display name of the maintenance site.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }

    /// <summary>ID of the Place linked to this maintenance site (joinable against the Places API).
    /// Omitted if the site is not linked to a place.</summary>
    [JsonPropertyName("placeId")] public string? PlaceId { get; init; }

    /// <summary>External identifiers for the linked Place. Populated only when the request sets
    /// <c>includeExternalIds=true</c>.</summary>
    [JsonPropertyName("placeExternalIds")] public IReadOnlyDictionary<string, string>? PlaceExternalIds { get; init; }
}

/// <summary>A specified amount of money. Mirrors the spec's
/// <c>WorkOrderMoneyObjectResponseBody</c>.</summary>
public sealed record WorkOrderMoney
{
    /// <summary>Amount of the currency (decimal string, e.g. <c>94.01</c>). Spec-required.</summary>
    [JsonPropertyName("amount")] public required string Amount { get; init; }

    /// <summary>Currency type. Currently only <c>usd</c> is supported. Spec-required.</summary>
    [JsonPropertyName("currency")] public required string Currency { get; init; }
}
