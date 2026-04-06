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

    public Task<Vehicle> UpdateAsync(string id, UpdateVehicleRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Vehicle>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public IAsyncEnumerable<VehicleLocation> ListLocationsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleLocation>($"{BasePath}/locations", cancellationToken: cancellationToken);

    public IAsyncEnumerable<VehicleStats> ListStatsAsync(string types, CancellationToken cancellationToken = default)
        => PaginateAsync<VehicleStats>($"{BasePath}/stats?types={Uri.EscapeDataString(types)}", cancellationToken: cancellationToken);
}
