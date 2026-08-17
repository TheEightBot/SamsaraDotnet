namespace Samsara.Sdk.Models.Drivers;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a driver in the Samsara system.
/// </summary>
public sealed record Driver
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; init; }

    /// <summary>The driver's email address. Optional.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

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

    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    [JsonPropertyName("isDeactivated")]
    public bool? IsDeactivated { get; init; }

    [JsonPropertyName("currentIdCardCode")]
    public string? CurrentIdCardCode { get; init; }

    [JsonPropertyName("profileImageUrl")]
    public string? ProfileImageUrl { get; init; }

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

    /// <summary>
    /// Attributes attached to the driver. Mirrors the spec's <c>attributeTiny</c>
    /// schema.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<DriverAttribute>? Attributes { get; init; }

    /// <summary>
    /// The driver's ELD settings. Mirrors the spec's <c>DriverEldSettings</c>
    /// schema.
    /// </summary>
    [JsonPropertyName("eldSettings")]
    public DriverEldSettings? EldSettings { get; init; }

    /// <summary>
    /// Whether the driver has driving-related features hidden in the Driver App
    /// (vehicle selection, HOS, routing, team driving, documents, trip logs).
    /// Defaults to <c>false</c> when omitted. Available to Connected Forms
    /// customers only.
    /// </summary>
    [JsonPropertyName("hasDrivingFeaturesHidden")]
    public bool? HasDrivingFeaturesHidden { get; init; }

    /// <summary>
    /// Whether the driver has vehicle unpinning enabled. Defaults to <c>true</c>
    /// when omitted.
    /// </summary>
    [JsonPropertyName("hasVehicleUnpinningEnabled")]
    public bool? HasVehicleUnpinningEnabled { get; init; }

    /// <summary>
    /// The peer group tag this driver belongs to, used for gamification.
    /// Mirrors the spec's <c>tagTinyResponse</c> schema, which is structurally
    /// identical to the shared <see cref="TagReference"/> record.
    /// </summary>
    [JsonPropertyName("peerGroupTag")]
    public TagReference? PeerGroupTag { get; init; }

    /// <summary>
    /// Tag which determines which trailers a driver will see when selecting
    /// trailers. Mirrors the spec's <c>DriverTrailerGroupTag</c> schema, which is
    /// structurally identical to the shared <see cref="TagReference"/> record.
    /// </summary>
    [JsonPropertyName("trailerGroupTag")]
    public TagReference? TrailerGroupTag { get; init; }

    /// <summary>
    /// Tag which determines which vehicles a driver will see when selecting
    /// vehicles. Mirrors the spec's <c>DriverVehicleGroupTag</c> schema, which is
    /// structurally identical to the shared <see cref="TagReference"/> record.
    /// </summary>
    [JsonPropertyName("vehicleGroupTag")]
    public TagReference? VehicleGroupTag { get; init; }

    /// <summary>
    /// US Driver Ruleset override for this driver. Mirrors the spec's
    /// <c>UsDriverRulesetOverride</c> schema. Omitted from the response when the
    /// driver has no override configured.
    /// </summary>
    [JsonPropertyName("usDriverRulesetOverride")]
    public UsDriverRulesetOverride? UsDriverRulesetOverride { get; init; }

    [JsonPropertyName("waitingTimeDutyStatusEnabled")]
    public bool? WaitingTimeDutyStatusEnabled { get; init; }
}

/// <summary>
/// Carrier-specific settings for a driver (ELD compliance). Mirrors the spec's
/// <c>DriverCarrierSettings</c> schema. If the driver's carrier differs from the
/// organization's carrier settings, the override value is used.
/// </summary>
public sealed record DriverCarrierSettings
{
    /// <summary>Carrier for a given driver (max length 255).</summary>
    [JsonPropertyName("carrierName")]
    public string? CarrierName { get; init; }

    /// <summary>
    /// Carrier US DOT number. If this differs from the organization's settings,
    /// the override value is used.
    /// </summary>
    [JsonPropertyName("dotNumber")]
    public long? DotNumber { get; init; }

