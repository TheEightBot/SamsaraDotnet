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
    IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TagSafetyScore> ListTagSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TagGroupSafetyScore> ListTagGroupSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
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
    Task<object> V1GetDriverSafetyScoreAsync(string driverId, long startMs, long endMs, CancellationToken cancellationToken = default);
    /// <summary>Vehicle safety score (v1 legacy). <paramref name="startMs"/>/<paramref name="endMs"/> are spec-required.</summary>
    Task<object> V1GetVehicleSafetyScoreAsync(string vehicleId, long startMs, long endMs, CancellationToken cancellationToken = default);
    /// <summary>Batch update safety events (beta).</summary>
    Task<object> PatchEventsBatchAsync(object request, CancellationToken cancellationToken = default);
}
