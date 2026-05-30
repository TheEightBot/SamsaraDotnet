namespace Samsara.Sdk.Models.Media;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a single uploaded media file. Mirrors the spec's
/// <c>UploadedMediaObjectResponseBody</c> returned by
/// <c>GET /cameras/media</c>.
/// </summary>
public sealed record MediaFile
{
    /// <summary>
    /// Timestamp at which the media item was made available (RFC 3339).
    /// Spec-required for <c>GET /cameras/media</c>.
    /// </summary>
    [JsonPropertyName("availableAtTime")]
    public required string AvailableAtTime { get; init; }

    /// <summary>
    /// End time of the media (RFC 3339). Spec-required for
    /// <c>GET /cameras/media</c>.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required string EndTime { get; init; }

    /// <summary>
    /// Input source for this media. Spec-required. Valid values:
    /// <c>dashcamForwardFacing</c>, <c>dashcamInwardFacing</c>,
    /// <c>analog1</c>, <c>analog2</c>, <c>analog3</c>, <c>analog4</c>.
    /// </summary>
    [JsonPropertyName("input")]
    public required string Input { get; init; }

    /// <summary>
    /// Type of media. Spec-required. Valid values: <c>image</c>,
    /// <c>videoHighRes</c>, <c>videoLowRes</c>, <c>hyperlapse</c>.
    /// </summary>
    [JsonPropertyName("mediaType")]
    public required string MediaType { get; init; }

    /// <summary>
    /// Start time of the media (RFC 3339). Spec-required for
    /// <c>GET /cameras/media</c>.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required string StartTime { get; init; }

    /// <summary>
    /// Trigger reason for this media capture. Spec-required. Valid values:
    /// <c>api</c>, <c>panicButton</c>, <c>periodicStill</c>, <c>rfidEvent</c>,
    /// <c>safetyEvent</c>, <c>tripEndStill</c>, <c>tripStartStill</c>,
    /// <c>videoRetrieval</c>.
    /// </summary>
    [JsonPropertyName("triggerReason")]
    public required string TriggerReason { get; init; }

    /// <summary>
    /// Vehicle ID for which this media was captured. Spec-required for
    /// <c>GET /cameras/media</c>.
    /// </summary>
    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    /// <summary>
    /// Camera role for this media. Optional in spec. Valid values include
    /// <c>leftMirrorMount</c>, <c>leftSide</c>, <c>rightMirrorMount</c>,
    /// <c>rightSide</c>, <c>rearHigh</c>, <c>rearBumper</c>, <c>inCab</c>,
    /// <c>front</c>, <c>hopper</c>, <c>other1</c>-<c>other4</c>,
    /// <c>leftBev</c>, <c>rightBev</c>, <c>rearBev</c>, <c>frontBev</c>,
    /// <c>otherBev</c>, <c>bevNotUsed</c>.
    /// </summary>
    [JsonPropertyName("cameraRole")]
    public string? CameraRole { get; init; }

    /// <summary>
    /// URL info for this media. Only populated when the media's
    /// <c>status</c> is <c>available</c>.
    /// </summary>
    [JsonPropertyName("urlInfo")]
    public MediaUrlInfo? UrlInfo { get; init; }
}

/// <summary>
/// URL info for a media item. Mirrors the spec's
/// <c>UrlInfoObjectResponseBody</c>. Populated only when a media item's
/// status is <c>available</c>.
/// </summary>
public sealed record MediaUrlInfo
{
    /// <summary>
    /// Signed URL for the media. The URL expires in 8 hours (after which a
    /// new GET request must be made). Spec-required when present.
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    /// <summary>
    /// Timestamp at which the signed URL expires (RFC 3339).
    /// Spec-required when present.
    /// </summary>
    [JsonPropertyName("urlExpiryTime")]
    public required string UrlExpiryTime { get; init; }
}

/// <summary>
/// Represents a media retrieval job. Used for both
/// <c>GET /cameras/media/retrieval</c> (spec
/// <c>MediaObjectResponseBody</c>) and
/// <c>POST /cameras/media/retrieval</c> (spec
/// <c>PostMediaRetrievalObjectResponseBody</c>). Because the two endpoints
/// return disjoint shapes, most spec-required fields are modeled as nullable
/// here even when individually required on one endpoint or the other.
/// </summary>
public sealed record MediaRetrieval
{
    // ── GET /cameras/media/retrieval (MediaObjectResponseBody) ──────────────

    /// <summary>
    /// Input source for this media. Spec-required on
    /// <c>GET /cameras/media/retrieval</c>. Not returned by
    /// <c>POST /cameras/media/retrieval</c>. Valid values:
    /// <c>dashcamDriverFacing</c>, <c>dashcamRoadFacing</c>,
    /// <c>analog1</c>, <c>analog2</c>, <c>analog3</c>, <c>analog4</c>.
    /// </summary>
    [JsonPropertyName("input")]
    public string? Input { get; init; }