    /// <summary>
    /// Address of the place of business at which a driver ordinarily reports for
    /// work (max length 255). Mirrors the spec's
    /// <c>DriverHomeTerminalAddress</c> schema.
    /// </summary>
    [JsonPropertyName("homeTerminalAddress")]
    public string? HomeTerminalAddress { get; init; }

    /// <summary>
    /// Name of the place of business at which a driver ordinarily reports for
    /// work (max length 255). Mirrors the spec's <c>DriverHomeTerminalName</c>
    /// schema.
    /// </summary>
    [JsonPropertyName("homeTerminalName")]
    public string? HomeTerminalName { get; init; }

    /// <summary>
    /// Main office address for a given driver (max length 255). If this differs
    /// from the organization's settings, the override value is used.
    /// </summary>
    [JsonPropertyName("mainOfficeAddress")]
    public string? MainOfficeAddress { get; init; }
}

/// <summary>
/// Vehicle reference on a driver object. Mirrors the spec's
/// <c>DriverStaticAssignedVehicle</c> schema.
/// </summary>
public sealed record DriverVehicleRef
{
    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified attribute attached to a driver. Mirrors the spec's
/// <c>attributeTiny</c> schema, and the structurally identical
/// <c>UpdateDriverRequest_attributes</c> request schema.
/// </summary>
/// <remarks>
/// Named <c>DriverAttribute</c> rather than the stripped spec name
/// <c>AttributeTiny</c>: <c>attributeTiny</c> is a cross-domain spec schema, and
/// claiming the unqualified name inside the driver namespace would make a
/// driver-scoped record the de-facto shared type. The Safety domain already
/// mirrors the equivalent schema as <c>SafetyEventAttribute</c> for the same
/// reason.
/// </remarks>
public sealed record DriverAttribute
{
    /// <summary>The Samsara ID of the attribute object.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the attribute.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>String values associated with this attribute.</summary>
    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    /// <summary>Number values associated with this attribute.</summary>
    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }

    /// <summary>
    /// Date values associated with this attribute (RFC 3339 date format:
    /// <c>YYYY-MM-DD</c>).
    /// </summary>
    [JsonPropertyName("dateValues")]
    public IReadOnlyList<string>? DateValues { get; init; }
}

/// <summary>
/// A minified attribute supplied when creating a driver. Mirrors the spec's
/// <c>CreateDriverRequest_attributes</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <see cref="DriverAttribute"/>: the create-request schema has no
/// <c>dateValues</c> member. Named <c>CreateDriverAttribute</c> because the
/// stripped spec name would be an unusable identifier
/// (<c>CreateDriverRequest_attributes</c>).
/// </remarks>
public sealed record CreateDriverAttribute
{
    /// <summary>The Samsara ID of the attribute object.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the attribute.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>String values associated with this attribute.</summary>
    [JsonPropertyName("stringValues")]
    public IReadOnlyList<string>? StringValues { get; init; }

    /// <summary>Number values associated with this attribute.</summary>
    [JsonPropertyName("numberValues")]
    public IReadOnlyList<double>? NumberValues { get; init; }
}

/// <summary>
/// The driver's ELD settings. Mirrors the spec's <c>DriverEldSettings</c>
/// schema.
/// </summary>
public sealed record DriverEldSettings
{
    /// <summary>
    /// The driver's ELD rulesets and overrides — the full set of rulesets that
    /// may apply depending on the driver's activity. To interface with the
    /// specific US driver override, use the driver's
    /// <c>usDriverRulesetOverride</c> property instead. Mirrors the spec's
    /// <c>DriverEldRulesets</c> schema.
    /// </summary>
    [JsonPropertyName("rulesets")]
    public IReadOnlyList<DriverEldRuleset>? Rulesets { get; init; }
}

/// <summary>
/// A single ELD ruleset applied to a driver. Mirrors the spec's
/// <c>DriverEldRuleset</c> schema.
/// </summary>
public sealed record DriverEldRuleset
{
    /// <summary>
    /// The rest-break required setting of the ELD ruleset applied to this driver.
    /// Valid values: <c>Property (off-duty/sleeper)</c>,
    /// <c>Explosives/HazMat (on-duty)</c>. Mirrors the spec's
    /// <c>DriverEldRulesetRestBreak</c> schema.
    /// </summary>
    [JsonPropertyName("break")]
    public string? Break { get; init; }

