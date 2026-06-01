namespace Samsara.Sdk.Models.Organization;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>Compliance settings for the organization. Mirrors the spec's
/// <c>SettingsComplianceResponseObjectResponseBody</c>.</summary>
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
}

/// <summary>Request body for updating compliance settings (<c>PATCH /fleet/settings/compliance</c>).</summary>
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
}

/// <summary>Driver app settings for the organization. Mirrors the spec's
/// <c>DriverAppSettingsResponseObjectResponseBody</c>.</summary>
public sealed record DriverAppSettings
{
    [JsonPropertyName("driverFleetId")] public string? DriverFleetId { get; init; }
    [JsonPropertyName("gamification")] public bool? Gamification { get; init; }

    /// <summary>Gamification configuration. Mirrors the spec's
    /// <c>DriverAppSettingsGamificationConfigTinyObjectResponseBody</c>.</summary>
    [JsonPropertyName("gamificationConfig")] public DriverAppGamificationConfig? GamificationConfig { get; init; }

    [JsonPropertyName("orgVehicleSearch")] public bool? OrgVehicleSearch { get; init; }
    [JsonPropertyName("trailerSelection")] public bool? TrailerSelection { get; init; }

    /// <summary>Trailer-selection configuration. Mirrors the spec's
    /// <c>DriverAppSettingsTrailerSelectionConfigTinyObjectResponseBody</c>.</summary>
    [JsonPropertyName("trailerSelectionConfig")] public DriverAppTrailerSelectionConfig? TrailerSelectionConfig { get; init; }
}

/// <summary>Request body for updating driver app settings (<c>PATCH /fleet/settings/driver-app</c>).</summary>
public sealed record UpdateDriverAppSettingsRequest
{
    [JsonPropertyName("driverFleetId")] public string? DriverFleetId { get; init; }
    [JsonPropertyName("gamification")] public bool? Gamification { get; init; }

    /// <summary>Gamification configuration. Mirrors the spec's
    /// <c>DriverAppSettingsGamificationConfigTinyObjectResponseBody</c>.</summary>
    [JsonPropertyName("gamificationConfig")] public DriverAppGamificationConfig? GamificationConfig { get; init; }

    [JsonPropertyName("orgVehicleSearch")] public bool? OrgVehicleSearch { get; init; }
    [JsonPropertyName("trailerSelection")] public bool? TrailerSelection { get; init; }

    /// <summary>Trailer-selection configuration. Mirrors the spec's
    /// <c>DriverAppSettingsTrailerSelectionConfigTinyObjectResponseBody</c>.</summary>
    [JsonPropertyName("trailerSelectionConfig")] public DriverAppTrailerSelectionConfig? TrailerSelectionConfig { get; init; }
}

/// <summary>Gamification configuration for the driver app. Mirrors the spec's
/// <c>DriverAppSettingsGamificationConfigTinyObjectResponseBody</c>.</summary>
public sealed record DriverAppGamificationConfig
{
    /// <summary>Whether driver names are anonymized on gamification leaderboards.</summary>
    [JsonPropertyName("anonymizeDriverNames")] public bool? AnonymizeDriverNames { get; init; }
}

/// <summary>Trailer-selection configuration for the driver app. Mirrors the spec's
/// <c>DriverAppSettingsTrailerSelectionConfigTinyObjectResponseBody</c>.</summary>
public sealed record DriverAppTrailerSelectionConfig
{
    /// <summary>Whether drivers may create trailers from the app.</summary>
    [JsonPropertyName("driverTrailerCreationEnabled")] public bool? DriverTrailerCreationEnabled { get; init; }

    /// <summary>Maximum number of trailers a driver may select.</summary>
    [JsonPropertyName("maxNumOfTrailersSelected")] public int? MaxNumOfTrailersSelected { get; init; }

    /// <summary>Whether drivers may search the whole org's trailers.</summary>
    [JsonPropertyName("orgTrailerSearch")] public bool? OrgTrailerSearch { get; init; }
}

