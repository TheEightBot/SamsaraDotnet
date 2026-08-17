namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a vehicle maintenance DVIR (Driver Vehicle Inspection Report).
/// Mirrors the spec's <c>DvirStreamResponseDataResponseBody</c> / <c>Dvir</c>
/// inner schema returned by <c>GET /dvirs/stream</c>, <c>GET /dvirs/{id}</c>,
/// <c>POST /fleet/dvirs</c>, and <c>PATCH /fleet/dvirs/{id}</c>.
/// </summary>
public sealed record MaintenanceDvir
{
    /// <summary>Samsara ID of the DVIR. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

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

    /// <summary>Time when the DVIR ended (RFC 3339).</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>Formatted address where the DVIR was performed.</summary>
    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }

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

    /// <summary>Second signature on the DVIR. Mirrors the spec's
    /// <c>AuthorSignatureObjectResponseBody</c>.</summary>
    [JsonPropertyName("secondSignature")]
    public MaintenanceDvirSignature? SecondSignature { get; init; }

    /// <summary>Time when the DVIR started (RFC 3339).</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>Third signature on the DVIR. Mirrors the spec's
    /// <c>AuthorSignatureObjectResponseBody</c>.</summary>
    [JsonPropertyName("thirdSignature")]
    public MaintenanceDvirSignature? ThirdSignature { get; init; }

    /// <summary>Trailer associated with the DVIR. Mirrors the spec's
    /// <c>TrailerDvirObjectResponseBody</c>.</summary>
    [JsonPropertyName("trailer")]
    public MaintenanceDvirAssetRef? Trailer { get; init; }

    /// <summary>
    /// Defects registered for the trailer which was part of the DVIR. Each item
    /// mirrors the spec's <c>dvirTrailerDefectsItems</c>.
    /// </summary>
    [JsonPropertyName("trailerDefects")]
    public IReadOnlyList<DvirDefect>? TrailerDefects { get; init; }

    /// <summary>Display name of the trailer (POST/PATCH responses).</summary>
    [JsonPropertyName("trailerName")]
    public string? TrailerName { get; init; }

    /// <summary>Vehicle associated with the DVIR. Mirrors the spec's
    /// <c>VehicleDvirObjectResponseBody</c>.</summary>
    [JsonPropertyName("vehicle")]
    public MaintenanceDvirAssetRef? Vehicle { get; init; }

    /// <summary>
    /// Defects registered for the vehicle which was part of the DVIR. The spec
    /// points both defect arrays at the same <c>dvirTrailerDefectsItems</c>
    /// schema, so one record serves both.
    /// </summary>
    [JsonPropertyName("vehicleDefects")]
    public IReadOnlyList<DvirDefect>? VehicleDefects { get; init; }

    /// <summary>
    /// Walkaround photos attached to the DVIR. Each item mirrors the spec's
    /// <c>WalkaroundPhotoObjectResponseBody</c>. Returned by
    /// <c>GET /dvirs/stream</c> and <c>GET /dvirs/{id}</c> only.
    /// </summary>
    [JsonPropertyName("walkaroundPhotos")]
    public IReadOnlyList<WalkaroundPhoto>? WalkaroundPhotos { get; init; }
}

/// <summary>
/// A defect registered against the vehicle or trailer inspected by a DVIR.
/// Mirrors the spec's <c>dvirTrailerDefectsItems</c>, the item schema shared by
/// both <c>Dvir.trailerDefects</c> and <c>Dvir.vehicleDefects</c>
/// (<c>POST /fleet/dvirs</c>, <c>PATCH /fleet/dvirs/{id}</c>).
/// </summary>
/// <remarks>
/// Distinct from <see cref="DefectRecord"/>, which mirrors the standalone
/// defects API (<c>GET /defects/stream</c>); this shape carries a
/// <c>defectType</c> name rather than a <c>defectTypeId</c> and has no
/// <c>dvirId</c>. Spec marks <c>id</c> and <c>isResolved</c> REQUIRED; both stay
/// nullable because this is a response record.
/// </remarks>
public sealed record DvirDefect
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
    /// The trailer this defect was submitted for. The spec's inline
    /// <c>{ id, name }</c> shape, served by the shared
    /// <see cref="EntityReference"/>.
    /// </summary>
    [JsonPropertyName("trailer")]
    public EntityReference? Trailer { get; init; }

    /// <summary>The vehicle this defect was submitted for.</summary>
    [JsonPropertyName("vehicle")]
    public DvirDefectVehicle? Vehicle { get; init; }
}

