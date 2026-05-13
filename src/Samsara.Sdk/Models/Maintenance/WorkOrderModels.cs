namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>Represents a maintenance work order.</summary>
public sealed record WorkOrder
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("vehicleName")] public string? VehicleName { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("dueDate")] public DateTimeOffset? DueDate { get; init; }
    [JsonPropertyName("completedDate")] public DateTimeOffset? CompletedDate { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
    [JsonPropertyName("laborCostCents")] public long? LaborCostCents { get; init; }
    [JsonPropertyName("partsCostCents")] public long? PartsCostCents { get; init; }
    [JsonPropertyName("serviceTaskIds")] public IReadOnlyList<string>? ServiceTaskIds { get; init; }
    [JsonPropertyName("createdAt")] public DateTimeOffset? CreatedAt { get; init; }
    [JsonPropertyName("updatedAt")] public DateTimeOffset? UpdatedAt { get; init; }
}

/// <summary>Request body for creating a work order.</summary>
public sealed record CreateWorkOrderRequest
{
    [JsonPropertyName("title")] public required string Title { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("dueDate")] public DateTimeOffset? DueDate { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
    [JsonPropertyName("serviceTaskIds")] public IReadOnlyList<string>? ServiceTaskIds { get; init; }
}

/// <summary>Request body for updating a work order.</summary>
public sealed record UpdateWorkOrderRequest
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("dueDate")] public DateTimeOffset? DueDate { get; init; }
    [JsonPropertyName("notes")] public string? Notes { get; init; }
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
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("laborCostCents")] public long? LaborCostCents { get; init; }
}

/// <summary>Represents an invoice scan job.</summary>
public sealed record InvoiceScan
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("workOrderId")] public string? WorkOrderId { get; init; }
}

/// <summary>Request body for posting an invoice scan.</summary>
public sealed record PostInvoiceScanRequest
{
    [JsonPropertyName("imageBase64")] public required string ImageBase64 { get; init; }
    [JsonPropertyName("workOrderId")] public string? WorkOrderId { get; init; }
}