/// <summary>Safety settings for the organization. Mirrors the spec's
/// <c>SafetySettingsObjectResponseBody</c> returned by <c>GET /fleet/settings/safety</c>.</summary>
public sealed record SafetySettings
{
    /// <summary>Default vehicle type for the organization. Spec-required.</summary>
    [JsonPropertyName("defaultVehicleType")] public required string DefaultVehicleType { get; init; }

    /// <summary>Distracted-driving detection alert settings. Spec-required. Mirrors the spec's
    /// <c>DistractedDrivingDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("distractedDrivingDetectionAlerts")] public required SafetyDistractedDrivingAlertSettings DistractedDrivingDetectionAlerts { get; init; }

    /// <summary>Following-distance detection alert settings. Spec-required. Mirrors the spec's
    /// <c>FollowingDistanceDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("followingDistanceDetectionAlerts")] public required SafetyFollowingDistanceAlertSettings FollowingDistanceDetectionAlerts { get; init; }

    /// <summary>Forward-collision detection alert settings. Spec-required. Mirrors the spec's
    /// <c>ForwardCollisionDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("forwardCollisionDetectionAlerts")] public required SafetyForwardCollisionAlertSettings ForwardCollisionDetectionAlerts { get; init; }

    /// <summary>Harsh-event sensitivity settings (v1). Spec-required. Mirrors the spec's
    /// <c>HarshEventSensitivitySettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("harshEventSensitivity")] public required SafetyHarshEventSensitivitySettings HarshEventSensitivity { get; init; }

    /// <summary>Harsh-event sensitivity settings (v2). Spec-required. Mirrors the spec's
    /// <c>HarshEventSensitivityV2SettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("harshEventSensitivityV2")] public required SafetyHarshEventSensitivityV2Settings HarshEventSensitivityV2 { get; init; }

    /// <summary>Policy-violation detection alert settings. Spec-required. Mirrors the spec's
    /// <c>PolicyViolationsDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("policyViolationsDetectionAlerts")] public required SafetyPolicyViolationsAlertSettings PolicyViolationsDetectionAlerts { get; init; }

    /// <summary>Rolling-stop detection alert settings. Spec-required. Mirrors the spec's
    /// <c>RollingStopDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("rollingStopDetectionAlerts")] public required SafetyRollingStopAlertSettings RollingStopDetectionAlerts { get; init; }

    /// <summary>Safety-score weight configuration. Spec-required. Mirrors the spec's
    /// <c>SafetyScoreConfigurationSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("safetyScoreConfiguration")] public required SafetyScoreConfiguration SafetyScoreConfiguration { get; init; }

    /// <summary>Target safety score for the organization. Spec-required.</summary>
    [JsonPropertyName("safetyScoreTarget")] public required long SafetyScoreTarget { get; init; }

    /// <summary>Speeding alert settings. Spec-required. Mirrors the spec's
    /// <c>SpeedingSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("speedingSettings")] public required SafetySpeedingSettings SpeedingSettings { get; init; }

    /// <summary>Voice-coaching settings. Spec-required. Mirrors the spec's
    /// <c>VoiceCoachingSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("voiceCoaching")] public required SafetyVoiceCoachingSettings VoiceCoaching { get; init; }
}

/// <summary>Distracted-driving detection alert settings. Mirrors the spec's
/// <c>DistractedDrivingDetectionAlertSettingsObjectResponseBody</c>. The nested
/// <c>inattentiveDrivingDetectionAlerts</c>/<c>mobileUsageDetectionAlerts</c> sub-objects are left as
/// <see cref="JsonElement"/> to preserve their full nested payloads.</summary>
public sealed record SafetyDistractedDrivingAlertSettings
{
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }
    [JsonPropertyName("inattentiveDrivingDetectionAlerts")] public JsonElement? InattentiveDrivingDetectionAlerts { get; init; }
    [JsonPropertyName("mobileUsageDetectionAlerts")] public JsonElement? MobileUsageDetectionAlerts { get; init; }
}

/// <summary>Following-distance detection alert settings. Mirrors the spec's
/// <c>FollowingDistanceDetectionAlertSettingsObjectResponseBody</c>.</summary>
public sealed record SafetyFollowingDistanceAlertSettings
{
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }
    [JsonPropertyName("hasInCabAudioAlertsEnabled")] public bool? HasInCabAudioAlertsEnabled { get; init; }
    [JsonPropertyName("durationMs")] public long? DurationMs { get; init; }
    [JsonPropertyName("speedingThresholdMph")] public double? SpeedingThresholdMph { get; init; }
}

