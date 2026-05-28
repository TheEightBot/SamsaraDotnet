namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

public sealed record TachographActivity
{
    [JsonPropertyName("activity")]
    public IReadOnlyList<object>? Activity { get; init; }

    [JsonPropertyName("driver")]
    public object? Driver { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("activityType")]
    public string? ActivityType { get; init; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    [JsonPropertyName("durationMs")]
    public long? DurationMs { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }
}

public sealed record TachographFile
{
    [JsonPropertyName("driver")]
    public object? Driver { get; init; }

    [JsonPropertyName("files")]
    public IReadOnlyList<object>? Files { get; init; }

    [JsonPropertyName("vehicle")]
    public object? Vehicle { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("fileType")]
    public string? FileType { get; init; }

    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; init; }

    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }
}
