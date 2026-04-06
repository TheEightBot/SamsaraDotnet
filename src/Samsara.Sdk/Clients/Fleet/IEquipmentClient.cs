namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for managing Samsara equipment (trailers, powered assets, unpowered assets).
/// </summary>
public interface IEquipmentClient
{
    IAsyncEnumerable<Equipment> ListAsync(CancellationToken cancellationToken = default);
    Task<Equipment> GetAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<EquipmentLocation> ListLocationsAsync(CancellationToken cancellationToken = default);
}