    /// <summary>
    /// Type of media. Spec-required on
    /// <c>GET /cameras/media/retrieval</c>. Not returned by
    /// <c>POST /cameras/media/retrieval</c>. Valid values: <c>image</c>,
    /// <c>videoHighRes</c>, <c>videoLowRes</c>, <c>hyperlapse</c>.
    /// </summary>
    [JsonPropertyName("mediaType")]
    public string? MediaType { get; init; }

    /// <summary>
    /// Status of the media. Spec-required on
    /// <c>GET /cameras/media/retrieval</c>. Not returned by
    /// <c>POST /cameras/media/retrieval</c>. Valid values:
    /// <c>available</c>, <c>invalid</c>, <c>pending</c>, <c>failed</c>,
    /// <c>unavailable</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// Vehicle ID for which this media was captured. Spec-required on
    /// <c>GET /cameras/media/retrieval</c>. Not returned by
    /// <c>POST /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    /// <summary>
    /// Start time of the media (RFC 3339). Spec-required on
    /// <c>GET /cameras/media/retrieval</c>. Not returned by
    /// <c>POST /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>
    /// End time of the media (RFC 3339). Spec-required on
    /// <c>GET /cameras/media/retrieval</c>. Not returned by
    /// <c>POST /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    /// <summary>
    /// Timestamp at which the media item was made available (RFC 3339).
    /// Optional on <c>GET /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("availableAtTime")]
    public string? AvailableAtTime { get; init; }

    /// <summary>
    /// Camera role for this media. Optional on
    /// <c>GET /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("cameraRole")]
    public string? CameraRole { get; init; }

    /// <summary>
    /// URL info for this media. Only populated when <see cref="Status"/> is
    /// <c>available</c>.
    /// </summary>
    [JsonPropertyName("urlInfo")]
    public MediaUrlInfo? UrlInfo { get; init; }

    // ── POST /cameras/media/retrieval (PostMediaRetrievalObjectResponseBody) ──

    /// <summary>
    /// Quota status for this media capture request (e.g. "Current monthly
    /// usage is 80000.4 seconds of high-res video out of 900000.0
    /// available."). Spec-required on
    /// <c>POST /cameras/media/retrieval</c>. Not returned by
    /// <c>GET /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("quotaStatus")]
    public string? QuotaStatus { get; init; }

    /// <summary>
    /// Retrieval ID associated with the media capture request.
    /// Spec-required on <c>POST /cameras/media/retrieval</c>. Not returned by
    /// <c>GET /cameras/media/retrieval</c>.
    /// </summary>
    [JsonPropertyName("retrievalId")]
    public string? RetrievalId { get; init; }
}

/// <summary>
/// Request body for <c>POST /cameras/media/retrieval</c>. Mirrors the spec
/// <c>MediaRetrievalPostMediaRetrievalRequestBody</c> schema.
/// </summary>
public sealed record CreateMediaRetrievalRequest
{
    /// <summary>
    /// Vehicle ID for which to initiate media capture. Spec-required.
    /// </summary>
    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    /// <summary>
    /// Start time in RFC 3339 format (millisecond precision and timezones
    /// supported). Spec-required.
    /// </summary>
    [JsonPropertyName("startTime")]
    public required DateTimeOffset StartTime { get; init; }

    /// <summary>
    /// End time in RFC 3339 format. If <c>endTime</c> equals
    /// <c>startTime</c>, an image is captured at <c>startTime</c>. Otherwise
    /// must be at least 1 second after <c>startTime</c> and within the
    /// maximum allowed duration per video retrieval type. Spec-required.
    /// </summary>
    [JsonPropertyName("endTime")]
    public required DateTimeOffset EndTime { get; init; }

    /// <summary>
    /// Desired camera inputs for which to capture media. Only media with
    /// valid inputs (the device has that input stream and was recording at
    /// the time) will be uploaded. An empty list is invalid. Spec-required.
    /// Valid values: <c>dashcamRoadFacing</c>, <c>dashcamDriverFacing</c>,
    /// <c>analog1</c>, <c>analog2</c>, <c>analog3</c>, <c>analog4</c>.
    /// </summary>
    [JsonPropertyName("inputs")]
    public required IReadOnlyList<string> Inputs { get; init; }

    /// <summary>
    /// Desired media type. If a video is requested, <c>endTime</c> must be
    /// after <c>startTime</c>. If an image is requested, <c>endTime</c>
    /// must equal <c>startTime</c>. Spec-required. Valid values:
    /// <c>image</c>, <c>videoHighRes</c>, <c>videoLowRes</c>,
    /// <c>hyperlapse</c>.
    /// </summary>
    [JsonPropertyName("mediaType")]
    public required string MediaType { get; init; }
}
