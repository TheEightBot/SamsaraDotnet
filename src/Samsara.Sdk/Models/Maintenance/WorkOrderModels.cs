namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>Represents a maintenance work order. Mirrors the spec's
/// <c>WorkOrderWithTimeEntriesObjectResponseBody</c> (returned by
/// <c>GET /maintenance/work-orders</c> and <c>GET /maintenance/work-orders/stream</c>) and
/// <c>WorkOrderObjectResponseBody</c> (returned by <c>POST</c>/<c>PATCH</c>). The two spec schemas
/// are identical apart from the element type of <see cref="ServiceTaskInstances"/>, so a single
/// record serves both — see <see cref="ServiceTaskInstance"/>.</summary>
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
    [JsonPropertyName("discount")] public WorkOrderDiscount? Discount { get; init; }
    [JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }
    [JsonPropertyName("engineHours")] public long? EngineHours { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }
    [JsonPropertyName("items")] public IReadOnlyList<WorkOrderItem>? Items { get; init; }
    [JsonPropertyName("maintenanceSite")] public WorkOrderMaintenanceSite? MaintenanceSite { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("poNumber")] public string? PoNumber { get; init; }
    [JsonPropertyName("priority")] public string? Priority { get; init; }
    [JsonPropertyName("serviceTaskInstances")] public IReadOnlyList<ServiceTaskInstance>? ServiceTaskInstances { get; init; }
    [JsonPropertyName("tax")] public WorkOrderTax? Tax { get; init; }
    [JsonPropertyName("unallocatedLabor")] public WorkOrderUnallocatedLabor? UnallocatedLabor { get; init; }
    [JsonPropertyName("vendorUuid")] public string? VendorUuid { get; init; }
    [JsonPropertyName("attachments")] public IReadOnlyList<WorkOrderAttachment>? Attachments { get; init; }

    /// <summary>IDs of the work order template(s) this work order was created from.</summary>
    [JsonPropertyName("workOrderTemplateIds")] public IReadOnlyList<string>? WorkOrderTemplateIds { get; init; }
}

/// <summary>Request body for creating a work order. Mirrors the spec's
/// <c>WorkOrdersPostWorkOrdersRequestBody</c>.</summary>
public sealed record CreateWorkOrderRequest
{
    [JsonPropertyName("assetId")] public required string AssetId { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("discount")] public WorkOrderDiscount? Discount { get; init; }
    [JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }
    [JsonPropertyName("engineHours")] public long? EngineHours { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }
    [JsonPropertyName("items")] public IReadOnlyList<WorkOrderItem>? Items { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("placeExternalId")] public string? PlaceExternalId { get; init; }
    [JsonPropertyName("placeId")] public string? PlaceId { get; init; }
    [JsonPropertyName("poNumber")] public string? PoNumber { get; init; }
    [JsonPropertyName("priority")] public string? Priority { get; init; }
    [JsonPropertyName("serviceTaskInstances")] public IReadOnlyList<ServiceTaskInstanceInput>? ServiceTaskInstances { get; init; }
    [JsonPropertyName("tax")] public WorkOrderTax? Tax { get; init; }
    [JsonPropertyName("vendorUuid")] public string? VendorUuid { get; init; }
}

/// <summary>Request body for updating a work order. Mirrors the spec's
/// <c>WorkOrdersPatchWorkOrdersRequestBody</c>.</summary>
public sealed record UpdateWorkOrderRequest
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("assignedUserId")] public string? AssignedUserId { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("closingNotes")] public string? ClosingNotes { get; init; }
    [JsonPropertyName("completedAtTime")] public DateTimeOffset? CompletedAtTime { get; init; }
    [JsonPropertyName("description")] public string? Description { get; init; }
    [JsonPropertyName("discount")] public WorkOrderDiscount? Discount { get; init; }
    [JsonPropertyName("dueAtTime")] public DateTimeOffset? DueAtTime { get; init; }
    [JsonPropertyName("engineHours")] public long? EngineHours { get; init; }
    [JsonPropertyName("invoiceNumber")] public string? InvoiceNumber { get; init; }
    [JsonPropertyName("items")] public IReadOnlyList<WorkOrderItem>? Items { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("placeExternalId")] public string? PlaceExternalId { get; init; }
    [JsonPropertyName("placeId")] public string? PlaceId { get; init; }
    [JsonPropertyName("poNumber")] public string? PoNumber { get; init; }
    [JsonPropertyName("priority")] public string? Priority { get; init; }
    [JsonPropertyName("serviceTaskInstances")] public IReadOnlyList<ServiceTaskInstanceInput>? ServiceTaskInstances { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("tax")] public WorkOrderTax? Tax { get; init; }
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
/// <c>WorkOrderMoneyObjectResponseBody</c> and the byte-identical
/// <c>WorkOrderMoneyObjectRequestBody</c>, so one record serves both sides.</summary>
public sealed record WorkOrderMoney
{
    /// <summary>Amount of the currency (decimal string, e.g. <c>94.01</c>). Spec-required.</summary>
    [JsonPropertyName("amount")] public required string Amount { get; init; }

