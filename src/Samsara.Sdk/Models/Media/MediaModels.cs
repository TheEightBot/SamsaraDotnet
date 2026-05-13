namespace Samsara.Sdk.Models.Media;

using System.Text.Json.Serialization;

public sealed record MediaFile
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("mediaType")]
    public string? MediaType { get; init; }

    [JsonPropertyName("capturedAtTime")]
    public string? CapturedAtTime { get; init; }

    [JsonPropertyName("uploadedAtTime")]
    public string? UploadedAtTime { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("thumbnailUrl")]
    public string? ThumbnailUrl { get; init; }

    [JsonPropertyName("cameraId")]
    public string? CameraId { get; init; }

    [JsonPropertyName("safetyEventId")]
    public string? SafetyEventId { get; init; }

    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }
}

/// <summary>Represents a media retrieval job.</summary>
public sealed record MediaRetrieval
{
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("url")] public string? Url { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("cameraId")] public string? CameraId { get; init; }
    [JsonPropertyName("startTime")] public DateTimeOffset? StartTime { get; init; }
    [JsonPropertyName("endTime")] public DateTimeOffset? EndTime { get; init; }
}

/// <summary>Request body for creating a media retrieval job.</summary>
public sealed record CreateMediaRetrievalRequest
{
    [JsonPropertyName("vehicleId")] public required string VehicleId { get; init; }
    [JsonPropertyName("cameraId")] public required string CameraId { get; init; }
    [JsonPropertyName("startTime")] public required DateTimeOffset StartTime { get; init; }
    [JsonPropertyName("endTime")] public required DateTimeOffset EndTime { get; init; }
}
