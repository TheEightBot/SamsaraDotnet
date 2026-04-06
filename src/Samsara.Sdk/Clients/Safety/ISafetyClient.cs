namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Safety;

/// <summary>
/// Client for Samsara safety events and scores.
/// </summary>
public interface ISafetyClient
{
    IAsyncEnumerable<SafetyEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<SafetyEvent> GetEventAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
