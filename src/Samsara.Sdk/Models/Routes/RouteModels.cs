namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a route in the Samsara system.
/// </summary>
public sealed record Route
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("driver")]
    public RouteDriver? Driver { get; init; }

    [JsonPropertyName("vehicle")]
    public RouteVehicle? Vehicle { get; init; }

    [JsonPropertyName("stops")]
    public IReadOnlyList<RouteStop>? Stops { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("scheduledRouteStartTime")]
    public DateTimeOffset? ScheduledRouteStartTime { get; init; }

    [JsonPropertyName("scheduledRouteEndTime")]
    public DateTimeOffset? ScheduledRouteEndTime { get; init; }

    [JsonPropertyName("actualRouteStartTime")]
    public DateTimeOffset? ActualRouteStartTime { get; init; }

    [JsonPropertyName("actualRouteEndTime")]
    public DateTimeOffset? ActualRouteEndTime { get; init; }

    [JsonPropertyName("orgLocalTimezone")]
    public string? OrgLocalTimezone { get; init; }

    [JsonPropertyName("recurringRouteLiveSharingLinks")]
    public IReadOnlyList<RouteLiveSharingLink>? RecurringRouteLiveSharingLinks { get; init; }
}

/// <summary>
/// Driver assigned to a route.
/// </summary>
public sealed record RouteDriver
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External identifiers for the driver.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Vehicle assigned to a route.
/// </summary>
public sealed record RouteVehicle
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External identifiers for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A stop within a route.
/// </summary>
public sealed record RouteStop
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("actualArrivalTime")]
    public DateTimeOffset? ActualArrivalTime { get; init; }

    [JsonPropertyName("actualDepartureTime")]
    public DateTimeOffset? ActualDepartureTime { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("sequenceNumber")]
    public int? SequenceNumber { get; init; }

    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }

    [JsonPropertyName("enRouteTime")]
    public DateTimeOffset? EnRouteTime { get; init; }

    [JsonPropertyName("eta")]
    public DateTimeOffset? Eta { get; init; }

    [JsonPropertyName("skippedTime")]
    public DateTimeOffset? SkippedTime { get; init; }

    [JsonPropertyName("actualDistanceMeters")]
    public long? ActualDistanceMeters { get; init; }

    [JsonPropertyName("plannedDistanceMeters")]
    public long? PlannedDistanceMeters { get; init; }

    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    [JsonPropertyName("address")]
    public RouteStopAddress? Address { get; init; }

    [JsonPropertyName("orders")]
    public IReadOnlyList<RouteStopOrderTaskReference>? Orders { get; init; }

    /// <summary>
    /// Appointment windows agreed with the customer for this stop. Mirrors the spec's
    /// <c>RouteStopAppointmentWindowResponseBody</c> array.
    /// </summary>
    [JsonPropertyName("appointmentWindows")]
    public IReadOnlyList<RouteStopAppointmentWindow>? AppointmentWindows { get; init; }

    /// <summary>
    /// Documents associated with this stop. Mirrors the spec's
    /// <c>GoaDocumentTinyResponseResponseBody</c> array — a plain <c>{ id, name }</c>
    /// reference, so it reuses the shared <see cref="EntityReference"/>.
    /// </summary>
    [JsonPropertyName("documents")]
    public IReadOnlyList<EntityReference>? Documents { get; init; }

    /// <summary>
    /// Form attachments associated with this stop. Mirrors the spec's
    /// <c>RouteStopFormResponseObjectResponseBody</c> array.
    /// </summary>
    [JsonPropertyName("forms")]
    public IReadOnlyList<RouteStopForm>? Forms { get; init; }

    /// <summary>
    /// Issues raised against this stop. Mirrors the spec's
    /// <c>GoaIssueTinyResponseResponseBody</c> array.
    /// </summary>
    [JsonPropertyName("issues")]
    public IReadOnlyList<RouteStopIssue>? Issues { get; init; }

    /// <summary>
    /// Live-sharing links scoped to this stop's location. Mirrors the spec's
    /// <c>LiveSharingLinkResponseObjectResponseBody</c> array — the same schema
    /// <see cref="Route.RecurringRouteLiveSharingLinks"/> uses, so it reuses
    /// <see cref="RouteLiveSharingLink"/>.
    /// </summary>
    [JsonPropertyName("locationLiveSharingLinks")]
    public IReadOnlyList<RouteLiveSharingLink>? LocationLiveSharingLinks { get; init; }
}

