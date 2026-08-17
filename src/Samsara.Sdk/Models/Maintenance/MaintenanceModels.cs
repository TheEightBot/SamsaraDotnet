namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>
/// A vehicle maintenance DVIR (Driver Vehicle Inspection Report) as returned by
/// the v2 DVIR endpoints. Mirrors the spec's
/// <c>DvirStreamResponseDataResponseBody</c> (<c>GET /dvirs/stream</c>) and its
/// property-identical twin <c>DvirGetDvirResponseBody</c>
/// (<c>GET /dvirs/{id}</c>).
/// </summary>
/// <remarks>
/// The v1 <c>Dvir</c> schema returned by <c>POST /fleet/dvirs</c> and
/// <c>PATCH /fleet/dvirs/{id}</c> is a genuinely different object and is modeled
/// separately by <see cref="V1MaintenanceDvir"/>. See the 2026-08-17b design note
/// in <c>docs/api-sync/30-maintenance.md</c>. All properties are nullable because
/// this is a response record.
/// </remarks>
public sealed record MaintenanceDvir
{
    /// <summary>Samsara ID of the DVIR. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Author signature for the DVIR. Spec-required for stream/get and present
    /// on POST/PATCH responses. Mirrors the spec's
    /// <c>AuthorSignatureObjectResponseBody</c>.
    /// </summary>
    [JsonPropertyName("authorSignature")]
    public MaintenanceDvirSignature? AuthorSignature { get; init; }

    /// <summary>
    /// Timestamp at which the DVIR began submission (RFC 3339 string).
    /// Spec-required for <c>GET /dvirs/stream</c> and <c>GET /dvirs/{id}</c>.
    /// </summary>
    [JsonPropertyName("dvirSubmissionBeginTime")]
    public string? DvirSubmissionBeginTime { get; init; }

    /// <summary>
    /// Timestamp at which the DVIR was submitted (RFC 3339 string).
    /// Spec-required for <c>GET /dvirs/stream</c> and <c>GET /dvirs/{id}</c>.
    /// </summary>
    [JsonPropertyName("dvirSubmissionTime")]
    public string? DvirSubmissionTime { get; init; }

    /// <summary>
    /// DVIR type (e.g. <c>preTrip</c>, <c>postTrip</c>, <c>mechanic</c>). Spec-required.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Timestamp at which the DVIR was last updated (RFC 3339 string).
    /// Spec-required for <c>GET /dvirs/stream</c> and <c>GET /dvirs/{id}</c>.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }

    /// <summary>IDs of defects associated with this DVIR.</summary>
    [JsonPropertyName("defectIds")]
    public IReadOnlyList<string>? DefectIds { get; init; }

    /// <summary>Formatted address where the DVIR was performed.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

    /// <summary>Mechanic notes attached to the DVIR.</summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>Odometer reading at the time of the DVIR, in meters.</summary>
    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }

    /// <summary>
    /// Safety status reported on the DVIR (e.g. <c>safe</c>, <c>unsafe</c>,
    /// <c>resolved</c>).
    /// </summary>
    [JsonPropertyName("safetyStatus")]
    public string? SafetyStatus { get; init; }

    /// <summary>Second signature on the DVIR. Mirrors the spec's
    /// <c>AuthorSignatureObjectResponseBody</c>.</summary>
    [JsonPropertyName("secondSignature")]
    public MaintenanceDvirSignature? SecondSignature { get; init; }

    /// <summary>Third signature on the DVIR. Mirrors the spec's
    /// <c>AuthorSignatureObjectResponseBody</c>.</summary>
    [JsonPropertyName("thirdSignature")]
    public MaintenanceDvirSignature? ThirdSignature { get; init; }

    /// <summary>
    /// Trailer associated with the DVIR. Mirrors the spec's
    /// <c>TrailerDvirObjectResponseBody</c> (<c>{externalIds, id}</c>).
    /// </summary>
    [JsonPropertyName("trailer")]
    public MaintenanceDvirAssetRef? Trailer { get; init; }

    /// <summary>
    /// Vehicle associated with the DVIR. Mirrors the spec's
    /// <c>VehicleDvirObjectResponseBody</c> (<c>{externalIds, id}</c>).
    /// </summary>
    [JsonPropertyName("vehicle")]
    public MaintenanceDvirAssetRef? Vehicle { get; init; }

    /// <summary>
    /// Walkaround photos attached to the DVIR. Each item mirrors the spec's
    /// <c>WalkaroundPhotoObjectResponseBody</c>.
    /// </summary>
    [JsonPropertyName("walkaroundPhotos")]
    public IReadOnlyList<WalkaroundPhoto>? WalkaroundPhotos { get; init; }
}