    /// <summary>
    /// The cycle of the ELD ruleset applied to this driver (e.g.
    /// <c>USA 70 hour / 8 day</c>). Mirrors the spec's
    /// <c>DriverEldRulesetCycle</c> schema.
    /// </summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; init; }

    /// <summary>
    /// The jurisdiction of the ELD ruleset applied to this driver: <c>CS</c> or
    /// <c>CN</c> for Canada South and Canada North respectively, otherwise the
    /// ISO 3166-2 postal code of the supported state or territory. Mirrors the
    /// spec's <c>DriverEldRulesetJurisdiction</c> schema.
    /// </summary>
    [JsonPropertyName("jurisdiction")]
    public string? Jurisdiction { get; init; }

    /// <summary>
    /// The restart of the ELD ruleset applied to this driver. Valid values:
    /// <c>None</c>, <c>34-hour Restart</c>, <c>24-hour Restart</c>,
    /// <c>36-hour Restart</c>, <c>72-hour Restart</c>. Mirrors the spec's
    /// <c>DriverEldRulesetRestart</c> schema.
    /// </summary>
    [JsonPropertyName("restart")]
    public string? Restart { get; init; }

    /// <summary>
    /// The shift of the ELD ruleset applied to this driver. Valid values:
    /// <c>US Interstate Property</c>, <c>US Interstate Passenger</c>,
    /// <c>Texas Intrastate</c>. Mirrors the spec's
    /// <c>DriverEldRulesetShift</c> schema.
    /// </summary>
    [JsonPropertyName("shift")]
    public string? Shift { get; init; }
}

/// <summary>
/// Hours-of-service settings for a driver. Mirrors the spec's
/// <c>DriverHosSetting</c> schema, and the structurally identical
/// <c>UpdateDriverRequest_hosSetting</c> schema.
/// </summary>
public sealed record DriverHosSetting
{
    /// <summary>
    /// Flag indicating this driver may use the Heavy Haul exemption in ELD logs.
    /// Defaults to <c>false</c> when omitted. Mirrors the spec's
    /// <c>DriverHeavyHaulExemptionToggleEnabled</c> schema.
    /// </summary>
    [JsonPropertyName("heavyHaulExemptionToggleEnabled")]
    public bool? HeavyHaulExemptionToggleEnabled { get; init; }
}

/// <summary>
/// US Driver Ruleset override for a given driver. Mirrors the spec's
/// <c>UsDriverRulesetOverride</c> schema. If the driver operates under a ruleset
/// different from the organization default, the override is used. Updating this
/// value only updates the override setting for this driver; explicitly setting
/// the property to <c>null</c> on a request deletes the driver's override.
/// </summary>
/// <remarks>
/// <para>
/// The spec marks all four members <c>required</c> on BOTH the request and the
/// response. This response-side record keeps every member nullable: the live
/// API omits fields its own spec marks required, and
/// <c>SamsaraSerializerOptions.Default</c> is deliberately lenient, so
/// <c>required</c> here has previously caused runtime deserialization crashes.
/// </para>
/// <para>
/// The 2026-08-17 spec-parity sweep split the request half out into
/// <see cref="UsDriverRulesetOverrideInput"/> rather than leaving this record
/// serving both sides. Keeping one all-nullable record was not viable: the
/// override is replace-semantics — the API applies the object wholesale, so a
/// caller who populates only <c>cycle</c> silently blanks the other three
/// settings. Encoding all four as <c>required</c> on the request DTO makes that
/// a compile error instead of a production surprise, which is exactly the split
/// precedent set by <c>ServiceTaskInstanceInput</c> / <c>PartInstanceInput</c>.
/// </para>
/// </remarks>
public sealed record UsDriverRulesetOverride
{
    /// <summary>
    /// The driver's working cycle (e.g. <c>USA Property (8/70)</c>,
    /// <c>Texas (7/70)</c>).
    /// </summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; init; }

    /// <summary>
    /// Amount of rest necessary for the driver to restart their cycle. Valid
    /// values: <c>34-hour Restart</c>, <c>24-hour Restart</c>,
    /// <c>36-hour Restart</c>, <c>72-hour Restart</c>, <c>None</c>.
    /// </summary>
    [JsonPropertyName("restart")]
    public string? Restart { get; init; }

