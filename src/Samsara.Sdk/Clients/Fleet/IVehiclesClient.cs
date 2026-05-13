namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara vehicles.
/// </summary>
public interface IVehiclesClient
{
    IAsyncEnumerable<Vehicle> ListAsync(CancellationToken cancellationToken = default);
    Task<Vehicle> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Vehicle> CreateAsync(CreateVehicleRequest request, CancellationToken cancellationToken = default);
    Task<Vehicle> UpdateAsync(string id, UpdateVehicleRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> ListLocationsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStats> ListStatsAsync(string types, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> GetLocationsFeedAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleLocation> GetLocationsHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStats> GetStatsFeedAsync(string types, CancellationToken cancellationToken = default);
    IAsyncEnumerable<VehicleStats> GetStatsHistoryAsync(string types, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<SpeedingInterval> GetSpeedingIntervalsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
