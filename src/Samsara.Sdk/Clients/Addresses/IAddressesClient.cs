namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Addresses;

/// <summary>
/// Client for managing Samsara addresses and geofences.
/// </summary>
public interface IAddressesClient
{
    /// <summary>
    /// Lists addresses (<c>GET /addresses</c>), optionally filtered by tag, parent tag,
    /// and creation time.
    /// </summary>
    /// <param name="parentTagIds">Optional list of parent tag IDs to filter by.</param>
    /// <param name="tagIds">Optional list of tag IDs to filter by.</param>
    /// <param name="createdAfterTime">Optional lower-bound RFC 3339 timestamp; only addresses created after this time are returned.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<Address> ListAsync(
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        string? createdAfterTime = null,
        CancellationToken cancellationToken = default);

    Task<Address> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Address> CreateAsync(CreateAddressRequest request, CancellationToken cancellationToken = default);
    Task<Address> UpdateAsync(string id, UpdateAddressRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
