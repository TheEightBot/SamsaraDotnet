namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>Client for managing Samsara assets.</summary>
public interface IAssetsClient
{
    /// <summary>
    /// Lists assets (<c>GET /assets</c>), optionally filtered by type, tag, parent tag,
    /// ids, external ids, attributes, and modification time.
    /// </summary>
    /// <param name="type">Optional asset type filter (e.g. <c>trailer</c>, <c>unpowered</c>).</param>
    /// <param name="updatedAfterTime">Optional lower-bound RFC 3339 timestamp; only assets updated after this time are returned.</param>
    /// <param name="includeExternalIds">When <c>true</c>, includes <c>externalIds</c> in each asset.</param>
    /// <param name="includeTags">When <c>true</c>, includes <c>tags</c> in each asset.</param>
    /// <param name="includeAttributes">When <c>true</c>, includes <c>attributes</c> in each asset.</param>
    /// <param name="tagIds">Optional comma-separated list of tag IDs to filter by.</param>
    /// <param name="parentTagIds">Optional comma-separated list of parent tag IDs to filter by.</param>
    /// <param name="ids">Optional list of asset IDs to filter by.</param>
    /// <param name="externalIds">Optional list of external IDs (<c>namespace:value</c>) to filter by.</param>
    /// <param name="attributeValueIds">Optional comma-separated list of attribute-value IDs to filter by.</param>
    /// <param name="attributes">Optional list of attribute range filters (see spec example).</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<Asset> ListAsync(
        string? type = null,
        string? updatedAfterTime = null,
        bool? includeExternalIds = null,
        bool? includeTags = null,
        bool? includeAttributes = null,
        string? tagIds = null,
        string? parentTagIds = null,
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? externalIds = null,
        string? attributeValueIds = null,
        IReadOnlyList<string>? attributes = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a new asset (<c>POST /assets</c>).</summary>
    Task<Asset> CreateAsync(CreateAssetRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update an existing asset (<c>PATCH /assets?id=...</c>).</summary>
    /// <param name="id">Required asset id (passed as a query parameter per the spec).</param>
    /// <param name="request">Update body.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<Asset> UpdateAsync(string id, UpdateAssetRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete an existing asset (<c>DELETE /assets?id=...</c>).</summary>
    /// <param name="id">Required asset id (passed as a query parameter per the spec).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    IAsyncEnumerable<AssetLocationAndSpeed> GetLocationAndSpeedStreamAsync(CancellationToken cancellationToken = default);

    /// <summary>Legacy v1 list of all assets.</summary>
    Task<object> V1GetAllAssetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// All assets' current locations (v1, <c>GET /v1/fleet/assets/locations</c>).
    /// </summary>
    Task<object> V1GetAllAssetCurrentLocationsAsync(
        string? startingAfter = null,
        string? endingBefore = null,
        double? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// All assets' reefer states (v1, <c>GET /v1/fleet/assets/reefers</c>). The
    /// time window is required by the spec and expressed in milliseconds since
    /// epoch.
    /// </summary>
    /// <param name="startMs">Required lower bound (Unix epoch ms).</param>
    /// <param name="endMs">Required upper bound (Unix epoch ms).</param>
    /// <param name="startingAfter">Optional cursor for pagination.</param>
    /// <param name="endingBefore">Optional reverse cursor for pagination.</param>
    /// <param name="limit">Optional page-size hint.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<object> V1GetAssetsReefersAsync(
        long startMs,
        long endMs,
        string? startingAfter = null,
        string? endingBefore = null,
        double? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Per-asset location history (v1, <c>GET /v1/fleet/assets/{asset_id}/locations</c>).
    /// </summary>
    /// <param name="assetId">Asset id (path segment).</param>
    /// <param name="startMs">Required lower bound (Unix epoch ms).</param>
    /// <param name="endMs">Required upper bound (Unix epoch ms).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<object> V1GetAssetLocationAsync(
        string assetId,
        long startMs,
        long endMs,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Per-asset reefer state (v1, <c>GET /v1/fleet/assets/{asset_id}/reefer</c>).
    /// </summary>
    /// <param name="assetId">Asset id (path segment).</param>
    /// <param name="startMs">Required lower bound (Unix epoch ms).</param>
    /// <param name="endMs">Required upper bound (Unix epoch ms).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<object> V1GetAssetReeferAsync(
        string assetId,
        long startMs,
        long endMs,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<object> GetDepreciationTransactionsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> GetInputsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> ListDeviceRecoveryMissingAsync(CancellationToken cancellationToken = default);
    Task<object> MarkAssetMissingAsync(string id, object request, CancellationToken cancellationToken = default);
    Task<object> RecoverAssetAsync(string id, object request, CancellationToken cancellationToken = default);
}
