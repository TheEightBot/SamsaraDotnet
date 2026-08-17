namespace Samsara.Sdk.Models.Beta;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// A saved report configuration — the <c>data[]</c> item of
/// <c>GET /reports/configs</c>. Mirrors the spec's
/// <c>ReportConfigObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>columns</c>, <c>datasetId</c>, <c>id</c> and <c>name</c>
/// REQUIRED; every property stays nullable because this is a response record.
/// </remarks>
public sealed record ReportConfig
{
    /// <summary>Samsara ID of the report configuration. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Display name of the report configuration. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>ID of the dataset the report reads from. Spec marks REQUIRED.</summary>
    [JsonPropertyName("datasetId")]
    public string? DatasetId { get; init; }

    /// <summary>The columns the report produces. Spec marks REQUIRED.</summary>
    [JsonPropertyName("columns")]
    public IReadOnlyList<ReportColumn>? Columns { get; init; }

    /// <summary>Filters applied when the report runs.</summary>
    [JsonPropertyName("filters")]
    public ReportFilters? Filters { get; init; }
}

/// <summary>
/// A column in a report configuration or report result. Mirrors the spec's
/// <c>columnResponseBody</c> (<c>GET /reports/configs</c>) and its
/// property-identical twin <c>reportColumnsObjectResponseBody</c>
/// (<c>GET /reports/runs/data</c>).
/// </summary>
/// <remarks>
/// One record serves both schemas: they declare the same three properties with
/// the same spellings and the same <c>dataType</c> enumeration (the spec merely
/// lists the enum members in a different order). Spec marks <c>dataType</c> and
/// <c>name</c> REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record ReportColumn
{
    /// <summary>Column name. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Column data type: <c>string</c>, <c>integer</c>, <c>float</c>,
    /// <c>timestamp</c> or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("dataType")]
    public string? DataType { get; init; }

    /// <summary>
    /// Unit the column's values are expressed in (e.g. <c>miles</c>,
    /// <c>litersPerHour</c>, <c>seconds</c>). See the spec for the full
    /// enumeration.
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
}

/// <summary>
/// Filters on a report configuration. Mirrors the spec's
/// <c>filtersResponseBody</c>.
/// </summary>
/// <remarks>
/// The request-side twin <c>filtersRequestBody</c> is modelled separately by
/// <see cref="ReportFiltersInput"/> because its nested time range marks
/// properties required.
/// </remarks>
public sealed record ReportFilters
{
    /// <summary>The report's primary time range.</summary>
    [JsonPropertyName("primaryTimeRange")]
    public ReportPrimaryTimeRange? PrimaryTimeRange { get; init; }
}

/// <summary>
/// The primary time range on a report configuration. Mirrors the spec's
/// <c>primaryTimeRangeResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks both properties REQUIRED; they stay nullable because this is a
/// response record. The request-side twin is <see cref="ReportPrimaryTimeRangeInput"/>.
/// </remarks>
public sealed record ReportPrimaryTimeRange
{
    /// <summary>Start of the range (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    /// <summary>End of the range (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// A dataset a report can be built from — the <c>data[]</c> item of
/// <c>GET /reports/datasets</c>. Mirrors the spec's
/// <c>ReportsDatasetResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>displayName</c>, <c>fields</c>, <c>hasTimeRangeFilter</c> and
/// <c>id</c> REQUIRED; every property stays nullable because this is a response
/// record.
/// </remarks>
public sealed record ReportDataset
{
    /// <summary>Samsara ID of the dataset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Display name of the dataset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }

    /// <summary>The fields available on the dataset. Spec marks REQUIRED.</summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<ReportDatasetField>? Fields { get; init; }

    /// <summary>Whether the dataset supports a time-range filter. Spec marks REQUIRED.</summary>
    [JsonPropertyName("hasTimeRangeFilter")]
    public bool? HasTimeRangeFilter { get; init; }

    /// <summary>Maximum span, in days, that a time-range filter may cover.</summary>
    [JsonPropertyName("timeRangeLimitDays")]
    public long? TimeRangeLimitDays { get; init; }
}

/// <summary>
/// A field available on a report dataset. Mirrors the spec's
/// <c>ReportsDatasetFieldResponseBody</c>.
/// </summary>
/// <remarks>
/// Distinct from <see cref="ReportColumn"/>: a dataset field carries both a
/// machine <c>name</c> and a <c>displayName</c>. Spec marks <c>dataType</c>,
/// <c>displayName</c> and <c>name</c> REQUIRED; they stay nullable because this
/// is a response record.
/// </remarks>
public sealed record ReportDatasetField
{
    /// <summary>Machine name of the field, used in a report config column. Spec marks REQUIRED.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Human-readable name of the field. Spec marks REQUIRED.</summary>
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }

    /// <summary>
    /// Field data type: <c>string</c>, <c>integer</c>, <c>float</c>,
    /// <c>timestamp</c> or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("dataType")]
    public string? DataType { get; init; }

    /// <summary>
    /// Unit the field's values are expressed in. See the spec for the full
    /// enumeration.
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; init; }
}

