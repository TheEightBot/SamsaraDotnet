namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;

/// <summary>
/// A ridership passenger. Mirrors the spec's
/// <c>RidershipPassengerObjectResponseBody</c> (the <c>data</c> payload of
/// <c>GET/POST/PUT /ridership/passengers</c> and <c>GET /ridership/passengers/{id}</c>).
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record RidershipPassenger
{
    /// <summary>The unique Samsara ID of the passenger. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>First name of the passenger. Spec-required.</summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; init; }

    /// <summary>Last name of the passenger. Spec-required.</summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; init; }

    /// <summary>
    /// Classification or grade level of the passenger (e.g. <c>k</c>, <c>grade1</c>,
    /// … <c>grade12</c>, <c>pk1</c>–<c>pk4</c>, <c>unknown</c>).
    /// </summary>
    [JsonPropertyName("classification")]
    public string? Classification { get; init; }

    /// <summary>Whether the passenger is active. Spec-required.</summary>
    [JsonPropertyName("isActive")]
    public bool? IsActive { get; init; }

    /// <summary>A map of external ids for the passenger.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Identifiers (e.g. RFID cards) associated with the passenger.</summary>
    [JsonPropertyName("identifiers")]
    public IReadOnlyList<RidershipPassengerIdentifier>? Identifiers { get; init; }

    /// <summary>Special instructions recorded for the passenger.</summary>
    [JsonPropertyName("specialInstructions")]
    public RidershipPassengerSpecialInstructions? SpecialInstructions { get; init; }

    /// <summary>IDs of tags associated with this passenger.</summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    /// <summary>The time the passenger was created, RFC 3339. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>The time the passenger was last updated, RFC 3339. Spec-required.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// An identifier (for example an RFID card) associated with a passenger. Mirrors the
/// spec's <c>RidershipPassengerIdentifierObjectResponseBody</c>.
/// </summary>
public sealed record RidershipPassengerIdentifier
{
    /// <summary>The unique Samsara ID of the identifier. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Type of the identifier: <c>rfid</c> or <c>unknown</c>. Spec-required.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>The identifier value. Spec-required.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }

    /// <summary>Status: <c>active</c>, <c>inactive</c> or <c>unknown</c>. Spec-required.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// Special instructions recorded for a passenger. Mirrors the spec's
/// <c>RidershipPassengerSpecialInstructionsObjectResponseBody</c> and its
/// byte-identical request twin <c>RidershipPassengerSpecialInstructionsInputRequestBody</c>
/// (both declare the same two optional booleans, so a single record serves both
/// directions).
/// </summary>
public sealed record RidershipPassengerSpecialInstructions
{
    /// <summary>Whether a guardian is required for the passenger.</summary>
    [JsonPropertyName("isGuardianRequired")]
    public bool? IsGuardianRequired { get; init; }

    /// <summary>Whether the passenger requires special education accommodations.</summary>
    [JsonPropertyName("isSpecialEducation")]
    public bool? IsSpecialEducation { get; init; }
}

/// <summary>
/// Body for creating (<c>POST /ridership/passengers</c>) or replacing
/// (<c>PUT /ridership/passengers</c>) a passenger. Mirrors the spec's
/// <c>RidershipPassengersCreateRidershipPassengerRequestBody</c> and its
/// byte-identical twin <c>RidershipPassengersUpdateRidershipPassengerRequestBody</c>.
/// </summary>
public sealed record RidershipPassengerInput
{
    /// <summary>First name of the passenger. Spec marks REQUIRED.</summary>
    [JsonPropertyName("firstName")]
    public required string FirstName { get; init; }

    /// <summary>Last name of the passenger. Spec marks REQUIRED.</summary>
    [JsonPropertyName("lastName")]
    public required string LastName { get; init; }

    /// <summary>
    /// Classification or grade level. Valid values: <c>unknown</c>, <c>pk1</c>,
    /// <c>pk2</c>, <c>pk3</c>, <c>pk4</c>, <c>k</c>, <c>grade1</c> … <c>grade12</c>.
    /// </summary>
    [JsonPropertyName("classification")]
    public string? Classification { get; init; }

    /// <summary>A map of external ids to associate with the passenger.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Identifiers to associate with the passenger.</summary>
    [JsonPropertyName("identifiers")]
    public IReadOnlyList<RidershipPassengerIdentifierInput>? Identifiers { get; init; }

    /// <summary>Special instructions for the passenger.</summary>
    [JsonPropertyName("specialInstructions")]
    public RidershipPassengerSpecialInstructions? SpecialInstructions { get; init; }

    /// <summary>IDs of tags to associate with this passenger.</summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }
}

/// <summary>
/// An identifier written to a passenger. Mirrors the spec's
/// <c>RidershipPassengerIdentifierInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>RidershipPassengerIdentifier</c>: the request
/// carries no <c>id</c> (Samsara assigns it) and marks every member REQUIRED.
/// </remarks>
public sealed record RidershipPassengerIdentifierInput
{
    /// <summary>Type of the identifier: <c>rfid</c> or <c>unknown</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>The identifier value. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }

    /// <summary>
    /// Status: <c>active</c>, <c>inactive</c> or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; init; }
}

/// <summary>
/// The passenger assignments configured for a route. Mirrors the spec's
/// <c>RidershipRouteSetupObjectResponseBody</c>.
/// </summary>
public sealed record RidershipRouteSetup
{
    /// <summary>The route ID. Spec-required.</summary>
    [JsonPropertyName("routeId")]
    public string? RouteId { get; init; }

    /// <summary>List of passenger assignments. Spec-required.</summary>
    [JsonPropertyName("passengers")]
    public IReadOnlyList<RidershipRouteSetupPassenger>? Passengers { get; init; }

    /// <summary>The time the route setup was created, RFC 3339. Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>The time the route setup was last updated, RFC 3339. Spec-required.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// One passenger's pick-up/drop-off assignment on a route. Mirrors the spec's
/// <c>RidershipRouteSetupPassengerObjectResponseBody</c>.
/// </summary>
public sealed record RidershipRouteSetupPassenger
{
    /// <summary>The Samsara UUID of the passenger. Spec-required.</summary>
    [JsonPropertyName("passengerId")]
    public string? PassengerId { get; init; }

    /// <summary>The stop ID for the passenger's pick-up.</summary>
    [JsonPropertyName("pickUpStopId")]
    public string? PickUpStopId { get; init; }

    /// <summary>The stop ID for the passenger's drop-off.</summary>
    [JsonPropertyName("dropOffStopId")]
    public string? DropOffStopId { get; init; }
}

/// <summary>
/// A passenger assignment written to a route setup. Mirrors the spec's
/// <c>RidershipRouteSetupPassengerInputRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <c>RidershipRouteSetupPassenger</c> because the
/// spec marks <c>passengerId</c> REQUIRED on the way in; on the way out the SDK
/// keeps every member nullable.
/// </remarks>
public sealed record RidershipRouteSetupPassengerInput
{
    /// <summary>
    /// The Samsara UUID of the passenger, or an external ID in <c>key:value</c>
    /// format. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("passengerId")]
    public required string PassengerId { get; init; }

    /// <summary>The routing stop task ID for the passenger's pick-up.</summary>
    [JsonPropertyName("pickUpStopId")]
    public string? PickUpStopId { get; init; }

    /// <summary>The routing stop task ID for the passenger's drop-off.</summary>
    [JsonPropertyName("dropOffStopId")]
    public string? DropOffStopId { get; init; }
}

/// <summary>
/// Body for <c>POST /ridership/route-setups</c>. Mirrors the spec's
/// <c>RidershipRouteSetupsCreateRidershipRouteSetupRequestBody</c>.
/// </summary>
public sealed record RidershipRouteSetupCreateRequest
{
    /// <summary>
    /// The Samsara route ID returned by the Routing API, or an external ID in
    /// <c>key:value</c> format. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("routeId")]
    public required string RouteId { get; init; }

    /// <summary>Passenger assignments for the route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("passengers")]
    public required IReadOnlyList<RidershipRouteSetupPassengerInput> Passengers { get; init; }
}

/// <summary>
/// Body for <c>PUT /ridership/route-setups</c> (the route is identified by the
/// <c>routeId</c> query parameter). Mirrors the spec's
/// <c>RidershipRouteSetupsUpdateRidershipRouteSetupRequestBody</c>.
/// </summary>
public sealed record RidershipRouteSetupUpdateRequest
{
    /// <summary>Replacement passenger assignments for the route. Spec marks REQUIRED.</summary>
    [JsonPropertyName("passengers")]
    public required IReadOnlyList<RidershipRouteSetupPassengerInput> Passengers { get; init; }
}