/// <summary>Forward-collision detection alert settings. Mirrors the spec's
/// <c>ForwardCollisionDetectionAlertSettingsObjectResponseBody</c>.</summary>
public sealed record SafetyForwardCollisionAlertSettings
{
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }
    [JsonPropertyName("hasInCabAudioAlertsEnabled")] public bool? HasInCabAudioAlertsEnabled { get; init; }

    /// <summary>Detection sensitivity (<c>near</c>, <c>medium</c>, <c>far</c>).</summary>
    [JsonPropertyName("sensitivity")] public string? Sensitivity { get; init; }
}

/// <summary>Harsh-event sensitivity settings (v1). Mirrors the spec's
/// <c>HarshEventSensitivitySettingsObjectResponseBody</c>. The per-axis g-force sub-objects are left as
/// <see cref="JsonElement"/> to preserve their full nested payloads.</summary>
public sealed record SafetyHarshEventSensitivitySettings
{
    [JsonPropertyName("harshAccelSensitivityGForce")] public JsonElement? HarshAccelSensitivityGForce { get; init; }
    [JsonPropertyName("harshBrakeSensitivityGForce")] public JsonElement? HarshBrakeSensitivityGForce { get; init; }
    [JsonPropertyName("harshTurnSensitivityGForce")] public JsonElement? HarshTurnSensitivityGForce { get; init; }
}

/// <summary>Harsh-event sensitivity settings (v2). Mirrors the spec's
/// <c>HarshEventSensitivityV2SettingsObjectResponseBody</c>. The per-axis sensitivity sub-objects are
/// left as <see cref="JsonElement"/> to preserve their full nested payloads.</summary>
public sealed record SafetyHarshEventSensitivityV2Settings
{
    [JsonPropertyName("harshAccelSensitivity")] public JsonElement? HarshAccelSensitivity { get; init; }
    [JsonPropertyName("harshBrakeSensitivity")] public JsonElement? HarshBrakeSensitivity { get; init; }
    [JsonPropertyName("harshTurnSensitivity")] public JsonElement? HarshTurnSensitivity { get; init; }
}

/// <summary>Policy-violation detection alert settings. Mirrors the spec's
/// <c>PolicyViolationsDetectionAlertSettingsObjectResponseBody</c>.</summary>
public sealed record SafetyPolicyViolationsAlertSettings
{
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }
    [JsonPropertyName("hasInCabAudioAlertsEnabled")] public bool? HasInCabAudioAlertsEnabled { get; init; }
    [JsonPropertyName("speedingThresholdMph")] public double? SpeedingThresholdMph { get; init; }
    [JsonPropertyName("eventsAvailableForTesting")] public IReadOnlyList<string>? EventsAvailableForTesting { get; init; }
    [JsonPropertyName("eventsToCoach")] public IReadOnlyList<string>? EventsToCoach { get; init; }
}

/// <summary>Rolling-stop detection alert settings. Mirrors the spec's
/// <c>RollingStopDetectionAlertSettingsObjectResponseBody</c>.</summary>
public sealed record SafetyRollingStopAlertSettings
{
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }
    [JsonPropertyName("speedingThresholdMph")] public double? SpeedingThresholdMph { get; init; }
}

/// <summary>Speeding alert settings. Mirrors the spec's
/// <c>SpeedingSettingsObjectResponseBody</c>. The <c>severityLevels</c> array entries are left as
/// <see cref="JsonElement"/> to preserve their full per-level payloads.</summary>
public sealed record SafetySpeedingSettings
{
    /// <summary>Unit for speeding thresholds (<c>milesPerHour</c>, <c>kilometersPerHour</c>, <c>percentage</c>).</summary>
    [JsonPropertyName("unit")] public string? Unit { get; init; }

    /// <summary>Configured speeding severity levels.</summary>
    [JsonPropertyName("severityLevels")] public IReadOnlyList<JsonElement>? SeverityLevels { get; init; }
}