/// <summary>
/// A report run. Mirrors the spec's <c>ReportRunObjectResponseBody</c> — the
/// <c>data[]</c> item of <c>GET /reports/runs</c> and the <c>data</c> payload of
/// <c>POST /reports/runs</c>.
/// </summary>
/// <remarks>
/// Both endpoints resolve to the same schema, so one record serves both. Spec
/// marks <c>createdAtTime</c>, <c>id</c>, <c>status</c> and <c>updatedAtTime</c>
/// REQUIRED; they stay nullable because this is a response record.
/// </remarks>
public sealed record ReportRun
{
    /// <summary>Samsara ID of the report run. Spec marks REQUIRED.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Run status: <c>complete</c>, <c>pending</c>, <c>failed</c>,
    /// <c>canceled</c> or <c>unknown</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Time the run was created (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Time the run was last updated (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// The tabular result of a completed report run — the <c>data</c> payload of
/// <c>GET /reports/runs/data</c>. Mirrors the spec's
/// <c>GetReportRunsDataObjectResponseBody</c>.
/// </summary>
/// <remarks>
/// Spec marks <c>columns</c>, <c>rows</c> and <c>status</c> REQUIRED; they stay
/// nullable because this is a response record.
/// </remarks>
public sealed record ReportRunData
{
    /// <summary>
    /// Run status: <c>complete</c>, <c>pending</c>, <c>failed</c>,
    /// <c>canceled</c> or <c>unknown</c>. Rows are only populated once the run is
    /// <c>complete</c>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// The columns of the result set, in the same order as the values in each
    /// row. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("columns")]
    public IReadOnlyList<ReportColumn>? Columns { get; init; }

    /// <summary>
    /// The result rows. Each row is a positional list of cell values aligned with
    /// <see cref="Columns"/>.
    /// </summary>
    /// <remarks>
    /// The cell type is deliberately <see cref="JsonElement"/>: the spec declares
    /// the row item as a bare <c>{ "type": "object" }</c> with no
    /// <c>properties</c>, <c>enum</c> or composition keywords
    /// (<c>components.schemas.GetReportRunsDataObjectResponseBody.properties.rows.items.items</c>),
    /// because a cell's runtime type is whatever the corresponding column's
    /// <see cref="ReportColumn.DataType"/> says it is. Read a cell with
    /// <c>GetString()</c>, <c>GetDouble()</c>, <c>GetInt64()</c> or
    /// <c>GetDateTimeOffset()</c> according to that column's data type. Spec marks
    /// this property REQUIRED.
    /// </remarks>
    [JsonPropertyName("rows")]
    public IReadOnlyList<IReadOnlyList<JsonElement>>? Rows { get; init; }
}

/// <summary>
/// Request body for <c>POST /reports/runs</c>. Mirrors the spec's
/// <c>ReportsCreateReportRunRequestBody</c>.
/// </summary>
public sealed record CreateReportRunRequest
{
    /// <summary>
    /// The configuration to run. Either reference an existing configuration by
    /// <see cref="CreateReportConfigInput.Id"/> or describe one inline. Spec
    /// marks REQUIRED.
    /// </summary>
    [JsonPropertyName("reportConfig")]
    public required CreateReportConfigInput ReportConfig { get; init; }
}

/// <summary>
/// The report configuration supplied to <c>POST /reports/runs</c>. Mirrors the
/// spec's <c>createReportConfigObjectRequestBody</c>.
/// </summary>
/// <remarks>
/// The request shape is not the response shape: it references columns by
/// <c>fieldName</c> rather than describing them, so it is modelled separately
/// from <see cref="ReportConfig"/>. The spec marks nothing required here.
/// </remarks>
public sealed record CreateReportConfigInput
{
    /// <summary>Samsara ID of an existing report configuration to run.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>ID of the dataset to read from, when describing a configuration inline.</summary>
    [JsonPropertyName("datasetId")]
    public string? DatasetId { get; init; }

    /// <summary>The columns to produce, when describing a configuration inline.</summary>
    [JsonPropertyName("columns")]
    public IReadOnlyList<CreateReportColumnInput>? Columns { get; init; }

    /// <summary>Filters to apply to the run.</summary>
    [JsonPropertyName("filters")]
    public ReportFiltersInput? Filters { get; init; }
}

/// <summary>
/// A requested report column. Mirrors the spec's
/// <c>createReportConfigColumnRequestBody</c>.
/// </summary>
public sealed record CreateReportColumnInput
{
    /// <summary>
    /// Machine name of the dataset field to include, as reported by
    /// <see cref="ReportDatasetField.Name"/>. Spec marks REQUIRED.
    /// </summary>
    [JsonPropertyName("fieldName")]
    public required string FieldName { get; init; }
}

/// <summary>
/// Filters supplied to <c>POST /reports/runs</c>. Mirrors the spec's
/// <c>filtersRequestBody</c>.
/// </summary>
/// <remarks>
/// Split from the response-side <see cref="ReportFilters"/> because the nested
/// time range marks its properties required on the request side.
/// </remarks>
public sealed record ReportFiltersInput
{
    /// <summary>The primary time range to run the report over.</summary>
    [JsonPropertyName("primaryTimeRange")]
    public ReportPrimaryTimeRangeInput? PrimaryTimeRange { get; init; }
}

/// <summary>
/// The primary time range supplied to <c>POST /reports/runs</c>. Mirrors the
/// spec's <c>primaryTimeRangeRequestBody</c>.
/// </summary>
public sealed record ReportPrimaryTimeRangeInput
{
    /// <summary>Start of the range (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("startTime")]
    public required DateTimeOffset StartTime { get; init; }

    /// <summary>End of the range (RFC 3339). Spec marks REQUIRED.</summary>
    [JsonPropertyName("endTime")]
    public required DateTimeOffset EndTime { get; init; }
}
