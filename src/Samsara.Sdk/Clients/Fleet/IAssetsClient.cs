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

    /// <summary>
    /// Stream asset location-and-speed readings
    /// (<c>GET /assets/location-and-speed/stream</c>). Results are paginated;
    /// omit <paramref name="endTime"/> to poll real-time data with the
    /// returned end-cursor.
    /// </summary>
    /// <param name="startTime">Optional start time (RFC 3339). Defaults to now if not provided.</param>
    /// <param name="endTime">Optional end time (RFC 3339). Defaults to never if not provided; if omitted, pagination will not cease and a valid cursor is always returned.</param>
    /// <param name="ids">Optional list of asset IDs to filter by.</param>
    /// <param name="includeSpeed">When <c>true</c>, returns the <c>speed</c> object on each reading.</param>
    /// <param name="includeReverseGeo">When <c>true</c>, returns the <c>address</c> object; not returned for high-frequency locations.</param>
    /// <param name="includeGeofenceLookup">When <c>true</c>, returns the <c>geofence</c> object. Cannot be combined with <paramref name="includeHighFrequencyLocations"/>.</param>
    /// <param name="includeHighFrequencyLocations">When <c>true</c>, returns high-frequency location data (up to 1Hz). Cannot be combined with <paramref name="includeGeofenceLookup"/>.</param>
    /// <param name="includeExternalIds">When <c>true</c>, returns external IDs on supported entities.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    IAsyncEnumerable<AssetLocationAndSpeed> GetLocationAndSpeedStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? ids = null,
        bool? includeSpeed = null,
        bool? includeReverseGeo = null,
        bool? includeGeofenceLookup = null,
        bool? includeHighFrequencyLocations = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Legacy v1 list of all assets (<c>GET /v1/fleet/assets</c>). The v1
    /// response is not <c>{ data }</c>-enveloped; the returned record mirrors
    /// the whole body, whose only member is <c>assets</c>.
    /// </summary>
    Task<V1AssetListResponse> V1GetAllAssetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// All assets' current locations (v1, <c>GET /v1/fleet/assets/locations</c>).
    /// The returned record mirrors the whole v1 body, so the bidirectional
    /// cursor block is available on
    /// <c>V1AssetCurrentLocationsResponse.Pagination</c>.
    /// </summary>
    Task<V1AssetCurrentLocationsResponse> V1GetAllAssetCurrentLocationsAsync(
        string? startingAfter = null,
        string? endingBefore = null,
        double? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// All assets' reefer states (v1, <c>GET /v1/fleet/assets/reefers</c>). The
    /// time window is required by the spec and expressed in milliseconds since
    /// epoch.
    /// </summary>
    /// <remarks>
    /// Unlike its sibling v1 endpoints this one <em>does</em> use the
    /// <c>{ data, pagination }</c> envelope, so the SDK unwraps it and returns
    /// the item list directly. Page forward with
    /// <paramref name="startingAfter"/> using the last returned asset.
    /// </remarks>
    /// <param name="startMs">Required lower bound (Unix epoch ms).</param>
    /// <param name="endMs">Required upper bound (Unix epoch ms).</param>
    /// <param name="startingAfter">Optional cursor for pagination.</param>
    /// <param name="endingBefore">Optional reverse cursor for pagination.</param>
    /// <param name="limit">Optional page-size hint.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<IReadOnlyList<V1AssetsReefer>> V1GetAssetsReefersAsync(
        long startMs,
        long endMs,
        string? startingAfter = null,
        string? endingBefore = null,
        double? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Per-asset location history (v1, <c>GET /v1/fleet/assets/{asset_id}/locations</c>).
    /// The v1 body is a bare JSON array, so the readings are returned directly.
    /// </summary>
    /// <param name="assetId">Asset id (path segment).</param>
    /// <param name="startMs">Required lower bound (Unix epoch ms).</param>
    /// <param name="endMs">Required upper bound (Unix epoch ms).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<IReadOnlyList<V1AssetLocation>> V1GetAssetLocationAsync(
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
    Task<V1AssetReefer> V1GetAssetReeferAsync(
        string assetId,
        long startMs,
        long endMs,
        CancellationToken cancellationToken = default);

    /// <summary>Asset depreciation transactions (beta, <c>GET /assets/depreciation</c>).</summary>
    IAsyncEnumerable<AssetDepreciationTransaction> GetDepreciationTransactionsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? assetIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Asset auxiliary-input stream (beta, <c>GET /assets/inputs/stream</c>).</summary>
    IAsyncEnumerable<AssetInputReading> GetInputsStreamAsync(
        IReadOnlyList<string> ids,
        string type,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        bool? includeTags = null,
        bool? includeAttributes = null,
        CancellationToken cancellationToken = default);

    /// <summary>Assets currently marked missing in device recovery (beta).</summary>
    IAsyncEnumerable<DeviceRecoveryMissingState> ListDeviceRecoveryMissingAsync(CancellationToken cancellationToken = default);

    /// <summary>Mark an asset as missing in device-recovery (beta).</summary>
    Task<DeviceRecoveryMissingState> MarkAssetMissingAsync(string id, MarkAssetMissingRequest request, CancellationToken cancellationToken = default);

    /// <summary>Mark an asset as recovered (beta).</summary>
    Task<DeviceRecoveryRecoveredState> RecoverAssetAsync(string id, RecoverAssetRequest request, CancellationToken cancellationToken = default);
}
