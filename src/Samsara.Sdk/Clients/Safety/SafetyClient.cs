namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Safety;

internal sealed class SafetyClient : SamsaraServiceClientBase, ISafetyClient
{
    public SafetyClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<SafetyEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<SafetyEvent>(QueryBuilder.WithTimeRange("safety-events", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleSafetyScore>(QueryBuilder.WithTimeRange("safety-scores/vehicles", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DriverSafetyScore>(QueryBuilder.WithTimeRange("safety-scores/drivers", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<TagSafetyScore> ListTagSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TagSafetyScore>(QueryBuilder.WithTimeRange("safety-scores/tags", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<TagGroupSafetyScore> ListTagGroupSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TagGroupSafetyScore>(QueryBuilder.WithTimeRange("safety-scores/tag-group", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<SafetyEvent> GetEventsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<SafetyEvent>(QueryBuilder.WithTimeRange("safety-events/stream", startTime, endTime), cancellationToken: cancellationToken);

    /// <summary>Driver safety score (v1 legacy, <c>GET /v1/fleet/drivers/{driverId}/safety/score</c>).</summary>
    public Task<object> V1GetDriverSafetyScoreAsync(string driverId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/fleet/drivers/{Uri.EscapeDataString(driverId)}/safety/score", cancellationToken);

    /// <summary>Vehicle safety score (v1 legacy, <c>GET /v1/fleet/vehicles/{vehicleId}/safety/score</c>).</summary>
    public Task<object> V1GetVehicleSafetyScoreAsync(string vehicleId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/fleet/vehicles/{Uri.EscapeDataString(vehicleId)}/safety/score", cancellationToken);

    /// <summary>Batch update safety events (beta, <c>PATCH /safety-events/batch</c>).</summary>
    public Task<object> PatchEventsBatchAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>("safety-events/batch", request, cancellationToken);
}
