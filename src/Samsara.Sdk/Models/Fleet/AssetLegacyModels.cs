namespace Samsara.Sdk.Models.Fleet;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Assignments;
using Samsara.Sdk.Models.Common;

// ---------------------------------------------------------------------------
// Body shapes for the legacy v1 and Beta endpoints exposed by AssetsClient.
//
// They live in their own file (rather than AssetModels.cs) because they mirror
// a different generation of the API: the /v1/fleet/assets/* family returns
// bespoke, non-`{ data, pagination }` envelopes with int64 identifiers, and the
// Beta device-recovery family uses snake_case wire names. Keeping them apart
// from the v2 `Asset` shapes prevents the name-collision-by-proximity that
// produced the earlier flattened records.
// ---------------------------------------------------------------------------

// ── Legacy v1: GET /v1/fleet/assets ─────────────────────────────────────────

/// <summary>
/// Response body of <c>GET /v1/fleet/assets</c>. Mirrors the spec's
/// <c>inline_response_200_1</c> schema, whose only member is the
/// <c>assets</c> array.
/// </summary>
public sealed record V1AssetListResponse
{
    /// <summary>The organization's assets.</summary>
    [JsonPropertyName("assets")]
    public IReadOnlyList<V1Asset>? Assets { get; init; }
}

/// <summary>
/// Basic information about an asset on the legacy v1 API. Mirrors the spec's
/// <c>V1Asset</c> schema.
/// </summary>
/// <remarks>
/// Distinct from the v2 <see cref="Asset"/> record: identifiers here are int64
/// rather than string, and the field set is a strict subset.
/// </remarks>
public sealed record V1Asset
{
    /// <summary>Asset ID. Spec type int64; spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Asset name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Serial number of the host asset.</summary>
    [JsonPropertyName("assetSerialNumber")]
    public string? AssetSerialNumber { get; init; }

    /// <summary>The cable connected to the asset.</summary>
    [JsonPropertyName("cable")]
    public V1AssetCable? Cable { get; init; }

    /// <summary>Engine hours.</summary>
    [JsonPropertyName("engineHours")]
    public long? EngineHours { get; init; }

    /// <summary>The ID of the vehicle associated to the asset, if present. Spec type int64.</summary>
    [JsonPropertyName("vehicleId")]
    public long? VehicleId { get; init; }
}

/// <summary>
/// The cable connected to a legacy v1 asset. Mirrors the spec's
/// <c>V1Asset_cable</c> schema.
/// </summary>
public sealed record V1AssetCable
{
    /// <summary>Asset type reported by the cable (e.g. <c>Thermo King</c>).</summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }
}

// ── Legacy v1: GET /v1/fleet/assets/locations ───────────────────────────────

/// <summary>
/// Response body of <c>GET /v1/fleet/assets/locations</c>. Mirrors the spec's
/// <c>inline_response_200_2</c> schema (<c>assets</c> plus the bidirectional v1
/// cursor block).
/// </summary>
public sealed record V1AssetCurrentLocationsResponse
{
    /// <summary>The assets and their current locations.</summary>
    [JsonPropertyName("assets")]
    public IReadOnlyList<V1AssetWithCurrentLocations>? Assets { get; init; }

    /// <summary>Bidirectional cursor pagination metadata (spec schema <c>V1Pagination</c>).</summary>
    [JsonPropertyName("pagination")]
    public V1PaginationInfo? Pagination { get; init; }
}

/// <summary>
/// An asset together with its current location readings. Mirrors the spec's
/// <c>V1AssetCurrentLocationsResponse</c> schema.
/// </summary>
/// <remarks>
/// Named <c>…WithCurrentLocations</c> rather than the stripped spec name so the
/// enclosing envelope can keep the natural
/// <see cref="V1AssetCurrentLocationsResponse"/> name.
/// </remarks>
public sealed record V1AssetWithCurrentLocations
{
    /// <summary>Asset ID. Spec type int64; spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Asset name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Asset serial number.</summary>
    [JsonPropertyName("assetSerialNumber")]
    public string? AssetSerialNumber { get; init; }

