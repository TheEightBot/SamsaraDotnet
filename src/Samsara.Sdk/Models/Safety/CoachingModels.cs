namespace Samsara.Sdk.Models.Safety;

using System.Text.Json.Serialization;

/// <summary>
/// Driver reference embedded in coaching responses. Mirrors the spec's
/// <c>DriverWithExternalIdObjectResponseBody</c> (Samsara driver id +
/// optional external id map).
/// </summary>
public sealed record CoachingDriver
{
    /// <summary>Samsara ID of the driver. Spec marks REQUIRED.</summary>
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    /// <summary>Map of external IDs for the driver. Returned when <c>includeExternalIds=true</c>.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Represents a driver-to-coach assignment returned by
/// <c>GET /coaching/driver-coach-assignments</c> and
/// <c>PUT /coaching/driver-coach-assignments</c>.
/// </summary>
public sealed record DriverCoachAssignment
{
    /// <summary>
    /// Driver embedded in the assignment, as a nested object. Spec-required on the
    /// <c>GET</c> list response; the <c>PUT</c> response instead carries the flat
    /// <see cref="DriverId"/> scalar and omits this object, so it is nullable on the
    /// shared record.
    /// </summary>
    [JsonPropertyName("driver")]
    public CoachingDriver? Driver { get; init; }

    /// <summary>
    /// Coach ID associated with the assignment. Spec-required on the <c>GET</c> list
    /// response; optional on the <c>PUT</c> response (null when the assignment was
    /// cleared), so it is nullable on the shared record.
    /// </summary>
    [JsonPropertyName("coachId")]
    public string? CoachId { get; init; }

    /// <summary>Time the coach assignment was created (UTC). Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public required DateTimeOffset CreatedAtTime { get; init; }

    /// <summary>Time the coach assignment was last updated (UTC). Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public required DateTimeOffset UpdatedAtTime { get; init; }

    /// <summary>
    /// The assignment's driver ID as a flat scalar. Returned by the
    /// <c>PUT /coaching/driver-coach-assignments</c> response (which omits the nested
    /// <see cref="Driver"/> object); null on the <c>GET</c> list response, which nests the
    /// driver under <see cref="Driver"/> instead.
    /// </summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }
}

/// <summary>
/// Request body for setting a driver-coach assignment.
/// </summary>
/// <remarks>
/// The Samsara API actually accepts <c>driverId</c> and <c>coachId</c> as
/// query parameters, not a JSON body. This record is preserved so existing
/// callers can keep using it; the <c>ICoachingClient.SetAssignmentAsync</c>
/// overload that accepts an instance unpacks it onto the query string.
/// </remarks>
public sealed record SetDriverCoachAssignmentRequest
{
    /// <summary>Required Samsara driver ID to assign a coach to.</summary>
    [JsonPropertyName("driverId")] public required string DriverId { get; init; }

    /// <summary>
    /// Coach (Samsara user) ID to assign. <c>null</c> removes the existing
    /// coach assignment for the driver.
    /// </summary>
    [JsonPropertyName("coachId")] public string? CoachId { get; init; }
}

/// <summary>
/// Behavior associated with a coaching session. Mirrors the spec's
/// <c>behaviorResponseBody</c>.
/// </summary>
public sealed record CoachingBehavior
{
    /// <summary>Unique ID for the coaching behavior. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Coachable behavior type (e.g. <c>acceleration</c>, <c>noSeatbelt</c>). Spec marks REQUIRED.</summary>
    [JsonPropertyName("coachableBehaviorType")]
    public required string CoachableBehaviorType { get; init; }

    /// <summary>Time of last coached date for the same behavior label. Spec marks REQUIRED.</summary>
    [JsonPropertyName("lastCoachedTime")]
    public required DateTimeOffset LastCoachedTime { get; init; }

    /// <summary>Time of coaching behavior update (UTC). Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public required DateTimeOffset UpdatedAtTime { get; init; }

    /// <summary>Associated note for the behavior, when present.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>
    /// Coachable events for the behavior. Only returned when
    /// <c>includeCoachableEvents=true</c>. Modeled as <c>object</c> because the
    /// spec's <c>coachableEventResponseBody</c> is not strongly modeled in the
    /// SDK.
    /// </summary>
    [JsonPropertyName("coachableEvents")]
    public IReadOnlyList<object>? CoachableEvents { get; init; }
}

/// <summary>
/// Represents a coaching session returned by <c>GET /coaching/sessions/stream</c>.
/// </summary>
public sealed record CoachingSession
{
    /// <summary>Unique ID for the coaching session. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Object references for behaviors within the session. Spec marks REQUIRED.</summary>
    [JsonPropertyName("behaviors")]
    public required IReadOnlyList<CoachingBehavior> Behaviors { get; init; }

    /// <summary>
    /// Coaching type for the session. Spec marks REQUIRED. Valid values:
    /// <c>fullySharedWithManager</c>, <c>selfCoaching</c>, <c>unknown</c>,
    /// <c>unshared</c>, <c>withManager</c>.
    /// </summary>
    [JsonPropertyName("coachingType")]
    public required string CoachingType { get; init; }

    /// <summary>Driver associated with the session. Spec marks REQUIRED.</summary>
    [JsonPropertyName("driver")]
    public required CoachingDriver Driver { get; init; }

    /// <summary>Time the coaching session is due (UTC). Spec marks REQUIRED.</summary>
    [JsonPropertyName("dueAtTime")]
    public required DateTimeOffset DueAtTime { get; init; }

    /// <summary>
    /// Status for the coaching session. Spec marks REQUIRED. Valid values:
    /// <c>unknown</c>, <c>upcoming</c>, <c>completed</c>, <c>deleted</c>.
    /// </summary>
    [JsonPropertyName("sessionStatus")]
    public required string SessionStatus { get; init; }

    /// <summary>Time the coaching session was last updated (UTC). Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public required DateTimeOffset UpdatedAtTime { get; init; }

    /// <summary>Unique user ID for an incomplete coaching session, if assigned.</summary>
    [JsonPropertyName("assignedCoachId")]
    public string? AssignedCoachId { get; init; }

    /// <summary>Unique user ID for a completed coaching session.</summary>
    [JsonPropertyName("completedCoachId")]
    public string? CompletedCoachId { get; init; }

    /// <summary>Time the coaching session was completed in UTC, if applicable.</summary>
    [JsonPropertyName("completedAtTime")]
    public DateTimeOffset? CompletedAtTime { get; init; }

    /// <summary>Associated note for the session, when present.</summary>
    [JsonPropertyName("sessionNote")]
    public string? SessionNote { get; init; }
}