/// <summary>
/// A vehicle maintenance DVIR (Driver Vehicle Inspection Report) as returned by
/// the v1 DVIR endpoints. Mirrors the spec's <c>Dvir</c> schema, the
/// <c>data</c> payload of <c>POST /fleet/dvirs</c> and
/// <c>PATCH /fleet/dvirs/{id}</c>.
/// </summary>
/// <remarks>
/// <para>
/// Distinct from <see cref="MaintenanceDvir"/>, which mirrors the v2
/// stream/get schema. The two schemas overlap but neither contains the other:
/// this one has <c>endTime</c>, <c>startTime</c>, <c>licensePlate</c>,
/// <c>location</c>, <c>trailerName</c>, <c>trailerDefects</c> and
/// <c>vehicleDefects</c>; the v2 one has <c>defectIds</c>,
/// <c>dvirSubmissionBeginTime</c>, <c>dvirSubmissionTime</c>,
/// <c>updatedAtTime</c>, <c>formattedAddress</c> and <c>walkaroundPhotos</c>.
/// Their nested signature and asset-reference objects differ too — see
/// <see cref="V1MaintenanceDvirSignature"/>, <see cref="V1MaintenanceVehicleRef"/>
/// and <see cref="V1MaintenanceTrailerRef"/>.
/// </para>
/// <para>
/// Spec marks <c>id</c> REQUIRED; it stays nullable because this is a response
/// record.
/// </para>
/// </remarks>
public sealed record V1MaintenanceDvir
{
    /// <summary>Samsara ID of the DVIR. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Author signature for the DVIR. Mirrors the spec's
    /// <c>DvirAuthorSignature</c>.
    /// </summary>
    [JsonPropertyName("authorSignature")]
    public V1MaintenanceDvirSignature? AuthorSignature { get; init; }

    /// <summary>Time when the DVIR ended (RFC 3339).</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>License plate of the vehicle inspected.</summary>
    [JsonPropertyName("licensePlate")]
    public string? LicensePlate { get; init; }

    /// <summary>Free-form description of the location at which the DVIR was performed.</summary>
    [JsonPropertyName("location")]
    public string? Location { get; init; }

    /// <summary>Mechanic notes attached to the DVIR.</summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>Odometer reading at the time of the DVIR, in meters.</summary>
    [JsonPropertyName("odometerMeters")]
    public long? OdometerMeters { get; init; }

    /// <summary>
    /// Safety status reported on the DVIR (e.g. <c>safe</c>, <c>unsafe</c>,
    /// <c>resolved</c>).
    /// </summary>
    [JsonPropertyName("safetyStatus")]
    public string? SafetyStatus { get; init; }

    /// <summary>
    /// Second signature on the DVIR. Mirrors the spec's
    /// <c>DvirSecondSignature</c>, which is property-identical to
    /// <c>DvirAuthorSignature</c>.
    /// </summary>
    [JsonPropertyName("secondSignature")]
    public V1MaintenanceDvirSignature? SecondSignature { get; init; }

    /// <summary>Time when the DVIR started (RFC 3339).</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>
    /// Third signature on the DVIR. Mirrors the spec's
    /// <c>DvirThirdSignature</c>, which is property-identical to
    /// <c>DvirAuthorSignature</c>.
    /// </summary>
    [JsonPropertyName("thirdSignature")]
    public V1MaintenanceDvirSignature? ThirdSignature { get; init; }

