namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;

/// <summary>
/// A fleet installer photo upload session (beta). Mirrors the spec's
/// <c>FleetInstallerPhotoUploadSessionResponseBody</c> and its superset
/// <c>FleetInstallerPhotoUploadCreateDataResponseBody</c> — the two schemas are
/// identical except that the create response also carries
/// an <c>uploadContext</c>, so a single record serves
/// <c>GET /fleet/installer/photo-uploads</c>,
/// <c>POST /fleet/installer/photo-uploads</c> and
/// <c>POST /fleet/installer/photo-uploads/complete</c>.
/// </summary>
/// <remarks>
/// Response records are fully nullable: the SDK deserializes leniently, so a
/// spec-required member the API omits must not land in a non-nullable property.
/// </remarks>
public sealed record InstallerPhotoUploadSession
{
    /// <summary>Unique session UUID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Samsara device ID. Spec marks REQUIRED.</summary>
    [JsonPropertyName("deviceId")]
    public string? DeviceId { get; init; }

    /// <summary>
    /// Hardware the photo is for: <c>vehicleGateway</c>, <c>assetGateway</c>,
    /// <c>camera</c>, <c>cameraConnector</c>, <c>environmentalMonitor</c>,
    /// <c>assetTag</c>, <c>trackingLabel</c> or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("hardwareType")]
    public string? HardwareType { get; init; }

    /// <summary>
    /// Photo purpose: <c>installPhoto</c>, <c>assetPhoto</c> or <c>unknown</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("photoType")]
    public string? PhotoType { get; init; }

    /// <summary>
    /// File format: <c>imageJpeg</c>, <c>imagePng</c> or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fileFormatType")]
    public string? FileFormatType { get; init; }

    /// <summary>Original file name supplied at session creation. Spec marks REQUIRED.</summary>
    [JsonPropertyName("fileName")]
    public string? FileName { get; init; }

    /// <summary>File size in bytes. Spec marks REQUIRED.</summary>
    [JsonPropertyName("sizeBytes")]
    public long? SizeBytes { get; init; }

    /// <summary>Base64-encoded MD5 of the file bytes. Spec marks REQUIRED.</summary>
    [JsonPropertyName("contentMd5")]
    public string? ContentMd5 { get; init; }

    /// <summary>
    /// Session state: <c>awaitingUpload</c>, <c>processing</c>, <c>finished</c>
    /// or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("processingStatus")]
    public string? ProcessingStatus { get; init; }

    /// <summary>Session creation timestamp (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>
    /// Timestamp of last state change (RFC 3339). Equals
    /// <see cref="CreatedAtTime"/> at session creation. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

    /// <summary>
    /// The presigned upload target. Present only on the
    /// <c>POST /fleet/installer/photo-uploads</c> response, where the spec marks
    /// it REQUIRED; absent from the list and complete responses.
    /// </summary>
    [JsonPropertyName("uploadContext")]
    public InstallerPhotoUploadContext? UploadContext { get; init; }
}

/// <summary>
/// The presigned S3 target for step 2 of a fleet installer photo upload.
/// Mirrors the spec's <c>FleetInstallerPhotoUploadContextResponseBody</c>.
/// </summary>
public sealed record InstallerPhotoUploadContext
{
    /// <summary>Presigned S3 PUT URL. Valid until <see cref="ExpiresAtTime"/>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("uploadUrl")]
    public string? UploadUrl { get; init; }

    /// <summary>
    /// HTTP headers the client MUST include verbatim on the step-2 PUT request.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("headers")]
    public IReadOnlyDictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Presigned URL expiry (RFC 3339). The step-2 PUT must complete before this
    /// time. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public DateTimeOffset? ExpiresAtTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/installer/photo-uploads</c>
/// (<c>postFleetInstallerPhotoUpload</c>, beta). Mirrors the spec's
/// <c>FleetInstallerPhotoUploadsPostFleetInstallerPhotoUploadRequestBody</c>.
/// </summary>
public sealed record CreateInstallerPhotoUploadRequest
{
    /// <summary>
    /// Samsara device ID. The device must belong to the caller's organization.
    /// Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("deviceId")]
    public required string DeviceId { get; init; }

    /// <summary>
    /// Hardware the photo is for: <c>vehicleGateway</c>, <c>assetGateway</c>,
    /// <c>camera</c>, <c>cameraConnector</c>, <c>environmentalMonitor</c>,
    /// <c>assetTag</c> or <c>trackingLabel</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("hardwareType")]
    public required string HardwareType { get; init; }

    /// <summary>Photo purpose: <c>installPhoto</c> or <c>assetPhoto</c>. Spec REQUIRED.</summary>
    [JsonPropertyName("photoType")]
    public required string PhotoType { get; init; }

    /// <summary>File format: <c>imageJpeg</c> or <c>imagePng</c>. Spec REQUIRED.</summary>
    [JsonPropertyName("fileFormatType")]
    public required string FileFormatType { get; init; }

    /// <summary>
    /// Original file name. Max 255 characters; printable characters only. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("fileName")]
    public required string FileName { get; init; }

    /// <summary>
    /// File size in bytes. Validated against the maximum allowed size (10 MB)
    /// and signed into the presigned URL. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("sizeBytes")]
    public required int SizeBytes { get; init; }

    /// <summary>
    /// Base64-encoded MD5 of the file bytes, signed into the presigned URL as
    /// <c>Content-MD5</c>. Spec REQUIRED.
    /// </summary>
    [JsonPropertyName("contentMd5")]
    public required string ContentMd5 { get; init; }
}