    /// <summary>
    /// The rest break required for this driver. Valid values:
    /// <c>Property (off-duty/sleeper)</c>,
    /// <c>California Mealbreak (off-duty/sleeper)</c>, <c>None</c>.
    /// </summary>
    [JsonPropertyName("restbreak")]
    public string? Restbreak { get; init; }

    /// <summary>
    /// The jurisdiction of the ruleset applied to this driver: the ISO 3166-2
    /// postal code of a supported US state, or the empty string for the US
    /// Federal ruleset. Valid values: the empty string, <c>AK</c>, <c>CA</c>, <c>FL</c>,
    /// <c>NE</c>, <c>NC</c>, <c>OK</c>, <c>OR</c>, <c>SC</c>, <c>TX</c>,
    /// <c>WI</c>.
    /// </summary>
    [JsonPropertyName("usStateToOverride")]
    public string? UsStateToOverride { get; init; }
}

/// <summary>
/// US Driver Ruleset override posted on <see cref="CreateDriverRequest"/> and
/// <see cref="UpdateDriverRequest"/>. Mirrors the request half of the spec's
/// <c>UsDriverRulesetOverride</c> schema, which marks all four members REQUIRED.
/// </summary>
/// <remarks>
/// The API applies the override wholesale (replace, not merge), so all four
/// members are <c>required</c>: an override built from a subset of them would
/// silently blank the settings the caller left out. Serializing the enclosing
/// <c>usDriverRulesetOverride</c> property as <c>null</c> deletes the driver's
/// existing override. The response half is
/// <see cref="UsDriverRulesetOverride"/>, which stays all-nullable.
/// </remarks>
public sealed record UsDriverRulesetOverrideInput
{
    /// <summary>
    /// The driver's working cycle (e.g. <c>USA Property (8/70)</c>,
    /// <c>Texas (7/70)</c>). Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("cycle")]
    public required string Cycle { get; init; }

    /// <summary>
    /// Amount of rest necessary for the driver to restart their cycle. Valid
    /// values: <c>34-hour Restart</c>, <c>24-hour Restart</c>,
    /// <c>36-hour Restart</c>, <c>72-hour Restart</c>, <c>None</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("restart")]
    public required string Restart { get; init; }

    /// <summary>
    /// The rest break required for this driver. Valid values:
    /// <c>Property (off-duty/sleeper)</c>,
    /// <c>California Mealbreak (off-duty/sleeper)</c>, <c>None</c>.
    /// Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("restbreak")]
    public required string Restbreak { get; init; }

    /// <summary>
    /// The jurisdiction of the ruleset applied to this driver: the ISO 3166-2
    /// postal code of a supported US state, or the empty string for the US
    /// Federal ruleset. Valid values: the empty string, <c>AK</c>, <c>CA</c>,
    /// <c>FL</c>, <c>NE</c>, <c>NC</c>, <c>OK</c>, <c>OR</c>, <c>SC</c>,
    /// <c>TX</c>, <c>WI</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("usStateToOverride")]
    public required string UsStateToOverride { get; init; }
}

