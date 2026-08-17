namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;

/// <summary>
/// Models for the beta Ground Intelligence API (<c>/ground-intelligence/*</c>).
/// </summary>
/// <remarks>
/// <para>
/// The <c>/ground-intelligence/issues</c> resource is <b>not</b> the same resource as
/// <c>/issues</c> (see <c>Samsara.Sdk.Models.Issues</c>); the two only share the
/// <c>listIssues</c> operationId. These records mirror the Ground Intelligence
/// schemas exclusively.
/// </para>
/// <para>
/// Timestamps on these schemas are declared <c>type: string</c> with no
/// <c>format: date-time</c>, so they are modelled as <c>string</c>.
/// </para>
/// </remarks>
public sealed record GroundIntelligenceIssue
{
    /// <summary>Unique identifier for the issue.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Customer-facing type for this issue (e.g. <c>pothole</c>,
    /// <c>roadCracking</c>, <c>patchedPothole</c>).
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Current customer-facing review status: <c>needsReview</c>,
    /// <c>reviewed</c>, <c>resolved</c> or <c>dismissed</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Customer-facing severity level for this issue.</summary>
    [JsonPropertyName("severity")]
    public string? Severity { get; init; }

    /// <summary>Where the issue was observed.</summary>
    [JsonPropertyName("location")]
    public GroundIntelligenceIssueLocation? Location { get; init; }

    /// <summary>The road segment associated with this issue, when available.</summary>
    [JsonPropertyName("roadSegment")]
    public GroundIntelligenceIssueRoadSegment? RoadSegment { get; init; }

    /// <summary>Number of evidence records aggregated into this issue.</summary>
    [JsonPropertyName("observationCount")]
    public long? ObservationCount { get; init; }

    /// <summary>URL to view this issue in the Samsara dashboard.</summary>
    [JsonPropertyName("dashboardUrl")]
    public string? DashboardUrl { get; init; }

    /// <summary>Time when this issue was first observed.</summary>
    [JsonPropertyName("firstSeenTime")]
    public string? FirstSeenTime { get; init; }

    /// <summary>Time when this issue was most recently observed.</summary>
    [JsonPropertyName("lastSeenTime")]
    public string? LastSeenTime { get; init; }

