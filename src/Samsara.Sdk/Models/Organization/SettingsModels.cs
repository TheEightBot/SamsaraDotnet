namespace Samsara.Sdk.Models.Organization;

using System.Text.Json.Serialization;

/// <summary>Compliance settings for the organization.</summary>
public sealed record ComplianceSettings
{
    [JsonPropertyName("hosEnabled")] public bool? HosEnabled { get; init; }
    [JsonPropertyName("dvirEnabled")] public bool? DvirEnabled { get; init; }
    [JsonPropertyName("eldExemptEnabled")] public bool? EldExemptEnabled { get; init; }
    [JsonPropertyName("defaultCycleRule")] public string? DefaultCycleRule { get; init; }
    [JsonPropertyName("defaultHosRule")] public string? DefaultHosRule { get; init; }
}

/// <summary>Request body for updating compliance settings.</summary>
public sealed record UpdateComplianceSettingsRequest
{
    [JsonPropertyName("hosEnabled")] public bool? HosEnabled { get; init; }
    [JsonPropertyName("dvirEnabled")] public bool? DvirEnabled { get; init; }
    [JsonPropertyName("eldExemptEnabled")] public bool? EldExemptEnabled { get; init; }
    [JsonPropertyName("defaultCycleRule")] public string? DefaultCycleRule { get; init; }
    [JsonPropertyName("defaultHosRule")] public string? DefaultHosRule { get; init; }
}

/// <summary>Driver app settings for the organization.</summary>
public sealed record DriverAppSettings
{
    [JsonPropertyName("messageEnabled")] public bool? MessageEnabled { get; init; }
    [JsonPropertyName("navigationEnabled")] public bool? NavigationEnabled { get; init; }
    [JsonPropertyName("driverRewardsEnabled")] public bool? DriverRewardsEnabled { get; init; }
    [JsonPropertyName("vehiclePreviewEnabled")] public bool? VehiclePreviewEnabled { get; init; }
    [JsonPropertyName("coachingAlertsEnabled")] public bool? CoachingAlertsEnabled { get; init; }
}

/// <summary>Request body for updating driver app settings.</summary>
public sealed record UpdateDriverAppSettingsRequest
{
    [JsonPropertyName("messageEnabled")] public bool? MessageEnabled { get; init; }
    [JsonPropertyName("navigationEnabled")] public bool? NavigationEnabled { get; init; }
    [JsonPropertyName("driverRewardsEnabled")] public bool? DriverRewardsEnabled { get; init; }
    [JsonPropertyName("vehiclePreviewEnabled")] public bool? VehiclePreviewEnabled { get; init; }
}

/// <summary>Safety settings for the organization.</summary>
public sealed record SafetySettings
{
    [JsonPropertyName("forwardCollisionWarningEnabled")] public bool? ForwardCollisionWarningEnabled { get; init; }
    [JsonPropertyName("laneDepartureWarningEnabled")] public bool? LaneDepartureWarningEnabled { get; init; }
    [JsonPropertyName("speedingEnabled")] public bool? SpeedingEnabled { get; init; }
    [JsonPropertyName("harshAccelerationEnabled")] public bool? HarshAccelerationEnabled { get; init; }
    [JsonPropertyName("harshBrakingEnabled")] public bool? HarshBrakingEnabled { get; init; }
    [JsonPropertyName("harshCorneringEnabled")] public bool? HarshCorneringEnabled { get; init; }
}