    /// <summary>
    /// Trailer associated with the DVIR. Mirrors the spec's <c>DvirTrailer</c>,
    /// which resolves to <c>trailerTinyResponse</c> (<c>{id, name}</c>).
    /// </summary>
    [JsonPropertyName("trailer")]
    public V1MaintenanceTrailerRef? Trailer { get; init; }

    /// <summary>
    /// Defects registered for the trailer which was part of the DVIR. Each item
    /// mirrors the spec's <c>dvirTrailerDefectsItems</c>.
    /// </summary>
    [JsonPropertyName("trailerDefects")]
    public IReadOnlyList<V1DefectRecord>? TrailerDefects { get; init; }

    /// <summary>Display name of the trailer.</summary>
    [JsonPropertyName("trailerName")]
    public string? TrailerName { get; init; }

    /// <summary>DVIR type (e.g. <c>preTrip</c>, <c>postTrip</c>, <c>mechanic</c>).</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Vehicle associated with the DVIR. Mirrors the spec's <c>DvirVehicle</c>,
    /// which resolves to <c>vehicleTinyResponse</c>
    /// (<c>{ExternalIds, id, name}</c>).
    /// </summary>
    [JsonPropertyName("vehicle")]
    public V1MaintenanceVehicleRef? Vehicle { get; init; }

    /// <summary>
    /// Defects registered for the vehicle which was part of the DVIR. The spec
    /// points both defect arrays at the same <c>dvirTrailerDefectsItems</c>
    /// schema, so one record serves both.
    /// </summary>
    [JsonPropertyName("vehicleDefects")]
    public IReadOnlyList<V1DefectRecord>? VehicleDefects { get; init; }
}

/// <summary>
/// A DVIR defect as returned by the v1 defect endpoint. Mirrors the spec's
/// <c>Defect</c> schema — the <c>data</c> payload of
/// <c>PATCH /fleet/defects/{id}</c> — and its property-identical twin
/// <c>dvirTrailerDefectsItems</c>, the item schema shared by
/// <c>Dvir.trailerDefects</c> and <c>Dvir.vehicleDefects</c>
/// (<c>POST /fleet/dvirs</c>, <c>PATCH /fleet/dvirs/{id}</c>).
/// </summary>
/// <remarks>
/// <para>
/// <c>Defect</c> and <c>dvirTrailerDefectsItems</c> declare the identical
/// 11-property set with identical spellings, so one record mirrors both; unlike
/// the v1/v2 pairing there is no divergence to lose. If Samsara ever separates
/// them, split this record rather than widening it.
/// </para>
/// <para>
/// Distinct from <see cref="DefectRecord"/>, which mirrors the v2 defects API
/// (<c>GET /defects/stream</c>, <c>GET /defects/{id}</c>); this shape carries a
/// <c>defectType</c> name rather than a <c>defectTypeId</c>, has no
/// <c>dvirId</c>, <c>defectPhotos</c>, <c>defectSafetyStatus</c> or
/// <c>updatedAtTime</c>, and adds <c>mechanicNotesUpdatedAtTime</c>. Spec marks
/// <c>id</c> and <c>isResolved</c> REQUIRED; both stay nullable because this is a
/// response record.
/// </para>
/// </remarks>
public sealed record V1DefectRecord
{
    /// <summary>ID of the defect. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Signifies if this defect is resolved. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isResolved")]
    public bool? IsResolved { get; init; }

    /// <summary>Comment on the defect.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>Time when the defect was created (RFC 3339).</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>The type of DVIR defect (display name, e.g. <c>Air Compressor</c>).</summary>
    [JsonPropertyName("defectType")]
    public string? DefectType { get; init; }

    /// <summary>The mechanic's notes on the defect.</summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>Time when mechanic notes were last updated (RFC 3339).</summary>
    [JsonPropertyName("mechanicNotesUpdatedAtTime")]
    public string? MechanicNotesUpdatedAtTime { get; init; }

