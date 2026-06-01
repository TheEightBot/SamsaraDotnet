namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

/// <summary>
/// A driver's tachograph activity history entry, returned by
/// <c>GET /fleet/drivers/tachograph-activity/history</c>.
/// </summary>
public sealed record TachographActivity
{
    /// <summary>The driver's tachograph activities over the requested window.</summary>
    [JsonPropertyName("activity")]
    public IReadOnlyList<TachographActivityEntry>? Activity { get; init; }

    /// <summary>The driver the activities belong to.</summary>
    [JsonPropertyName("driver")]
    public TachographDriver? Driver { get; init; }
}

/// <summary>
/// A single tachograph activity interval. Mirrors the spec's
/// <c>TachographActivity</c> array-item schema.
/// </summary>
public sealed record TachographActivityEntry
{
    /// <summary>Start time of the activity, in RFC 3339 format.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End time of the activity, in RFC 3339 format.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>The activity state (e.g. <c>driving</c>, <c>work</c>, <c>rest</c>, <c>available</c>).</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Whether the activity was entered manually by the driver.</summary>
    [JsonPropertyName("isManualEntry")]
    public bool? IsManualEntry { get; init; }
}

/// <summary>
/// A tachograph file-history entry, returned by
/// <c>GET /fleet/drivers/tachograph-files/history</c> (which carries a
/// <see cref="Driver"/>) and <c>GET /fleet/vehicles/tachograph-files/history</c>
/// (which carries a <see cref="Vehicle"/>).
/// </summary>
public sealed record TachographFile
{
    /// <summary>The driver the files belong to (driver-files endpoint).</summary>
    [JsonPropertyName("driver")]
    public TachographDriver? Driver { get; init; }

    /// <summary>The vehicle the files belong to (vehicle-files endpoint).</summary>
    [JsonPropertyName("vehicle")]
    public TachographVehicle? Vehicle { get; init; }

    /// <summary>The tachograph files over the requested window.</summary>
    [JsonPropertyName("files")]
    public IReadOnlyList<TachographFileEntry>? Files { get; init; }
}

/// <summary>
/// A single tachograph file. Mirrors the spec's <c>TachographDriverFile</c>
/// (driver-files endpoint) and <c>TachographVehicleFile</c> (vehicle-files
/// endpoint); the union of their fields is exposed here.
/// </summary>
public sealed record TachographFileEntry
{
    /// <summary>Unique identifier of the file.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Signed download URL for the file.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Time the file was created, in RFC 3339 format.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>The driver card number (driver-files endpoint only).</summary>
    [JsonPropertyName("cardNumber")]
    public string? CardNumber { get; init; }

    /// <summary>The vehicle identification number (vehicle-files endpoint only).</summary>
    [JsonPropertyName("vehicleIdentificationNumber")]
    public string? VehicleIdentificationNumber { get; init; }
}

/// <summary>
/// A minified driver reference on a tachograph response. Mirrors the spec's
/// <c>driverTinyResponse</c>.
/// </summary>
public sealed record TachographDriver
{
    /// <summary>Samsara ID of the driver.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified vehicle reference on a tachograph response. Mirrors the spec's
/// <c>vehicleTinyResponse</c>.
/// </summary>
public sealed record TachographVehicle
{
    /// <summary>Samsara ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External identifiers for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}