/// <summary>
/// An appointment window on a <see cref="RouteStop"/>. Mirrors the spec's
/// <c>RouteStopAppointmentWindowResponseBody</c>.
/// </summary>
/// <remarks>
/// The request half is <see cref="RouteStopAppointmentWindowInput"/>. The two spec
/// schemas are structurally identical and mark both members REQUIRED, but they stay
/// split so <c>required</c> appears only on the request DTO — on the response side it
/// would turn a sparse payload into a deserialization crash.
/// <para>
/// The times are <c>string</c>, not <c>DateTimeOffset</c>: the spec types them as bare
/// <c>string</c> with no <c>date-time</c> format, matching
/// <see cref="RouteLiveSharingLink.ExpiresAtTime"/>.
/// </para>
/// </remarks>
public sealed record RouteStopAppointmentWindow
{
    /// <summary>The start time of the appointment window, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>The end time of the appointment window, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}

/// <summary>
/// A form attachment returned on a <see cref="RouteStop"/>. Mirrors the spec's
/// <c>RouteStopFormResponseObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="RouteStopFormInput"/>: the response half additionally
/// carries <c>id</c> (the form <em>submission</em> id), which the request half has no
/// notion of.
/// </remarks>
public sealed record RouteStopForm
{
    /// <summary>ID of the form submission. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>ID of the form template.</summary>
    [JsonPropertyName("formTemplateId")]
    public string? FormTemplateId { get; init; }

    /// <summary>Whether the driver must complete the form before departing the stop.</summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }
}

/// <summary>
/// A minified issue reference on a <see cref="RouteStop"/>. Mirrors the spec's
/// <c>GoaIssueTinyResponseResponseBody</c>.
/// </summary>
/// <remarks>
/// Not <see cref="EntityReference"/>: this schema is <c>id</c>-only, so the shared
/// <c>{ id, name }</c> reference would add a <c>name</c> the API never returns here.
/// </remarks>
public sealed record RouteStopIssue
{
    /// <summary>ID of the issue. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// The saved address a <see cref="RouteStop"/> points at. Mirrors the spec's
/// <c>GoaAddressTinyResponseResponseBody</c> (a minified Address object).
/// </summary>
/// <remarks>
/// Not <c>EntityReference</c>: that record is a bare <c>{ id, name }</c> pair and
/// would drop <c>externalIds</c>, which this schema carries.
/// </remarks>
public sealed record RouteStopAddress
{
    /// <summary>Id of the address. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the address. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external ids for the address.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A canonical order task attached to a <see cref="RouteStop"/>. Mirrors the
/// spec's <c>RouteStopOrderTaskReferenceObjectResponseBody</c>.
/// </summary>
public sealed record RouteStopOrderTaskReference
{
    /// <summary>Samsara-generated canonical order UUID. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Order task ID attached to this stop. Spec-required.</summary>
    [JsonPropertyName("taskId")]
    public string? TaskId { get; init; }
}

/// <summary>
/// A live-sharing link on a <see cref="Route"/>. Mirrors the spec's
/// <c>LiveSharingLinkResponseObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Named <c>RouteLiveSharingLink</c> because <c>Samsara.Sdk.Models.Fleet.LiveSharingLink</c>
/// already exists and mirrors a different schema
/// (<c>LiveSharingLinkFullResponseObjectResponseBody</c>, which additionally
/// carries <c>id</c>, <c>type</c> and <c>description</c>).
/// </remarks>
public sealed record RouteLiveSharingLink
{
    /// <summary>Name of the Live Sharing Link. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The shareable URL of the vehicle's location. Spec-required.</summary>
    [JsonPropertyName("liveSharingUrl")]
    public string? LiveSharingUrl { get; init; }

    /// <summary>Date that this link expires, in RFC 3339 format.</summary>
    [JsonPropertyName("expiresAtTime")]
    public string? ExpiresAtTime { get; init; }
}

/// <summary>
/// A one-time location used as a route stop (not saved as an address).
/// </summary>
public sealed record SingleUseLocation
{
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>
    /// Radius in meters for the geofence around this location. Must be positive.
    /// </summary>
    [JsonPropertyName("radiusMeters")]
    public double? RadiusMeters { get; init; }
}

/// <summary>
/// Route settings.
/// </summary>
public sealed record RouteSettings
{
    [JsonPropertyName("routeCompletionCondition")]
    public string? RouteCompletionCondition { get; init; }

