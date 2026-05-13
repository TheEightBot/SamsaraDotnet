namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>Client for managing Samsara assets.</summary>
public interface IAssetsClient
{
    IAsyncEnumerable<Asset> ListAsync(CancellationToken cancellationToken = default);
    Task<Asset> CreateAsync(CreateAssetRequest request, CancellationToken cancellationToken = default);
    Task<Asset> UpdateAsync(UpdateAssetRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string[] ids, CancellationToken cancellationToken = default);
    IAsyncEnumerable<AssetLocationAndSpeed> GetLocationAndSpeedStreamAsync(CancellationToken cancellationToken = default);
}