    /// <summary>Time when this issue record was created.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>Time when this issue record was most recently updated.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// The location of a <see cref="GroundIntelligenceIssue"/>. Mirrors the spec's
/// <c>ListIssuesEntityGroundIntelligenceIssueGroundIntelligenceIssueLocationTypeResponseBody</c>.
/// </summary>
public sealed record GroundIntelligenceIssueLocation
{
    /// <summary>Shape of the issue location.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>The point the issue was observed at.</summary>
    [JsonPropertyName("point")]
    public GroundIntelligenceLatLng? Point { get; init; }
}

/// <summary>
/// A WGS84 coordinate pair on a Ground Intelligence issue. Mirrors the spec's
/// <c>ListIssuesEntityGroundIntelligenceIssueLatLngTypeResponseBody</c>.
/// </summary>
public sealed record GroundIntelligenceLatLng
{
    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// The road segment a Ground Intelligence issue sits on. Mirrors the spec's
/// <c>ListIssuesEntityGroundIntelligenceIssueGroundIntelligenceIssueRoadSegmentTypeResponseBody</c>.
/// </summary>
public sealed record GroundIntelligenceIssueRoadSegment
{
    /// <summary>Road name associated with this issue, when available.</summary>
    [JsonPropertyName("roadName")]
    public string? RoadName { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /ground-intelligence/issues</c>
/// (<c>updateGroundIntelligenceIssue</c>, beta). Mirrors the spec's
/// <c>EntityGroundIntelligenceIssuesServiceUpdateGroundIntelligenceIssueRequestBody</c>.
/// The spec marks no field required.
/// </summary>
public sealed record UpdateGroundIntelligenceIssueRequest
{
    /// <summary>
    /// New review status: <c>needsReview</c>, <c>reviewed</c>, <c>resolved</c>
    /// or <c>dismissed</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// New issue type: <c>pothole</c>, <c>roadCracking</c> or <c>patchedPothole</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Why the issue is being dismissed: <c>notMyJurisdiction</c>,
    /// <c>knownIssue</c>, <c>duplicate</c>, <c>inaccurateDetection</c> or <c>other</c>.
    /// </summary>
    [JsonPropertyName("dismissalReason")]
    public string? DismissalReason { get; init; }

    /// <summary>Optional note about the dismissal. Set to null to clear.</summary>
    [JsonPropertyName("dismissalNote")]
    public string? DismissalNote { get; init; }
}

/// <summary>
/// A Ground Intelligence watchpoint — a monitored location (beta). Mirrors the
/// spec's <c>EntityCreateWatchpointTypeResponseBody</c> /
/// <c>EntityUpdateWatchpointTypeResponseBody</c> (byte-identical twins).
/// </summary>
public sealed record Watchpoint
{
    /// <summary>Unique identifier for the watchpoint. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Customer-provided name for the watchpoint, or null when unavailable. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Customer-provided note about the watchpoint, or null when unavailable. Spec marks REQUIRED.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>Where the watchpoint is monitoring. Spec marks REQUIRED.</summary>
    [JsonPropertyName("location")]
    public WatchpointLatLng? Location { get; init; }

    /// <summary>
    /// Monitoring cadence: <c>unknown</c>, <c>justOnce</c>, <c>daily</c>,
    /// <c>weekly</c> or <c>monthly</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; init; }

    /// <summary>
    /// What is being observed (e.g. <c>roadDefect</c>, <c>utilityCut</c>,
    /// <c>guardrail</c>, <c>streetlight</c>, <c>signage</c>, <c>stormDrain</c>,
    /// <c>graffiti</c>, <c>vegetation</c>, <c>blight</c>, <c>illegalDumping</c>,
    /// <c>littering</c>, <c>highVegetationWeeds</c>, <c>fire</c>, <c>other</c>,
    /// <c>unknown</c>). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("observationType")]
    public string? ObservationType { get; init; }

    /// <summary>Watchpoint status: <c>unknown</c>, <c>active</c> or <c>completed</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Number of observations collected for the watchpoint. Spec marks REQUIRED.</summary>
    [JsonPropertyName("observationCount")]
    public long? ObservationCount { get; init; }

    /// <summary>Time when the most recent observation was collected, or null when unavailable. Spec marks REQUIRED.</summary>
    [JsonPropertyName("lastObservationTime")]
    public string? LastObservationTime { get; init; }

    /// <summary>Start of the current monitoring run. Spec marks REQUIRED.</summary>
    [JsonPropertyName("monitoringStartTime")]
    public string? MonitoringStartTime { get; init; }

    /// <summary>Server-derived end of the current monitoring window. Spec marks REQUIRED.</summary>
    [JsonPropertyName("monitoringEndTime")]
    public string? MonitoringEndTime { get; init; }

    /// <summary>Organization-scoped URL that opens this watchpoint in the Samsara dashboard. Spec marks REQUIRED.</summary>
    [JsonPropertyName("samsaraDashboardUrl")]
    public string? SamsaraDashboardUrl { get; init; }

    /// <summary>Time when the watchpoint resource was created. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>Time when the public watchpoint projection was most recently updated. Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }
}

/// <summary>
/// A WGS84 coordinate pair on a watchpoint. Mirrors the spec's
/// <c>WatchpointLatLngTypeResponseBody</c>.
/// </summary>
public sealed record WatchpointLatLng
{
    /// <summary>Latitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec marks REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// The seed coordinate for a new watchpoint. Mirrors the spec's
/// <c>WatchpointLatLngTypeRequestBody</c>, which — unlike its response twin —
/// marks both members required, so it needs its own <c>*Input</c> record.
/// </summary>
public sealed record WatchpointLatLngInput
{
    /// <summary>Latitude in decimal degrees. Spec REQUIRED.</summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    /// <summary>Longitude in decimal degrees. Spec REQUIRED.</summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}

/// <summary>
/// Request body for <c>POST /ground-intelligence/watchpoints</c>
/// (<c>createWatchpoint</c>, beta). Mirrors the spec's
/// <c>EntityWatchpointsServiceCreateWatchpointRequestBody</c>.
/// </summary>
public sealed record CreateWatchpointRequest
{
    /// <summary>Where to monitor. Spec REQUIRED.</summary>
    [JsonPropertyName("location")]
    public required WatchpointLatLngInput Location { get; init; }

    /// <summary>
    /// Monitoring cadence: <c>justOnce</c>, <c>daily</c>, <c>weekly</c> or
    /// <c>monthly</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("mode")]
    public required string Mode { get; init; }

    /// <summary>
    /// What to observe (e.g. <c>roadDefect</c>, <c>utilityCut</c>,
    /// <c>guardrail</c>, <c>streetlight</c>, <c>signage</c>, <c>stormDrain</c>,
    /// <c>graffiti</c>, <c>vegetation</c>, <c>blight</c>, <c>illegalDumping</c>,
    /// <c>littering</c>, <c>highVegetationWeeds</c>, <c>fire</c>, <c>other</c>).
    /// Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("observationType")]
    public required string ObservationType { get; init; }

    /// <summary>Customer-provided name for the watchpoint.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Customer-provided note about the watchpoint.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /ground-intelligence/watchpoints</c>
/// (<c>updateWatchpoint</c>, beta). Mirrors the spec's
/// <c>EntityWatchpointsServiceUpdateWatchpointRequestBody</c>. The spec marks no
/// field required, which is why this is a separate record from
/// <see cref="CreateWatchpointRequest"/>.
/// </summary>
public sealed record UpdateWatchpointRequest
{
    /// <summary>Customer-provided name for the watchpoint. Set to null to clear.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Customer-provided note about the watchpoint. Set to null to clear.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>
    /// New observation type (e.g. <c>roadDefect</c>, <c>utilityCut</c>,
    /// <c>guardrail</c>, <c>streetlight</c>, <c>signage</c>, <c>stormDrain</c>,
    /// <c>graffiti</c>, <c>vegetation</c>, <c>blight</c>, <c>illegalDumping</c>,
    /// <c>littering</c>, <c>highVegetationWeeds</c>, <c>fire</c>, <c>other</c>).
    /// </summary>
    [JsonPropertyName("observationType")]
    public string? ObservationType { get; init; }
}