    [JsonPropertyName("routeStartingCondition")]
    public string? RouteStartingCondition { get; init; }

    [JsonPropertyName("sequencingMethod")]
    public string? SequencingMethod { get; init; }
}

/// <summary>
/// Request body for creating a new route.
/// </summary>
public sealed record CreateRouteRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("stops")]
    public required IReadOnlyList<CreateRouteStopRequest> Stops { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("recomputeScheduledTimes")]
    public bool? RecomputeScheduledTimes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// Request body for a stop in a new route. Mirrors the spec's
/// <c>CreateRouteStopWithOrdersRequestObjectRequestBody</c>
/// (<c>POST /fleet/routes</c> → <c>stops[]</c>).
/// </summary>
public sealed record CreateRouteStopRequest
{
    /// <summary>
    /// Canonical orders to upsert and attach to this stop. Mirrors the spec's
    /// <c>RouteStopOrderUpsertInputRequestBody</c> array.
    /// </summary>
    [JsonPropertyName("orders")]
    public IReadOnlyList<RouteStopOrderInput>? Orders { get; init; }

    /// <summary>
    /// Appointment windows agreed with the customer for this stop. Mirrors the spec's
    /// <c>RouteStopAppointmentWindowRequestBody</c> array.
    /// </summary>
    [JsonPropertyName("appointmentWindows")]
    public IReadOnlyList<RouteStopAppointmentWindowInput>? AppointmentWindows { get; init; }

    /// <summary>
    /// Forms to attach to this stop. Mirrors the spec's
    /// <c>RouteStopFormRequestObjectRequestBody</c> array.
    /// </summary>
    [JsonPropertyName("forms")]
    public IReadOnlyList<RouteStopFormInput>? Forms { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }

    [JsonPropertyName("sequenceNumber")]
    public int? SequenceNumber { get; init; }

    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }
}

/// <summary>
/// Request body for updating a route (PATCH).
/// </summary>
public sealed record UpdateRouteRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("settings")]
    public RouteSettings? Settings { get; init; }

    [JsonPropertyName("stops")]
    public IReadOnlyList<UpdateRouteStopRequest>? Stops { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("recomputeScheduledTimes")]
    public bool? RecomputeScheduledTimes { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// Request body for updating a stop within a route. Mirrors the spec's
/// <c>UpdateRoutesStopRequestObjectRequestBody</c>
/// (<c>PATCH /fleet/routes/{id}</c> → <c>stops[]</c>).
/// </summary>
/// <remarks>
/// The update stop schema differs from
/// <c>CreateRouteStopWithOrdersRequestObjectRequestBody</c> only by the addition of
/// <c>id</c>; the <c>orders</c>, <c>appointmentWindows</c> and <c>forms</c> members
/// <see href="https://developers.samsara.com">reference the identical child schemas</see>,
/// so both stop records share <see cref="RouteStopOrderInput"/>,
/// <see cref="RouteStopAppointmentWindowInput"/> and <see cref="RouteStopFormInput"/>.
/// </remarks>
public sealed record UpdateRouteStopRequest
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Canonical orders to upsert and attach to this stop. Mirrors the spec's
    /// <c>RouteStopOrderUpsertInputRequestBody</c> array.
    /// </summary>
    [JsonPropertyName("orders")]
    public IReadOnlyList<RouteStopOrderInput>? Orders { get; init; }

    /// <summary>
    /// Appointment windows agreed with the customer for this stop. Mirrors the spec's
    /// <c>RouteStopAppointmentWindowRequestBody</c> array.
    /// </summary>
    [JsonPropertyName("appointmentWindows")]
    public IReadOnlyList<RouteStopAppointmentWindowInput>? AppointmentWindows { get; init; }

    /// <summary>
    /// Forms to attach to this stop. Mirrors the spec's
    /// <c>RouteStopFormRequestObjectRequestBody</c> array.
    /// </summary>
    [JsonPropertyName("forms")]
    public IReadOnlyList<RouteStopFormInput>? Forms { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    [JsonPropertyName("singleUseLocation")]
    public SingleUseLocation? SingleUseLocation { get; init; }

    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("sequenceNumber")]
    public int? SequenceNumber { get; init; }

    [JsonPropertyName("ontimeWindowAfterArrivalMs")]
    public long? OntimeWindowAfterArrivalMs { get; init; }

    [JsonPropertyName("ontimeWindowBeforeArrivalMs")]
    public long? OntimeWindowBeforeArrivalMs { get; init; }
}