/// <summary>Voice-coaching settings. Mirrors the spec's
/// <c>VoiceCoachingSettingsObjectResponseBody</c>.</summary>
public sealed record SafetyVoiceCoachingSettings
{
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }

    /// <summary>Coaching voice language (e.g. <c>english</c>, <c>spanish</c>, <c>french</c>).</summary>
    [JsonPropertyName("language")] public string? Language { get; init; }

    [JsonPropertyName("speedingThresholdMph")] public double? SpeedingThresholdMph { get; init; }
    [JsonPropertyName("eventsToCoach")] public IReadOnlyList<string>? EventsToCoach { get; init; }
}

/// <summary>Safety-score weight configuration. Mirrors the spec's
/// <c>SafetyScoreConfigurationSettingsObjectResponseBody</c>: a flat set of integer weights per event
/// type used when computing the organization's safety score.</summary>
public sealed record SafetyScoreConfiguration
{
    [JsonPropertyName("aiInattentiveDrivingDetectionWeight")] public int? AiInattentiveDrivingDetectionWeight { get; init; }
    [JsonPropertyName("crashWeight")] public int? CrashWeight { get; init; }
    [JsonPropertyName("defensiveDrivingWeight")] public int? DefensiveDrivingWeight { get; init; }
    [JsonPropertyName("didNotYieldWeight")] public int? DidNotYieldWeight { get; init; }
    [JsonPropertyName("drowsyWeight")] public int? DrowsyWeight { get; init; }
    [JsonPropertyName("eatingDrinkingWeight")] public int? EatingDrinkingWeight { get; init; }
    [JsonPropertyName("followingDistanceModerateWeight")] public int? FollowingDistanceModerateWeight { get; init; }
    [JsonPropertyName("followingDistanceSevereWeight")] public int? FollowingDistanceSevereWeight { get; init; }
    [JsonPropertyName("followingDistanceWeight")] public int? FollowingDistanceWeight { get; init; }
    [JsonPropertyName("forwardCollisionWarningWeight")] public int? ForwardCollisionWarningWeight { get; init; }
    [JsonPropertyName("harshAccelWeight")] public int? HarshAccelWeight { get; init; }
    [JsonPropertyName("harshBrakeWeight")] public int? HarshBrakeWeight { get; init; }
    [JsonPropertyName("harshTurnWeight")] public int? HarshTurnWeight { get; init; }
    [JsonPropertyName("heavySpeedingWeight")] public int? HeavySpeedingWeight { get; init; }
    [JsonPropertyName("inattentiveDrivingWeight")] public int? InattentiveDrivingWeight { get; init; }
    [JsonPropertyName("laneDepartureWeight")] public int? LaneDepartureWeight { get; init; }
    [JsonPropertyName("lateResponseWeight")] public int? LateResponseWeight { get; init; }
    [JsonPropertyName("lightSpeedingWeight")] public int? LightSpeedingWeight { get; init; }
    [JsonPropertyName("maxSpeedWeight")] public int? MaxSpeedWeight { get; init; }
    [JsonPropertyName("mobileUsageWeight")] public int? MobileUsageWeight { get; init; }
    [JsonPropertyName("moderateSpeedingWeight")] public int? ModerateSpeedingWeight { get; init; }
    [JsonPropertyName("nearCollisionWeight")] public int? NearCollisionWeight { get; init; }
    [JsonPropertyName("noSeatbeltWeight")] public int? NoSeatbeltWeight { get; init; }
    [JsonPropertyName("obstructedCameraWeight")] public int? ObstructedCameraWeight { get; init; }
    [JsonPropertyName("ranRedLightWeight")] public int? RanRedLightWeight { get; init; }
    [JsonPropertyName("rollingStopWeight")] public int? RollingStopWeight { get; init; }
    [JsonPropertyName("severeSpeedingWeight")] public int? SevereSpeedingWeight { get; init; }
    [JsonPropertyName("smokingWeight")] public int? SmokingWeight { get; init; }
    [JsonPropertyName("speedingWeight")] public int? SpeedingWeight { get; init; }
    [JsonPropertyName("vulnerableRoadUserWeight")] public int? VulnerableRoadUserWeight { get; init; }
}
