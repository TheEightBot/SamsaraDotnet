namespace Samsara.Sdk.Models.Maintenance;

using System.Text.Json.Serialization;

/// <summary>
/// A technician's clock-in/clock-out shift (beta). Mirrors the spec's
/// <c>TechnicianShiftObjectResponseBody</c>, returned by <c>GET</c>, <c>POST</c> and
/// <c>PATCH /maintenance/technician-shifts</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>id</c>, <c>clockInAtTime</c>, <c>clockInSource</c>, <c>createdAtTime</c>,
/// <c>updatedAtTime</c>, <c>status</c> and <c>version</c> REQUIRED; they stay nullable because
/// this is a response record.
/// </remarks>
public sealed record TechnicianShift
{
    /// <summary>Stable server-generated shift UUID.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Derived shift status: <c>inProgress</c>, <c>completed</c> or <c>unknown</c>.</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Samsara user captured when the shift was created.</summary>
    [JsonPropertyName("userId")]
    public string? UserId { get; init; }

    /// <summary>Linked driver captured when the shift was created.</summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    /// <summary>Maintenance-shop place ID.</summary>
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    /// <summary>Shift start.</summary>
    [JsonPropertyName("clockInAtTime")]
    public DateTimeOffset? ClockInAtTime { get; init; }

    /// <summary>Surface that recorded clock-in: <c>api</c>, <c>cloud</c>, <c>mobile</c> or <c>unknown</c>.</summary>
    [JsonPropertyName("clockInSource")]
    public string? ClockInSource { get; init; }

    /// <summary>Shift end; omitted while the shift is open.</summary>
    [JsonPropertyName("clockOutAtTime")]
    public DateTimeOffset? ClockOutAtTime { get; init; }

    /// <summary>Surface that recorded clock-out; omitted while the shift is open.</summary>
    [JsonPropertyName("clockOutSource")]
    public string? ClockOutSource { get; init; }

    /// <summary>External identifiers for the shift, with at most 30 pairs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }

    /// <summary>External identifiers for the technician, with at most 30 pairs.</summary>
    [JsonPropertyName("externalTechnicianIds")]
    public IReadOnlyDictionary<string, string>? ExternalTechnicianIds { get; init; }

    /// <summary>
    /// Server-managed revision. Echo it back in <see cref="UpdateTechnicianShiftRequest.Version"/>.
    /// </summary>
    [JsonPropertyName("version")]
    public long? Version { get; init; }

    /// <summary>Server-recorded creation time.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Last material shift update.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Request body for <c>POST /maintenance/technician-shifts</c> (<c>createTechnicianShift</c>,
/// beta). Mirrors the spec's <c>TechnicianShiftsCreateTechnicianShiftRequestBody</c>.
/// </summary>
public sealed record CreateTechnicianShiftRequest
{
    /// <summary>Samsara user the shift belongs to. Spec REQUIRED.</summary>
    [JsonPropertyName("userId")]
    public required string UserId { get; init; }

    /// <summary>Shift start. Spec REQUIRED.</summary>
    [JsonPropertyName("clockInAtTime")]
    public required DateTimeOffset ClockInAtTime { get; init; }

    /// <summary>Shift end, strictly after the start. Omit to open the shift.</summary>
    [JsonPropertyName("clockOutAtTime")]
    public DateTimeOffset? ClockOutAtTime { get; init; }

    /// <summary>Maintenance-shop place ID.</summary>
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    /// <summary>External identifiers for the shift, with at most 30 pairs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Request body for <c>PATCH /maintenance/technician-shifts</c> (<c>patchTechnicianShift</c>,
/// beta). Mirrors the spec's <c>TechnicianShiftsPatchTechnicianShiftRequestBody</c>. The shift
/// is identified by the <c>id</c> query parameter.
/// </summary>
public sealed record UpdateTechnicianShiftRequest
{
    /// <summary>
    /// The <see cref="TechnicianShift.Version"/> from the last response. Spec REQUIRED: a stale
    /// value on a material change is rejected with <c>409 Conflict</c>.
    /// </summary>
    [JsonPropertyName("version")]
    public required long Version { get; init; }

    /// <summary>Corrected shift start.</summary>
    [JsonPropertyName("clockInAtTime")]
    public DateTimeOffset? ClockInAtTime { get; init; }

    /// <summary>Shift end, strictly after the start.</summary>
    [JsonPropertyName("clockOutAtTime")]
    public DateTimeOffset? ClockOutAtTime { get; init; }

    /// <summary>Maintenance-shop place ID.</summary>
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    /// <summary>External identifiers for the shift, with at most 30 pairs.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}