/// <summary>
/// An appointment window posted on a route stop. Mirrors the spec's
/// <c>RouteStopAppointmentWindowRequestBody</c>, which marks both members REQUIRED.
/// </summary>
/// <remarks>
/// The times are <c>string</c>, not <c>DateTimeOffset</c>: the spec types them as bare
/// <c>string</c> with no <c>date-time</c> format.
/// </remarks>
public sealed record RouteStopAppointmentWindowInput
{
    /// <summary>The start time of the appointment window, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>The end time of the appointment window, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }
}

/// <summary>
/// A form to attach to a route stop. Mirrors the spec's
/// <c>RouteStopFormRequestObjectRequestBody</c>.
/// </summary>
public sealed record RouteStopFormInput
{
    /// <summary>ID of the form template to attach to the stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("formTemplateId")]
    public required string FormTemplateId { get; init; }

    /// <summary>Whether the driver must complete the form before departing the stop.</summary>
    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }
}

/// <summary>
/// A canonical order to upsert and attach to the containing route stop. Mirrors the
/// spec's <c>RouteStopOrderUpsertInputRequestBody</c>.
/// </summary>
/// <remarks>
/// The response half of a stop's <c>orders</c> is <see cref="RouteStopOrderTaskReference"/>,
/// a bare <c>{ id, taskId }</c> pair — the API accepts a full order tree on write and
/// returns references on read.
/// </remarks>
public sealed record RouteStopOrderInput
{
    /// <summary>Existing Samsara order UUID. Required unless <see cref="ExternalIds"/> is supplied.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Human-readable order label.</summary>
    [JsonPropertyName("samsaraCustomerOrderName")]
    public string? SamsaraCustomerOrderName { get; init; }

    /// <summary>Org-scoped external identifiers.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Order-specific customer metadata.</summary>
    [JsonPropertyName("customerProperties")]
    public IReadOnlyList<FleetOrderCustomerPropertyInput>? CustomerProperties { get; init; }

    /// <summary>Tasks to upsert and attach to the containing stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("tasks")]
    public required IReadOnlyList<FleetOrderTaskInput> Tasks { get; init; }
}

/// <summary>
/// Customer-defined order metadata posted on a <see cref="RouteStopOrderInput"/> or a
/// <see cref="FleetOrderTaskInput"/>. Mirrors the spec's
/// <c>FleetOrderCustomerPropertyObjectRequestBody</c>, which marks both members REQUIRED.
/// </summary>
/// <remarks>
/// Deliberately not <c>HubOrderCustomPropertyInput</c>: that record mirrors
/// <c>OrderCustomPropertyInputRequestBody</c>, which is keyed by
/// <c>customPropertyId</c> (a reference to a hub custom-property <em>definition</em>)
/// rather than by a free-form <c>key</c>.
/// </remarks>
public sealed record FleetOrderCustomerPropertyInput
{
    /// <summary>Customer-defined property key. Spec marks REQUIRED.</summary>
    [JsonPropertyName("key")]
    public required string Key { get; init; }

    /// <summary>Customer-defined property value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}

/// <summary>
/// An order task to create or update. Mirrors the spec's
/// <c>FleetOrderTaskUpsertInputRequestBody</c>.
/// </summary>
public sealed record FleetOrderTaskInput
{
    /// <summary>Existing task ID. Omit to create a task.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Task type: <c>delivery</c>, <c>pickup</c> or <c>pickupDelivery</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("taskType")]
    public required string TaskType { get; init; }

    /// <summary>Hub UUID for this task. Nested route writes derive it from route context when omitted.</summary>
    [JsonPropertyName("hubId")]
    public string? HubId { get; init; }

    /// <summary>Task position constraint: <c>none</c>, <c>first</c> or <c>last</c>.</summary>
    [JsonPropertyName("positionConstraintType")]
    public string? PositionConstraintType { get; init; }

    /// <summary>Dispatcher-visible note.</summary>
    [JsonPropertyName("dispatcherNotes")]
    public string? DispatcherNotes { get; init; }

    /// <summary>Driver-visible note.</summary>
    [JsonPropertyName("driverNotes")]
    public string? DriverNotes { get; init; }

