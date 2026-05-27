namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

/// <summary>
/// An enrolled vehicle with its latest CARB CTC compliance status.
/// </summary>
public sealed record CarbCtcVehicle
{
    /// <summary>Samsara vehicle ID.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>Unique identifier for this CARB CTC enrollment.</summary>
    [JsonPropertyName("enrollmentId")] public required string EnrollmentId { get; init; }

    /// <summary>Vehicle Identification Number associated with the CARB CTC enrollment.</summary>
    [JsonPropertyName("enrollmentVin")] public required string EnrollmentVin { get; init; }

    /// <summary>
    /// Current CARB CTC compliance test status.
    /// Valid values: <c>notScheduled</c>, <c>scheduled</c>, <c>inProgress</c>, <c>awaitingResult</c>,
    /// <c>pass</c>, <c>fail</c>, <c>error</c>, <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("testStatus")] public required string TestStatus { get; init; }

    /// <summary>Human-readable context for the test status.</summary>
    [JsonPropertyName("testStatusDetails")] public string? TestStatusDetails { get; init; }

    /// <summary>When the most recent data collection happened, in RFC 3339 format.</summary>
    [JsonPropertyName("lastCollectionAtTime")] public DateTimeOffset? LastCollectionAtTime { get; init; }

    /// <summary>When the next data collection is scheduled, in RFC 3339 format.</summary>
    [JsonPropertyName("nextCollectionAtTime")] public DateTimeOffset? NextCollectionAtTime { get; init; }
}

/// <summary>
/// A collection history entry for a vehicle enrolled in the CARB CTC program.
/// </summary>
public sealed record CarbCtcVehicleHistory
{
    /// <summary>Samsara vehicle ID.</summary>
    [JsonPropertyName("id")] public required string Id { get; init; }

    /// <summary>Unique identifier for this CARB CTC enrollment.</summary>
    [JsonPropertyName("enrollmentId")] public required string EnrollmentId { get; init; }

    /// <summary>Vehicle Identification Number associated with the CARB CTC enrollment.</summary>
    [JsonPropertyName("enrollmentVin")] public required string EnrollmentVin { get; init; }

    /// <summary>When the collection happened, in RFC 3339 format.</summary>
    [JsonPropertyName("happenedAtTime")] public required DateTimeOffset HappenedAtTime { get; init; }

    /// <summary>
    /// The outcome of the collection test. Valid values: <c>pass</c>, <c>fail</c>, <c>error</c>, <c>unknown</c>.
    /// </summary>
    [JsonPropertyName("testResult")] public required string TestResult { get; init; }

    /// <summary>Human-readable context for the test result.</summary>
    [JsonPropertyName("testResultDetails")] public string? TestResultDetails { get; init; }
}