/// <summary>
/// The vehicle a DVIR defect was submitted for. Mirrors the minified vehicle
/// object inlined in the spec's <c>dvirTrailerDefectsItems.vehicle</c>
/// <c>allOf</c>.
/// </summary>
/// <remarks>
/// This is NOT <c>vehicleTinyResponse</c>: the inlined schema spells the
/// external-id map <c>ExternalIds</c> with a capital E, unlike every other
/// vehicle tiny in the spec. The <c>JsonPropertyName</c> mirrors the spec
/// verbatim; the SDK's serializer options set
/// <c>PropertyNameCaseInsensitive</c>, so the property binds whichever casing
/// the live API actually sends.
/// </remarks>
public sealed record DvirDefectVehicle
{
    /// <summary>ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the vehicle.</summary>
    [JsonPropertyName("ExternalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
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
/// A trailer or vehicle reference on a DVIR or defect. Mirrors the spec's
/// <c>TrailerDvirObjectResponseBody</c> / <c>VehicleDvirObjectResponseBody</c>
/// (and the equivalent <c>DefectTrailerResponseResponseBody</c> /
/// <c>DefectVehicleResponseResponseBody</c> on the defects endpoints).
/// </summary>
public sealed record MaintenanceDvirAssetRef
{
    /// <summary>Samsara ID of the asset.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the asset. Returned by the v1 DVIR endpoints
    /// (<c>POST /fleet/dvirs</c>, <c>PATCH /fleet/dvirs/{id}</c>), whose
    /// <c>trailer</c>/<c>vehicle</c> resolve to <c>trailerTinyResponse</c> /
    /// <c>vehicleTinyResponse</c>; the v2 stream shape omits it.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the asset.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// A signature captured on a DVIR. Mirrors the spec's
/// <c>AuthorSignatureObjectResponseBody</c>.
/// </summary>
public sealed record MaintenanceDvirSignature
{
    /// <summary>The user who signed. Spec-required.</summary>
    [JsonPropertyName("signatoryUser")]
    public MaintenanceSignatoryUser? SignatoryUser { get; init; }

    /// <summary>Timestamp at which the DVIR was signed (RFC 3339). Spec-required.</summary>
    [JsonPropertyName("signedAtTime")]
    public string? SignedAtTime { get; init; }

    /// <summary>Type of signature (e.g. <c>driver</c>, <c>mechanic</c>). Spec-required.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}

/// <summary>
/// The user who signed a DVIR. Mirrors the spec's
/// <c>SignatoryUserObjectResponseBody</c>.
/// </summary>
public sealed record MaintenanceSignatoryUser
{
    /// <summary>Samsara ID of the signatory user. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Name of the signatory user. Returned by the v1 DVIR endpoints, whose
    /// <c>DvirSignature.signatoryUser</c> resolves to <c>userTinyResponse</c>;
    /// the v2 <c>SignatoryUserObjectResponseBody</c> omits it.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>A map of external IDs for the user.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
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
/// A defect record from the defects API. Mirrors the spec's
/// <c>DefectsResponseDataResponseBody</c> / <c>DvirDefectGetDefectResponseBody</c>
/// (and <c>Defect</c> for <c>PATCH /fleet/defects/{id}</c>).
/// </summary>
public sealed record DefectRecord
{
    /// <summary>Samsara ID of the defect. Spec-required.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// ID of the DVIR this defect belongs to. Spec-required on
    /// <c>GET /defects/stream</c> and <c>GET /defects/{id}</c>.
    /// </summary>
    [JsonPropertyName("dvirId")]
    public string? DvirId { get; init; }

    /// <summary>
    /// Comment describing the defect. Spec-required on <c>GET /defects/stream</c>
    /// and <c>GET /defects/{id}</c>, but optional on the <c>PATCH /fleet/defects/{id}</c>
    /// response (<c>Defect</c>), so modeled nullable to avoid a deserialization throw.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>Whether the defect has been resolved. Spec-required (response).</summary>
    [JsonPropertyName("isResolved")]
    public required bool IsResolved { get; init; }

    /// <summary>Timestamp at which the defect was created (RFC 3339).</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>
    /// Photos attached to the defect. Each item mirrors the spec's
    /// <c>DefectPhotoResponseResponseBody</c>.
    /// </summary>
    [JsonPropertyName("defectPhotos")]
    public IReadOnlyList<DefectPhoto>? DefectPhotos { get; init; }

    /// <summary>
    /// Safety status of the defect (<c>safe</c> or <c>unsafe</c>). Returned by
    /// <c>GET /defects/stream</c> and <c>GET /defects/{id}</c>.
    /// </summary>
    [JsonPropertyName("defectSafetyStatus")]
    public string? DefectSafetyStatus { get; init; }

    /// <summary>ID of the defect type associated with this defect.</summary>
    [JsonPropertyName("defectTypeId")]
    public string? DefectTypeId { get; init; }

    /// <summary>Free-form mechanic notes on the defect.</summary>
    [JsonPropertyName("mechanicNotes")]
    public string? MechanicNotes { get; init; }

    /// <summary>
    /// Timestamp at which mechanic notes were last updated (RFC 3339).
    /// Returned on <c>PATCH /fleet/defects/{id}</c>.
    /// </summary>
    [JsonPropertyName("mechanicNotesUpdatedAtTime")]
    public string? MechanicNotesUpdatedAtTime { get; init; }

    /// <summary>Timestamp at which the defect was resolved (RFC 3339).</summary>
    [JsonPropertyName("resolvedAtTime")]
    public string? ResolvedAtTime { get; init; }

    /// <summary>Details about who resolved the defect. Mirrors the spec's
    /// <c>DvirResolvedByObjectResponseBody</c>.</summary>
    [JsonPropertyName("resolvedBy")]
    public DefectResolvedBy? ResolvedBy { get; init; }

    /// <summary>Trailer the defect was reported against. Mirrors the spec's
    /// <c>DefectTrailerResponseResponseBody</c>.</summary>
    [JsonPropertyName("trailer")]
    public MaintenanceDvirAssetRef? Trailer { get; init; }

    /// <summary>Timestamp at which the defect was last updated (RFC 3339).</summary>
    [JsonPropertyName("updatedAtTime")]
    public string? UpdatedAtTime { get; init; }

    /// <summary>Vehicle the defect was reported against. Mirrors the spec's
    /// <c>DefectVehicleResponseResponseBody</c>.</summary>
    [JsonPropertyName("vehicle")]
    public MaintenanceDvirAssetRef? Vehicle { get; init; }

    /// <summary>
    /// Type of defect (free-form string). Present on the
    /// <c>PATCH /fleet/defects/{id}</c> response (<c>Defect</c>); not returned by the
    /// stream/get endpoints, which expose <see cref="DefectTypeId"/> instead.
    /// </summary>
    [JsonPropertyName("defectType")]
    public string? DefectType { get; init; }
}

/// <summary>
/// Details about the user who resolved a defect. Mirrors the spec's
/// <c>DvirResolvedByObjectResponseBody</c>.
/// </summary>
public sealed record DefectResolvedBy
{
    /// <summary>Samsara ID of the resolving user. Spec-required.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the resolving user. Spec-required.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Type of the resolving user (e.g. <c>driver</c>, <c>mechanic</c>). Spec-required.</summary>
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