/// <summary>
/// Request body for creating a new driver.
/// </summary>
public sealed record CreateDriverRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("username")]
    public required string Username { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }

    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; init; }

    /// <summary>The driver's email address. Optional.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

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

    [JsonPropertyName("currentIdCardCode")]
    public string? CurrentIdCardCode { get; init; }

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

    [JsonPropertyName("staticAssignedVehicleId")]
    public string? StaticAssignedVehicleId { get; init; }

    [JsonPropertyName("peerGroupTagId")]
    public string? PeerGroupTagId { get; init; }

    [JsonPropertyName("trailerGroupTagId")]
    public string? TrailerGroupTagId { get; init; }

    [JsonPropertyName("vehicleGroupTagId")]
    public string? VehicleGroupTagId { get; init; }

    [JsonPropertyName("waitingTimeDutyStatusEnabled")]
    public bool? WaitingTimeDutyStatusEnabled { get; init; }

    /// <summary>
    /// Attributes to associate with the driver. Mirrors the spec's
    /// <c>CreateDriverRequest_attributes</c> schema.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<CreateDriverAttribute>? Attributes { get; init; }

    /// <summary>
    /// Carrier settings override for the driver. Mirrors the spec's
    /// <c>DriverCarrierSettings</c> schema.
    /// </summary>
    [JsonPropertyName("carrierSettings")]
    public DriverCarrierSettings? CarrierSettings { get; init; }

    /// <summary>
    /// Whether the driver has driving-related features hidden in the Driver App.
    /// Defaults to <c>false</c> when omitted. Available to Connected Forms
    /// customers only.
    /// </summary>
    [JsonPropertyName("hasDrivingFeaturesHidden")]
    public bool? HasDrivingFeaturesHidden { get; init; }

    /// <summary>
    /// Whether the driver has vehicle unpinning enabled. Defaults to <c>true</c>
    /// when omitted.
    /// </summary>
    [JsonPropertyName("hasVehicleUnpinningEnabled")]
    public bool? HasVehicleUnpinningEnabled { get; init; }

    /// <summary>
    /// Hours-of-service settings for the driver. Mirrors the spec's
    /// <c>DriverHosSetting</c> schema.
    /// </summary>
    [JsonPropertyName("hosSetting")]
    public DriverHosSetting? HosSetting { get; init; }

    /// <summary>
    /// Base64-encoded profile image data. Uploaded during driver creation. When
    /// Camera ID is enabled, the image is used to train face recognition.
    /// </summary>
    [JsonPropertyName("profileImageBase64")]
    public string? ProfileImageBase64 { get; init; }

    /// <summary>
    /// URL to the driver's profile image. Can be used to set a profile image
    /// from an external URL during creation (max length 1024).
    /// </summary>
    [JsonPropertyName("profileImageUrl")]
    public string? ProfileImageUrl { get; init; }

    /// <summary>
    /// US Driver Ruleset override for the driver. Mirrors the request half of
    /// the spec's <c>UsDriverRulesetOverride</c> schema.
    /// </summary>
    [JsonPropertyName("usDriverRulesetOverride")]
    public UsDriverRulesetOverrideInput? UsDriverRulesetOverride { get; init; }
}

/// <summary>
/// Request body for updating a driver (PATCH).
/// </summary>
public sealed record UpdateDriverRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; init; }

    /// <summary>The driver's email address. Optional.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

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

    [JsonPropertyName("driverActivationStatus")]
    public string? DriverActivationStatus { get; init; }

    [JsonPropertyName("deactivatedAtTime")]
    public DateTimeOffset? DeactivatedAtTime { get; init; }

    [JsonPropertyName("currentIdCardCode")]
    public string? CurrentIdCardCode { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("password")]
    public string? Password { get; init; }

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

    [JsonPropertyName("staticAssignedVehicleId")]
    public string? StaticAssignedVehicleId { get; init; }

    [JsonPropertyName("peerGroupTagId")]
    public string? PeerGroupTagId { get; init; }

    [JsonPropertyName("trailerGroupTagId")]
    public string? TrailerGroupTagId { get; init; }

    [JsonPropertyName("vehicleGroupTagId")]
    public string? VehicleGroupTagId { get; init; }

    [JsonPropertyName("waitingTimeDutyStatusEnabled")]
    public bool? WaitingTimeDutyStatusEnabled { get; init; }

    /// <summary>
    /// Attributes to associate with the driver. Mirrors the spec's
    /// <c>UpdateDriverRequest_attributes</c> schema, which is structurally
    /// identical to <c>attributeTiny</c> and so reuses
    /// <see cref="DriverAttribute"/>.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<DriverAttribute>? Attributes { get; init; }

    /// <summary>
    /// Carrier settings override for the driver. Mirrors the spec's
    /// <c>DriverCarrierSettings</c> schema.
    /// </summary>
    [JsonPropertyName("carrierSettings")]
    public DriverCarrierSettings? CarrierSettings { get; init; }

    /// <summary>
    /// Whether the driver has driving-related features hidden in the Driver App.
    /// Defaults to <c>false</c> when omitted. Available to Connected Forms
    /// customers only.
    /// </summary>
    [JsonPropertyName("hasDrivingFeaturesHidden")]
    public bool? HasDrivingFeaturesHidden { get; init; }

    /// <summary>
    /// Whether the driver has vehicle unpinning enabled. Defaults to <c>true</c>
    /// when omitted.
    /// </summary>
    [JsonPropertyName("hasVehicleUnpinningEnabled")]
    public bool? HasVehicleUnpinningEnabled { get; init; }

    /// <summary>
    /// Hours-of-service settings for the driver. Mirrors the spec's
    /// <c>UpdateDriverRequest_hosSetting</c> schema, which is structurally
    /// identical to <c>DriverHosSetting</c> and so reuses
    /// <see cref="DriverHosSetting"/>.
    /// </summary>
    [JsonPropertyName("hosSetting")]
    public DriverHosSetting? HosSetting { get; init; }

    /// <summary>
    /// Base64-encoded profile image data.
    /// </summary>
    [JsonPropertyName("profileImageBase64")]
    public string? ProfileImageBase64 { get; init; }

    /// <summary>
    /// URL to the driver's profile image (max length 1024).
    /// </summary>
    [JsonPropertyName("profileImageUrl")]
    public string? ProfileImageUrl { get; init; }

    /// <summary>
    /// US Driver Ruleset override for the driver. Mirrors the request half of
    /// the spec's <c>UsDriverRulesetOverride</c> schema. Explicitly serializing
    /// this as <c>null</c> deletes the driver's existing override.
    /// </summary>
    [JsonPropertyName("usDriverRulesetOverride")]
    public UsDriverRulesetOverrideInput? UsDriverRulesetOverride { get; init; }
}

