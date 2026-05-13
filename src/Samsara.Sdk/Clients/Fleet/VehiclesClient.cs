namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class VehiclesClient : SamsaraServiceClientBase, IVehiclesClient
{
    private const string BasePath = "fleet/vehicles";

    public VehiclesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Vehicle> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Vehicle>(BasePath, cancellationToken: cancellationToken);

    public Task<Vehicle> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Vehicle>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Vehicle> CreateAsync(CreateVehicleRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Vehicle>(BasePath, request, cancellationToken);

    public Task<Vehicle> UpdateAsync(string id, UpdateVehicleRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Vehicle>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<VehicleLocation> ListLocationsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>($"{BasePath}/locations", cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> ListStatsAsync(string types, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>($"{BasePath}/stats?types={Uri.EscapeDataString(types)}", cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleLocation> GetLocationsFeedAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>($"{BasePath}/locations/feed", cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleLocation> GetLocationsHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>(QueryBuilder.WithTimeRange($"{BasePath}/locations/history", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> GetStatsFeedAsync(string types, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>($"{BasePath}/stats/feed?types={Uri.EscapeDataString(types)}", cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> GetStatsHistoryAsync(string types, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>(QueryBuilder.WithTimeRange($"{BasePath}/stats/history?types={Uri.EscapeDataString(types)}", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<SpeedingInterval> GetSpeedingIntervalsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<SpeedingInterval>(QueryBuilder.WithTimeRange("speeding-intervals/stream", startTime, endTime), cancellationToken: cancellationToken);
}
