namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Addresses;

/// <summary>
/// Client for managing Samsara addresses and geofences.
/// </summary>
public interface IAddressesClient
{
    IAsyncEnumerable<Address> ListAsync(CancellationToken cancellationToken = default);
    Task<Address> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Address> CreateAsync(CreateAddressRequest request, CancellationToken cancellationToken = default);
    Task<Address> UpdateAsync(string id, UpdateAddressRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
