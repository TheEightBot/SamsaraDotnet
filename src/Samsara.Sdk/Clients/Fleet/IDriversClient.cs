namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Drivers;

/// <summary>
/// Client for managing Samsara drivers.
/// </summary>
public interface IDriversClient
{
    IAsyncEnumerable<Driver> ListAsync(CancellationToken cancellationToken = default);
    Task<Driver> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Driver> CreateAsync(CreateDriverRequest request, CancellationToken cancellationToken = default);
    Task<Driver> UpdateAsync(string id, UpdateDriverRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
