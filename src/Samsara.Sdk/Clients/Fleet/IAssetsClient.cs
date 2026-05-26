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
    /// <summary>Legacy v1 list of all assets.</summary>
    Task<object> V1GetAllAssetsAsync(CancellationToken cancellationToken = default);
    Task<object> V1GetAllAssetCurrentLocationsAsync(CancellationToken cancellationToken = default);
    Task<object> V1GetAssetsReefersAsync(CancellationToken cancellationToken = default);
    Task<object> V1GetAssetLocationAsync(string assetId, CancellationToken cancellationToken = default);
    Task<object> V1GetAssetReeferAsync(string assetId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> GetDepreciationTransactionsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> GetInputsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> ListDeviceRecoveryMissingAsync(CancellationToken cancellationToken = default);
    Task<object> MarkAssetMissingAsync(string id, object request, CancellationToken cancellationToken = default);
    Task<object> RecoverAssetAsync(string id, object request, CancellationToken cancellationToken = default);
}
