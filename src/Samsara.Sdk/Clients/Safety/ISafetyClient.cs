namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Safety;

/// <summary>
/// Client for Samsara safety events and scores.
/// </summary>
public interface ISafetyClient
{
    /// <summary>
    /// List safety events by ID (<c>GET /safety-events</c>). <paramref name="safetyEventIds"/> is
    /// spec-required.
    /// </summary>
    IAsyncEnumerable<SafetyEvent> ListEventsAsync(
        IReadOnlyList<string> safetyEventIds,
        bool? includeAsset = null,
        bool? includeDriver = null,
        bool? includeVgOnlyEvents = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// List vehicle safety scores (<c>GET /safety-scores/vehicles</c>). Optionally filter by
    /// <paramref name="vehicleIds"/>.
    /// </summary>
    IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? vehicleIds = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// List driver safety scores (<c>GET /safety-scores/drivers</c>). Optionally filter by
    /// <paramref name="driverIds"/>.
    /// </summary>
    IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? driverIds = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// List tag safety scores (<c>GET /safety-scores/tags</c>). <paramref name="scoreType"/> is
    /// spec-required (valid values: <c>driver</c>, <c>vehicle</c>). Optionally filter by
    /// <paramref name="tagIds"/>.
    /// </summary>
    IAsyncEnumerable<TagSafetyScore> ListTagSafetyScoresAsync(string scoreType, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? tagIds = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the combined tag-group safety score (<c>GET /safety-scores/tag-group</c>).
    /// <paramref name="scoreType"/> is spec-required (valid values: <c>driver</c>, <c>vehicle</c>).
    /// Optionally filter by <paramref name="tagIds"/>.
    /// </summary>
    IAsyncEnumerable<TagGroupSafetyScore> ListTagGroupSafetyScoresAsync(string scoreType, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, IReadOnlyList<string>? tagIds = null, CancellationToken cancellationToken = default);
    /// <summary>
    /// Stream safety events over a time window (<c>GET /safety-events/stream</c>).
    /// <paramref name="startTime"/> is spec-required.
    /// </summary>
    IAsyncEnumerable<SafetyEvent> GetEventsStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        string? queryByTimeField = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? assignedCoaches = null,
        IReadOnlyList<string>? behaviorLabels = null,
        IReadOnlyList<string>? eventStates = null,
        bool? includeAsset = null,
        bool? includeDriver = null,
        bool? includeVgOnlyEvents = null,
        CancellationToken cancellationToken = default);
    /// <summary>Driver safety score (v1 legacy). <paramref name="startMs"/>/<paramref name="endMs"/> are spec-required.</summary>
    Task<V1DriverSafetyScore> V1GetDriverSafetyScoreAsync(string driverId, long startMs, long endMs, CancellationToken cancellationToken = default);
    /// <summary>Vehicle safety score (v1 legacy). <paramref name="startMs"/>/<paramref name="endMs"/> are spec-required.</summary>
    Task<V1VehicleSafetyScore> V1GetVehicleSafetyScoreAsync(string vehicleId, long startMs, long endMs, CancellationToken cancellationToken = default);
    /// <summary>Batch update safety events (beta).</summary>
    Task<SafetyEventsBatchResult> PatchEventsBatchAsync(PatchSafetyEventsBatchRequest request, CancellationToken cancellationToken = default);
}