    /// <summary>Expected service duration in seconds.</summary>
    [JsonPropertyName("serviceDurationSeconds")]
    public int? ServiceDurationSeconds { get; init; }

    /// <summary>Existing service window UUIDs to retire.</summary>
    [JsonPropertyName("serviceWindowIdsToRemove")]
    public IReadOnlyList<string>? ServiceWindowIdsToRemove { get; init; }

    /// <summary>Task-specific customer metadata.</summary>
    [JsonPropertyName("customerProperties")]
    public IReadOnlyList<FleetOrderCustomerPropertyInput>? CustomerProperties { get; init; }

    /// <summary>Task quantities.</summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<FleetOrderQuantityInput>? Quantities { get; init; }

    /// <summary>Service windows to create or update.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<FleetOrderServiceWindowInput>? ServiceWindows { get; init; }

    /// <summary>Saved or one-time service location for this task.</summary>
    [JsonPropertyName("serviceLocation")]
    public FleetOrderServiceLocationInput? ServiceLocation { get; init; }
}

/// <summary>
/// A labelled quantity for an order task. Mirrors the spec's
/// <c>FleetOrderQuantityInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Deliberately not <c>HubOrderQuantityInput</c>: that record mirrors
/// <c>OrderQuantityInputRequestBody</c>, a <c>{ capacityId, quantity }</c> pair with
/// both members REQUIRED. This schema instead carries a free-form <c>label</c> and
/// <c>value</c> (both REQUIRED) with an <em>optional</em> <c>capacityId</c>.
/// </remarks>
public sealed record FleetOrderQuantityInput
{
    /// <summary>Human-readable quantity label. Spec marks REQUIRED.</summary>
    [JsonPropertyName("label")]
    public required string Label { get; init; }

    /// <summary>Finite, non-negative quantity value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required double Value { get; init; }

    /// <summary>Optional capacity UUID for the task hub.</summary>
    [JsonPropertyName("capacityId")]
    public string? CapacityId { get; init; }
}

/// <summary>
/// A service window to create or update on an order task. Mirrors the spec's
/// <c>FleetOrderServiceWindowUpsertInputRequestBody</c>, which marks nothing REQUIRED.
/// </summary>
/// <remarks>
/// Deliberately not <c>HubServiceWindowInput</c>: that record mirrors
/// <c>HubLocationServiceWindowInputRequestBody</c>, a recurring weekly window
/// (<c>daysOfWeek</c> plus <c>HH:MM:SS</c> strings, all REQUIRED). This schema is an
/// absolute RFC 3339 window addressed by <c>id</c>.
/// </remarks>
public sealed record FleetOrderServiceWindowInput
{
    /// <summary>Existing service window UUID. Omit to create a window.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Service window start, in RFC 3339 format.</summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>Service window end, in RFC 3339 format.</summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// Saved or one-time service location for an order task. Mirrors the spec's
/// <c>FleetOrderServiceLocationInputRequestBody</c>.
/// </summary>
public sealed record FleetOrderServiceLocationInput
{
    /// <summary>Location discriminator: <c>savedAddress</c> or <c>customAddress</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("serviceLocationType")]
    public required string ServiceLocationType { get; init; }

    /// <summary>Saved address ID. Required for <c>savedAddress</c> locations.</summary>
    [JsonPropertyName("addressId")]
    public string? AddressId { get; init; }

    /// <summary>A one-time structured service address. Used for <c>customAddress</c> locations.</summary>
    [JsonPropertyName("customAddress")]
    public FleetOrderCustomAddressInput? CustomAddress { get; init; }
}

/// <summary>
/// A one-time structured service address. Mirrors the spec's
/// <c>FleetOrderCustomAddressInputRequestBody</c>, which marks nothing REQUIRED.
/// </summary>
/// <remarks>
/// Deliberately not <see cref="SingleUseLocation"/>: that record mirrors
/// <c>RoutesSingleUseAddressObjectRequestBody</c>, a flat
/// <c>{ address, latitude, longitude }</c> triple with the coordinates REQUIRED. This
/// schema is a fully structured postal address with optional coordinates.
/// </remarks>
public sealed record FleetOrderCustomAddressInput
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

    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>Represents a route audit log event (route feed object), returned by
/// <c>GET /fleet/routes/audit-logs/feed</c>.</summary>
public sealed record RouteAuditEvent
{
    /// <summary>The before/after changes that were applied as part of this route update (spec REQUIRED).</summary>
    [JsonPropertyName("changes")] public required RouteAuditChanges Changes { get; init; }

