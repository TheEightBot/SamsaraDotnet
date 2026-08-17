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

// ---------------------------------------------------------------------------
// Industrial asset write bodies — POST /industrial/assets,
// PATCH /industrial/assets/{id}, PATCH /industrial/assets/{id}/data-outputs.
// ---------------------------------------------------------------------------

/// <summary>
/// Request body for <c>POST /industrial/assets</c>. Mirrors the spec's
/// <c>AssetCreate</c> schema.
/// </summary>
/// <remarks>
/// Split from <see cref="UpdateIndustrialAssetRequest"/> (spec <c>AssetPatch</c>)
/// because the two differ in required-ness: create marks <c>name</c> REQUIRED,
/// patch marks nothing required.
/// </remarks>
public sealed record CreateIndustrialAssetRequest
{
    /// <summary>The name of the asset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>The id of the parent asset that the asset belongs to.</summary>
    [JsonPropertyName("parentId")]
    public string? ParentId { get; init; }

    /// <summary>The ids of the tags that the asset should belong to.</summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    /// <summary>
    /// The custom fields of the asset (spec schema <c>CustomMetadata</c>, an
    /// object whose additional properties are strings).
    /// </summary>
    [JsonPropertyName("customMetadata")]
    public IReadOnlyDictionary<string, string>? CustomMetadata { get; init; }

    /// <summary>
    /// The asset's location. For <c>locationType</c> <c>point</c>, latitude and
    /// longitude are required; for <c>address</c>, <c>formattedAddress</c> must
    /// be supplied.
    /// </summary>
    [JsonPropertyName("location")]
    public IndustrialAssetLocation? Location { get; init; }

    /// <summary>
    /// The format of the location; required when a location is provided. Valid
    /// values: <c>point</c>, <c>address</c>, <c>dataInput</c>.
    /// </summary>
    [JsonPropertyName("locationType")]
    public string? LocationType { get; init; }

    /// <summary>
    /// Required when <c>locationType</c> is <c>dataInput</c>: the id of the
    /// location data input that determines the asset's location.
    /// </summary>
    [JsonPropertyName("locationDataInputId")]
    public string? LocationDataInputId { get; init; }

