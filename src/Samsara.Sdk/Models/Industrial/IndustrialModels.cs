namespace Samsara.Sdk.Models.Industrial;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an industrial asset returned by <c>GET /industrial/assets</c>,
/// <c>POST /industrial/assets</c>, <c>PATCH /industrial/assets/{id}</c>, and
/// <c>PATCH /industrial/assets/{id}/data-outputs</c>. Mirrors the spec's
/// <c>AssetResponse</c> inner schema (plus per-endpoint extensions for the
/// data-outputs response).
/// </summary>
public sealed record IndustrialAsset
{
    /// <summary>The unique identifier of the asset. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The name of the asset. Required on the standard asset payloads
    /// (<c>GET</c>/<c>POST /industrial/assets</c>, <c>PATCH /industrial/assets/{id}</c>);
    /// nullable here because the same record is reused for the
    /// <c>PATCH /industrial/assets/{id}/data-outputs</c> response where it is absent.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The running status of the asset. <c>true</c> for On, <c>false</c> for Off.
    /// Required on the standard asset payloads; nullable here because the
    /// data-outputs response (which reuses this record) omits it.
    /// </summary>
    [JsonPropertyName("isRunning")]
    public bool? IsRunning { get; init; }

    /// <summary>
    /// Status code of the data-outputs request (200 = success, 500 = internal
    /// server error). Spec-required on <c>PatchAssetDataOutputsSingleResponseResponseBody</c>;
    /// nullable here because the same record is reused for the standard asset
    /// payloads where the field is absent.
    /// </summary>
    [JsonPropertyName("statusCode")]
    public long? StatusCode { get; init; }

    /// <summary>
    /// Error message returned by the data-outputs endpoint when the write failed.
    /// Optional per spec; <c>null</c> on the standard asset payloads.
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Free-form custom metadata attached to the asset. Spec models this as
    /// <c>CustomMetadata</c> (an object whose additional properties are strings).
    /// </summary>
    [JsonPropertyName("customMetadata")]
    public IReadOnlyDictionary<string, string>? CustomMetadata { get; init; }

    /// <summary>
    /// The list of data outputs configured on the asset.
    /// </summary>
    [JsonPropertyName("dataOutputs")]
    public IReadOnlyList<IndustrialAssetDataOutput>? DataOutputs { get; init; }

    /// <summary>The location of the asset.</summary>
    [JsonPropertyName("location")]
    public IndustrialAssetLocation? Location { get; init; }

    /// <summary>
    /// The associated location data input (only applicable when
    /// <see cref="LocationType"/> is <c>dataInput</c>).
    /// </summary>
    [JsonPropertyName("locationDataInput")]
    public IndustrialAssetLocationDataInput? LocationDataInput { get; init; }

    /// <summary>
    /// The format of the location. Valid values: <c>point</c>, <c>address</c>,
    /// <c>dataInput</c>.
    /// </summary>
    [JsonPropertyName("locationType")]
    public string? LocationType { get; init; }

    /// <summary>The asset's parent, if part of a hierarchy.</summary>
    [JsonPropertyName("parentAsset")]
    public IndustrialAssetParent? ParentAsset { get; init; }

    /// <summary>
    /// The associated running status data input. <see cref="IsRunning"/> is
    /// <c>true</c> when the data input's value is <c>1</c>.
    /// </summary>
    [JsonPropertyName("runningStatusDataInput")]
    public IndustrialAssetRunningStatusDataInput? RunningStatusDataInput { get; init; }

    /// <summary>The list of tags associated with the asset.</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<IndustrialAssetTag>? Tags { get; init; }
}

/// <summary>
/// A data output configured on an industrial asset. Mirrors the spec's
/// <c>AssetDataOutput</c>.
/// </summary>
public sealed record IndustrialAssetDataOutput
{
    /// <summary>ID of the data output.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the data output.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Name of the data group the data output is associated with.</summary>
    [JsonPropertyName("dataGroup")]
    public string? DataGroup { get; init; }

    /// <summary>ID of the device the data output is configured on.</summary>
    [JsonPropertyName("deviceId")]
    public string? DeviceId { get; init; }

    /// <summary>The associated data input.</summary>
    [JsonPropertyName("dataInput")]
    public IndustrialAssetDataInput? DataInput { get; init; }
}