    /// <summary>The route this update applies to (spec REQUIRED).</summary>
    [JsonPropertyName("route")] public required Route Route { get; init; }

    /// <summary>The source of this route update (e.g. <c>automatic</c>, <c>driver</c>, <c>admin</c>) — spec REQUIRED.</summary>
    [JsonPropertyName("source")] public required string Source { get; init; }

    /// <summary>The type of route update (e.g. <c>route tracking</c>) — spec REQUIRED.</summary>
    [JsonPropertyName("type")] public required string Type { get; init; }

    /// <summary>The timestamp of the route update in RFC 3339 format (spec REQUIRED).</summary>
    [JsonPropertyName("time")] public required DateTimeOffset Time { get; init; }

    /// <summary>The operation that was performed as part of this route update (e.g. <c>stop scheduled</c>).</summary>
    [JsonPropertyName("operation")] public string? Operation { get; init; }
}

/// <summary>The before/after route snapshots captured by a <see cref="RouteAuditEvent"/>.
/// Mirrors the spec's <c>RouteChangesResponseBody</c>.</summary>
public sealed record RouteAuditChanges
{
    /// <summary>The route state before the update. Spec-required.</summary>
    [JsonPropertyName("before")] public required RouteAuditSnapshot Before { get; init; }

    /// <summary>The route state after the update. Spec-required.</summary>
    [JsonPropertyName("after")] public required RouteAuditSnapshot After { get; init; }
}

/// <summary>A minimal route snapshot (the changed stops only) inside a
/// <see cref="RouteAuditChanges"/>. Mirrors the spec's
/// <c>MinimalRouteAuditLogsResponseBody</c>.</summary>
public sealed record RouteAuditSnapshot
{
    /// <summary>The stops captured in this snapshot.</summary>
    [JsonPropertyName("stops")] public IReadOnlyList<RouteAuditStop>? Stops { get; init; }
}

/// <summary>A minimal route-stop snapshot inside a route audit log change.
/// Mirrors the spec's <c>MinimalRouteStopAuditLogsResponseBody</c>.</summary>
public sealed record RouteAuditStop
{
    /// <summary>Unique identifier of the stop. Spec-required.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>The state of the stop (e.g. <c>scheduled</c>, <c>enRoute</c>, <c>skipped</c>).</summary>
    [JsonPropertyName("state")] public string? State { get; init; }

    /// <summary>External identifiers for the stop.</summary>
    [JsonPropertyName("externalIds")] public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>The live-sharing URL for the stop.</summary>
    [JsonPropertyName("liveSharingUrl")] public string? LiveSharingUrl { get; init; }

    /// <summary>Scheduled arrival time, in RFC 3339 format.</summary>
    [JsonPropertyName("scheduledArrivalTime")] public DateTimeOffset? ScheduledArrivalTime { get; init; }

    /// <summary>Scheduled departure time, in RFC 3339 format.</summary>
    [JsonPropertyName("scheduledDepartureTime")] public DateTimeOffset? ScheduledDepartureTime { get; init; }

    /// <summary>Actual arrival time, in RFC 3339 format.</summary>
    [JsonPropertyName("actualArrivalTime")] public DateTimeOffset? ActualArrivalTime { get; init; }

    /// <summary>Actual departure time, in RFC 3339 format.</summary>
    [JsonPropertyName("actualDepartureTime")] public DateTimeOffset? ActualDepartureTime { get; init; }

    /// <summary>The time the asset went en route to the stop, in RFC 3339 format.</summary>
    [JsonPropertyName("enRouteTime")] public DateTimeOffset? EnRouteTime { get; init; }

    /// <summary>Estimated time of arrival, in RFC 3339 format.</summary>
    [JsonPropertyName("eta")] public DateTimeOffset? Eta { get; init; }

    /// <summary>The time the stop was skipped, in RFC 3339 format.</summary>
    [JsonPropertyName("skippedTime")] public DateTimeOffset? SkippedTime { get; init; }
}

/// <summary>
/// A single route event.
/// One item of the <c>data</c> array returned by <c>GET /route-events/stream</c> (operationId
/// <c>getRouteEventsStream</c>).
/// Mirrors the spec schema <c>RouteEventResponseResponseBody</c>.
/// </summary>
public sealed record RouteEvent
{
    /// <summary>Contains additional information specific to the event type.</summary>
    [JsonPropertyName("eventDetails")]
    public RouteEventDetails? EventDetails { get; init; }

