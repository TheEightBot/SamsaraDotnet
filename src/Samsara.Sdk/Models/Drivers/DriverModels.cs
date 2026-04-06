namespace Samsara.Sdk.Models.Drivers;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a driver in the Samsara system.
/// </summary>
public sealed record Driver
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("password")]
    public string? Password { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("licenseNumber")]
    public string? LicenseNumber { get; init; }

    [JsonPropertyName("licenseState")]
    public string? LicenseState { get; init; }

    [JsonPropertyName("eldExempt")]
    public bool? EldExempt { get; init; }

    [JsonPropertyName("eldExemptReason")]
    public string? EldExemptReason { get; init; }

    [JsonPropertyName("eldBigDayExemptionEnabled")]
    public bool? EldBigDayExemptionEnabled { get; init; }

    [JsonPropertyName("eldAdverseWeatherExemptionEnabled")]
    public bool? EldAdverseWeatherExemptionEnabled { get; init; }

    [JsonPropertyName("eldPcEnabled")]
    public bool? EldPcEnabled { get; init; }

    [JsonPropertyName("eldYmEnabled")]
    public bool? EldYmEnabled { get; init; }

    [JsonPropertyName("eldDayStartHour")]
    public int? EldDayStartHour { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("currentVehicleId")]
    public string? CurrentVehicleId { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    [JsonPropertyName("carrierSettings")]
    public DriverCarrierSettings? CarrierSettings { get; init; }

    [JsonPropertyName("staticAssignedVehicle")]
    public DriverVehicleRef? StaticAssignedVehicle { get; init; }

    [JsonPropertyName("tachographCardNumber")]
    public string? TachographCardNumber { get; init; }

    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Carrier-specific settings for a driver (ELD compliance).
/// </summary>
public sealed record DriverCarrierSettings
{
    [JsonPropertyName("carrierName")]
    public string? CarrierName { get; init; }

    [JsonPropertyName("dotNumber")]
    public long? DotNumber { get; init; }

    [JsonPropertyName("mainOfficeAddress")]
    public string? MainOfficeAddress { get; init; }
}

/// <summary>
/// Vehicle reference on a driver object.
/// </summary>
public sealed record DriverVehicleRef
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Request body for creating a new driver.
/// </summary>
public sealed record CreateDriverRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("licenseNumber")]
    public string? LicenseNumber { get; init; }

    [JsonPropertyName("licenseState")]
    public string? LicenseState { get; init; }

    [JsonPropertyName("eldExempt")]
    public bool? EldExempt { get; init; }

    [JsonPropertyName("eldExemptReason")]
    public string? EldExemptReason { get; init; }

    [JsonPropertyName("eldBigDayExemptionEnabled")]
    public bool? EldBigDayExemptionEnabled { get; init; }

    [JsonPropertyName("eldAdverseWeatherExemptionEnabled")]
    public bool? EldAdverseWeatherExemptionEnabled { get; init; }

    [JsonPropertyName("eldPcEnabled")]
    public bool? EldPcEnabled { get; init; }

    [JsonPropertyName("eldYmEnabled")]
    public bool? EldYmEnabled { get; init; }

    [JsonPropertyName("eldDayStartHour")]
    public int? EldDayStartHour { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    [JsonPropertyName("tachographCardNumber")]
    public string? TachographCardNumber { get; init; }
}

/// <summary>
/// Request body for updating a driver (PATCH).
/// </summary>
public sealed record UpdateDriverRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("licenseNumber")]
    public string? LicenseNumber { get; init; }

    [JsonPropertyName("licenseState")]
    public string? LicenseState { get; init; }

    [JsonPropertyName("eldExempt")]
    public bool? EldExempt { get; init; }

    [JsonPropertyName("eldExemptReason")]
    public string? EldExemptReason { get; init; }

    [JsonPropertyName("eldBigDayExemptionEnabled")]
    public bool? EldBigDayExemptionEnabled { get; init; }

    [JsonPropertyName("eldPcEnabled")]
    public bool? EldPcEnabled { get; init; }

    [JsonPropertyName("eldYmEnabled")]
    public bool? EldYmEnabled { get; init; }

    [JsonPropertyName("eldDayStartHour")]
    public int? EldDayStartHour { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    [JsonPropertyName("tachographCardNumber")]
    public string? TachographCardNumber { get; init; }
}
