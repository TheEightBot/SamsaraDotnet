namespace Samsara.Sdk.Models.Organization;

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
/// <c>DistractedDrivingDetectionAlertSettingsObjectResponseBody</c>.</summary>
public sealed record SafetyDistractedDrivingAlertSettings
{
    /// <summary>Whether AI event detection for distracted-driving behaviors is turned on.</summary>
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }

    /// <summary>Inattentive-driving detection alert settings. Mirrors the spec's
    /// <c>InattentiveDrivingDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("inattentiveDrivingDetectionAlerts")] public InattentiveDrivingDetectionAlertSettings? InattentiveDrivingDetectionAlerts { get; init; }

    /// <summary>Mobile-usage detection alert settings. Mirrors the spec's
    /// <c>MobileUsageDetectionAlertSettingsObjectResponseBody</c>.</summary>
    [JsonPropertyName("mobileUsageDetectionAlerts")] public MobileUsageDetectionAlertSettings? MobileUsageDetectionAlerts { get; init; }
}

/// <summary>Inattentive-driving detection alert settings. Mirrors the spec's
/// <c>InattentiveDrivingDetectionAlertSettingsObjectResponseBody</c>, nested under
/// <c>distractedDrivingDetectionAlerts</c> on <c>GET /fleet/settings/safety</c>.</summary>
public sealed record InattentiveDrivingDetectionAlertSettings
{
    /// <summary>Whether in-cab audio alerts for inattentive driving are turned on.</summary>
    [JsonPropertyName("hasInCabAudioAlertsEnabled")] public bool? HasInCabAudioAlertsEnabled { get; init; }

    /// <summary>Whether AI event detection for inattentive driving is turned on.</summary>
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }

    /// <summary>Severity of inattentive-driving events that raise an alert
    /// (<c>low</c>, <c>medium</c>, <c>high</c>).</summary>
    [JsonPropertyName("severity")] public string? Severity { get; init; }

    /// <summary>Alert when speed is over this many miles per hour.</summary>
    [JsonPropertyName("speedingThresholdMph")] public double? SpeedingThresholdMph { get; init; }
}

/// <summary>Mobile-usage detection alert settings. Mirrors the spec's
/// <c>MobileUsageDetectionAlertSettingsObjectResponseBody</c>, nested under
/// <c>distractedDrivingDetectionAlerts</c> on <c>GET /fleet/settings/safety</c>.</summary>
public sealed record MobileUsageDetectionAlertSettings
{
    /// <summary>Whether in-cab audio alerts for mobile usage are turned on.</summary>
    [JsonPropertyName("hasInCabAudioAlertsEnabled")] public bool? HasInCabAudioAlertsEnabled { get; init; }

    /// <summary>Whether AI event detection for mobile usage is turned on.</summary>
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }

    /// <summary>Alert when speed is over this many miles per hour.</summary>
    [JsonPropertyName("speedingThresholdMph")] public double? SpeedingThresholdMph { get; init; }
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

/// <summary>Harsh-event sensitivity settings (v1, CM11/CM12/CM22 devices). Mirrors the spec's
/// <c>HarshEventSensitivitySettingsObjectResponseBody</c>. Each per-axis sub-object mirrors a spec
/// schema whose shape is <c>{ heavyDuty, lightDuty, passenger }</c>; all six such schemas are
/// structurally identical, so the SDK models them with the single shared
/// <see cref="HarshSensitivityByVehicleType"/> record.</summary>
public sealed record SafetyHarshEventSensitivitySettings
{
    /// <summary>Harsh-acceleration g-force sensitivity per vehicle class. Mirrors the spec's
    /// <c>HarshAccelSensitivityGForceSettingsObjectResponseBody</c>; values are numeric g-force
    /// strings (e.g. <c>"0.29"</c>).</summary>
    [JsonPropertyName("harshAccelSensitivityGForce")] public HarshSensitivityByVehicleType? HarshAccelSensitivityGForce { get; init; }

    /// <summary>Harsh-brake g-force sensitivity per vehicle class. Mirrors the spec's
    /// <c>HarshBrakeSensitivityGForceSettingsObjectResponseBody</c>; values are numeric g-force
    /// strings (e.g. <c>"0.29"</c>).</summary>
    [JsonPropertyName("harshBrakeSensitivityGForce")] public HarshSensitivityByVehicleType? HarshBrakeSensitivityGForce { get; init; }

    /// <summary>Harsh-turn g-force sensitivity per vehicle class. Mirrors the spec's
    /// <c>HarshTurnSensitivityGForceSettingsObjectResponseBody</c>; values are numeric g-force
    /// strings (e.g. <c>"0.29"</c>).</summary>
    [JsonPropertyName("harshTurnSensitivityGForce")] public HarshSensitivityByVehicleType? HarshTurnSensitivityGForce { get; init; }
}