    /// <summary>Currency type. Currently only <c>usd</c> is supported. Spec-required.</summary>
    [JsonPropertyName("currency")] public required string Currency { get; init; }
}

/// <summary>How much a work order is discounted. Either <see cref="Money"/> or
/// <see cref="BasisPoints"/> is specified, never both. Mirrors the spec's
/// <c>WorkOrderDiscountObjectResponseBody</c> and the byte-identical
/// <c>WorkOrderDiscountObjectRequestBody</c>, so one record serves both sides.</summary>
public sealed record WorkOrderDiscount
{
    /// <summary>The discount in basis points. 100 basis points = 1%.</summary>
    [JsonPropertyName("basisPoints")] public long? BasisPoints { get; init; }

    /// <summary>The discount as a fixed amount of money.</summary>
    [JsonPropertyName("money")] public WorkOrderMoney? Money { get; init; }
}

/// <summary>How much tax is applied to a work order. Either <see cref="Money"/> or
/// <see cref="BasisPoints"/> is specified, never both. Mirrors the spec's
/// <c>WorkOrderTaxObjectResponseBody</c> and the byte-identical request-side schemas
/// <c>WorkOrderTaxObjectRequestBody</c> (PATCH) and <c>WorkOrderTaxCreateObjectRequestBody</c>
/// (POST), so one record serves all three.</summary>
public sealed record WorkOrderTax
{
    /// <summary>The tax in basis points. 100 basis points = 1%.</summary>
    [JsonPropertyName("basisPoints")] public long? BasisPoints { get; init; }

    /// <summary>The tax as a fixed amount of money.</summary>
    [JsonPropertyName("money")] public WorkOrderMoney? Money { get; init; }
}

/// <summary>An item (DVIR, fault, form, issue, …) related to a work order. Mirrors the spec's
/// <c>WorkOrderItemObjectResponseBody</c> and the byte-identical
/// <c>WorkOrderItemObjectRequestBody</c>, so one record serves both sides; the
/// <c>required</c> modifiers reflect the request-side contract (both fields are spec-required on
/// every mapped schema).</summary>
public sealed record WorkOrderItem
{
    /// <summary>ID of the item. Spec-required.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>The type of item. Valid values: <c>DVIR</c>, <c>FAULT</c>, <c>FORM</c>,
    /// <c>ISSUE</c>, <c>ITEM_TYPE_UNSPECIFIED</c>, <c>MAINTENANCE_PREDICTION_EVENT</c>,
    /// <c>SCHEDULED_MAINTENANCE</c>. Spec-required.</summary>
    [JsonPropertyName("type")] public required string Type { get; init; }
}

/// <summary>Unallocated labor from time entries not associated with any service task. Mirrors the
/// spec's <c>WorkOrderUnallocatedLaborObjectResponseBody</c> (response-only schema).</summary>
public sealed record WorkOrderUnallocatedLabor
{
    /// <summary>The total cost of the unallocated labor.</summary>
    [JsonPropertyName("cost")] public WorkOrderMoney? Cost { get; init; }

    /// <summary>The total unallocated labor time in minutes.</summary>
    [JsonPropertyName("timeMinutes")] public long? TimeMinutes { get; init; }
}

/// <summary>A file attached to a work order. Mirrors the spec's
/// <c>WorkOrderAttachmentObjectResponseBody</c> (response-only schema).</summary>
public sealed record WorkOrderAttachment
{
    /// <summary>ID of the media record.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>Status of the media record. Valid values: <c>unknown</c>, <c>processing</c>,
    /// <c>finished</c>.</summary>
    [JsonPropertyName("processingStatus")] public string? ProcessingStatus { get; init; }

    /// <summary>URL containing a link to the associated media content. Included only if
    /// <see cref="ProcessingStatus"/> is <c>finished</c>.</summary>
    [JsonPropertyName("url")] public string? Url { get; init; }