/// <summary>
/// Data input embedded on an asset's data-output. Mirrors the spec's
/// <c>AssetDataInput</c>.
/// </summary>
public sealed record IndustrialAssetDataInput
{
    /// <summary>ID of the data input.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the data input.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Name of the data group that the data input is associated with.</summary>
    [JsonPropertyName("dataGroup")]
    public string? DataGroup { get; init; }

    /// <summary>Units of data for this data input.</summary>
    [JsonPropertyName("units")]
    public string? Units { get; init; }

    /// <summary>The last reported point of the data input.</summary>
    [JsonPropertyName("lastPoint")]
    public IndustrialAssetDataInputLastPoint? LastPoint { get; init; }
}

/// <summary>
/// Last reported point of a data input embedded on an asset's data-output.
/// Mirrors the spec's <c>AssetDataInput_lastPoint</c>.
/// </summary>
public sealed record IndustrialAssetDataInputLastPoint
{
    /// <summary>UTC timestamp (RFC 3339) of the last reported point.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>Numeric value of the data point.</summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// Location of an industrial asset. Mirrors the spec's <c>AssetLocation</c>.
/// </summary>
public sealed record IndustrialAssetLocation
{
    /// <summary>Formatted address of the location.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }
}

/// <summary>
/// Pointer to the data input that supplies an asset's location.
/// Mirrors the spec's <c>AssetResponse_locationDataInput</c>.
/// </summary>
public sealed record IndustrialAssetLocationDataInput
{
    /// <summary>ID of the data input. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// Reference to an asset's parent.
/// Mirrors the spec's <c>AssetResponse_parentAsset</c>.
/// </summary>
public sealed record IndustrialAssetParent
{
    /// <summary>The id of the parent asset. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>The name of the parent asset. Spec-required.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>
/// Pointer to the data input that supplies an asset's running status.
/// Mirrors the spec's <c>AssetResponse_runningStatusDataInput</c>.
/// </summary>
public sealed record IndustrialAssetRunningStatusDataInput
{
    /// <summary>ID of the data input. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}

/// <summary>
/// A minified tag associated with an asset. Mirrors the spec's <c>tagTinyResponse</c>.
/// </summary>
public sealed record IndustrialAssetTag
{
    /// <summary>ID of the tag.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the tag.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>ID of the parent tag, if any.</summary>
    [JsonPropertyName("parentTagId")]
    public string? ParentTagId { get; init; }
}

/// <summary>
/// A data input belonging to an industrial asset (used by
/// <c>GET /industrial/data-inputs</c>). Mirrors the spec's
/// <c>DataInputTinyResponse</c> inner schema. The time-series data points live on
/// the data-points endpoints and are modelled by <see cref="DataInputDataPoint"/>.
/// </summary>
public sealed record DataInput
{
    /// <summary>Unique identifier for the data input. Optional per the spec list response.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of this data input.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Unique identifier for the data input's asset.</summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>Data group for this data input (e.g. <c>Flow</c>).</summary>
    [JsonPropertyName("dataGroup")]
    public string? DataGroup { get; init; }

    /// <summary>Units of data for this data input.</summary>
    [JsonPropertyName("units")]
    public string? Units { get; init; }
}

/// <summary>
/// A snapshot data point for an industrial data input. Used by
/// <c>GET /industrial/data-inputs/data-points</c>,
/// <c>GET /industrial/data-inputs/data-points/feed</c>, and
/// <c>GET /industrial/data-inputs/data-points/history</c>. Mirrors the union of
/// <c>DataInputTinyResponse</c> with <c>DataInputSnapshot_allOf</c> for the
/// snapshot endpoint and <c>DataInputResponse_allOf</c> for the feed/history
/// endpoints.
/// </summary>
public sealed record DataInputDataPoint
{
    /// <summary>Unique identifier for the data input.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Name of the data input.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Unique identifier for the data input's asset.</summary>
    [JsonPropertyName("assetId")]
    public string? AssetId { get; init; }

    /// <summary>Data group for this data input.</summary>
    [JsonPropertyName("dataGroup")]
    public string? DataGroup { get; init; }

    /// <summary>Units of data for this data input.</summary>
    [JsonPropertyName("units")]
    public string? Units { get; init; }

    // ── Snapshot fields (DataInputSnapshot_allOf) ─────────────────────────────