    /// <summary>
    /// Time the event was processed in RFC 3339 format. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("eventTime")]
    public DateTimeOffset? EventTime { get; init; }

    /// <summary>
    /// Type of the event that occurred. One of: <c>stopArrived</c>, <c>stopCompleted</c>,
    /// <c>stopEnRoute</c>, <c>stopSkipped</c>, <c>stopTaskCompleted</c>,
    /// <c>stopTaskSkipped</c>, <c>stopEtaUpdated</c>, <c>unspecified</c>. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("eventType")]
    public string? EventType { get; init; }

    /// <summary>
    /// Time the event happened in RFC 3339 format. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("happenedAtTime")]
    public DateTimeOffset? HappenedAtTime { get; init; }

    /// <summary>Unique ID of the route event. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Normalized route object this event belongs to. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("route")]
    public RouteEventRouteReference? Route { get; init; }

    /// <summary>Normalized stop object this event belongs to.</summary>
    [JsonPropertyName("stop")]
    public RouteEventStopReference? Stop { get; init; }
}

/// <summary>
/// Contains additional information specific to the event type.
/// Mirrors the spec schema <c>RouteEventDetailsResponseBody</c>.
/// </summary>
public sealed record RouteEventDetails
{
    /// <summary>Details for stop ETA updated events.</summary>
    [JsonPropertyName("stopEtaUpdated")]
    public RouteEventStopEtaUpdated? StopEtaUpdated { get; init; }

    /// <summary>Details for stop task completed events.</summary>
    [JsonPropertyName("stopTaskCompleted")]
    public RouteEventStopTaskCompleted? StopTaskCompleted { get; init; }

    /// <summary>Details for stop task skipped events.</summary>
    [JsonPropertyName("stopTaskSkipped")]
    public RouteEventStopTaskSkipped? StopTaskSkipped { get; init; }
}

/// <summary>
/// Details for stop ETA updated events.
/// Mirrors the spec schema <c>StopEtaUpdatedEventDetailsResponseBody</c>.
/// </summary>
public sealed record RouteEventStopEtaUpdated
{
    /// <summary>
    /// Estimated arrival time in milliseconds since epoch. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("etaMs")]
    public string? EtaMs { get; init; }

    /// <summary>
    /// Time when the ETA was updated in milliseconds since epoch. Spec marks this required
    /// on the response.
    /// </summary>
    [JsonPropertyName("etaUpdatedAtMs")]
    public string? EtaUpdatedAtMs { get; init; }
}

/// <summary>
/// Details for stop task completed events.
/// Mirrors the spec schema <c>StopTaskCompletedEventDetailsResponseBody</c>.
/// </summary>
public sealed record RouteEventStopTaskCompleted
{
    /// <summary>ID of the completed stop task. Spec marks this required on the response.</summary>
    [JsonPropertyName("taskId")]
    public string? TaskId { get; init; }

    /// <summary>
    /// Type of the completed stop task. One of: <c>form</c>, <c>document</c>. Spec marks
    /// this required on the response.
    /// </summary>
    [JsonPropertyName("taskType")]
    public string? TaskType { get; init; }
}

/// <summary>
/// Details for stop task skipped events.
/// Mirrors the spec schema <c>StopTaskSkippedEventDetailsResponseBody</c>.
/// </summary>
public sealed record RouteEventStopTaskSkipped
{
    /// <summary>ID of the skipped stop task. Spec marks this required on the response.</summary>
    [JsonPropertyName("taskId")]
    public string? TaskId { get; init; }

    /// <summary>
    /// Type of the skipped stop task. One of: <c>form</c>, <c>document</c>. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("taskType")]
    public string? TaskType { get; init; }
}

/// <summary>
/// Normalized route object this event belongs to.
/// Mirrors the spec schema <c>RouteEventRouteResponseResponseBody</c>.
/// </summary>
public sealed record RouteEventRouteReference
{
    /// <summary>A map of external ids.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>ID of the route this event belongs to. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}

/// <summary>
/// Normalized stop object this event belongs to.
/// Mirrors the spec schema <c>RouteEventStopResponseResponseBody</c>.
/// </summary>
public sealed record RouteEventStopReference
{
    /// <summary>A map of external ids.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>ID of the stop this event belongs to. Spec marks this required on the response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}
