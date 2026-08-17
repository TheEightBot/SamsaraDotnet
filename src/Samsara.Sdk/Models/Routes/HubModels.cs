namespace Samsara.Sdk.Models.Routes;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// A hub returned by <c>GET /hubs</c> (the only hub endpoint in the spec —
/// there is no hub get-by-id, create, update, or delete). Address CRUD lives
/// on the separate <c>Addresses</c> client (<c>/addresses</c>).
/// </summary>
public sealed record Hub
{
    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Hub timezone (spec REQUIRED on <c>GET /hubs</c>).</summary>
    [JsonPropertyName("timeZone")]
    public required string TimeZone { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED on <c>GET /hubs</c>).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED on <c>GET /hubs</c>).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>Hub capacity returned by <c>GET /hub/capacities</c>.</summary>
public sealed record HubCapacity
{
    /// <summary>Capacity identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Capacity name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Unit of measurement (spec REQUIRED).</summary>
    [JsonPropertyName("unit")]
    public required string Unit { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>A custom property for hubs (returned by <c>GET /hub/customProperties</c>).</summary>
public sealed record HubCustomProperty
{
    /// <summary>Custom property identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Custom property name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>CSV column names that map to this custom property (spec REQUIRED).</summary>
    [JsonPropertyName("csvColumns")]
    public required string CsvColumns { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>A hub location returned by <c>GET /hub/locations</c>, <c>POST /hub/locations</c>, and <c>PATCH /hub/location/{id}</c>.</summary>
public sealed record HubLocation
{
    /// <summary>The Samsara-generated unique identifier for the location (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>The name of the location (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The physical address of the location (spec REQUIRED).</summary>
    [JsonPropertyName("address")]
    public required string Address { get; init; }

    /// <summary>The customer-provided identifier for the location (spec REQUIRED).</summary>
    [JsonPropertyName("customerLocationId")]
    public required string CustomerLocationId { get; init; }

    /// <summary>The ID of the hub this location belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Indicates if the location is a depot (spec REQUIRED).</summary>
    [JsonPropertyName("isDepot")]
    public required bool IsDepot { get; init; }

    /// <summary>Latitude coordinate of the location (spec REQUIRED).</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude coordinate of the location (spec REQUIRED).</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

    /// <summary>Instructions for the driver (spec REQUIRED).</summary>
    [JsonPropertyName("driverInstructions")]
    public required string DriverInstructions { get; init; }

    /// <summary>Notes for the planner (spec REQUIRED).</summary>
    [JsonPropertyName("plannerNotes")]
    public required string PlannerNotes { get; init; }

    /// <summary>Estimated service time at this location in seconds (spec REQUIRED).</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public required int ServiceTimeSeconds { get; init; }

    /// <summary>Service windows during which work can be performed at this location (spec REQUIRED).</summary>
    [JsonPropertyName("serviceWindows")]
    public required IReadOnlyList<HubServiceWindow> ServiceWindows { get; init; }

    /// <summary>
    /// Skills required for service at this location (spec REQUIRED). On the
    /// response this is an array of skill <em>objects</em>
    /// (<c>SkillObjectResponseBody</c>), unlike the request shapes where it is a
    /// bare array of skill ID strings.
    /// </summary>
    [JsonPropertyName("skillsRequired")]
    public required IReadOnlyList<HubSkillReference> SkillsRequired { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>
/// A single hub location input object posted as part of the
/// <c>{ data: HubLocationInputObjectRequestBody[] }</c> envelope to
/// <c>POST /hub/locations</c>. Renamed from <c>CreateHubLocationRequest</c>
/// during the 2026-05-27 model sync — the prior name now refers to the
/// outer envelope (<see cref="CreateHubLocationsRequest"/>).
/// </summary>
public sealed record CreateHubLocationInput
{
    /// <summary>The physical address of the location (spec REQUIRED).</summary>
    [JsonPropertyName("address")]
    public required string Address { get; init; }

    /// <summary>The customer-provided identifier for the location (spec REQUIRED).</summary>
    [JsonPropertyName("customerLocationId")]
    public required string CustomerLocationId { get; init; }

    /// <summary>The ID of the hub this location belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Indicates if the location is a depot (spec REQUIRED).</summary>
    [JsonPropertyName("isDepot")]
    public required bool IsDepot { get; init; }

    /// <summary>The name of the location (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Latitude coordinate (optional — geocoded from address when omitted).</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude coordinate (optional — geocoded from address when omitted).</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Instructions for the driver.</summary>
    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }

    /// <summary>Notes for the planner.</summary>
    [JsonPropertyName("plannerNotes")]
    public string? PlannerNotes { get; init; }

    /// <summary>Estimated service time at this location in seconds.</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public int? ServiceTimeSeconds { get; init; }

    /// <summary>Recurring service windows for the location.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<HubServiceWindowInput>? ServiceWindows { get; init; }

    /// <summary>
    /// Skill IDs required for service at this location. The request shape is a
    /// bare array of ID strings, unlike the response, which returns skill
    /// objects (see <see cref="HubSkillReference"/>).
    /// </summary>
    [JsonPropertyName("skillsRequired")]
    public IReadOnlyList<string>? SkillsRequired { get; init; }
}

/// <summary>
/// Envelope request body for <c>POST /hub/locations</c> — the spec wraps the
/// array of inputs in <c>{ data: [...] }</c>.
/// </summary>
public sealed record CreateHubLocationsRequest
{
    [JsonPropertyName("data")]
    public required IReadOnlyList<CreateHubLocationInput> Data { get; init; }
}

/// <summary>
/// Inner request body for updating a hub location — posted inside the
/// <see cref="UpdateHubLocationEnvelopeRequest"/> envelope.
/// </summary>
public sealed record UpdateHubLocationRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("address")]
    public string? Address { get; init; }

    [JsonPropertyName("customerLocationId")]
    public string? CustomerLocationId { get; init; }

    [JsonPropertyName("isDepot")]
    public bool? IsDepot { get; init; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    [JsonPropertyName("driverInstructions")]
    public string? DriverInstructions { get; init; }

    [JsonPropertyName("plannerNotes")]
    public string? PlannerNotes { get; init; }

    [JsonPropertyName("serviceTimeSeconds")]
    public int? ServiceTimeSeconds { get; init; }

    /// <summary>Recurring service windows for the location.</summary>
    [JsonPropertyName("serviceWindows")]
    public IReadOnlyList<HubServiceWindowInput>? ServiceWindows { get; init; }

    /// <summary>
    /// Skill IDs required for service at this location — a bare array of ID
    /// strings on the request side.
    /// </summary>
    [JsonPropertyName("skillsRequired")]
    public IReadOnlyList<string>? SkillsRequired { get; init; }
}

/// <summary>
/// Envelope request body for <c>PATCH /hub/location/{id}</c> — the spec wraps
/// the update payload in <c>{ data: T }</c>.
/// </summary>
public sealed record UpdateHubLocationEnvelopeRequest
{
    [JsonPropertyName("data")]
    public required UpdateHubLocationRequest Data { get; init; }
}

/// <summary>A hub skill (returned by <c>GET /hub/skills</c>).</summary>
public sealed record HubSkill
{
    /// <summary>Skill identifier (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Hub identifier (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Skill name (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>A hub dispatch plan returned by <c>GET /hub/plans</c> and <c>POST /hub/plan</c>.</summary>
public sealed record HubPlan
{
    /// <summary>The Samsara-generated unique identifier (UUID) for the plan (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>The name of the plan (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The ID of the hub this plan belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>Shift start time for the plan in RFC 3339 format (spec REQUIRED).</summary>
    [JsonPropertyName("shiftStartTime")]
    public required DateTimeOffset ShiftStartTime { get; init; }

    /// <summary>Creation timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("createdAt")]
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>Last update timestamp (spec REQUIRED).</summary>
    [JsonPropertyName("updatedAt")]
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>Request body for creating a hub plan (<c>POST /hub/plan</c>).</summary>
public sealed record CreateHubPlanRequest
{
    /// <summary>The ID of the hub the plan belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>The name of the plan (spec REQUIRED).</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The ID of a saved session configuration (preset) to apply when creating the plan.</summary>
    [JsonPropertyName("sessionConfigurationId")]
    public string? SessionConfigurationId { get; init; }

    /// <summary>Shift start time for the plan in RFC 3339 format. Defaults to 9:00 AM on the next business day in the hub's timezone when omitted.</summary>
    [JsonPropertyName("shiftStartTime")]
    public DateTimeOffset? ShiftStartTime { get; init; }
}

/// <summary>A hub plan order returned by <c>GET /hub/plan/orders</c> and <c>POST /hub/plan/orders</c>.</summary>
public sealed record HubPlanOrder
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("planId")] public required string PlanId { get; init; }
    [JsonPropertyName("hubId")] public required string HubId { get; init; }
    [JsonPropertyName("customerOrderId")] public required string CustomerOrderId { get; init; }
    [JsonPropertyName("priority")] public required long Priority { get; init; }
    [JsonPropertyName("createdAtTime")] public required DateTimeOffset CreatedAtTime { get; init; }
    [JsonPropertyName("updatedAtTime")] public required DateTimeOffset UpdatedAtTime { get; init; }
    [JsonPropertyName("customProperties")] public required IReadOnlyList<HubOrderCustomProperty> CustomProperties { get; init; }
    [JsonPropertyName("quantities")] public required IReadOnlyList<HubOrderQuantity> Quantities { get; init; }

    /// <summary>Skill IDs required to fulfill the order — a bare array of ID strings.</summary>
    [JsonPropertyName("skillsRequired")] public required IReadOnlyList<string> SkillsRequired { get; init; }
    [JsonPropertyName("routeId")] public string? RouteId { get; init; }
    [JsonPropertyName("pickup")] public HubOrderTask? Pickup { get; init; }
    [JsonPropertyName("delivery")] public HubOrderTask? Delivery { get; init; }
}

/// <summary>
/// A single hub plan order input object posted as part of the
/// <c>{ data: OrderInputObjectRequestBody[] }</c> envelope to
/// <c>POST /hub/plan/orders</c>. Renamed from <c>CreateHubPlanOrdersRequest</c>
/// during the 2026-05-27 model sync — the prior name now refers to the
/// outer envelope (<see cref="CreateHubPlanOrdersRequest"/>).
/// </summary>
public sealed record CreateHubPlanOrderInput
{
    /// <summary>The customer-provided identifier for the order (spec REQUIRED).</summary>
    [JsonPropertyName("customerOrderId")]
    public required string CustomerOrderId { get; init; }

    /// <summary>The ID of the hub the order belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("hubId")]
    public required string HubId { get; init; }

    /// <summary>The ID of the plan the order belongs to (spec REQUIRED).</summary>
    [JsonPropertyName("planId")]
    public required string PlanId { get; init; }

    /// <summary>An array of custom property values for the order.</summary>
    [JsonPropertyName("customProperties")]
    public IReadOnlyList<HubOrderCustomPropertyInput>? CustomProperties { get; init; }

    /// <summary>Delivery task details (spec ref <c>OrderTaskRequestBody</c>).</summary>
    [JsonPropertyName("delivery")]
    public HubOrderTaskInput? Delivery { get; init; }

    /// <summary>Pickup task details (spec ref <c>OrderTaskRequestBody</c>).</summary>
    [JsonPropertyName("pickup")]
    public HubOrderTaskInput? Pickup { get; init; }

    /// <summary>Priority of the order (e.g., 1 for high, 5 for low).</summary>
    [JsonPropertyName("priority")]
    public long? Priority { get; init; }

    /// <summary>An array of quantities for the order.</summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<HubOrderQuantityInput>? Quantities { get; init; }

    /// <summary>An array of skill IDs required to fulfill the order.</summary>
    [JsonPropertyName("skillsRequired")]
    public IReadOnlyList<string>? SkillsRequired { get; init; }
}

/// <summary>
/// Envelope request body for <c>POST /hub/plan/orders</c> — the spec wraps the
/// array of plan order inputs in <c>{ data: [...] }</c>.
/// </summary>
public sealed record CreateHubPlanOrdersRequest
{
    [JsonPropertyName("data")]
    public required IReadOnlyList<CreateHubPlanOrderInput> Data { get; init; }
}

/// <summary>
/// Pickup or delivery task as returned on a hub plan order. Mirrors the task
/// object on the <c>POST</c>/<c>GET /hub/plan/orders</c> response. All fields
/// are optional. Note the Hub Plans API is Beta; this shape may evolve.
/// </summary>
/// <remarks>
/// The request half is <see cref="HubOrderTaskInput"/>. They were split during
/// the 2026-08-17 spec-parity sweep because the spec marks
/// <c>appointmentWindow.startTime</c> / <c>endTime</c> REQUIRED, and
/// <c>required</c> is only ever correct on a request DTO — on the response
/// side it would turn a sparse payload into a deserialization crash.
/// </remarks>
public sealed record HubOrderTask
{
    /// <summary>Street address for the task location.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Appointment window during which the task should occur.</summary>
    [JsonPropertyName("appointmentWindow")]
    public HubOrderAppointmentWindow? AppointmentWindow { get; init; }

    /// <summary>Identifier of a saved customer location for the task.</summary>
    [JsonPropertyName("customerLocationId")]
    public string? CustomerLocationId { get; init; }

    /// <summary>Latitude of the task location (decimal degrees).</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of the task location (decimal degrees).</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Free-form notes for the task.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Ordering position of the task within the order.</summary>
    [JsonPropertyName("position")]
    public string? Position { get; init; }

    /// <summary>Expected service time at the task location, in seconds.</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public int? ServiceTimeSeconds { get; init; }
}

/// <summary>
/// Appointment window returned on a <see cref="HubOrderTask"/>. Mirrors the
/// spec <c>AppointmentWindow</c> schema on the order response.
/// </summary>
/// <remarks>
/// Spec marks both members REQUIRED, but they stay nullable: response
/// properties are never marked <c>required</c> in this SDK. The request half is
/// <see cref="HubOrderAppointmentWindowInput"/>.
/// </remarks>
public sealed record HubOrderAppointmentWindow
{
    /// <summary>Start of the appointment window in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>End of the appointment window in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// Pickup or delivery task posted on a <see cref="CreateHubPlanOrderInput"/>.
/// Mirrors the spec <c>OrderTaskRequestBody</c> schema. All fields are
/// optional. Note the Hub Plans API is Beta; this shape may evolve.
/// </summary>
public sealed record HubOrderTaskInput
{
    /// <summary>Street address for the task location.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Appointment window during which the task should occur.</summary>
    [JsonPropertyName("appointmentWindow")]
    public HubOrderAppointmentWindowInput? AppointmentWindow { get; init; }

    /// <summary>Identifier of a saved customer location for the task.</summary>
    [JsonPropertyName("customerLocationId")]
    public string? CustomerLocationId { get; init; }

    /// <summary>Latitude of the task location (decimal degrees).</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude of the task location (decimal degrees).</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Free-form notes for the task.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>Ordering position of the task within the order (<c>first</c>, <c>last</c>, <c>any</c>).</summary>
    [JsonPropertyName("position")]
    public string? Position { get; init; }

    /// <summary>Expected service time at the task location, in seconds.</summary>
    [JsonPropertyName("serviceTimeSeconds")]
    public int? ServiceTimeSeconds { get; init; }
}

/// <summary>
/// Appointment window posted on a <see cref="HubOrderTaskInput"/>. Mirrors the
/// spec <c>AppointmentWindowRequestBody</c> schema, which marks both members
/// REQUIRED — a window is meaningless without both ends, and the API rejects a
/// partial one.
/// </summary>
public sealed record HubOrderAppointmentWindowInput
{
    /// <summary>Start of the appointment window in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public required DateTimeOffset StartTime { get; init; }

    /// <summary>End of the appointment window in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public required DateTimeOffset EndTime { get; init; }
}

/// <summary>
/// A recurring service window returned on a <see cref="HubLocation"/>. Mirrors
/// the spec <c>ServiceWindowObjectResponseBody</c> schema.
/// </summary>
/// <remarks>
/// The request half is <see cref="HubServiceWindowInput"/>. The two spec schemas
/// are structurally identical and mark all three members REQUIRED, but they stay
/// split for the same reason as <c>HubOrderAppointmentWindow</c>: <c>required</c>
/// is only ever correct on a request DTO — on the response side it turns a
/// sparse payload into a deserialization crash.
/// </remarks>
public sealed record HubServiceWindow
{
    /// <summary>Days of the week when the service window applies (e.g. <c>monday</c>). Spec marks REQUIRED.</summary>
    [JsonPropertyName("daysOfWeek")]
    public IReadOnlyList<string>? DaysOfWeek { get; init; }

    /// <summary>Start time of the service window, in <c>HH:MM:SS</c> format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End time of the service window, in <c>HH:MM:SS</c> format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}

/// <summary>
/// A recurring service window posted on a hub location. Mirrors the spec
/// <c>HubLocationServiceWindowInputRequestBody</c> schema, which marks all three
/// members REQUIRED.
/// </summary>
public sealed record HubServiceWindowInput
{
    /// <summary>Days of the week when the service window applies (e.g. <c>monday</c>). Spec marks REQUIRED.</summary>
    [JsonPropertyName("daysOfWeek")]
    public required IReadOnlyList<string> DaysOfWeek { get; init; }

    /// <summary>Start time of the service window, in <c>HH:MM:SS</c> format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>End time of the service window, in <c>HH:MM:SS</c> format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }
}

/// <summary>
/// A skill reference returned in <c>HubLocation.skillsRequired</c>. Mirrors the
/// spec <c>SkillObjectResponseBody</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <see cref="HubSkill"/>, which mirrors the fuller
/// <c>GET /hub/skills</c> resource (it additionally carries <c>hubId</c>,
/// <c>createdAt</c> and <c>updatedAt</c>).
/// </remarks>
public sealed record HubSkillReference
{
    /// <summary>The unique identifier for the skill. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The name of the skill. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A custom property value returned on a <see cref="HubPlanOrder"/>. Mirrors the
/// spec <c>OrderCustomPropertyResponseBody</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <see cref="HubCustomProperty"/>, which is the custom property
/// <em>definition</em> returned by <c>GET /hub/customProperties</c>. The request
/// half is <see cref="HubOrderCustomPropertyInput"/>, which is a genuinely
/// different shape — it has no <c>name</c>, since the name comes from the
/// definition the <c>customPropertyId</c> points at.
/// </remarks>
public sealed record HubOrderCustomProperty
{
    /// <summary>The ID of the custom property definition. Spec marks REQUIRED.</summary>
    [JsonPropertyName("customPropertyId")]
    public string? CustomPropertyId { get; init; }

    /// <summary>The name of the custom property. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The value of the custom property for this order. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// A custom property value posted on a <see cref="CreateHubPlanOrderInput"/>.
/// Mirrors the spec <c>OrderCustomPropertyInputRequestBody</c> schema.
/// </summary>
public sealed record HubOrderCustomPropertyInput
{
    /// <summary>The ID of the custom property definition. Spec marks REQUIRED.</summary>
    [JsonPropertyName("customPropertyId")]
    public required string CustomPropertyId { get; init; }

    /// <summary>The value of the custom property for this order. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}

/// <summary>
/// A per-capacity quantity returned on a <see cref="HubPlanOrder"/>. Mirrors the
/// spec <c>OrderQuantityResponseBody</c> schema.
/// </summary>
/// <remarks>
/// The request half is <see cref="HubOrderQuantityInput"/>. The two spec schemas
/// are structurally identical and mark both members REQUIRED, but they stay
/// split so <c>required</c> appears only on the request DTO — on the response
/// side it would turn a sparse payload into a deserialization crash.
/// </remarks>
public sealed record HubOrderQuantity
{
    /// <summary>The ID of the hub capacity this quantity is measured against. Spec marks REQUIRED.</summary>
    [JsonPropertyName("capacityId")]
    public string? CapacityId { get; init; }

    /// <summary>The quantity, in the capacity's unit of measurement. Spec marks REQUIRED.</summary>
    [JsonPropertyName("quantity")]
    public double? Quantity { get; init; }
}

/// <summary>
/// A per-capacity quantity posted on a <see cref="CreateHubPlanOrderInput"/>.
/// Mirrors the spec <c>OrderQuantityInputRequestBody</c> schema, which marks both
/// members REQUIRED.
/// </summary>
public sealed record HubOrderQuantityInput
{
    /// <summary>The ID of the hub capacity this quantity is measured against. Spec marks REQUIRED.</summary>
    [JsonPropertyName("capacityId")]
    public required string CapacityId { get; init; }

    /// <summary>The quantity, in the capacity's unit of measurement. Spec marks REQUIRED.</summary>
    [JsonPropertyName("quantity")]
    public required double Quantity { get; init; }
}

// ---------------------------------------------------------------------------
// Hub plan routes — GET /hub/plan/routes.
// ---------------------------------------------------------------------------

/// <summary>
/// A planned route within a hub plan, as returned by
/// <c>GET /hub/plan/routes</c>. Mirrors the spec's
/// <c>RouteObjectResponseBody</c> schema.
/// </summary>
/// <remarks>
/// Named <c>HubPlanRoute</c> rather than the stripped spec name <c>Route</c>:
/// this is the hub-planning shape, unrelated to the dispatch
/// <see cref="Samsara.Sdk.Models.Routes.Route"/> resource served by
/// <c>/fleet/routes</c>.
/// </remarks>
public sealed record HubPlanRoute
{
    /// <summary>The Samsara-generated unique identifier (UUID) for the route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The name of the route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The type of route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>The ID of the hub this route belongs to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("hubId")]
    public string? HubId { get; init; }

    /// <summary>The ID of the plan this route belongs to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("planId")]
    public string? PlanId { get; init; }

    /// <summary>The dispatch route identifier, once the route has been dispatched.</summary>
    [JsonPropertyName("dispatchRouteId")]
    public string? DispatchRouteId { get; init; }

    /// <summary>The cost of the route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("cost")]
    public double? Cost { get; init; }

    /// <summary>The total distance of the route in meters. Spec marks REQUIRED.</summary>
    [JsonPropertyName("distanceMeters")]
    public long? DistanceMeters { get; init; }

    /// <summary>The total duration of the route in seconds. Spec marks REQUIRED.</summary>
    [JsonPropertyName("durationSeconds")]
    public long? DurationSeconds { get; init; }

    /// <summary>Whether the route has been edited. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isEdited")]
    public bool? IsEdited { get; init; }

    /// <summary>Whether the route is pinned. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isPinned")]
    public bool? IsPinned { get; init; }

    /// <summary>The organization location timezone calculated from the hub. Spec marks REQUIRED.</summary>
    [JsonPropertyName("orgLocationTimezone")]
    public string? OrgLocationTimezone { get; init; }

    /// <summary>
    /// The driver assigned to the route (spec schema
    /// <c>RouteDriverObjectResponseBody</c>). Only returned when the route is
    /// assigned.
    /// </summary>
    [JsonPropertyName("driver")]
    public EntityReference? Driver { get; init; }

    /// <summary>
    /// The vehicle assigned to the route (spec schema
    /// <c>RouteVehicleObjectResponseBody</c>). Only returned when the route is
    /// assigned.
    /// </summary>
    [JsonPropertyName("vehicle")]
    public EntityReference? Vehicle { get; init; }

    /// <summary>
    /// Per-capacity quantity information for the route (spec schema
    /// <c>QuantityObjectResponseBody</c>).
    /// </summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<HubOrderQuantity>? Quantities { get; init; }

    /// <summary>The stops on the route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("stops")]
    public IReadOnlyList<HubRouteStop>? Stops { get; init; }

    /// <summary>The scheduled start time of the route, calculated from the first stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("scheduledRouteStartTime")]
    public DateTimeOffset? ScheduledRouteStartTime { get; init; }

    /// <summary>The scheduled end time of the route, calculated from the last stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("scheduledRouteEndTime")]
    public DateTimeOffset? ScheduledRouteEndTime { get; init; }

    /// <summary>The timestamp (UTC) when the route was created. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>The timestamp (UTC) when the route was last updated. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; init; }
}

/// <summary>
/// A stop on a <see cref="HubPlanRoute"/>. Mirrors the spec's
/// <c>RouteStopObjectResponseBody</c> schema.
/// </summary>
public sealed record HubRouteStop
{
    /// <summary>The Samsara-generated unique identifier (UUID) for the stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The name of the stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The hub location identifier from dispatch.</summary>
    [JsonPropertyName("hubLocationId")]
    public string? HubLocationId { get; init; }

    /// <summary>Additional notes for the stop.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    /// <summary>The order tasks associated with this stop.</summary>
    [JsonPropertyName("orders")]
    public IReadOnlyList<HubRouteOrderTask>? Orders { get; init; }

    /// <summary>The scheduled arrival time at the stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("scheduledArrivalTime")]
    public DateTimeOffset? ScheduledArrivalTime { get; init; }

    /// <summary>The scheduled departure time from the stop. Spec marks REQUIRED.</summary>
    [JsonPropertyName("scheduledDepartureTime")]
    public DateTimeOffset? ScheduledDepartureTime { get; init; }

    /// <summary>A one-off location used for this stop, when it is not a saved hub location.</summary>
    [JsonPropertyName("singleUseLocation")]
    public HubRouteSingleUseLocation? SingleUseLocation { get; init; }
}

/// <summary>
/// An order task associated with a <see cref="HubRouteStop"/>. Mirrors the spec's
/// <c>OrderTaskObjectResponseBody</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <see cref="HubOrderTask"/>, the pickup/delivery task returned
/// on <c>GET /hub/plan/orders</c>: that schema carries the task's address and
/// appointment window, this one carries the order's quantities, skills and
/// custom properties.
/// </remarks>
public sealed record HubRouteOrderTask
{
    /// <summary>The order identifier. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The task type (pickup or delivery). Spec marks REQUIRED.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>The external ID of the location associated with this order.</summary>
    [JsonPropertyName("customerLocationId")]
    public string? CustomerLocationId { get; init; }

    /// <summary>The service window time range, as a formatted string.</summary>
    [JsonPropertyName("serviceWindow")]
    public string? ServiceWindow { get; init; }

    /// <summary>
    /// Per-capacity quantity information for the order (spec schema
    /// <c>QuantityObjectResponseBody</c>).
    /// </summary>
    [JsonPropertyName("quantities")]
    public IReadOnlyList<HubOrderQuantity>? Quantities { get; init; }

    /// <summary>
    /// Skills required to service the order (spec schema
    /// <c>OrderTaskSkillObjectResponseBody</c>).
    /// </summary>
    [JsonPropertyName("requiredSkills")]
    public IReadOnlyList<HubSkillReference>? RequiredSkills { get; init; }

    /// <summary>Custom properties for the order.</summary>
    [JsonPropertyName("customProperties")]
    public IReadOnlyList<HubRouteOrderCustomProperty>? CustomProperties { get; init; }
}

/// <summary>
/// A custom property on a <see cref="HubRouteOrderTask"/>. Mirrors the spec's
/// <c>OrderTaskCustomPropertyObjectResponseBody</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <see cref="HubOrderCustomProperty"/>, which spells the
/// identifier <c>customPropertyId</c>; this schema spells it <c>id</c>.
/// </remarks>
public sealed record HubRouteOrderCustomProperty
{
    /// <summary>The custom property identifier. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The custom property name. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The custom property value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// A one-off location used for a route stop. Mirrors the spec's
/// <c>SingleUseLocationObjectResponseBody</c> schema.
/// </summary>
public sealed record HubRouteSingleUseLocation
{
    /// <summary>The full address string. Spec marks REQUIRED.</summary>
    [JsonPropertyName("address")]
    public string? Address { get; init; }

    /// <summary>Latitude coordinate. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude coordinate. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

// ---------------------------------------------------------------------------
// Hub route templates — GET /hub/route-templates.
// ---------------------------------------------------------------------------

/// <summary>
/// A reusable route template for a hub, as returned by
/// <c>GET /hub/route-templates</c>. Mirrors the spec's
/// <c>HubRouteTemplateObjectResponseBody</c> schema.
/// </summary>
public sealed record HubRouteTemplate
{
    /// <summary>The unique identifier for the route template. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The name of the route template. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The hub identifier this route template belongs to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("hubId")]
    public string? HubId { get; init; }

    /// <summary>The IANA timezone of the hub (e.g. <c>America/Los_Angeles</c>). Spec marks REQUIRED.</summary>
    [JsonPropertyName("hubTimezone")]
    public string? HubTimezone { get; init; }

    /// <summary>
    /// Default start time of day for the route template, in <c>HH:MM</c> format
    /// in the hub's local timezone.
    /// </summary>
    [JsonPropertyName("defaultStartTimeOfDay")]
    public string? DefaultStartTimeOfDay { get; init; }

    /// <summary>The depot the template starts from.</summary>
    [JsonPropertyName("defaultDepotStart")]
    public HubRouteTemplateDepot? DefaultDepotStart { get; init; }

    /// <summary>The depot the template ends at.</summary>
    [JsonPropertyName("defaultDepotEnd")]
    public HubRouteTemplateDepot? DefaultDepotEnd { get; init; }

    /// <summary>Ordered list of stop locations in the route template. Spec marks REQUIRED.</summary>
    [JsonPropertyName("locations")]
    public IReadOnlyList<HubRouteTemplateLocation>? Locations { get; init; }

    /// <summary>Total distance of the route in meters. Spec marks REQUIRED.</summary>
    [JsonPropertyName("distanceMeters")]
    public long? DistanceMeters { get; init; }

    /// <summary>Total duration of the route in seconds. Spec marks REQUIRED.</summary>
    [JsonPropertyName("durationSeconds")]
    public long? DurationSeconds { get; init; }

    /// <summary>When the route template was created, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>When the route template was last updated, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// A depot location on a <see cref="HubRouteTemplate"/>. Mirrors the spec's
/// <c>HubRouteTemplateDepotObjectResponseBody</c> schema.
/// </summary>
public sealed record HubRouteTemplateDepot
{
    /// <summary>The unique identifier for the depot location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The name of the depot location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The formatted address of the depot location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>The customer-provided external identifier for the depot location.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Latitude coordinate of the depot location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude coordinate of the depot location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// A stop location within a <see cref="HubRouteTemplate"/>. Mirrors the spec's
/// <c>HubRouteTemplateLocationObjectResponseBody</c> schema.
/// </summary>
/// <remarks>
/// Structurally close to <see cref="HubRouteTemplateDepot"/> but a distinct spec
/// schema: a template location has a <c>position</c> and no <c>id</c>.
/// </remarks>
public sealed record HubRouteTemplateLocation
{
    /// <summary>The name of the location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The formatted address of the location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>The customer-provided identifier for the location.</summary>
    [JsonPropertyName("externalId")]
    public string? ExternalId { get; init; }

    /// <summary>Latitude coordinate of the location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude coordinate of the location. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>The 1-based position of this stop in the route template. Spec marks REQUIRED.</summary>
    [JsonPropertyName("position")]
    public long? Position { get; init; }
}
