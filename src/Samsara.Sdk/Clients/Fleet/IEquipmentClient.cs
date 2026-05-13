namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara equipment (trailers, powered assets, unpowered assets).
/// </summary>
public interface IEquipmentClient
{
    IAsyncEnumerable<Equipment> ListAsync(CancellationToken cancellationToken = default);
    Task<Equipment> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Equipment> CreateAsync(CreateEquipmentRequest request, CancellationToken cancellationToken = default);
    Task<Equipment> UpdateAsync(string id, UpdateEquipmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<EquipmentLocation> ListLocationsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<EquipmentLocation> GetLocationsFeedAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<EquipmentLocation> GetLocationsHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<EquipmentStats> GetStatsFeedAsync(string? types = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<EquipmentStats> GetStatsHistoryAsync(string? types = null, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
