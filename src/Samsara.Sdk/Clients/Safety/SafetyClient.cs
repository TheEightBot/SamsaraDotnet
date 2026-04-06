namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Safety;

internal sealed class SafetyClient : SamsaraServiceClientBase, ISafetyClient
{
    public SafetyClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<SafetyEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<SafetyEvent>(QueryBuilder.WithTimeRange("fleet/safety/events", startTime, endTime), cancellationToken: cancellationToken);

    public Task<SafetyEvent> GetEventAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<SafetyEvent>($"fleet/safety/events/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<VehicleSafetyScore> ListVehicleSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleSafetyScore>(QueryBuilder.WithTimeRange("fleet/vehicles/safety/scores", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<DriverSafetyScore> ListDriverSafetyScoresAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DriverSafetyScore>(QueryBuilder.WithTimeRange("fleet/drivers/safety/scores", startTime, endTime), cancellationToken: cancellationToken);
}
