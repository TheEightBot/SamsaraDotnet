namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara vehicles.
/// </summary>
public interface IVehiclesClient
{
    IAsyncEnumerable<Vehicle> ListAsync(CancellationToken cancellationToken = default);
    Task<Vehicle> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Vehicle> UpdateAsync(string id, UpdateVehicleRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> ListLocationsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStats> ListStatsAsync(string types, CancellationToken cancellationToken = default);
}