    /// <summary>The cable connected to the asset.</summary>
    [JsonPropertyName("cable")]
    public V1AssetCable? Cable { get; init; }

    /// <summary>Engine hours.</summary>
    [JsonPropertyName("engineHours")]
    public long? EngineHours { get; init; }

    /// <summary>Current location of the asset.</summary>
    [JsonPropertyName("location")]
    public IReadOnlyList<V1AssetCurrentLocation>? Location { get; init; }
}

/// <summary>
/// A current-location reading for a legacy v1 asset. Mirrors the spec's
/// <c>V1AssetCurrentLocation</c> schema.
/// </summary>
/// <remarks>
/// Distinct from <see cref="V1AssetLocation"/>, the history shape returned by
/// <c>GET /v1/fleet/assets/{assetId}/locations</c>: that schema spells the
/// timestamp <c>time</c> where this one spells it <c>timeMs</c>.
/// </remarks>
public sealed record V1AssetCurrentLocation
{
    /// <summary>The latitude of the location in degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>The longitude of the location in degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Best-effort (street, city, state) for the latitude and longitude.</summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>GPS-calculated speed in miles per hour.</summary>
    [JsonPropertyName("speedMilesPerHour")]
    public double? SpeedMilesPerHour { get; init; }

    /// <summary>Time in Unix milliseconds since epoch when the asset was at the location.</summary>
    [JsonPropertyName("timeMs")]
    public double? TimeMs { get; init; }
}

// ── Legacy v1: GET /v1/fleet/assets/{assetId}/locations ─────────────────────

/// <summary>
/// A historical location reading for a legacy v1 asset. Mirrors the item schema
/// of the spec's <c>V1AssetLocationResponse</c> array.
/// </summary>
public sealed record V1AssetLocation
{
    /// <summary>The latitude of the location in degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>The longitude of the location in degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Best-effort (street, city, state) for the latitude and longitude.</summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>GPS-calculated speed in miles per hour.</summary>
    [JsonPropertyName("speedMilesPerHour")]
    public double? SpeedMilesPerHour { get; init; }

    /// <summary>Time in Unix milliseconds since epoch when the asset was at the location.</summary>
    [JsonPropertyName("time")]
    public double? Time { get; init; }
}

// ── Legacy v1: reefers ──────────────────────────────────────────────────────

/// <summary>
/// Reefer-specific details for one asset as returned by the list endpoint
/// <c>GET /v1/fleet/assets/reefers</c>. Mirrors the spec's <c>V1AssetsReefer</c>
/// schema.
/// </summary>
/// <remarks>
/// Deliberately separate from <see cref="V1AssetReefer"/> (the single-asset
/// <c>GET /v1/fleet/assets/{assetId}/reefer</c> shape). The two carry the same
/// four top-level members, but their <c>reeferStats</c> objects use different
/// property names — see <see cref="V1AssetsReeferStats"/>.
/// </remarks>
public sealed record V1AssetsReefer
{
    /// <summary>Asset ID.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Asset name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Asset type (e.g. <c>Thermo King</c>).</summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }

    /// <summary>All state changes of the reefer for the included stat types.</summary>
    [JsonPropertyName("reeferStats")]
    public V1AssetsReeferStats? ReeferStats { get; init; }
}

/// <summary>
/// Reefer state changes on the list endpoint. Mirrors the spec's
/// <c>V1AssetsReefer_reeferStats</c> schema.
/// </summary>
/// <remarks>
/// The single-asset endpoint returns a different member set
/// (<see cref="V1AssetReeferStats"/>): <c>alarms</c> instead of
/// <c>reeferAlarms</c>, <c>returnAirTemp</c> instead of
/// <c>returnAirTemperature</c>, and no ambient/discharge series at all.
/// </remarks>
public sealed record V1AssetsReeferStats
{
    /// <summary>Ambient temperature of the reefer (air around the Samsara Asset Gateway).</summary>
    [JsonPropertyName("ambientAirTemperature")]
    public IReadOnlyList<V1ReeferTemperatureSample>? AmbientAirTemperature { get; init; }