    /// <summary>
    /// The asset's <c>isRunning</c> status will be <c>true</c> when the
    /// associated data input's value is 1.
    /// </summary>
    [JsonPropertyName("runningStatusDataInputId")]
    public string? RunningStatusDataInputId { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /industrial/assets/{id}</c>. Mirrors the spec's
/// <c>AssetPatch</c> schema — the same members as
/// <see cref="CreateIndustrialAssetRequest"/>, none of them required.
/// </summary>
public sealed record UpdateIndustrialAssetRequest
{
    /// <summary>The name of the asset.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The id of the parent asset that the asset belongs to. Pass an empty
    /// string to remove the asset from its parent.
    /// </summary>
    [JsonPropertyName("parentId")]
    public string? ParentId { get; init; }

    /// <summary>The ids of the tags that the asset should belong to.</summary>
    [JsonPropertyName("tagIds")]
    public IReadOnlyList<string>? TagIds { get; init; }

    /// <summary>
    /// The custom fields of the asset (spec schema <c>CustomMetadata</c>, an
    /// object whose additional properties are strings).
    /// </summary>
    [JsonPropertyName("customMetadata")]
    public IReadOnlyDictionary<string, string>? CustomMetadata { get; init; }

    /// <summary>The asset's location.</summary>
    [JsonPropertyName("location")]
    public IndustrialAssetLocation? Location { get; init; }

    /// <summary>
    /// The format of the location; required when a location is provided. Valid
    /// values: <c>point</c>, <c>address</c>, <c>dataInput</c>.
    /// </summary>
    [JsonPropertyName("locationType")]
    public string? LocationType { get; init; }

    /// <summary>
    /// Required when <c>locationType</c> is <c>dataInput</c>: the id of the
    /// location data input that determines the asset's location.
    /// </summary>
    [JsonPropertyName("locationDataInputId")]
    public string? LocationDataInputId { get; init; }

    /// <summary>
    /// The asset's <c>isRunning</c> status will be <c>true</c> when the
    /// associated data input's value is 1.
    /// </summary>
    [JsonPropertyName("runningStatusDataInputId")]
    public string? RunningStatusDataInputId { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /industrial/assets/{id}/data-outputs</c>. Mirrors
/// the spec's <c>AssetDataOutputsPatchAssetDataOutputsRequestBody</c> schema.
/// </summary>
public sealed record UpdateIndustrialAssetDataOutputsRequest
{
    /// <summary>
    /// A map of data output IDs to values. All data outputs must belong to the
    /// same asset; only the specified IDs are written. Spec marks REQUIRED.
    /// </summary>
    /// <remarks>
    /// The spec declares <c>values</c> as a bare <c>{ type: object }</c> with no
    /// <c>properties</c> — the value type depends on the data output's
    /// configured type — so the map's value stays a
    /// <see cref="System.Text.Json.JsonElement"/>.
    /// </remarks>
    [JsonPropertyName("values")]
    public required IReadOnlyDictionary<string, System.Text.Json.JsonElement> Values { get; init; }
}

// ---------------------------------------------------------------------------
// Legacy v1 Vision API — GET /v1/industrial/vision/*.
// ---------------------------------------------------------------------------

/// <summary>
/// A vision camera installed in the organization. Mirrors the item schema of the
/// spec's <c>V1VisionCamerasResponse</c> array
/// (<c>GET /v1/industrial/vision/cameras</c>).
/// </summary>
public sealed record V1VisionCamera
{
    /// <summary>The camera's identifier.</summary>
    [JsonPropertyName("cameraId")]
    public long? CameraId { get; init; }

    /// <summary>The camera's display name.</summary>
    [JsonPropertyName("cameraName")]
    public string? CameraName { get; init; }

    /// <summary>The camera's ethernet IP address.</summary>
    [JsonPropertyName("ethernetIp")]
    public string? EthernetIp { get; init; }

    /// <summary>The camera's Wi-Fi IP address.</summary>
    [JsonPropertyName("wifiIp")]
    public string? WifiIp { get; init; }
}

/// <summary>
/// A program configured on a vision camera. Mirrors the item schema of the
/// spec's <c>V1ProgramsForTheCameraResponse</c> array
/// (<c>GET /v1/industrial/vision/cameras/{camera_id}/programs</c>).
/// </summary>
public sealed record V1VisionProgram
{
    /// <summary>The program's identifier.</summary>
    [JsonPropertyName("programId")]
    public long? ProgramId { get; init; }

    /// <summary>The program's display name.</summary>
    [JsonPropertyName("programName")]
    public string? ProgramName { get; init; }
}

/// <summary>
/// Response body of <c>GET /v1/industrial/vision/runs</c>. Mirrors the spec's
/// <c>V1VisionRunsResponse</c> schema, whose only member is
/// <c>visionRuns</c>.
/// </summary>
public sealed record V1VisionRunsResponse
{
    /// <summary>The vision runs in the requested window.</summary>
    [JsonPropertyName("visionRuns")]
    public IReadOnlyList<V1VisionRun>? VisionRuns { get; init; }
}

/// <summary>
/// A vision run summary. Mirrors the item schema of the spec's
/// <c>V1VisionRunsResponse.visionRuns</c> array.
/// </summary>
/// <remarks>
/// Distinct from <see cref="V1VisionCameraRun"/>: this shape identifies its
/// program by bare <c>programId</c>, whereas the per-camera endpoint returns a
/// nested <c>program</c> object.
/// </remarks>
public sealed record V1VisionRun
{
    /// <summary>The identifier of the camera that produced the run.</summary>
    [JsonPropertyName("deviceId")]
    public long? DeviceId { get; init; }

    /// <summary>The identifier of the program the run executed.</summary>
    [JsonPropertyName("programId")]
    public long? ProgramId { get; init; }

    /// <summary>Run start time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("startedAtMs")]
    public long? StartedAtMs { get; init; }

    /// <summary>Run end time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("endedAtMs")]
    public long? EndedAtMs { get; init; }

    /// <summary>Aggregate counts for the run.</summary>
    [JsonPropertyName("reportMetadata")]
    public V1VisionRunSummary? ReportMetadata { get; init; }
}

/// <summary>
/// A vision run for one camera. Mirrors the item schema of the spec's
/// <c>V1VisionRunsByCameraResponse</c> array
/// (<c>GET /v1/industrial/vision/runs/{camera_id}</c>).
/// </summary>
public sealed record V1VisionCameraRun
{
    /// <summary>The identifier of the camera that produced the run.</summary>
    [JsonPropertyName("deviceId")]
    public long? DeviceId { get; init; }

    /// <summary>The program the run executed.</summary>
    [JsonPropertyName("program")]
    public V1VisionProgramReference? Program { get; init; }

    /// <summary>Run start time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("startedAtMs")]
    public long? StartedAtMs { get; init; }

    /// <summary>Run end time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("endedAtMs")]
    public long? EndedAtMs { get; init; }

    /// <summary>Aggregate counts for the run.</summary>
    [JsonPropertyName("reportMetadata")]
    public V1VisionRunSummary? ReportMetadata { get; init; }
}

/// <summary>
/// The most recent vision run for a camera, as returned by
/// <c>GET /v1/industrial/vision/run/camera/{camera_id}</c>. Mirrors the spec's
/// <c>V1VisionRunByCameraResponse</c> schema.
/// </summary>
public sealed record V1VisionLatestRun
{
    /// <summary>The identifier of the camera that produced the run.</summary>
    [JsonPropertyName("cameraId")]
    public long? CameraId { get; init; }

    /// <summary>The program the run executed.</summary>
    [JsonPropertyName("program")]
    public V1VisionProgramReference? Program { get; init; }

    /// <summary>Run start time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("startedAtMs")]
    public long? StartedAtMs { get; init; }

    /// <summary>Run end time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("endedAtMs")]
    public long? EndedAtMs { get; init; }

    /// <summary>Whether the run is still in progress.</summary>
    [JsonPropertyName("isOngoing")]
    public bool? IsOngoing { get; init; }

    /// <summary>Aggregate counts for the run.</summary>
    [JsonPropertyName("runSummary")]
    public V1VisionRunSummary? RunSummary { get; init; }

    /// <summary>The per-item inspection results captured during the run.</summary>
    [JsonPropertyName("inspectionResults")]
    public IReadOnlyList<V1VisionInspectionResult>? InspectionResults { get; init; }
}

/// <summary>
/// A vision run for one camera-and-program pair, as returned by
/// <c>GET /v1/industrial/vision/runs/{camera_id}/{program_id}/{started_at_ms}</c>.
/// Mirrors the spec's <c>V1VisionRunsByCameraAndProgramResponse</c> schema.
/// </summary>
public sealed record V1VisionProgramRun
{
    /// <summary>The identifier of the camera that produced the run.</summary>
    [JsonPropertyName("deviceId")]
    public long? DeviceId { get; init; }

    /// <summary>The identifier of the program the run executed.</summary>
    [JsonPropertyName("programId")]
    public long? ProgramId { get; init; }

    /// <summary>Run start time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("startedAtMs")]
    public long? StartedAtMs { get; init; }

    /// <summary>Run end time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("endedAtMs")]
    public long? EndedAtMs { get; init; }

    /// <summary>Aggregate counts for the run.</summary>
    [JsonPropertyName("reportMetadata")]
    public V1VisionRunSummary? ReportMetadata { get; init; }

    /// <summary>The per-item inspection results captured during the run.</summary>
    [JsonPropertyName("results")]
    public IReadOnlyList<V1VisionInspectionResult>? Results { get; init; }
}

/// <summary>
/// A reference to a vision program by id and name. Mirrors the spec's
/// <c>V1VisionRunByCameraResponse_program</c> schema (and the structurally
/// identical inline <c>program</c> object on
/// <c>V1VisionRunsByCameraResponse</c>).
/// </summary>
/// <remarks>
/// Not modelled with the shared <c>EntityReference</c> because the v1 vision
/// identifier is an int64, not a string.
/// </remarks>
public sealed record V1VisionProgramReference
{
    /// <summary>The program's identifier.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>The program's display name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Aggregate counts for a vision run. One record serves the spec's
/// <c>V1VisionRunByCameraResponse_runSummary</c> and
/// <c>V1VisionRunsResponse_reportMetadata</c> schemas, which are structurally
/// identical.
/// </summary>
public sealed record V1VisionRunSummary
{
    /// <summary>Average scanned items per minute. Supersedes the deprecated <c>scanRate</c>.</summary>
    [JsonPropertyName("itemsPerMinute")]
    public double? ItemsPerMinute { get; init; }

    /// <summary>No-read count for the run. Supersedes the deprecated <c>noReadScansCount</c>.</summary>
    [JsonPropertyName("noReadCount")]
    public long? NoReadCount { get; init; }

    /// <summary>Reject count for the run. Supersedes the deprecated <c>failedScansCount</c>.</summary>
    [JsonPropertyName("rejectCount")]
    public long? RejectCount { get; init; }

    /// <summary>Success count for the run. Supersedes the deprecated <c>successfulScansCount</c>.</summary>
    [JsonPropertyName("successCount")]
    public long? SuccessCount { get; init; }
}

/// <summary>
/// One item inspection captured during a vision run. Mirrors the spec's
/// <c>V1VisionRunByCameraResponse_inspectionResults</c> schema.
/// </summary>
public sealed record V1VisionInspectionResult
{
    /// <summary>Capture time, in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("captureAtMs")]
    public double? CaptureAtMs { get; init; }

    /// <summary>The overall inspection result.</summary>
    [JsonPropertyName("result")]
    public string? Result { get; init; }

    /// <summary>The per-step results that make up the inspection.</summary>
    [JsonPropertyName("stepResults")]
    public IReadOnlyList<V1VisionStepResult>? StepResults { get; init; }
}

/// <summary>
/// One step within a vision inspection. Mirrors the item schema of the spec's
/// <c>V1VisionStepResults</c> array.
/// </summary>
/// <remarks>
/// Exactly one of the tool-specific members is populated per step; which one
/// depends on the tool the step was configured with.
/// </remarks>
public sealed record V1VisionStepResult
{
    /// <summary>The step's name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The step's result.</summary>
    [JsonPropertyName("result")]
    public string? Result { get; init; }

    /// <summary>Result of an angle-check tool step.</summary>
    [JsonPropertyName("angleCheck")]
    public V1VisionAngleCheckResult? AngleCheck { get; init; }

    /// <summary>Results of a barcode tool step.</summary>
    [JsonPropertyName("barcode")]
    public IReadOnlyList<V1VisionBarcodeResult>? Barcode { get; init; }

    /// <summary>Result of a boolean-logic tool step.</summary>
    [JsonPropertyName("booleanLogic")]
    public V1VisionBooleanLogicResult? BooleanLogic { get; init; }

    /// <summary>Result of a caliper tool step.</summary>
    [JsonPropertyName("caliper")]
    public V1VisionCaliperResult? Caliper { get; init; }

    /// <summary>Result of a contour tool step.</summary>
    [JsonPropertyName("contour")]
    public V1VisionContourResult? Contour { get; init; }

    /// <summary>Result of a distance tool step.</summary>
    [JsonPropertyName("distance")]
    public V1VisionDistanceResult? Distance { get; init; }

    /// <summary>Result of an expiration-date tool step.</summary>
    [JsonPropertyName("expirationDate")]
    public V1VisionExpirationDateResult? ExpirationDate { get; init; }

    /// <summary>Result of a find-copies tool step.</summary>
    [JsonPropertyName("findCopies")]
    public V1VisionFindCopiesResult? FindCopies { get; init; }

    /// <summary>Result of a find-edge tool step.</summary>
    [JsonPropertyName("findEdge")]
    public V1VisionFindEdgeResult? FindEdge { get; init; }

    /// <summary>Result of a find-shapes tool step.</summary>
    [JsonPropertyName("findShapes")]
    public V1VisionFindShapesResult? FindShapes { get; init; }

    /// <summary>Result of a fixture tool step.</summary>
    [JsonPropertyName("fixture")]
    public V1VisionFixtureResult? Fixture { get; init; }

    /// <summary>Result of a label-match tool step.</summary>
    [JsonPropertyName("labelMatch")]
    public V1VisionLabelMatchResult? LabelMatch { get; init; }

    /// <summary>Result of a presence/absence tool step.</summary>
    [JsonPropertyName("presenceAbsence")]
    public V1VisionPresenceAbsenceResult? PresenceAbsence { get; init; }

    /// <summary>Result of a text-match tool step.</summary>
    [JsonPropertyName("textMatch")]
    public V1VisionTextMatchResult? TextMatch { get; init; }
}

/// <summary>
/// A configured low/high allowance range on a vision step result. One record
/// serves every <c>{ high, low }</c> object in the spec's
/// <c>V1VisionStepResults</c> schema (angle, contrast, sharpness, straightness
/// and the six presence/absence colour ranges).
/// </summary>
public sealed record V1VisionRange
{
    /// <summary>The lower bound of the configured range.</summary>
    [JsonPropertyName("low")]
    public long? Low { get; init; }

    /// <summary>The upper bound of the configured range.</summary>
    [JsonPropertyName("high")]
    public long? High { get; init; }
}

/// <summary>Result of an angle-check vision step.</summary>
public sealed record V1VisionAngleCheckResult
{
    /// <summary>The configured angle allowance range, in degrees.</summary>
    [JsonPropertyName("angleConfigured")]
    public V1VisionRange? AngleConfigured { get; init; }

    /// <summary>The counter-clockwise angle detected from the first edge to the second edge.</summary>
    [JsonPropertyName("angleFound")]
    public long? AngleFound { get; init; }

    /// <summary>The name of the first reference step used to check the angle.</summary>
    [JsonPropertyName("startStepName")]
    public string? StartStepName { get; init; }

    /// <summary>The name of the second reference step used to check the angle.</summary>
    [JsonPropertyName("endStepName")]
    public string? EndStepName { get; init; }
}

/// <summary>A single barcode read by a barcode vision step.</summary>
public sealed record V1VisionBarcodeResult
{
    /// <summary>The decoded barcode contents.</summary>
    [JsonPropertyName("contents")]
    public string? Contents { get; init; }

    /// <summary>The configured string the contents are matched against.</summary>
    [JsonPropertyName("matchString")]
    public string? MatchString { get; init; }

    /// <summary>The barcode symbology.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>Result of a boolean-logic vision step.</summary>
public sealed record V1VisionBooleanLogicResult
{
    /// <summary>The logical operator applied across the referenced steps.</summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; init; }

    /// <summary>The steps the operator was applied to.</summary>
    [JsonPropertyName("steps")]
    public IReadOnlyList<V1VisionBooleanLogicStep>? Steps { get; init; }
}

/// <summary>One operand of a boolean-logic vision step.</summary>
public sealed record V1VisionBooleanLogicStep
{
    /// <summary>The referenced step's name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The referenced step's result.</summary>
    [JsonPropertyName("result")]
    public string? Result { get; init; }
}

/// <summary>Result of a caliper vision step.</summary>
public sealed record V1VisionCaliperResult
{
    /// <summary>The configured angle allowance range.</summary>
    [JsonPropertyName("angleRange")]
    public V1VisionRange? AngleRange { get; init; }

    /// <summary>The configured contrast allowance range.</summary>
    [JsonPropertyName("contrastRange")]
    public V1VisionRange? ContrastRange { get; init; }

    /// <summary>The configured sharpness allowance range.</summary>
    [JsonPropertyName("sharpnessRange")]
    public V1VisionRange? SharpnessRange { get; init; }

    /// <summary>The configured straightness allowance range.</summary>
    [JsonPropertyName("straightnessRange")]
    public V1VisionRange? StraightnessRange { get; init; }

    /// <summary>The distance found between the found edges.</summary>
    [JsonPropertyName("distanceFound")]
    public double? DistanceFound { get; init; }

    /// <summary>The minimum allowed distance threshold.</summary>
    [JsonPropertyName("minDistance")]
    public double? MinDistance { get; init; }

    /// <summary>The maximum allowed distance threshold.</summary>
    [JsonPropertyName("maxDistance")]
    public double? MaxDistance { get; init; }

    /// <summary>
    /// The configured polarity for finding edges. Valid values:
    /// <c>LIGHT TO DARK</c>, <c>DARK TO LIGHT</c>.
    /// </summary>
    [JsonPropertyName("filterPolarity")]
    public string? FilterPolarity { get; init; }

    /// <summary>The measurement unit of the distances reported by this step.</summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
}

/// <summary>Result of a contour vision step.</summary>
public sealed record V1VisionContourResult
{
    /// <summary>The rotation angle found.</summary>
    [JsonPropertyName("angleDegrees")]
    public long? AngleDegrees { get; init; }

    /// <summary>The rotation angle allowance.</summary>
    [JsonPropertyName("angleTolerance")]
    public long? AngleTolerance { get; init; }

    /// <summary>The contour match percentage against the configured contour.</summary>
    [JsonPropertyName("matchPercentage")]
    public long? MatchPercentage { get; init; }

    /// <summary>The configured match threshold for contours.</summary>
    [JsonPropertyName("matchThreshold")]
    public long? MatchThreshold { get; init; }
}

/// <summary>Result of a distance vision step.</summary>
public sealed record V1VisionDistanceResult
{
    /// <summary>The distance found between the start and end references.</summary>
    [JsonPropertyName("distanceFound")]
    public long? DistanceFound { get; init; }

    /// <summary>The minimum allowed distance threshold.</summary>
    [JsonPropertyName("minDistance")]
    public long? MinDistance { get; init; }

    /// <summary>The maximum allowed distance threshold.</summary>
    [JsonPropertyName("maxDistance")]
    public long? MaxDistance { get; init; }

    /// <summary>The name of the first reference step the distance is measured from.</summary>
    [JsonPropertyName("startStepName")]
    public string? StartStepName { get; init; }

    /// <summary>The name of the second reference step the distance is measured to.</summary>
    [JsonPropertyName("endStepName")]
    public string? EndStepName { get; init; }

    /// <summary>Whether an offset angle range is enforced.</summary>
    [JsonPropertyName("enforceOffsetAngleRange")]
    public bool? EnforceOffsetAngleRange { get; init; }

    /// <summary>
    /// The minimum angle allowance, in degrees, when
    /// <c>enforceOffsetAngleRange</c> is <c>true</c>.
    /// </summary>
    [JsonPropertyName("minOffsetAngle")]
    public long? MinOffsetAngle { get; init; }

    /// <summary>
    /// The maximum angle allowance, in degrees, when
    /// <c>enforceOffsetAngleRange</c> is <c>true</c>.
    /// </summary>
    [JsonPropertyName("maxOffsetAngle")]
    public long? MaxOffsetAngle { get; init; }

    /// <summary>
    /// The counter-clockwise angle, in degrees, found between the horizontal
    /// axis of the start reference step and the end reference step.
    /// </summary>
    [JsonPropertyName("offsetAngleFound")]
    public long? OffsetAngleFound { get; init; }

    /// <summary>The measurement unit of the distances reported by this step.</summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
}

/// <summary>Result of an expiration-date vision step.</summary>
public sealed record V1VisionExpirationDateResult
{
    /// <summary>The configured offset applied to the matched date.</summary>
    [JsonPropertyName("dateOffset")]
    public long? DateOffset { get; init; }

    /// <summary>The date read from the item.</summary>
    [JsonPropertyName("foundDate")]
    public string? FoundDate { get; init; }

    /// <summary>The date the read value was matched against.</summary>
    [JsonPropertyName("matchDate")]
    public string? MatchDate { get; init; }
}

/// <summary>Result of a find-copies vision step.</summary>
public sealed record V1VisionFindCopiesResult
{
    /// <summary>The orientation angle tolerance, in degrees.</summary>
    [JsonPropertyName("angleTolerance")]
    public long? AngleTolerance { get; init; }

    /// <summary>The number of copies found.</summary>
    [JsonPropertyName("foundCount")]
    public long? FoundCount { get; init; }

    /// <summary>The minimum number of copies allowed.</summary>
    [JsonPropertyName("minCount")]
    public long? MinCount { get; init; }

    /// <summary>The maximum number of copies allowed.</summary>
    [JsonPropertyName("maxCount")]
    public long? MaxCount { get; init; }

    /// <summary>
    /// The minimum required similarity, in percent, of a found copy compared to
    /// the configured match region.
    /// </summary>
    [JsonPropertyName("threshold")]
    public long? Threshold { get; init; }
}

/// <summary>Result of a find-edge vision step.</summary>
public sealed record V1VisionFindEdgeResult
{
    /// <summary>The detected angle, in degrees.</summary>
    [JsonPropertyName("angleFound")]
    public long? AngleFound { get; init; }

    /// <summary>The configured angle allowance range.</summary>
    [JsonPropertyName("angleRange")]
    public V1VisionRange? AngleRange { get; init; }

    /// <summary>The detected contrast percentage.</summary>
    [JsonPropertyName("contrastPercent")]
    public long? ContrastPercent { get; init; }

    /// <summary>The configured contrast allowance range.</summary>
    [JsonPropertyName("contrastRange")]
    public V1VisionRange? ContrastRange { get; init; }

    /// <summary>The detected sharpness percentage.</summary>
    [JsonPropertyName("sharpnessPercent")]
    public long? SharpnessPercent { get; init; }

    /// <summary>The configured sharpness allowance range.</summary>
    [JsonPropertyName("sharpnessRange")]
    public V1VisionRange? SharpnessRange { get; init; }

    /// <summary>The detected straightness percentage.</summary>
    [JsonPropertyName("straightnessFound")]
    public long? StraightnessFound { get; init; }

    /// <summary>The configured straightness allowance range.</summary>
    [JsonPropertyName("straightnessRange")]
    public V1VisionRange? StraightnessRange { get; init; }

    /// <summary>
    /// The configured polarity for finding edges. Valid values:
    /// <c>LIGHT TO DARK</c>, <c>DARK TO LIGHT</c>.
    /// </summary>
    [JsonPropertyName("filterPolarity")]
    public string? FilterPolarity { get; init; }
}

/// <summary>Result of a find-shapes vision step.</summary>
public sealed record V1VisionFindShapesResult
{
    /// <summary>The number of shapes found.</summary>
    [JsonPropertyName("foundCount")]
    public long? FoundCount { get; init; }

    /// <summary>The minimum number of shapes allowed.</summary>
    [JsonPropertyName("minCount")]
    public long? MinCount { get; init; }

    /// <summary>The maximum number of shapes allowed.</summary>
    [JsonPropertyName("maxCount")]
    public long? MaxCount { get; init; }
}

/// <summary>Result of a fixture vision step.</summary>
public sealed record V1VisionFixtureResult
{
    /// <summary>The coordinates at which the fixture was located.</summary>
    [JsonPropertyName("coordinates")]
    public V1VisionFixtureCoordinates? Coordinates { get; init; }

    /// <summary>Whether the fixture was found.</summary>
    [JsonPropertyName("found")]
    public bool? Found { get; init; }

    /// <summary>The fixture's rotation, in degrees.</summary>
    [JsonPropertyName("rotationDegrees")]
    public long? RotationDegrees { get; init; }
}

/// <summary>Pixel coordinates at which a vision fixture was located.</summary>
public sealed record V1VisionFixtureCoordinates
{
    /// <summary>The horizontal coordinate.</summary>
    [JsonPropertyName("x")]
    public long? X { get; init; }

    /// <summary>The vertical coordinate.</summary>
    [JsonPropertyName("y")]
    public long? Y { get; init; }
}

/// <summary>Result of a label-match vision step.</summary>
public sealed record V1VisionLabelMatchResult
{
    /// <summary>The match score achieved.</summary>
    [JsonPropertyName("score")]
    public long? Score { get; init; }

    /// <summary>The configured score threshold.</summary>
    [JsonPropertyName("threshold")]
    public long? Threshold { get; init; }
}

/// <summary>Result of a presence/absence vision step.</summary>
public sealed record V1VisionPresenceAbsenceResult
{
    /// <summary>Whether the step checks for absence rather than presence.</summary>
    [JsonPropertyName("checkForAbsence")]
    public bool? CheckForAbsence { get; init; }

    /// <summary>The match score achieved.</summary>
    [JsonPropertyName("score")]
    public long? Score { get; init; }

    /// <summary>The configured score threshold.</summary>
    [JsonPropertyName("threshold")]
    public long? Threshold { get; init; }

    /// <summary>The configured grayscale allowance range.</summary>
    [JsonPropertyName("grayscaleRange")]
    public V1VisionRange? GrayscaleRange { get; init; }

    /// <summary>The configured hue allowance range.</summary>
    [JsonPropertyName("hueRange")]
    public V1VisionRange? HueRange { get; init; }

    /// <summary>The configured saturation allowance range.</summary>
    [JsonPropertyName("saturationRange")]
    public V1VisionRange? SaturationRange { get; init; }

    /// <summary>The configured value (brightness) allowance range.</summary>
    [JsonPropertyName("valueRange")]
    public V1VisionRange? ValueRange { get; init; }

    /// <summary>The configured red-channel allowance range.</summary>
    [JsonPropertyName("redRange")]
    public V1VisionRange? RedRange { get; init; }

    /// <summary>The configured green-channel allowance range.</summary>
    [JsonPropertyName("greenRange")]
    public V1VisionRange? GreenRange { get; init; }

    /// <summary>The configured blue-channel allowance range.</summary>
    [JsonPropertyName("blueRange")]
    public V1VisionRange? BlueRange { get; init; }
}

/// <summary>Result of a text-match vision step.</summary>
public sealed record V1VisionTextMatchResult
{
    /// <summary>The text read from the item.</summary>
    [JsonPropertyName("foundText")]
    public string? FoundText { get; init; }

    /// <summary>The configured string the text is matched against.</summary>
    [JsonPropertyName("matchString")]
    public string? MatchString { get; init; }
}

// ---------------------------------------------------------------------------
// Legacy v1 Machines API — POST /v1/machines/list, POST /v1/machines/history.
// ---------------------------------------------------------------------------

/// <summary>
/// Response body of <c>POST /v1/machines/list</c>. Mirrors the spec's
/// <c>inline_response_200_8</c> schema, whose only member is <c>machines</c>.
/// </summary>
public sealed record V1MachineListResponse
{
    /// <summary>The organization's industrial machines.</summary>
    [JsonPropertyName("machines")]
    public IReadOnlyList<V1Machine>? Machines { get; init; }
}

/// <summary>
/// An industrial machine on the legacy v1 API. Mirrors the spec's
/// <c>V1Machine</c> schema.
/// </summary>
public sealed record V1Machine
{
    /// <summary>ID of the machine. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Name of the machine.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Notes about the machine.</summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; init; }
}

/// <summary>
/// Request body of <c>POST /v1/machines/history</c>. Mirrors the inline request
/// schema of that operation, which requires both bounds.
/// </summary>
public sealed record V1MachineHistoryRequest
{
    /// <summary>Beginning of the time range, in Unix milliseconds. Spec marks REQUIRED.</summary>
    [JsonPropertyName("startMs")]
    public required long StartMs { get; init; }

    /// <summary>End of the time range, in Unix milliseconds. Spec marks REQUIRED.</summary>
    [JsonPropertyName("endMs")]
    public required long EndMs { get; init; }
}

/// <summary>
/// Response body of <c>POST /v1/machines/history</c>. Mirrors the spec's
/// <c>V1MachineHistoryResponse</c> schema, whose only member is
/// <c>machines</c>.
/// </summary>
public sealed record V1MachineHistoryResponse
{
    /// <summary>The machines and their vibration history.</summary>
    [JsonPropertyName("machines")]
    public IReadOnlyList<V1MachineHistoryEntry>? Machines { get; init; }
}

/// <summary>
/// One machine's vibration history. Mirrors the spec's
/// <c>V1MachineHistoryResponse_machines</c> schema.
/// </summary>
/// <remarks>
/// Supersedes the orphaned <see cref="MachineHistoryEntry"/> record, which was
/// never wired to a client method and spelled its identifier
/// <c>machineId</c> — a name the spec does not define.
/// </remarks>
public sealed record V1MachineHistoryEntry
{
    /// <summary>Machine ID.</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>Machine name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Vibration datapoints, each with a timestamp and an x/y/z measurement in
    /// mm/s.
    /// </summary>
    [JsonPropertyName("vibrations")]
    public IReadOnlyList<V1MachineVibrationSample>? Vibrations { get; init; }
}

/// <summary>
/// A single machine vibration datapoint. Mirrors the spec's
/// <c>V1MachineHistoryResponse_vibrations</c> schema.
/// </summary>
/// <remarks>
/// The spec spells the axis members with capital letters (<c>X</c>, <c>Y</c>,
/// <c>Z</c>), unlike the orphaned <see cref="MachineVibration"/> record this
/// supersedes.
/// </remarks>
public sealed record V1MachineVibrationSample
{
    /// <summary>Vibration on the x axis, in mm/s.</summary>
    [JsonPropertyName("X")]
    public double? X { get; init; }

    /// <summary>Vibration on the y axis, in mm/s.</summary>
    [JsonPropertyName("Y")]
    public double? Y { get; init; }

    /// <summary>Vibration on the z axis, in mm/s.</summary>
    [JsonPropertyName("Z")]
    public double? Z { get; init; }

    /// <summary>Timestamp in Unix milliseconds since epoch.</summary>
    [JsonPropertyName("time")]
    public long? Time { get; init; }
}
