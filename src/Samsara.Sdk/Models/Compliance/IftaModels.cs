namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

// ── Jurisdiction & vehicle reports ────────────────────────────────────────────

/// <summary>Per-jurisdiction IFTA totals (used in both report types).</summary>
public sealed record IftaJurisdictionSummary
{
    [JsonPropertyName("jurisdiction")] public string? Jurisdiction { get; init; }
    [JsonPropertyName("taxPaidLiters")] public double? TaxPaidLiters { get; init; }
    [JsonPropertyName("taxableMeters")] public double? TaxableMeters { get; init; }
    [JsonPropertyName("totalMeters")] public double? TotalMeters { get; init; }
}

/// <summary>Per-vehicle IFTA report row (item in <c>vehicleReports</c>).</summary>
public sealed record IftaVehicleReport
{
    [JsonPropertyName("vehicle")] public EntityReference? Vehicle { get; init; }

    [JsonPropertyName("jurisdictions")]
    public IReadOnlyList<IftaJurisdictionSummary>? Jurisdictions { get; init; }
}

/// <summary>Diagnostic counters returned alongside the IFTA reports.</summary>
public sealed record IftaReportTroubleshooting
{
    [JsonPropertyName("noPurchasesFound")] public bool? NoPurchasesFound { get; init; }
    [JsonPropertyName("unassignedFuelTypePurchases")] public int? UnassignedFuelTypePurchases { get; init; }
    [JsonPropertyName("unassignedFuelTypeVehicles")] public int? UnassignedFuelTypeVehicles { get; init; }
    [JsonPropertyName("unassignedVehiclePurchases")] public int? UnassignedVehiclePurchases { get; init; }
}

/// <summary><c>data</c> wrapper for <c>GET /fleet/reports/ifta/jurisdiction</c>.</summary>
public sealed record IftaJurisdictionReportsResponse
{
    [JsonPropertyName("year")] public int? Year { get; init; }
    [JsonPropertyName("month")] public string? Month { get; init; }
    [JsonPropertyName("quarter")] public string? Quarter { get; init; }

    [JsonPropertyName("jurisdictionReports")]
    public IReadOnlyList<IftaJurisdictionSummary>? JurisdictionReports { get; init; }

    [JsonPropertyName("troubleshooting")]
    public IftaReportTroubleshooting? Troubleshooting { get; init; }
}

/// <summary><c>data</c> wrapper for <c>GET /fleet/reports/ifta/vehicle</c>.</summary>
public sealed record IftaVehicleReportsResponse
{
    [JsonPropertyName("year")] public int? Year { get; init; }
    [JsonPropertyName("month")] public string? Month { get; init; }
    [JsonPropertyName("quarter")] public string? Quarter { get; init; }

    [JsonPropertyName("vehicleReports")]
    public IReadOnlyList<IftaVehicleReport>? VehicleReports { get; init; }

    [JsonPropertyName("troubleshooting")]
    public IftaReportTroubleshooting? Troubleshooting { get; init; }
}

// ── IFTA detail CSV job ───────────────────────────────────────────────────────

/// <summary>Output file from a completed IFTA detail CSV job.</summary>
public sealed record IftaDetailJobOutput
{
    [JsonPropertyName("createdAtTime")] public DateTimeOffset? CreatedAtTime { get; init; }
    [JsonPropertyName("downloadUrl")] public string? DownloadUrl { get; init; }
    [JsonPropertyName("downloadUrlExpirationTime")] public DateTimeOffset? DownloadUrlExpirationTime { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("recordCount")] public int? RecordCount { get; init; }
}

/// <summary>Args echoed back on a CSV job (matches <see cref="CreateIftaDetailJobRequest"/>).</summary>
public sealed record IftaDetailJobArgs
{
    [JsonPropertyName("startHour")] public string? StartHour { get; init; }
    [JsonPropertyName("endHour")] public string? EndHour { get; init; }
    [JsonPropertyName("vehicleIds")] public IReadOnlyList<string>? VehicleIds { get; init; }
}

/// <summary>An IFTA detail CSV export job (<c>POST/GET /ifta-detail/csv</c>).</summary>
public sealed record IftaDetailJob
{
    [JsonPropertyName("jobId")] public required string JobId { get; init; }
    [JsonPropertyName("jobStatus")] public string? JobStatus { get; init; }
    [JsonPropertyName("requestedAtTime")] public DateTimeOffset? RequestedAtTime { get; init; }
    [JsonPropertyName("startedAtTime")] public DateTimeOffset? StartedAtTime { get; init; }
    [JsonPropertyName("completedAtTime")] public DateTimeOffset? CompletedAtTime { get; init; }
    [JsonPropertyName("failedAtTime")] public DateTimeOffset? FailedAtTime { get; init; }
    [JsonPropertyName("details")] public string? Details { get; init; }
    [JsonPropertyName("args")] public IftaDetailJobArgs? Args { get; init; }
    [JsonPropertyName("files")] public IReadOnlyList<IftaDetailJobOutput>? Files { get; init; }
}

/// <summary>
/// Request body for <c>POST /ifta-detail/csv</c>. Hours are inclusive ISO timestamps; the
/// id filters are comma-separated strings (the spec uses strings, not arrays).
/// </summary>
public sealed record CreateIftaDetailJobRequest
{
    [JsonPropertyName("startHour")] public required string StartHour { get; init; }
    [JsonPropertyName("endHour")] public required string EndHour { get; init; }
    [JsonPropertyName("vehicleIds")] public string? VehicleIds { get; init; }
    [JsonPropertyName("vehicleTagIds")] public string? VehicleTagIds { get; init; }
    [JsonPropertyName("vehicleParentTagIds")] public string? VehicleParentTagIds { get; init; }
}