    /// <summary>Discharge air temperature of the reefer (air leaving the cooling unit).</summary>
    [JsonPropertyName("dischargeAirTemperature")]
    public IReadOnlyList<V1ReeferTemperatureSample>? DischargeAirTemperature { get; init; }

    /// <summary>Return air temperature of the reefer.</summary>
    [JsonPropertyName("returnAirTemperature")]
    public IReadOnlyList<V1ReeferTemperatureSample>? ReturnAirTemperature { get; init; }

    /// <summary>Set point temperature of the reefer.</summary>
    [JsonPropertyName("setPoint")]
    public IReadOnlyList<V1ReeferTemperatureSample>? SetPoint { get; init; }

    /// <summary>Engine hours of the reefer.</summary>
    [JsonPropertyName("engineHours")]
    public IReadOnlyList<V1ReeferEngineHoursSample>? EngineHours { get; init; }

    /// <summary>Fuel percentage of the reefer.</summary>
    [JsonPropertyName("fuelPercentage")]
    public IReadOnlyList<V1ReeferFuelPercentageSample>? FuelPercentage { get; init; }

    /// <summary>Power status of the reefer.</summary>
    [JsonPropertyName("powerStatus")]
    public IReadOnlyList<V1ReeferPowerStatusSample>? PowerStatus { get; init; }

    /// <summary>Reefer alarms.</summary>
    [JsonPropertyName("reeferAlarms")]
    public IReadOnlyList<V1ReeferAlarmsSample>? ReeferAlarms { get; init; }
}

/// <summary>
/// Reefer-specific details for a single asset, as returned by
/// <c>GET /v1/fleet/assets/{assetId}/reefer</c>. Mirrors the spec's
/// <c>V1AssetReeferResponse</c> schema.
/// </summary>
public sealed record V1AssetReefer
{
    /// <summary>Asset ID.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Asset name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Asset type (e.g. <c>Thermo King</c>).</summary>
    [JsonPropertyName("assetType")]
    public string? AssetType { get; init; }

    /// <summary>All state changes of the reefer for the included stat types.</summary>
    [JsonPropertyName("reeferStats")]
    public V1AssetReeferStats? ReeferStats { get; init; }
}

/// <summary>
/// Reefer state changes on the single-asset endpoint. Mirrors the spec's
/// <c>V1AssetReeferResponse_reeferStats</c> schema.
/// </summary>
public sealed record V1AssetReeferStats
{
    /// <summary>Return air temperature of the reefer.</summary>
    [JsonPropertyName("returnAirTemp")]
    public IReadOnlyList<V1ReeferTemperatureSample>? ReturnAirTemp { get; init; }

    /// <summary>Set point temperature of the reefer.</summary>
    [JsonPropertyName("setPoint")]
    public IReadOnlyList<V1ReeferTemperatureSample>? SetPoint { get; init; }

    /// <summary>Engine hours of the reefer.</summary>
    [JsonPropertyName("engineHours")]
    public IReadOnlyList<V1ReeferEngineHoursSample>? EngineHours { get; init; }

    /// <summary>Fuel percentage of the reefer.</summary>
    [JsonPropertyName("fuelPercentage")]
    public IReadOnlyList<V1ReeferFuelPercentageSample>? FuelPercentage { get; init; }

    /// <summary>Power status of the reefer.</summary>
    [JsonPropertyName("powerStatus")]
    public IReadOnlyList<V1ReeferPowerStatusSample>? PowerStatus { get; init; }

    /// <summary>Reefer alarms.</summary>
    [JsonPropertyName("alarms")]
    public IReadOnlyList<V1ReeferAlarmsSample>? Alarms { get; init; }
}

