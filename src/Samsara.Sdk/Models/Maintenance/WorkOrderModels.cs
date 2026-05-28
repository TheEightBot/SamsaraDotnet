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
    [JsonPropertyName("maintenanceSite")] public object? MaintenanceSite { get; init; }
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
    [JsonPropertyName("estimatedPartsCost")] public object? EstimatedPartsCost { get; init; }
    [JsonPropertyName("subcategory")] public string? Subcategory { get; init; }
    // Not in current spec; retained for back-compat.
    [JsonPropertyName("laborCostCents")] public long? LaborCostCents { get; init; }
}

/// <summary>Represents an invoice scan job.</summary>
public sealed record InvoiceScan
{
    [JsonPropertyName("workOrderId")] public required string WorkOrderId { get; init; }
    // Not in current spec; retained for back-compat.
    [JsonPropertyName("id")] public string? Id { get; init; }
    // Not in current spec; retained for back-compat.
    [JsonPropertyName("status")] public string? Status { get; init; }
}

/// <summary>Request body for posting an invoice scan.</summary>
public sealed record PostInvoiceScanRequest
{
    [JsonPropertyName("file")] public required object File { get; init; }
    [JsonPropertyName("assetId")] public string? AssetId { get; init; }
    [JsonPropertyName("workOrderId")] public string? WorkOrderId { get; init; }
    // Not in current spec; retained for back-compat.
    [JsonPropertyName("imageBase64")] public string? ImageBase64 { get; init; }
}
