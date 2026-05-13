namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Safety;

internal sealed class SafetyClient : SamsaraServiceClientBase, ISafetyClient
{
    public SafetyClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<SafetyEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<SafetyEvent>(QueryBuilder.WithTimeRange("safety-events", startTime, endTime), cancellationToken: cancellationToken);

    public Task<SafetyEvent> GetEventAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<SafetyEvent>($"safety-events/{Uri.EscapeDataString(id)}", cancellationToken);

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
}