/// <summary>
/// A single reefer temperature state change, in millidegrees Celsius. One record
/// serves the five structurally identical spec schemas
/// <c>V1AssetsReefer_reeferStats_ambientAirTemperature</c>,
/// <c>…_dischargeAirTemperature</c>,
/// <c>V1AssetReeferResponse_reeferStats_returnAirTemp</c> and
/// <c>…_setPoint</c>.
/// </summary>
public sealed record V1ReeferTemperatureSample
{
    /// <summary>Timestamp in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("changedAtMs")]
    public long? ChangedAtMs { get; init; }

    /// <summary>Temperature in millidegrees Celsius.</summary>
    [JsonPropertyName("tempInMilliC")]
    public long? TempInMilliC { get; init; }
}

/// <summary>
/// A single reefer engine-hours state change. Mirrors the spec's
/// <c>V1AssetReeferResponse_reeferStats_engineHours</c> schema.
/// </summary>
public sealed record V1ReeferEngineHoursSample
{
    /// <summary>Timestamp in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("changedAtMs")]
    public long? ChangedAtMs { get; init; }

    /// <summary>Engine hours of the reefer.</summary>
    [JsonPropertyName("engineHours")]
    public long? EngineHours { get; init; }
}

/// <summary>
/// A single reefer fuel-percentage state change. Mirrors the spec's
/// <c>V1AssetReeferResponse_reeferStats_fuelPercentage</c> schema.
/// </summary>
public sealed record V1ReeferFuelPercentageSample
{
    /// <summary>Timestamp in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("changedAtMs")]
    public long? ChangedAtMs { get; init; }

    /// <summary>Fuel percentage of the reefer.</summary>
    [JsonPropertyName("fuelPercentage")]
    public long? FuelPercentage { get; init; }
}

/// <summary>
/// A single reefer power-status state change. Mirrors the spec's
/// <c>V1AssetsReefer_reeferStats_powerStatus</c> schema (and its unenumerated
/// twin <c>V1AssetReeferResponse_reeferStats_powerStatus</c>).
/// </summary>
public sealed record V1ReeferPowerStatusSample
{
    /// <summary>Timestamp in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("changedAtMs")]
    public long? ChangedAtMs { get; init; }

    /// <summary>
    /// Power status of the reefer. Valid values: <c>Off</c>, <c>Active</c>,
    /// <c>Active (Start/Stop)</c>, <c>Active (Continuous)</c>. Modelled as a
    /// string because the spec's values contain spaces and punctuation that no
    /// C# enum member can carry.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// The set of reefer alarms reported at one instant. Mirrors the spec's
/// <c>V1AssetReeferResponse_reeferStats_alarms_1</c> schema.
/// </summary>
public sealed record V1ReeferAlarmsSample
{
    /// <summary>Timestamp when the alarms were reported, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("changedAtMs")]
    public long? ChangedAtMs { get; init; }

    /// <summary>The alarms reported at this timestamp.</summary>
    [JsonPropertyName("alarms")]
    public IReadOnlyList<V1ReeferAlarm>? Alarms { get; init; }
}

/// <summary>
/// A single reefer alarm. Mirrors the spec's
/// <c>V1AssetReeferResponse_reeferStats_alarms</c> schema.
/// </summary>
public sealed record V1ReeferAlarm
{
    /// <summary>ID of the alarm.</summary>
    [JsonPropertyName("alarmCode")]
    public long? AlarmCode { get; init; }

    /// <summary>Description of the alarm.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Recommended operator action.</summary>
    [JsonPropertyName("operatorAction")]
    public string? OperatorAction { get; init; }