    /// <summary>The most recent FFT spectra data point (snapshot endpoint only).</summary>
    [JsonPropertyName("fftSpectraPoint")]
    public FftSpectraDataPoint? FftSpectraPoint { get; init; }

    /// <summary>The most recent active J1939D1 status (snapshot endpoint only).</summary>
    [JsonPropertyName("j1939D1StatusPoint")]
    public J1939D1StatusDataPoint? J1939D1StatusPoint { get; init; }

    /// <summary>The most recent location data point (snapshot endpoint only).</summary>
    [JsonPropertyName("locationPoint")]
    public LocationDataPoint? LocationPoint { get; init; }

    /// <summary>The most recent numeric data point (snapshot endpoint only).</summary>
    [JsonPropertyName("numberPoint")]
    public NumberDataPoint? NumberPoint { get; init; }

    /// <summary>The most recent string data point (snapshot endpoint only).</summary>
    [JsonPropertyName("stringPoint")]
    public StringDataPoint? StringPoint { get; init; }

    // ── Feed / history fields (DataInputResponse_allOf) ───────────────────────

    /// <summary>List of FFT spectra data points (feed / history endpoints).</summary>
    [JsonPropertyName("fftSpectraPoints")]
    public IReadOnlyList<FftSpectraDataPoint>? FftSpectraPoints { get; init; }

    /// <summary>List of active J1939D1 statuses (feed / history endpoints).</summary>
    [JsonPropertyName("j1939D1StatusPoints")]
    public IReadOnlyList<J1939D1StatusDataPoint>? J1939D1StatusPoints { get; init; }

    /// <summary>List of location data points (feed / history endpoints).</summary>
    [JsonPropertyName("locationPoints")]
    public IReadOnlyList<LocationDataPoint>? LocationPoints { get; init; }

    /// <summary>List of numeric data points (feed / history endpoints).</summary>
    [JsonPropertyName("numberPoints")]
    public IReadOnlyList<NumberDataPoint>? NumberPoints { get; init; }

    /// <summary>List of string data points (feed / history endpoints).</summary>
    [JsonPropertyName("stringPoints")]
    public IReadOnlyList<StringDataPoint>? StringPoints { get; init; }
}

/// <summary>
/// A single numeric data point of a data input. Mirrors the spec's <c>NumberDataPoint</c>.
/// </summary>
public sealed record NumberDataPoint
{
    /// <summary>UTC timestamp (RFC 3339) of the data point.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>Numeric value of the data point.</summary>
    [JsonPropertyName("value")]
    public double? Value { get; init; }
}

/// <summary>
/// A single string data point of a data input. Mirrors the spec's <c>StringDataPoint</c>.
/// </summary>
public sealed record StringDataPoint
{
    /// <summary>UTC timestamp (RFC 3339) of the data point.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>String value of the data point.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// A single location data point of a data input. Mirrors the spec's
/// <c>LocationDataPoint</c>.
/// </summary>
public sealed record LocationDataPoint
{
    /// <summary>UTC timestamp (RFC 3339) of the data point.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>GPS location information of the data point.</summary>
    [JsonPropertyName("gpsLocation")]
    public LocationDataPointGpsLocation? GpsLocation { get; init; }
}

/// <summary>
/// GPS location information of a data input's data point. Mirrors the spec's
/// <c>LocationDataPoint_gpsLocation</c>.
/// </summary>
public sealed record LocationDataPointGpsLocation
{
    /// <summary>Formatted address of the location.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>Latitude in decimal degrees.</summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>Longitude in decimal degrees.</summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>Speed of GPS in meters per second.</summary>
    [JsonPropertyName("gpsMetersPerSecond")]
    public double? GpsMetersPerSecond { get; init; }

    /// <summary>Heading in degrees.</summary>
    [JsonPropertyName("headingDegrees")]
    public double? HeadingDegrees { get; init; }

    /// <summary>Address of the location.</summary>
    [JsonPropertyName("place")]
    public LocationDataPointPlace? Place { get; init; }
}

/// <summary>
/// Address of a location data point. Mirrors the spec's
/// <c>LocationDataPoint_gpsLocation_place</c>.
/// </summary>
public sealed record LocationDataPointPlace
{
    /// <summary>City.</summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>House number.</summary>
    [JsonPropertyName("houseNumber")]
    public string? HouseNumber { get; init; }