/// <summary>Request body for remotely signing out a driver.</summary>
public sealed record RemoteSignOutRequest
{
    [JsonPropertyName("driverId")] public required string DriverId { get; init; }
}

/// <summary>An authentication token for a driver.</summary>
public sealed record DriverAuthToken
{
    /// <summary>A one-time-use authentication token. Must be paired with the original code and driver identity in a separate request to exchange for a session.</summary>
    [JsonPropertyName("token")] public required string Token { get; init; }

    /// <summary>Expiration time of the token in Unix milliseconds since epoch. Clients must redeem the token before this timestamp.</summary>
    [JsonPropertyName("expirationTime")] public required long ExpirationTime { get; init; }
}

/// <summary>Request body for creating a driver auth token. One of <c>driverId</c>, <c>externalId</c>, or <c>username</c> is required.</summary>
public sealed record CreateDriverAuthTokenRequest
{
    /// <summary>Required. Random 12+ character string, used with the auth token to help secure the client from intercepted tokens.</summary>
    [JsonPropertyName("code")] public required string Code { get; init; }

    /// <summary>Optional. Samsara ID of the driver. One of <c>driverId</c>, <c>externalId</c>, or <c>username</c> is required.</summary>
    [JsonPropertyName("driverId")] public long? DriverId { get; init; }

    /// <summary>Optional. External ID of the driver, in the format <c>key:value</c> (e.g., <c>payrollId:ABFS18600</c>). One of <c>driverId</c>, <c>externalId</c>, or <c>username</c> is required.</summary>
    [JsonPropertyName("externalId")] public string? ExternalId { get; init; }

    /// <summary>Optional. Username of the driver. This is the login identifier configured when the driver is created. One of <c>driverId</c>, <c>externalId</c>, or <c>username</c> is required.</summary>
    [JsonPropertyName("username")] public string? Username { get; init; }
}

/// <summary>Represents a driver QR code.</summary>
public sealed record DriverQrCode
{
    /// <summary>ID for the driver the QR code belongs to.</summary>
    [JsonPropertyName("driverId")] public required long DriverId { get; init; }

    /// <summary>URL link to the driver assignment QR code. Included if a QR code has been created for the driver.</summary>
    [JsonPropertyName("qrCodeLink")] public string? QrCodeLink { get; init; }
}

/// <summary>Request body for creating a driver QR code.</summary>
public sealed record CreateDriverQrCodeRequest
{
    /// <summary>Unique ID of the driver.</summary>
    [JsonPropertyName("driverId")] public required long DriverId { get; init; }
}