/// <summary>Harsh-event sensitivity settings (v2, non-CM11/12/22 devices). Mirrors the spec's
/// <c>HarshEventSensitivityV2SettingsObjectResponseBody</c>. Each per-axis sub-object mirrors a spec
/// schema whose shape is <c>{ heavyDuty, lightDuty, passenger }</c>, modelled by the shared
/// <see cref="HarshSensitivityByVehicleType"/> record.</summary>
public sealed record SafetyHarshEventSensitivityV2Settings
{
    /// <summary>Harsh-acceleration sensitivity per vehicle class. Mirrors the spec's
    /// <c>HarshAccelSensitivityV2SettingsObjectResponseBody</c>; values are
    /// <c>unknown</c>, <c>invalid</c>, <c>off</c>, <c>low</c>, <c>normal</c>, <c>high</c>.</summary>
    [JsonPropertyName("harshAccelSensitivity")] public HarshSensitivityByVehicleType? HarshAccelSensitivity { get; init; }

    /// <summary>Harsh-brake sensitivity per vehicle class. Mirrors the spec's
    /// <c>HarshBrakeSensitivityV2SettingsObjectResponseBody</c>; values are <c>unknown</c>,
    /// <c>invalid</c>, <c>off</c>, <c>veryLow</c>, <c>low</c>, <c>normal</c>, <c>high</c>.</summary>
    [JsonPropertyName("harshBrakeSensitivity")] public HarshSensitivityByVehicleType? HarshBrakeSensitivity { get; init; }

    /// <summary>Harsh-turn sensitivity per vehicle class. Mirrors the spec's
    /// <c>HarshTurnSensitivityV2SettingsObjectResponseBody</c>; values are <c>unknown</c>,
    /// <c>invalid</c>, <c>off</c>, <c>veryLow</c>, <c>low</c>, <c>normal</c>, <c>high</c>.</summary>
    [JsonPropertyName("harshTurnSensitivity")] public HarshSensitivityByVehicleType? HarshTurnSensitivity { get; init; }
}

/// <summary>A harsh-event sensitivity setting broken down by vehicle class.
///
/// <para>This one record mirrors SIX structurally identical spec schemas — the three v1 g-force
/// schemas (<c>HarshAccelSensitivityGForceSettingsObjectResponseBody</c>,
/// <c>HarshBrakeSensitivityGForceSettingsObjectResponseBody</c>,
/// <c>HarshTurnSensitivityGForceSettingsObjectResponseBody</c>) and the three v2 schemas
/// (<c>HarshAccelSensitivityV2SettingsObjectResponseBody</c>,
/// <c>HarshBrakeSensitivityV2SettingsObjectResponseBody</c>,
/// <c>HarshTurnSensitivityV2SettingsObjectResponseBody</c>). Every one of them is
/// <c>{ heavyDuty, lightDuty, passenger }</c> with string values, so six near-identical C# records
/// would carry no extra information.</para>
///
/// <para>The record is named descriptively rather than after any one spec schema precisely because
/// it is shared: naming it after (say) the harsh-accel schema would misdescribe the other five uses.
/// The value domain differs by usage — the v1 schemas carry numeric g-force strings, the v2 schemas
/// carry sensitivity enum names — and is documented on the property that declares it.</para></summary>
public sealed record HarshSensitivityByVehicleType
{
    /// <summary>Sensitivity setting for heavy-duty vehicles.</summary>
    [JsonPropertyName("heavyDuty")] public string? HeavyDuty { get; init; }

    /// <summary>Sensitivity setting for light-duty vehicles.</summary>
    [JsonPropertyName("lightDuty")] public string? LightDuty { get; init; }

    /// <summary>Sensitivity setting for passenger cars.</summary>
    [JsonPropertyName("passenger")] public string? Passenger { get; init; }
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
/// <c>SpeedingSettingsObjectResponseBody</c>.</summary>
public sealed record SafetySpeedingSettings
{
    /// <summary>Unit for speeding thresholds (<c>milesPerHour</c>, <c>kilometersPerHour</c>, <c>percentage</c>).</summary>
    [JsonPropertyName("unit")] public string? Unit { get; init; }

    /// <summary>Configured speeding severity levels. Each entry mirrors the spec's
    /// <c>speedingSeverityLevelResponseBody</c>.</summary>
    [JsonPropertyName("severityLevels")] public IReadOnlyList<SpeedingSeverityLevel>? SeverityLevels { get; init; }
}

/// <summary>Settings for one speeding severity level. Mirrors the spec's
/// <c>speedingSeverityLevelResponseBody</c>, nested under <c>speedingSettings.severityLevels</c> on
/// <c>GET /fleet/settings/safety</c>. The spec marks every member required, but this is a response
/// shape, so the SDK deserializes them leniently as nullable.</summary>
public sealed record SpeedingSeverityLevel
{
    /// <summary>How long the vehicle must be speeding in this category before the event is
    /// attributed to this level, in milliseconds.</summary>
    [JsonPropertyName("durationMs")] public int? DurationMs { get; init; }

    /// <summary>Whether this severity level is enabled.</summary>
    [JsonPropertyName("isEnabled")] public bool? IsEnabled { get; init; }

    /// <summary>The severity level name (<c>light</c>, <c>moderate</c>, <c>heavy</c>, <c>severe</c>).</summary>
    [JsonPropertyName("severityLevel")] public string? SeverityLevel { get; init; }

    /// <summary>The minimum speed above the posted limit that is attributed to this severity level.
    /// Spec format is <c>float</c>; widened to <see langword="double"/> to match the SDK's numeric
    /// convention (see <c>DocumentField.NumberValue</c>).</summary>
    [JsonPropertyName("speedOverLimitThreshold")] public double? SpeedOverLimitThreshold { get; init; }
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