    /// <summary>
    /// Time when this defect was resolved (RFC 3339). Not returned while the
    /// defect is unresolved.
    /// </summary>
    [JsonPropertyName("resolvedAtTime")]
    public string? ResolvedAtTime { get; init; }

    /// <summary>
    /// The person who resolved this defect. Mirrors the spec's
    /// <c>Defect_resolvedBy</c>, which is byte-identical to the
    /// <c>DvirResolvedByObjectResponseBody</c> already modeled by
    /// <see cref="DefectResolvedBy"/>.
    /// </summary>
    [JsonPropertyName("resolvedBy")]
    public DefectResolvedBy? ResolvedBy { get; init; }

    /// <summary>
    /// The trailer this defect was submitted for. Resolves to
    /// <c>trailerTinyResponse</c> (<c>{id, name}</c>).
    /// </summary>
    [JsonPropertyName("trailer")]
    public V1MaintenanceTrailerRef? Trailer { get; init; }

    /// <summary>
    /// The vehicle this defect was submitted for. Resolves to
    /// <c>vehicleTinyResponse</c> (<c>{ExternalIds, id, name}</c>).
    /// </summary>
    [JsonPropertyName("vehicle")]
    public V1MaintenanceVehicleRef? Vehicle { get; init; }
}

/// <summary>
/// A walkaround photo attached to a DVIR. Mirrors the spec's
/// <c>WalkaroundPhotoObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks all three properties REQUIRED; they stay nullable because this is
/// a response record.
/// </remarks>
public sealed record WalkaroundPhoto
{
    /// <summary>Time when the walkaround photo was created (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>The name of the walkaround photo. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// The URL to the walkaround photo. Spec marks REQUIRED; the link is
    /// time-limited.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// A photo attached to a defect. Mirrors the spec's
/// <c>DefectPhotoResponseResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks both properties REQUIRED; they stay nullable because this is a
/// response record.
/// </remarks>
public sealed record DefectPhoto
{
    /// <summary>Time when the defect photo was created (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>
    /// The URL to the defect photo. Spec marks REQUIRED; the link is
    /// time-limited.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// A trailer or vehicle reference on a v2 DVIR or defect. Mirrors the spec's
/// <c>TrailerDvirObjectResponseBody</c> and its three property-identical twins
/// <c>VehicleDvirObjectResponseBody</c>, <c>DefectTrailerResponseResponseBody</c>
/// and <c>DefectVehicleResponseResponseBody</c> — all four <c>{externalIds, id}</c>.
/// </summary>
/// <remarks>
/// <para>
/// Reached from <see cref="MaintenanceDvir.Trailer"/>,
/// <see cref="MaintenanceDvir.Vehicle"/>, <see cref="DefectRecord.Trailer"/> and
/// <see cref="DefectRecord.Vehicle"/>: <c>GET /dvirs/stream</c>,
/// <c>GET /dvirs/{id}</c>, <c>GET /defects/stream</c>, <c>GET /defects/{id}</c>.
/// One record serves all four schemas because they are property-identical, not
/// because they are being unioned — the v1 shapes are genuinely different and
/// live on <see cref="V1MaintenanceTrailerRef"/> and
/// <see cref="V1MaintenanceVehicleRef"/>.
/// </para>
/// <para>
/// There is deliberately no <c>name</c> property: none of the four v2 schemas
/// defines one. Do not add it back to make the record look like its v1
/// counterparts.
/// </para>
/// </remarks>
public sealed record MaintenanceDvirAssetRef
{
    /// <summary>Samsara ID of the asset.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>A map of external IDs for the asset.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A trailer reference on a v1 DVIR or defect. Mirrors the spec's
/// <c>trailerTinyResponse</c>, reached via <c>DvirTrailer</c>
/// (<c>Dvir.trailer</c>), <c>Defect.trailer</c> and the inline
/// <c>dvirTrailerDefectsItems.trailer</c> <c>allOf</c> — all
/// <c>{id, name}</c>.
/// </summary>
/// <remarks>
/// <b>This schema has no external-IDs property at all.</b> Unlike its vehicle
/// sibling <see cref="V1MaintenanceVehicleRef"/>, <c>trailerTinyResponse</c>
/// defines only <c>id</c> and <c>name</c>. Do not add an <c>externalIds</c> map
/// here for symmetry with the vehicle record or with the v2
/// <see cref="MaintenanceDvirAssetRef"/> — the v1 API does not send one, and the
/// property would be permanently <see langword="null"/> while making the record
/// diverge from the schema it mirrors.
/// </remarks>
public sealed record V1MaintenanceTrailerRef
{
    /// <summary>Samsara ID of the trailer.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the trailer.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A vehicle reference on a v1 DVIR or defect. Mirrors the spec's
/// <c>vehicleTinyResponse</c>, reached via <c>DvirVehicle</c>
/// (<c>Dvir.vehicle</c>), <c>Defect.vehicle</c> and the inline
/// <c>dvirTrailerDefectsItems.vehicle</c> <c>allOf</c> — all
/// <c>{ExternalIds, id, name}</c>.
/// </summary>
/// <remarks>
/// <para>
/// <b>The capital-E <c>ExternalIds</c> is copied from the spec verbatim and must
/// not be "corrected".</b> <c>vehicleTinyResponse</c> is the <b>only</b> one of
/// the 123 spec schemas carrying an external-ID map that spells it with a capital
/// E. The other 122 use <c>externalIds</c> — including its own siblings
/// <c>VehicleDvirObjectResponseBody</c>, <c>GoaVehicleTinyResponseResponseBody</c>
/// and <c>VehicleWithGatewayTinyResponseResponseBody</c>, and including
/// <c>trailerTinyResponse</c>'s v2 counterpart. Within a single <c>Defect</c> the
/// <c>trailer</c> and <c>vehicle</c> siblings disagree with each other. It is
/// believed to be an upstream typo in Samsara's own spec.
/// </para>
/// <para>
/// The SDK mirrors the spec rather than the presumed intent, and this record is
/// reached only from v1 sites, so it never has to serve a schema spelling the map
/// lowercase. Flipping the <c>JsonPropertyName</c> to <c>externalIds</c> would
/// still deserialize — <c>SamsaraJsonContext</c> sets
/// <c>PropertyNameCaseInsensitive</c> — which is exactly why the regression would
/// be invisible in tests: the only visible effect is that
/// <c>check-model-sync</c> starts reporting a <c>missing-optional</c> finding for
/// <c>ExternalIds</c> on <c>POST /fleet/dvirs</c>, <c>PATCH /fleet/dvirs/{id}</c>
/// and <c>PATCH /fleet/defects/{id}</c> again. See the 2026-08-17b design note in
/// <c>docs/api-sync/30-maintenance.md</c>.
/// </para>
/// </remarks>
public sealed record V1MaintenanceVehicleRef
{
    /// <summary>Samsara ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A map of external IDs for the vehicle. The capital <c>E</c> is the spec's
    /// own spelling for <c>vehicleTinyResponse</c> — see the remarks on this
    /// record before changing it.
    /// </summary>
    [JsonPropertyName("ExternalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A signature captured on a v2 DVIR. Mirrors the spec's
/// <c>AuthorSignatureObjectResponseBody</c>, used by
/// <see cref="MaintenanceDvir.AuthorSignature"/>,
/// <see cref="MaintenanceDvir.SecondSignature"/> and
/// <see cref="MaintenanceDvir.ThirdSignature"/>.
/// </summary>
/// <remarks>
/// The v1 signature schemas nest a different signatory object — see
/// <see cref="V1MaintenanceDvirSignature"/>. Spec marks all three properties
/// REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record MaintenanceDvirSignature
{
    /// <summary>The user who signed. Spec marks REQUIRED.</summary>
    [JsonPropertyName("signatoryUser")]
    public MaintenanceSignatoryUser? SignatoryUser { get; init; }

    /// <summary>Timestamp at which the DVIR was signed (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("signedAtTime")]
    public string? SignedAtTime { get; init; }

    /// <summary>Type of signature (e.g. <c>driver</c>, <c>mechanic</c>). Spec marks REQUIRED.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// The user who signed a v2 DVIR. Mirrors the spec's
/// <c>SignatoryUserObjectResponseBody</c> — <c>{externalIds, id}</c>.
/// </summary>
/// <remarks>
/// There is deliberately no <c>name</c> property: the v2 schema does not define
/// one. The v1 signatory object does, and is modeled by
/// <see cref="V1MaintenanceSignatoryUser"/>.
/// </remarks>
public sealed record MaintenanceSignatoryUser
{
    /// <summary>Samsara ID of the signatory user. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>A map of external IDs for the user.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A signature captured on a v1 DVIR. Mirrors the spec's
/// <c>DvirAuthorSignature</c> and its property-identical twins
/// <c>DvirSecondSignature</c> and <c>DvirThirdSignature</c>.
/// </summary>
/// <remarks>
/// Differs from the v2 <see cref="MaintenanceDvirSignature"/> only in the nested
/// signatory object: v1 resolves <c>signatoryUser</c> to
/// <c>userTinyResponse</c> (<c>{id, name}</c>) while v2 resolves it to
/// <c>SignatoryUserObjectResponseBody</c> (<c>{externalIds, id}</c>). That one
/// difference is why the pair cannot share a record.
/// </remarks>
public sealed record V1MaintenanceDvirSignature
{
    /// <summary>The user who signed.</summary>
    [JsonPropertyName("signatoryUser")]
    public V1MaintenanceSignatoryUser? SignatoryUser { get; init; }

    /// <summary>Timestamp at which the DVIR was signed (RFC 3339).</summary>
    [JsonPropertyName("signedAtTime")]
    public string? SignedAtTime { get; init; }

    /// <summary>Type of signature (e.g. <c>driver</c>, <c>mechanic</c>).</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// The user who signed a v1 DVIR. Mirrors <c>userTinyResponse</c>
/// (<c>{id, name}</c>), the schema the v1 signature schemas' <c>signatoryUser</c>
/// <c>allOf</c> resolves to.
/// </summary>
/// <remarks>
/// There is deliberately no <c>externalIds</c> property: <c>userTinyResponse</c>
/// does not define one. The v2 signatory object does, and is modeled by
/// <see cref="MaintenanceSignatoryUser"/>.
/// </remarks>
public sealed record V1MaintenanceSignatoryUser
{
    /// <summary>Samsara ID of the signatory user.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the signatory user.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Diagnostic trouble code from vehicle.
/// </summary>
public sealed record DiagnosticTroubleCode
{
    [JsonPropertyName("dtcId")]
    public string? DtcId { get; init; }

    [JsonPropertyName("dtcDescription")]
    public string? DtcDescription { get; init; }

    [JsonPropertyName("dtcShortCode")]
    public string? DtcShortCode { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("checkEngineLight")]
    public CheckEngineLight? CheckEngineLight { get; init; }

    [JsonPropertyName("diagnosticType")]
    public string? DiagnosticType { get; init; }

    [JsonPropertyName("occurredAtMs")]
    public long? OccurredAtMs { get; init; }
}

/// <summary>
/// Check engine light info.
/// </summary>
public sealed record CheckEngineLight
{
    [JsonPropertyName("isOn")]
    public bool? IsOn { get; init; }

    [JsonPropertyName("emissionsIsOn")]
    public bool? EmissionsIsOn { get; init; }

    [JsonPropertyName("diagnosticIsOn")]
    public bool? DiagnosticIsOn { get; init; }

    [JsonPropertyName("protectIsOn")]
    public bool? ProtectIsOn { get; init; }
}

/// <summary>
/// A DVIR defect as returned by the v2 defects API. Mirrors the spec's
/// <c>DefectsResponseDataResponseBody</c> (<c>GET /defects/stream</c>) and its
/// property-identical twin <c>DvirDefectGetDefectResponseBody</c>
/// (<c>GET /defects/{id}</c>).
/// </summary>
/// <remarks>
/// The v1 <c>Defect</c> schema returned by <c>PATCH /fleet/defects/{id}</c> is a
/// genuinely different object and is modeled separately by
/// <see cref="V1DefectRecord"/>. See the 2026-08-17b design note in
/// <c>docs/api-sync/30-maintenance.md</c>. All properties are nullable because
/// this is a response record.
/// </remarks>
public sealed record DefectRecord
{
    /// <summary>Samsara ID of the defect. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>ID of the DVIR this defect belongs to. Spec marks REQUIRED.</summary>
    [JsonPropertyName("dvirId")]
    public string? DvirId { get; init; }

    /// <summary>Comment describing the defect. Spec marks REQUIRED.</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>Whether the defect has been resolved. Spec marks REQUIRED.</summary>
    [JsonPropertyName("isResolved")]
    public bool? IsResolved { get; init; }

    /// <summary>Timestamp at which the defect was created (RFC 3339).</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>
    /// Photos attached to the defect. Each item mirrors the spec's
    /// <c>DefectPhotoResponseResponseBody</c>.
    /// </summary>
    [JsonPropertyName("defectPhotos")]
    public IReadOnlyList<DefectPhoto>? DefectPhotos { get; init; }

    /// <summary>Safety status of the defect (<c>safe</c> or <c>unsafe</c>).</summary>
    [JsonPropertyName("defectSafetyStatus")]
    public string? DefectSafetyStatus { get; init; }

    /// <summary>ID of the defect type associated with this defect.</summary>
    [JsonPropertyName("defectTypeId")]
    public string? DefectTypeId { get; init; }

    /// <summary>Free-form mechanic notes on the defect.</summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>Timestamp at which the defect was resolved (RFC 3339).</summary>
    [JsonPropertyName("resolvedAtTime")]
    public string? ResolvedAtTime { get; init; }

    /// <summary>Details about who resolved the defect. Mirrors the spec's
    /// <c>DvirResolvedByObjectResponseBody</c>.</summary>
    [JsonPropertyName("resolvedBy")]
    public DefectResolvedBy? ResolvedBy { get; init; }

    /// <summary>
    /// Trailer the defect was reported against. Mirrors the spec's
    /// <c>DefectTrailerResponseResponseBody</c> (<c>{externalIds, id}</c>).
    /// </summary>
    [JsonPropertyName("trailer")]
    public MaintenanceDvirAssetRef? Trailer { get; init; }

    /// <summary>Timestamp at which the defect was last updated (RFC 3339).</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }

    /// <summary>
    /// Vehicle the defect was reported against. Mirrors the spec's
    /// <c>DefectVehicleResponseResponseBody</c> (<c>{externalIds, id}</c>).
    /// </summary>
    [JsonPropertyName("vehicle")]
    public MaintenanceDvirAssetRef? Vehicle { get; init; }
}

/// <summary>
/// Details about the user who resolved a defect. Mirrors the spec's
/// <c>DvirResolvedByObjectResponseBody</c> (v2) and its property-identical twin
/// <c>Defect_resolvedBy</c> (v1).
/// </summary>
/// <remarks>
/// Deliberately <b>not</b> split into a v1/v2 pair like its siblings: the two
/// schemas declare the identical <c>{id, name, type}</c> property set with
/// identical spellings, differing only in their <c>required</c> lists, which a
/// fully nullable response record does not express.
/// </remarks>
public sealed record DefectResolvedBy
{
    /// <summary>Samsara ID of the resolving user. Spec marks REQUIRED (v2).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the resolving user. Spec marks REQUIRED (v2).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Type of the resolving user (e.g. <c>driver</c>, <c>mechanic</c>). Spec marks REQUIRED (v2).</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /fleet/defects/{id}</c>. Mirrors the spec
/// <c>DefectPatch</c> schema: information about resolving a defect.
/// To resolve a defect, set <see cref="IsResolved"/> to <c>true</c> and
/// supply <see cref="ResolvedBy"/>.
/// </summary>
public sealed record UpdateDefectRequest
{
    /// <summary>Resolves the defect. Must be <c>true</c> to resolve.</summary>
    [JsonPropertyName("isResolved")]
    public bool? IsResolved { get; init; }

    /// <summary>The mechanic's notes on the defect.</summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>
    /// Time when the defect was resolved (RFC 3339, e.g. <c>2020-01-27T07:06:25Z</c>).
    /// Defaults to now if not provided.
    /// </summary>
    [JsonPropertyName("resolvedAtTime")]
    public string? ResolvedAtTime { get; init; }

    /// <summary>Details about who is resolving the defect. Mirrors the spec's
    /// <c>ResolvedBy</c> request schema.</summary>
    [JsonPropertyName("resolvedBy")]
    public UpdateDefectResolvedBy? ResolvedBy { get; init; }
}

/// <summary>
/// Information about the user resolving a defect, supplied on
/// <c>PATCH /fleet/defects/{id}</c>. Mirrors the spec's <c>ResolvedBy</c> schema.
/// </summary>
public sealed record UpdateDefectResolvedBy
{
    /// <summary>The ID of the user who is resolving the defect. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>The type of user resolving the defect. Must be <c>mechanic</c>. Spec-required.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}

/// <summary>
/// Represents a DVIR defect type. Mirrors the spec's
/// <c>DefectTypesResponseDataResponseBody</c> returned by <c>GET /defect-types</c>.
/// </summary>
public sealed record DefectType
{
    /// <summary>Samsara ID of the defect type. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Timestamp at which the defect type was created (RFC 3339). Spec-required.</summary>
    [JsonPropertyName("createdAtTime")]
    public required string CreatedAtTime { get; init; }

    /// <summary>Human-readable label for the defect type. Spec-required.</summary>
    [JsonPropertyName("label")]
    public required string Label { get; init; }

    /// <summary>
    /// Section of the DVIR this defect type belongs to (e.g. <c>vehicle</c>,
    /// <c>trailer</c>). Spec-required.
    /// </summary>
    [JsonPropertyName("sectionType")]
    public required string SectionType { get; init; }

    /// <summary>Severity associated with the defect type.</summary>
    [JsonPropertyName("severity")]
    public string? Severity { get; init; }
}

/// <summary>Request body for creating a DVIR.</summary>
public sealed record CreateDvirRequest
{
    [JsonPropertyName("authorId")] public required string AuthorId { get; init; }
    [JsonPropertyName("safetyStatus")] public required string SafetyStatus { get; init; }
    [JsonPropertyName("type")] public required string Type { get; init; }
    [JsonPropertyName("vehicleId")] public string? VehicleId { get; init; }
    [JsonPropertyName("trailerId")] public string? TrailerId { get; init; }
    [JsonPropertyName("licensePlate")] public string? LicensePlate { get; init; }
    [JsonPropertyName("location")] public string? Location { get; init; }
    [JsonPropertyName("mechanicNotes")] public string? MechanicNotes { get; init; }
    [JsonPropertyName("odometerMeters")] public long? OdometerMeters { get; init; }
    [JsonPropertyName("resolvedDefectIds")] public IReadOnlyList<string>? ResolvedDefectIds { get; init; }
}

/// <summary>Request body for updating a DVIR.</summary>
public sealed record UpdateDvirRequest
{
    [JsonPropertyName("authorId")] public required string AuthorId { get; init; }
    [JsonPropertyName("isResolved")] public required bool IsResolved { get; init; }
    [JsonPropertyName("mechanicNotes")] public string? MechanicNotes { get; init; }
    [JsonPropertyName("signedAtTime")] public DateTimeOffset? SignedAtTime { get; init; }
}