    /// <summary>Expiration time of <see cref="Url"/> in RFC 3339 (UTC) format.</summary>
    [JsonPropertyName("urlExpiresAt")] public DateTimeOffset? UrlExpiresAt { get; init; }
}

/// <summary>A service task attached to a work order. Mirrors the spec's
/// <c>ServiceTaskInstanceWithTimeEntriesObjectResponseBody</c>, which is a strict superset of
/// <c>ServiceTaskInstanceObjectResponseBody</c> (it adds <see cref="TimeEntries"/>). Since a single
/// <see cref="WorkOrder"/> record serves both work-order response schemas, this one record serves
/// both service-task-instance schemas: <see cref="TimeEntries"/> is populated only by
/// <c>GET /maintenance/work-orders</c> and <c>GET /maintenance/work-orders/stream</c>, and only for
/// organizations with maintenance time tracking enabled.</summary>
public sealed record ServiceTaskInstance
{
    /// <summary>ID of the service task instance.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>The hourly labor cost for the service task.</summary>
    [JsonPropertyName("laborHourlyCost")] public WorkOrderMoney? LaborHourlyCost { get; init; }

    /// <summary>The time of labor needed, in minutes.</summary>
    [JsonPropertyName("laborTimeMinutes")] public int? LaborTimeMinutes { get; init; }

    /// <summary>Free-form technician notes for the service task.</summary>
    [JsonPropertyName("notes")] public string? Notes { get; init; }

    /// <summary>Parts used by the service task.</summary>
    [JsonPropertyName("parts")] public IReadOnlyList<PartInstance>? Parts { get; init; }

    /// <summary>The total parts cost for the service task.</summary>
    [JsonPropertyName("partsCost")] public WorkOrderMoney? PartsCost { get; init; }

    /// <summary>ID of the service task definition this instance was created from.</summary>
    [JsonPropertyName("serviceTaskId")] public string? ServiceTaskId { get; init; }

    /// <summary>The status of the service task. Valid values: <c>Unknown</c>, <c>Open</c>,
    /// <c>In Progress</c>, <c>On Hold</c>, <c>Completed</c>.</summary>
    [JsonPropertyName("status")] public string? Status { get; init; }

    /// <summary>Subtasks for the service task.</summary>
    [JsonPropertyName("subtasks")] public IReadOnlyList<ServiceTaskSubtask>? Subtasks { get; init; }

    /// <summary>Technician time entries logged against this service task. Returned only by the
    /// list and stream endpoints (spec schema
    /// <c>ServiceTaskInstanceWithTimeEntriesObjectResponseBody</c>) and only for organizations with
    /// maintenance time tracking enabled.</summary>
    [JsonPropertyName("timeEntries")] public IReadOnlyList<WorkOrderTimeEntry>? TimeEntries { get; init; }
}

/// <summary>A part used by a service task on a work order. Mirrors the spec's
/// <c>PartInstanceObjectResponseBody</c> (response-only schema; the request-side twin is
/// <see cref="PartInstanceInput"/>, which differs in required-ness).</summary>
public sealed record PartInstance
{
    /// <summary>The cost of one unit of the part in cents. Overrides the part's defined cost.</summary>
    [JsonPropertyName("costCentsOverride")] public long? CostCentsOverride { get; init; }

    /// <summary>ID of the part instance.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>ID of the part definition.</summary>
    [JsonPropertyName("partId")] public string? PartId { get; init; }

    /// <summary>The quantity of the part, in the part's predefined unit of measure.</summary>
    [JsonPropertyName("quantity")] public double? Quantity { get; init; }
}

/// <summary>A subtask of a service task. Exactly one of <see cref="Form"/> or
/// <see cref="Procedure"/> is populated. Mirrors the spec's
/// <c>ServiceTaskSubtaskObjectResponseBody</c> (response-only schema).</summary>
public sealed record ServiceTaskSubtask
{
    /// <summary>The form subtask, when this subtask is a form.</summary>
    [JsonPropertyName("form")] public ServiceTaskFormSubtask? Form { get; init; }

    /// <summary>The procedure subtask, when this subtask is a procedure.</summary>
    [JsonPropertyName("procedure")] public ServiceTaskProcedureSubtask? Procedure { get; init; }
}

/// <summary>A form subtask of a service task. Mirrors the spec's
/// <c>ServiceTaskFormSubtaskObjectResponseBody</c> (response-only schema).</summary>
public sealed record ServiceTaskFormSubtask
{
    /// <summary>The UUID of the form submission.</summary>
    [JsonPropertyName("formSubmissionUuid")] public string? FormSubmissionUuid { get; init; }
}

