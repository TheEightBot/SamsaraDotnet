namespace Samsara.Sdk.Models.Drivers;

using System.Text.Json;
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
    /// Attributes attached to the driver (spec inner schema:
    /// <c>attributeTiny</c>). Each entry exposes <c>id</c>, <c>name</c>,
    /// <c>dateValues</c>, <c>numberValues</c>, and <c>stringValues</c>.
    /// Modeled as <c>object</c> for forward-compat with attribute shape changes.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("eldSettings")]
    public System.Text.Json.JsonElement? EldSettings { get; init; }

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

    [JsonPropertyName("peerGroupTag")]
    public System.Text.Json.JsonElement? PeerGroupTag { get; init; }

    [JsonPropertyName("trailerGroupTag")]
    public System.Text.Json.JsonElement? TrailerGroupTag { get; init; }

    [JsonPropertyName("vehicleGroupTag")]
    public System.Text.Json.JsonElement? VehicleGroupTag { get; init; }

    [JsonPropertyName("usDriverRulesetOverride")]
    public System.Text.Json.JsonElement? UsDriverRulesetOverride { get; init; }

    [JsonPropertyName("waitingTimeDutyStatusEnabled")]
    public bool? WaitingTimeDutyStatusEnabled { get; init; }
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
    public required string Username { get; init; }

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
    /// Attributes to associate with the driver (spec inner schema:
    /// <c>CreateDriverRequest_attributes</c>: <c>id</c>, <c>name</c>,
    /// <c>numberValues</c>, <c>stringValues</c>). Modeled as <c>object</c> to
    /// match the precedent set by request DTOs in
    /// <c>Equipment</c>, <c>Vehicle</c>, and <c>Attributes</c> domains and to
    /// remain forward-compatible with attribute shape changes.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("carrierSettings")]
    public System.Text.Json.JsonElement? CarrierSettings { get; init; }

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

    [JsonPropertyName("hosSetting")]
    public System.Text.Json.JsonElement? HosSetting { get; init; }

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

    [JsonPropertyName("usDriverRulesetOverride")]
    public System.Text.Json.JsonElement? UsDriverRulesetOverride { get; init; }
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
    /// Attributes to associate with the driver (spec inner schema:
    /// <c>CreateDriverRequest_attributes</c>). Modeled as <c>object</c> to
    /// match the precedent set by request DTOs in other domains and to remain
    /// forward-compatible with attribute shape changes.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<object>? Attributes { get; init; }

    [JsonPropertyName("carrierSettings")]
    public System.Text.Json.JsonElement? CarrierSettings { get; init; }

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

    [JsonPropertyName("hosSetting")]
    public System.Text.Json.JsonElement? HosSetting { get; init; }

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

    [JsonPropertyName("usDriverRulesetOverride")]
    public System.Text.Json.JsonElement? UsDriverRulesetOverride { get; init; }
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
