namespace Samsara.Sdk.Models.Preview;

using System.Text.Json.Serialization;

/// <summary>
/// A live fleet order (preview). Mirrors the spec's
/// <c>FleetOrderObjectResponseBody</c>, the payload of
/// <c>GET /preview/fleet/orders</c>, <c>GET /preview/fleet/orders/stream</c> and
/// each successful item of <c>POST /preview/fleet/orders/batch</c>.
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record FleetOrder
{
    /// <summary>Samsara-generated canonical order UUID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Human-readable order label.</summary>
    [JsonPropertyName("samsaraCustomerOrderName")]
    public string? SamsaraCustomerOrderName { get; init; }

    /// <summary>
    /// Org-scoped external identifiers. The spec declares this a free-form
    /// object; the SDK surfaces the string map the API documents by example.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Order-specific customer metadata. Spec marks REQUIRED.</summary>
    [JsonPropertyName("customerProperties")]
    public IReadOnlyList<FleetOrderCustomerProperty>? CustomerProperties { get; init; }

    /// <summary>Live order tasks. Spec marks REQUIRED.</summary>
    [JsonPropertyName("tasks")]
    public IReadOnlyList<FleetOrderTask>? Tasks { get; init; }

    /// <summary>Creation timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Last update timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// A customer-defined key/value pair on an order or task. Mirrors the spec's
/// <c>FleetOrderCustomerPropertyObjectResponseBody</c> and its request twin
/// <c>FleetOrderBatchCustomerPropertyInputRequestBody</c> — the two carry the
/// same two members, so one record serves both directions.
/// </summary>
public sealed record FleetOrderCustomerProperty
{
    /// <summary>Customer-defined property key.</summary>
    [JsonPropertyName("key")]
    public string? Key { get; init; }

    /// <summary>Customer-defined property value.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// A task on a live fleet order. Mirrors the spec's
/// <c>FleetOrderTaskObjectResponseBody</c>.
/// </summary>
public sealed record FleetOrderTask
{
    /// <summary>Opaque task ID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Hub UUID for the task. Spec marks REQUIRED.</summary>
    [JsonPropertyName("hubId")]
    public string? HubId { get; init; }

    /// <summary>
    /// Task type: <c>unknown</c>, <c>delivery</c>, <c>pickup</c> or
    /// <c>pickupDelivery</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("taskType")]
    public string? TaskType { get; init; }

    /// <summary>
    /// Task position constraint: <c>unknown</c>, <c>none</c>, <c>first</c> or
    /// <c>last</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("positionConstraintType")]
    public string? PositionConstraintType { get; init; }

    /// <summary>Task-specific customer metadata. Spec marks REQUIRED.</summary>
    [JsonPropertyName("customerProperties")]
    public IReadOnlyList<FleetOrderCustomerProperty>? CustomerProperties { get; init; }

    /// <summary>Task quantities. Spec marks REQUIRED.</summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<FleetOrderQuantity>? Quantities { get; init; }

    /// <summary>Task service windows. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<FleetOrderServiceWindow>? ServiceWindows { get; init; }

    /// <summary>Where the task is serviced.</summary>
    [JsonPropertyName("serviceLocation")]
    public FleetOrderServiceLocation? ServiceLocation { get; init; }

    /// <summary>Expected service duration in seconds.</summary>
    [JsonPropertyName("serviceDurationSeconds")]
    public long? ServiceDurationSeconds { get; init; }

    /// <summary>Route ID when this task is attached to a route.</summary>
    [JsonPropertyName("routeId")]
    public string? RouteId { get; init; }

    /// <summary>Dispatcher-visible note.</summary>
    [JsonPropertyName("dispatcherNotes")]
    public string? DispatcherNotes { get; init; }

    /// <summary>Driver-visible note.</summary>
    [JsonPropertyName("driverNotes")]
    public string? DriverNotes { get; init; }

    /// <summary>Creation timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Last update timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// A quantity on a fleet order task. Mirrors the spec's
/// <c>FleetOrderQuantityObjectResponseBody</c>.
/// </summary>
public sealed record FleetOrderQuantity
{
    /// <summary>Opaque quantity UUID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Human-readable quantity label. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>Quantity value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }

    /// <summary>Capacity UUID when this quantity uses a configured capacity.</summary>
    [JsonPropertyName("capacityId")]
    public string? CapacityId { get; init; }
}

/// <summary>
/// A service window on a fleet order task. Mirrors the spec's
/// <c>FleetOrderServiceWindowObjectResponseBody</c> and its upsert twin
/// <c>FleetOrderBatchServiceWindowUpsertInputRequestBody</c> — the two carry the
/// same three members, so one record serves both directions. On the upsert side,
/// omit <c>id</c> to create a new window.
/// </summary>
public sealed record FleetOrderServiceWindow
{
    /// <summary>Opaque service window UUID.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Service window start in RFC 3339 format.</summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>Service window end in RFC 3339 format.</summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// Where a fleet order task is serviced. Mirrors the spec's
/// <c>FleetOrderServiceLocationObjectResponseBody</c> and its request twin
/// <c>FleetOrderBatchServiceLocationInputRequestBody</c> — the two carry the
/// same three members, so one record serves both directions.
/// </summary>
public sealed record FleetOrderServiceLocation
{
    /// <summary>
    /// Location discriminator: <c>unknown</c>, <c>savedAddress</c> or
    /// <c>customAddress</c>. Required on the request side (must be
    /// <c>savedAddress</c> or <c>customAddress</c>).
    /// </summary>
    [JsonPropertyName("serviceLocationType")]
    public string? ServiceLocationType { get; init; }

    /// <summary>Saved address ID. Required for <c>savedAddress</c> locations.</summary>
    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    /// <summary>Ad-hoc address, for <c>customAddress</c> locations.</summary>
    [JsonPropertyName("customAddress")]
    public FleetOrderCustomAddress? CustomAddress { get; init; }
}

/// <summary>
/// An ad-hoc address on a fleet order task. Mirrors the spec's
/// <c>FleetOrderCustomAddressObjectResponseBody</c> and its byte-identical
/// request twin <c>FleetOrderBatchCustomAddressInputRequestBody</c>.
/// </summary>
public sealed record FleetOrderCustomAddress
{
    /// <summary>First address line.</summary>
    [JsonPropertyName("addressLine1")]
    public string? AddressLine1 { get; init; }

    /// <summary>Second address line.</summary>
    [JsonPropertyName("addressLine2")]
    public string? AddressLine2 { get; init; }

    /// <summary>City.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>State, province, or region.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Postal code.</summary>
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    /// <summary>Country.</summary>
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    /// <summary>Human-readable formatted address.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>Latitude in decimal degrees. Must be between -90 and 90.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Must be between -180 and 180.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// A soft-deletion marker for a fleet order, returned by
/// <c>GET /preview/fleet/orders/deletions</c> (<c>getOrderDeletions</c>).
/// Mirrors the spec's <c>FleetOrderDeletionMarkerObjectResponseBody</c>.
/// </summary>
public sealed record FleetOrderDeletionMarker
{
    /// <summary>Samsara-generated canonical order UUID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Deletion timestamp in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("deletedAtTime")]
    public DateTimeOffset? DeletedAtTime { get; init; }
}

/// <summary>
/// A quantity supplied on a fleet order batch upsert. Mirrors the spec's
/// <c>FleetOrderBatchQuantityInputRequestBody</c>, which — unlike its response
/// twin — has no <c>id</c> member.
/// </summary>
public sealed record FleetOrderBatchQuantityInput
{
    /// <summary>Human-readable quantity label. Required by the endpoint's validation.</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>Finite, non-negative quantity value. Required by the endpoint's validation.</summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }

    /// <summary>Optional capacity UUID for the task hub.</summary>
    [JsonPropertyName("capacityId")]
    public string? CapacityId { get; init; }
}

/// <summary>
/// A task to create or update in a fleet order batch upsert. Mirrors the spec's
/// <c>FleetOrderBatchTaskUpsertInputRequestBody</c>.
/// </summary>
public sealed record FleetOrderBatchTaskInput
{
    /// <summary>Hub UUID for this task. Spec REQUIRED.</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>
    /// Task type. Spec REQUIRED — must be <c>delivery</c>, <c>pickup</c> or
    /// <c>pickupDelivery</c>.
    /// </summary>
    [JsonPropertyName("taskType")]
    public required string TaskType { get; init; }

    /// <summary>Existing task ID. Omit to create a task.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Task position constraint. Must be <c>none</c>, <c>first</c> or <c>last</c>.</summary>
    [JsonPropertyName("positionConstraintType")]
    public string? PositionConstraintType { get; init; }

    /// <summary>Task-specific customer metadata.</summary>
    [JsonPropertyName("customerProperties")]
    public IReadOnlyList<FleetOrderCustomerProperty>? CustomerProperties { get; init; }

    /// <summary>Task quantities.</summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<FleetOrderBatchQuantityInput>? Quantities { get; init; }

    /// <summary>Service windows to create or update.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<FleetOrderServiceWindow>? ServiceWindows { get; init; }

    /// <summary>IDs of service windows to remove from this task.</summary>
    [JsonPropertyName("serviceWindowIdsToRemove")]
    public IReadOnlyList<string>? ServiceWindowIdsToRemove { get; init; }

    /// <summary>Where the task is serviced.</summary>
    [JsonPropertyName("serviceLocation")]
    public FleetOrderServiceLocation? ServiceLocation { get; init; }

    /// <summary>Expected non-negative service duration in seconds.</summary>
    [JsonPropertyName("serviceDurationSeconds")]
    public long? ServiceDurationSeconds { get; init; }

    /// <summary>Dispatcher-visible note.</summary>
    [JsonPropertyName("dispatcherNotes")]
    public string? DispatcherNotes { get; init; }

    /// <summary>Driver-visible note.</summary>
    [JsonPropertyName("driverNotes")]
    public string? DriverNotes { get; init; }
}

/// <summary>
/// One order to upsert in a fleet order batch. Mirrors the spec's
/// <c>FleetOrderBatchUpsertInputRequestBody</c>. Supply <c>id</c> or
/// <c>externalIds</c> to update an existing order.
/// </summary>
public sealed record FleetOrderBatchOrderInput
{
    /// <summary>Existing Samsara order UUID. Required unless <see cref="ExternalIds"/> is supplied.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Human-readable order label.</summary>
    [JsonPropertyName("samsaraCustomerOrderName")]
    public string? SamsaraCustomerOrderName { get; init; }

    /// <summary>
    /// Org-scoped external identifiers. The spec declares this a free-form
    /// object; the SDK surfaces the string map the API documents by example.
    /// </summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Order-specific customer metadata.</summary>
    [JsonPropertyName("customerProperties")]
    public IReadOnlyList<FleetOrderCustomerProperty>? CustomerProperties { get; init; }

    /// <summary>Tasks to create or update. Omitted existing tasks remain live.</summary>
    [JsonPropertyName("tasks")]
    public IReadOnlyList<FleetOrderBatchTaskInput>? Tasks { get; init; }

    /// <summary>IDs of tasks to remove from this order.</summary>
    [JsonPropertyName("taskIdsToRemove")]
    public IReadOnlyList<string>? TaskIdsToRemove { get; init; }
}

/// <summary>
/// Request body for <c>POST /preview/fleet/orders/batch</c>
/// (<c>postOrdersBatch</c>). The spec wraps the array in a <c>{ data: [...] }</c>
/// envelope and caps it at 250 orders, applied atomically.
/// </summary>
public sealed record OrdersBatchRequest
{
    /// <summary>Orders to upsert atomically. Maximum 250. Spec REQUIRED.</summary>
    [JsonPropertyName("data")]
    public required IReadOnlyList<FleetOrderBatchOrderInput> Data { get; init; }
}

/// <summary>
/// The result of <c>POST /preview/fleet/orders/batch</c>. Mirrors the spec's
/// <c>OrdersPostOrdersBatchResponseBody</c>, which is <b>not</b> a
/// <c>{ data: ... }</c> envelope — the batch result is the whole body.
/// </summary>
public sealed record OrdersBatchResult
{
    /// <summary>UUID for this batch operation. Spec marks REQUIRED.</summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; init; }

    /// <summary>One result per input order, in request order. Spec marks REQUIRED.</summary>
    [JsonPropertyName("responses")]
    public IReadOnlyList<FleetOrderBatchResponseItem>? Responses { get; init; }
}

/// <summary>
/// One per-order result inside an <see cref="OrdersBatchResult"/>. Mirrors the
/// spec's <c>FleetOrderBatchResponseItemResponseBody</c>.
/// </summary>
public sealed record FleetOrderBatchResponseItem
{
    /// <summary>HTTP-style status for this input. Spec marks REQUIRED.</summary>
    [JsonPropertyName("status")]
    public long? Status { get; init; }

    /// <summary>The upserted order, when this input succeeded.</summary>
    [JsonPropertyName("data")]
    public FleetOrder? Data { get; init; }

    /// <summary>Failure message, when this input failed.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
