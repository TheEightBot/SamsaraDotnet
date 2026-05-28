namespace Samsara.Sdk.Models.Organization;

using System.Text.Json.Serialization;

/// <summary>Compliance settings for the organization.</summary>
public sealed record ComplianceSettings
{
    [JsonPropertyName("allowUnregulatedVehiclesEnabled")] public bool? AllowUnregulatedVehiclesEnabled { get; init; }
    [JsonPropertyName("canadaHosEnabled")] public bool? CanadaHosEnabled { get; init; }
    [JsonPropertyName("carrierName")] public string? CarrierName { get; init; }
    [JsonPropertyName("dotNumber")] public long? DotNumber { get; init; }
    [JsonPropertyName("driverAutoDutyEnabled")] public bool? DriverAutoDutyEnabled { get; init; }
    [JsonPropertyName("editCertifiedLogsEnabled")] public bool? EditCertifiedLogsEnabled { get; init; }
    [JsonPropertyName("forceManualLocationForDutyStatusChangesEnabled")] public bool? ForceManualLocationForDutyStatusChangesEnabled { get; init; }
    [JsonPropertyName("forceReviewUnassignedHosEnabled")] public bool? ForceReviewUnassignedHosEnabled { get; init; }
    [JsonPropertyName("mainOfficeFormattedAddress")] public string? MainOfficeFormattedAddress { get; init; }
    [JsonPropertyName("persistentDutyStatusEnabled")] public bool? PersistentDutyStatusEnabled { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("hosEnabled")] public bool? HosEnabled { get; init; }
    [JsonPropertyName("dvirEnabled")] public bool? DvirEnabled { get; init; }
    [JsonPropertyName("eldExemptEnabled")] public bool? EldExemptEnabled { get; init; }
    [JsonPropertyName("defaultCycleRule")] public string? DefaultCycleRule { get; init; }
    [JsonPropertyName("defaultHosRule")] public string? DefaultHosRule { get; init; }
}

/// <summary>Request body for updating compliance settings.</summary>
public sealed record UpdateComplianceSettingsRequest
{
    [JsonPropertyName("allowUnregulatedVehiclesEnabled")] public bool? AllowUnregulatedVehiclesEnabled { get; init; }
    [JsonPropertyName("canadaHosEnabled")] public bool? CanadaHosEnabled { get; init; }
    [JsonPropertyName("carrierName")] public string? CarrierName { get; init; }
    [JsonPropertyName("dotNumber")] public long? DotNumber { get; init; }
    [JsonPropertyName("driverAutoDutyEnabled")] public bool? DriverAutoDutyEnabled { get; init; }
    [JsonPropertyName("editCertifiedLogsEnabled")] public bool? EditCertifiedLogsEnabled { get; init; }
    [JsonPropertyName("forceManualLocationForDutyStatusChangesEnabled")] public bool? ForceManualLocationForDutyStatusChangesEnabled { get; init; }
    [JsonPropertyName("forceReviewUnassignedHosEnabled")] public bool? ForceReviewUnassignedHosEnabled { get; init; }
    [JsonPropertyName("mainOfficeFormattedAddress")] public string? MainOfficeFormattedAddress { get; init; }
    [JsonPropertyName("persistentDutyStatusEnabled")] public bool? PersistentDutyStatusEnabled { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("hosEnabled")] public bool? HosEnabled { get; init; }
    [JsonPropertyName("dvirEnabled")] public bool? DvirEnabled { get; init; }
    [JsonPropertyName("eldExemptEnabled")] public bool? EldExemptEnabled { get; init; }
    [JsonPropertyName("defaultCycleRule")] public string? DefaultCycleRule { get; init; }
    [JsonPropertyName("defaultHosRule")] public string? DefaultHosRule { get; init; }
}