    /// <summary>
    /// Severity of the alarm: 1 = OK to run, 2 = check as specified,
    /// 3 = take immediate action.
    /// </summary>
    [JsonPropertyName("severity")]
    public long? Severity { get; init; }
}

// ── Beta: GET /assets/depreciation ──────────────────────────────────────────

/// <summary>
/// A depreciation or adjustment transaction against an asset. Mirrors the spec's
/// <c>DepreciationTransactionObjectResponseBody</c> schema.
/// </summary>
public sealed record AssetDepreciationTransaction
{
    /// <summary>The unique UUID of the transaction. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Samsara asset ID. Can be used with <c>/fleet/assets/{id}</c> to retrieve
    /// asset details. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>
    /// The transaction amount. Negative values indicate depreciation or
    /// write-downs, positive values indicate appreciation. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("amount")]
    public AssetDepreciationAmount? Amount { get; init; }

    /// <summary>The UUID of the cost center assigned to the asset.</summary>
    [JsonPropertyName("costCenterId")]
    public string? CostCenterId { get; init; }

    /// <summary>
    /// Transaction type. Valid values: <c>depreciation</c>, <c>adjustment</c>,
    /// <c>unknown</c>. Unknown types should be handled gracefully for forward
    /// compatibility. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("transactionType")]
    public string? TransactionType { get; init; }

    /// <summary>Optional description.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>
    /// When the depreciation or adjustment occurred from a financial/accounting
    /// perspective, in RFC 3339 format. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("eventTime")]
    public DateTimeOffset? EventTime { get; init; }

    /// <summary>When the transaction record was created, in RFC 3339 format. Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAt")]
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>
    /// When the transaction record was last modified (use for incremental sync),
    /// in RFC 3339 format. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; init; }
}

/// <summary>
/// A money amount on a depreciation transaction. Mirrors the spec's
/// <c>DepreciationTransactionMoneyObjectResponseBody</c> schema.
/// </summary>
public sealed record AssetDepreciationAmount
{
    /// <summary>The money amount, as a decimal string. Spec marks REQUIRED.</summary>
    [JsonPropertyName("amount")]
    public string? Amount { get; init; }

    /// <summary>
    /// The 3-letter ISO 4217 currency code. Valid values: <c>usd</c>,
    /// <c>gbp</c>, <c>cad</c>, <c>eur</c>, <c>chf</c>, <c>mxn</c>. Spec marks
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; init; }
}

// ── Beta: GET /assets/inputs/stream ─────────────────────────────────────────

/// <summary>
/// A single asset auxiliary-input reading. Mirrors the spec's
/// <c>assetsInputsResponseResponseBody</c> schema.
/// </summary>
public sealed record AssetInputReading
{
    /// <summary>Asset that the input data is from. Spec marks REQUIRED.</summary>
    [JsonPropertyName("asset")]
    public AssetInputAsset? Asset { get; init; }

    /// <summary>Auxiliary input metadata.</summary>
    [JsonPropertyName("auxInput")]
    public AssetInputAuxInput? AuxInput { get; init; }

    /// <summary>UTC timestamp in RFC 3339 format of the event. Spec marks REQUIRED.</summary>
    [JsonPropertyName("happenedAtTime")]
    public string? HappenedAtTime { get; init; }

    /// <summary>
    /// Units of the returned value. Valid values: <c>boolean</c>,
    /// <c>millivolts</c>, <c>microamps</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("units")]
    public string? Units { get; init; }

    /// <summary>Value of the data point. Spec marks REQUIRED.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// The asset an auxiliary-input reading belongs to. Mirrors the spec's
/// <c>AssetsInputsAssetResponseResponseBody</c> schema.
/// </summary>
public sealed record AssetInputAsset
{
    /// <summary>ID of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>A map of external IDs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>Tags associated with the asset (spec schema <c>GoaTagTinyResponseResponseBody</c>).</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<TagReference>? Tags { get; init; }

