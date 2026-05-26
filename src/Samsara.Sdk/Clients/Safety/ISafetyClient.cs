namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Safety;

/// <summary>
/// Client for Samsara safety events and scores.
/// </summary>
public interface ISafetyClient
{
    IAsyncEnumerable<SafetyEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TagSafetyScore> ListTagSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TagGroupSafetyScore> ListTagGroupSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<SafetyEvent> GetEventsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    /// <summary>Driver safety score (v1 legacy).</summary>
    Task<object> V1GetDriverSafetyScoreAsync(string driverId, CancellationToken cancellationToken = default);
    /// <summary>Vehicle safety score (v1 legacy).</summary>
    Task<object> V1GetVehicleSafetyScoreAsync(string vehicleId, CancellationToken cancellationToken = default);
    /// <summary>Batch update safety events (beta).</summary>
    Task<object> PatchEventsBatchAsync(object request, CancellationToken cancellationToken = default);
}