    /// <summary>Neighborhood.</summary>
    [JsonPropertyName("neighborhood")]
    public string? Neighborhood { get; init; }

    /// <summary>POI.</summary>
    [JsonPropertyName("poi")]
    public string? Poi { get; init; }

    /// <summary>Postcode.</summary>
    [JsonPropertyName("postcode")]
    public string? Postcode { get; init; }

    /// <summary>State.</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Street.</summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }
}

/// <summary>
/// FFT spectrum data point of a data input. Mirrors the spec's <c>FftSpectraDataPoint</c>.
/// </summary>
public sealed record FftSpectraDataPoint
{
    /// <summary>UTC timestamp (RFC 3339) of the data point.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>FFT spectrum data.</summary>
    [JsonPropertyName("fftSpectra")]
    public FftSpectraValue? FftSpectra { get; init; }
}

/// <summary>
/// FFT spectrum data. Mirrors the spec's <c>FftSpectraDataPoint_fftSpectra</c>.
/// </summary>
public sealed record FftSpectraValue
{
    /// <summary>Frequencies.</summary>
    [JsonPropertyName("frequencies")]
    public IReadOnlyList<double>? Frequencies { get; init; }

    /// <summary>X-axis data.</summary>
    [JsonPropertyName("x")]
    public IReadOnlyList<double>? X { get; init; }

    /// <summary>Y-axis data.</summary>
    [JsonPropertyName("y")]
    public IReadOnlyList<double>? Y { get; init; }

    /// <summary>Z-axis data.</summary>
    [JsonPropertyName("z")]
    public IReadOnlyList<double>? Z { get; init; }
}

/// <summary>
/// Active J1939D1 statuses of a device. Mirrors the spec's <c>J1939D1StatusDataPoint</c>.
/// </summary>
public sealed record J1939D1StatusDataPoint
{
    /// <summary>UTC timestamp (RFC 3339) of the data point.</summary>
    [JsonPropertyName("time")]
    public DateTimeOffset? Time { get; init; }

    /// <summary>List of active statuses.</summary>
    [JsonPropertyName("value")]
    public IReadOnlyList<J1939D1Status>? Value { get; init; }
}

/// <summary>
/// A single J1939D1 status. Mirrors the spec's <c>J1939D1StatusDataPoint_value</c>.
/// </summary>
public sealed record J1939D1Status
{
    /// <summary>Amber lamp status.</summary>
    [JsonPropertyName("amberLampStatus")]
    public int? AmberLampStatus { get; init; }

    /// <summary>Failure mode identifier.</summary>
    [JsonPropertyName("fmi")]
    public int? Fmi { get; init; }

    /// <summary>MIL (malfunction indicator lamp) status.</summary>
    [JsonPropertyName("milStatus")]
    public int? MilStatus { get; init; }

    /// <summary>Occurrence count.</summary>
    [JsonPropertyName("occuranceCount")]
    public int? OccuranceCount { get; init; }

    /// <summary>Protect lamp status.</summary>
    [JsonPropertyName("protectLampStatus")]
    public int? ProtectLampStatus { get; init; }

    /// <summary>Red lamp status.</summary>
    [JsonPropertyName("redLampStatus")]
    public int? RedLampStatus { get; init; }

    /// <summary>Suspect parameter number.</summary>
    [JsonPropertyName("spn")]
    public int? Spn { get; init; }

    /// <summary>Transmission identifier.</summary>
    [JsonPropertyName("txId")]
    public int? TxId { get; init; }
}

/// <summary>
/// Machine history entry. Used by the legacy v1 <c>POST /v1/machines/history</c> endpoint.
/// </summary>
public sealed record MachineHistoryEntry
{
    [JsonPropertyName("machineId")]
    public string? MachineId { get; init; }

    [JsonPropertyName("vibrations")]
    public IReadOnlyList<MachineVibration>? Vibrations { get; init; }
}

/// <summary>
/// A vibration reading for a machine. Used by the legacy v1
/// <c>POST /v1/machines/history</c> endpoint.
/// </summary>
public sealed record MachineVibration
{
    [JsonPropertyName("x")]
    public double? X { get; init; }

    [JsonPropertyName("y")]
    public double? Y { get; init; }

    [JsonPropertyName("z")]
    public double? Z { get; init; }

    [JsonPropertyName("time")]
    public long? Time { get; init; }
}