    /// <summary>Attributes associated with the asset (spec schema <c>GoaAttributeTinyResponseBody</c>).</summary>
    [JsonPropertyName("attributes")]
    public IReadOnlyList<AttributeTiny>? Attributes { get; init; }
}

/// <summary>
/// Auxiliary input metadata on an asset input reading. Mirrors the spec's
/// <c>assetsInputsAuxInputResponseBody</c> schema.
/// </summary>
public sealed record AssetInputAuxInput
{
    /// <summary>Name of the auxiliary input. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

// ── Beta: device recovery ───────────────────────────────────────────────────

/// <summary>
/// An asset currently marked as missing, including notification recipients.
/// Mirrors the spec's <c>MissingStateResponseBody</c> schema, returned by both
/// <c>GET /fleet/assets/device-recovery-missing</c> and
/// <c>POST /fleet/assets/device-recovery/{id}/missing</c>.
/// </summary>
/// <remarks>
/// The wire names on this family are snake_case, unlike the rest of the v2 API.
/// </remarks>
public sealed record DeviceRecoveryMissingState
{
    /// <summary>The unique Samsara ID of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The human-readable name of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A note associated with the missing asset.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>
    /// Timestamp when the asset was first marked as missing, in milliseconds
    /// since epoch.
    /// </summary>
    [JsonPropertyName("initiated_at_ms")]
    public long? InitiatedAtMs { get; init; }

    /// <summary>The ID of the user who first marked this asset as missing.</summary>
    [JsonPropertyName("initiated_by_user_id")]
    public long? InitiatedByUserId { get; init; }

    /// <summary>Users subscribed to location update notifications for this asset.</summary>
    [JsonPropertyName("notification_recipients")]
    public IReadOnlyList<DeviceRecoveryNotificationRecipient>? NotificationRecipients { get; init; }

    /// <summary>
    /// The source of the last update to this recovery state. Valid values:
    /// <c>dashboard</c>, <c>api</c>. Defaults to <c>dashboard</c>.
    /// </summary>
    [JsonPropertyName("update_source")]
    public string? UpdateSource { get; init; }

    /// <summary>
    /// Timestamp when the asset was marked as missing, in milliseconds since
    /// epoch. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("updated_at_ms")]
    public long? UpdatedAtMs { get; init; }

    /// <summary>The ID of the user who marked the asset as missing.</summary>
    [JsonPropertyName("updated_by_user_id")]
    public long? UpdatedByUserId { get; init; }
}

/// <summary>
/// A recovered asset with its recovery details, including recovery photos.
/// Mirrors the spec's <c>RecoveryStateResponseBody</c> schema returned by
/// <c>POST /fleet/assets/device-recovery/{id}/recovered</c>.
/// </summary>
public sealed record DeviceRecoveryRecoveredState
{
    /// <summary>The unique Samsara ID of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The human-readable name of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A note associated with the recovery.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>Users subscribed to location update notifications for this asset.</summary>
    [JsonPropertyName("notification_recipients")]
    public IReadOnlyList<DeviceRecoveryNotificationRecipient>? NotificationRecipients { get; init; }

    /// <summary>
    /// Photos associated with the recovery event. URLs are temporary and expire
    /// at <c>url_expires_at_ms</c>.
    /// </summary>
    [JsonPropertyName("recovery_photos")]
    public IReadOnlyList<DeviceRecoveryPhoto>? RecoveryPhotos { get; init; }

    /// <summary>
    /// The source of the last update to this recovery state. Valid values:
    /// <c>dashboard</c>, <c>api</c>. Defaults to <c>dashboard</c>.
    /// </summary>
    [JsonPropertyName("update_source")]
    public string? UpdateSource { get; init; }

    /// <summary>
    /// Timestamp when the recovery state was last updated, in milliseconds since
    /// epoch. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("updated_at_ms")]
    public long? UpdatedAtMs { get; init; }