/// <summary>A procedure subtask of a service task. Mirrors the spec's
/// <c>ServiceTaskProcedureSubtaskObjectResponseBody</c> (response-only schema).</summary>
public sealed record ServiceTaskProcedureSubtask
{
    /// <summary>Whether the procedure is complete.</summary>
    [JsonPropertyName("isCompleted")] public bool? IsCompleted { get; init; }

    /// <summary>The name of the procedure.</summary>
    [JsonPropertyName("name")] public string? Name { get; init; }
}

/// <summary>A technician time entry logged against a service task on a work order. Mirrors the
/// spec's <c>WorkOrderTimeEntryObjectResponseBody</c> (response-only schema).</summary>
public sealed record WorkOrderTimeEntry
{
    /// <summary>Time the technician clocked in to this service task, in RFC 3339 (UTC) format.</summary>
    [JsonPropertyName("clockInAtTime")] public DateTimeOffset? ClockInAtTime { get; init; }

    /// <summary>Time the technician clocked out of this service task, in RFC 3339 (UTC) format.
    /// Null while the technician is still clocked in.</summary>
    [JsonPropertyName("clockOutAtTime")] public DateTimeOffset? ClockOutAtTime { get; init; }

    /// <summary>Samsara driver ID linked to the technician who performed the work.</summary>
    [JsonPropertyName("driverId")] public string? DriverId { get; init; }

    /// <summary>Samsara dashboard user ID linked to the technician who performed the work.</summary>
    [JsonPropertyName("userId")] public string? UserId { get; init; }
}

/// <summary>A service task supplied when creating or updating a work order. Mirrors the spec's
/// <c>ServiceTaskInstanceInputObjectRequestBody</c>. Named with the <c>Input</c> suffix the spec
/// itself uses, because the response-side shape (<see cref="ServiceTaskInstance"/>) is a different
/// schema — it carries <c>subtasks</c>/<c>timeEntries</c> and marks <c>id</c> required, while this
/// one omits both and leaves <c>id</c> optional.</summary>
public sealed record ServiceTaskInstanceInput
{
    /// <summary>ID of the service task instance. Set only when updating an existing instance.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>The hourly labor cost for the service task.</summary>
    [JsonPropertyName("laborHourlyCost")] public WorkOrderMoney? LaborHourlyCost { get; init; }

    /// <summary>The time of labor needed, in minutes.</summary>
    [JsonPropertyName("laborTimeMinutes")] public int? LaborTimeMinutes { get; init; }

    /// <summary>Free-form technician notes for the service task.</summary>
    [JsonPropertyName("notes")] public string? Notes { get; init; }

    /// <summary>Parts for the service task.</summary>
    [JsonPropertyName("parts")] public IReadOnlyList<PartInstanceInput>? Parts { get; init; }

    /// <summary>The total parts cost for the service task.</summary>
    [JsonPropertyName("partsCost")] public WorkOrderMoney? PartsCost { get; init; }

    /// <summary>ID of the service task definition. Spec-required.</summary>
    [JsonPropertyName("serviceTaskId")] public required string ServiceTaskId { get; init; }

    /// <summary>The status of the service task. Valid values: <c>Unknown</c>, <c>Open</c>,
    /// <c>In Progress</c>, <c>On Hold</c>, <c>Completed</c>. Spec-required.</summary>
    [JsonPropertyName("status")] public required string Status { get; init; }
}

/// <summary>A part supplied on a service task when creating or updating a work order. Mirrors the
/// spec's <c>PartInstanceInputObjectRequestBody</c>. Kept separate from the response-side
/// <see cref="PartInstance"/> because the required-ness differs: the spec requires
/// <c>partId</c>/<c>quantity</c> here and additionally requires <c>id</c> on the response
/// schema.</summary>
public sealed record PartInstanceInput
{
    /// <summary>The cost of one unit of the part in cents. If omitted, the part's defined cost is
    /// used.</summary>
    [JsonPropertyName("costCentsOverride")] public long? CostCentsOverride { get; init; }

    /// <summary>ID of the part instance. Set only when updating an existing part instance.</summary>
    [JsonPropertyName("id")] public string? Id { get; init; }

    /// <summary>ID of the part definition. Spec-required.</summary>
    [JsonPropertyName("partId")] public required string PartId { get; init; }

    /// <summary>The quantity of the part, in the part's predefined unit of measure.
    /// Spec-required.</summary>
    [JsonPropertyName("quantity")] public required double Quantity { get; init; }
}