/// <summary>Driver app settings for the organization.</summary>
public sealed record DriverAppSettings
{
    [JsonPropertyName("driverFleetId")] public string? DriverFleetId { get; init; }
    [JsonPropertyName("gamification")] public bool? Gamification { get; init; }
    [JsonPropertyName("gamificationConfig")] public object? GamificationConfig { get; init; }
    [JsonPropertyName("orgVehicleSearch")] public bool? OrgVehicleSearch { get; init; }
    [JsonPropertyName("trailerSelection")] public bool? TrailerSelection { get; init; }
    [JsonPropertyName("trailerSelectionConfig")] public object? TrailerSelectionConfig { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("messageEnabled")] public bool? MessageEnabled { get; init; }
    [JsonPropertyName("navigationEnabled")] public bool? NavigationEnabled { get; init; }
    [JsonPropertyName("driverRewardsEnabled")] public bool? DriverRewardsEnabled { get; init; }
    [JsonPropertyName("vehiclePreviewEnabled")] public bool? VehiclePreviewEnabled { get; init; }
    [JsonPropertyName("coachingAlertsEnabled")] public bool? CoachingAlertsEnabled { get; init; }
}

/// <summary>Request body for updating driver app settings.</summary>
public sealed record UpdateDriverAppSettingsRequest
{
    [JsonPropertyName("driverFleetId")] public string? DriverFleetId { get; init; }
    [JsonPropertyName("gamification")] public bool? Gamification { get; init; }
    [JsonPropertyName("gamificationConfig")] public object? GamificationConfig { get; init; }
    [JsonPropertyName("orgVehicleSearch")] public bool? OrgVehicleSearch { get; init; }
    [JsonPropertyName("trailerSelection")] public bool? TrailerSelection { get; init; }
    [JsonPropertyName("trailerSelectionConfig")] public object? TrailerSelectionConfig { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("messageEnabled")] public bool? MessageEnabled { get; init; }
    [JsonPropertyName("navigationEnabled")] public bool? NavigationEnabled { get; init; }
    [JsonPropertyName("driverRewardsEnabled")] public bool? DriverRewardsEnabled { get; init; }
    [JsonPropertyName("vehiclePreviewEnabled")] public bool? VehiclePreviewEnabled { get; init; }
}

/// <summary>Safety settings for the organization.</summary>
public sealed record SafetySettings
{
    [JsonPropertyName("defaultVehicleType")] public required string DefaultVehicleType { get; init; }
    [JsonPropertyName("distractedDrivingDetectionAlerts")] public required object DistractedDrivingDetectionAlerts { get; init; }
    [JsonPropertyName("followingDistanceDetectionAlerts")] public required object FollowingDistanceDetectionAlerts { get; init; }
    [JsonPropertyName("forwardCollisionDetectionAlerts")] public required object ForwardCollisionDetectionAlerts { get; init; }
    [JsonPropertyName("harshEventSensitivity")] public required object HarshEventSensitivity { get; init; }
    [JsonPropertyName("harshEventSensitivityV2")] public required object HarshEventSensitivityV2 { get; init; }
    [JsonPropertyName("policyViolationsDetectionAlerts")] public required object PolicyViolationsDetectionAlerts { get; init; }
    [JsonPropertyName("rollingStopDetectionAlerts")] public required object RollingStopDetectionAlerts { get; init; }
    [JsonPropertyName("safetyScoreConfiguration")] public required object SafetyScoreConfiguration { get; init; }
    [JsonPropertyName("safetyScoreTarget")] public required long SafetyScoreTarget { get; init; }
    [JsonPropertyName("speedingSettings")] public required object SpeedingSettings { get; init; }
    [JsonPropertyName("voiceCoaching")] public required object VoiceCoaching { get; init; }

    // Not in current spec; retained for back-compat.
    [JsonPropertyName("forwardCollisionWarningEnabled")] public bool? ForwardCollisionWarningEnabled { get; init; }
    [JsonPropertyName("laneDepartureWarningEnabled")] public bool? LaneDepartureWarningEnabled { get; init; }
    [JsonPropertyName("speedingEnabled")] public bool? SpeedingEnabled { get; init; }
    [JsonPropertyName("harshAccelerationEnabled")] public bool? HarshAccelerationEnabled { get; init; }
    [JsonPropertyName("harshBrakingEnabled")] public bool? HarshBrakingEnabled { get; init; }
    [JsonPropertyName("harshCorneringEnabled")] public bool? HarshCorneringEnabled { get; init; }
}