    /// <summary>The ID of the user who last updated the recovery state.</summary>
    [JsonPropertyName("updated_by_user_id")]
    public long? UpdatedByUserId { get; init; }
}

/// <summary>
/// A user subscribed to recovery state change notifications, including their
/// name and email. Mirrors the spec's
/// <c>NotificationRecipientResponseResponseBody</c> schema.
/// </summary>
/// <remarks>
/// The request half is <see cref="DeviceRecoveryNotificationRecipientInput"/>,
/// which carries only <c>user_id</c> and <c>notification_types</c>.
/// </remarks>
public sealed record DeviceRecoveryNotificationRecipient
{
    /// <summary>The ID of the user. Spec marks REQUIRED.</summary>
    [JsonPropertyName("user_id")]
    public long? UserId { get; init; }

    /// <summary>The display name of the user. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The email address of the user. Spec marks REQUIRED.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>How the user is notified. Valid values: <c>email</c>. Spec marks REQUIRED.</summary>
    [JsonPropertyName("notification_types")]
    public IReadOnlyList<string>? NotificationTypes { get; init; }
}

/// <summary>
/// A photo associated with an asset recovery event. Mirrors the spec's
/// <c>RecoveryPhotoResponseBody</c> schema.
/// </summary>
public sealed record DeviceRecoveryPhoto
{
    /// <summary>Timestamp of when the photo was captured, in milliseconds since epoch. Spec marks REQUIRED.</summary>
    [JsonPropertyName("start_ms")]
    public long? StartMs { get; init; }

    /// <summary>
    /// The availability status of the photo. Valid values: <c>EXISTS</c>,
    /// <c>NOT_FOUND</c>, <c>SERVER_ERROR</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// A temporary presigned URL for the recovery photo, expiring at
    /// <c>url_expires_at_ms</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Timestamp when the presigned URL expires, in milliseconds since epoch. Spec marks REQUIRED.</summary>
    [JsonPropertyName("url_expires_at_ms")]
    public long? UrlExpiresAtMs { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/assets/device-recovery/{id}/missing</c>.
/// Mirrors the spec's <c>DeviceRecoveryMarkAssetMissingRequestBody</c> schema.
/// </summary>
public sealed record MarkAssetMissingRequest
{
    /// <summary>Optional note recorded when marking the asset as missing.</summary>
    [JsonPropertyName("note")]
    public string? Note { get; init; }

    /// <summary>List of users to notify when the asset is marked as missing.</summary>
    [JsonPropertyName("notification_recipients")]
    public IReadOnlyList<DeviceRecoveryNotificationRecipientInput>? NotificationRecipients { get; init; }
}

/// <summary>
/// A user to notify about recovery state changes. Mirrors the spec's
/// <c>NotificationRecipientRequestBody</c> schema.
/// </summary>
public sealed record DeviceRecoveryNotificationRecipientInput
{
    /// <summary>
    /// The ID of the user to notify. Users can be retrieved via the
    /// <c>getUser</c> endpoint. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("user_id")]
    public required long UserId { get; init; }

    /// <summary>
    /// How the user should be notified. Only <c>email</c> is supported. Spec
    /// marks REQUIRED.
    /// </summary>
    [JsonPropertyName("notification_types")]
    public required IReadOnlyList<string> NotificationTypes { get; init; }
}

/// <summary>
/// Request body for <c>POST /fleet/assets/device-recovery/{id}/recovered</c>.
/// Mirrors the spec's <c>DeviceRecoveryRecoverAssetRequestBody</c> schema.
/// </summary>
public sealed record RecoverAssetRequest
{
    /// <summary>
    /// The recovery status to set for the asset. Valid values:
    /// <c>RECOVERED</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; init; }

    /// <summary>
    /// The reason the asset was marked as missing. Valid values:
    /// <c>MISPLACED</c>, <c>STOLEN</c>, <c>NOT_SURE</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("missing_reason")]
    public required string MissingReason { get; init; }

    /// <summary>
    /// Whether the asset has been physically recovered. Valid values:
    /// <c>YES</c>, <c>NO</c>, <c>NOT_SURE</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("recovery_status")]
    public required string RecoveryStatus { get; init; }

    /// <summary>Optional additional details about the recovery.</summary>
    [JsonPropertyName("additional_details")]
    public string? AdditionalDetails { get; init; }
}
