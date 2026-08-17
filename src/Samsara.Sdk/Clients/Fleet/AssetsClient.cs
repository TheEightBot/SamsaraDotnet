namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class AssetsClient : SamsaraServiceClientBase, IAssetsClient
{
    private const string BasePath = "assets";

    public AssetsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Asset> ListAsync(
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
        CancellationToken cancellationToken = default)
        => PaginateAsync<Asset>(
            QueryBuilder.WithParams(BasePath,
                ("type", type),
                ("updatedAfterTime", updatedAfterTime),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("includeTags", includeTags?.ToString().ToLowerInvariant()),
                ("includeAttributes", includeAttributes?.ToString().ToLowerInvariant()),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("externalIds", externalIds is null ? null : string.Join(",", externalIds)),
                ("attributeValueIds", attributeValueIds),
                ("attributes", attributes is null ? null : string.Join(",", attributes))),
            cancellationToken: cancellationToken);

    public Task<Asset> CreateAsync(CreateAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Asset>(BasePath, request, cancellationToken);

    public Task<Asset> UpdateAsync(string id, UpdateAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Asset>(QueryBuilder.WithParams(BasePath, ("id", id)), request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);

    public IAsyncEnumerable<AssetLocationAndSpeed> GetLocationAndSpeedStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? ids = null,
        bool? includeSpeed = null,
        bool? includeReverseGeo = null,
        bool? includeGeofenceLookup = null,
        bool? includeHighFrequencyLocations = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<AssetLocationAndSpeed>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange($"{BasePath}/location-and-speed/stream", startTime, endTime),
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("includeSpeed", includeSpeed?.ToString().ToLowerInvariant()),
                ("includeReverseGeo", includeReverseGeo?.ToString().ToLowerInvariant()),
                ("includeGeofenceLookup", includeGeofenceLookup?.ToString().ToLowerInvariant()),
                ("includeHighFrequencyLocations", includeHighFrequencyLocations?.ToString().ToLowerInvariant()),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>Legacy v1 list of all assets (<c>GET /v1/fleet/assets</c>).</summary>
    public Task<V1AssetListResponse> V1GetAllAssetsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<V1AssetListResponse>("v1/fleet/assets", cancellationToken);

    /// <summary>All assets' current locations (v1, <c>GET /v1/fleet/assets/locations</c>).</summary>
    public Task<V1AssetCurrentLocationsResponse> V1GetAllAssetCurrentLocationsAsync(
        string? startingAfter = null,
        string? endingBefore = null,
        double? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<V1AssetCurrentLocationsResponse>(
            QueryBuilder.WithParams("v1/fleet/assets/locations",
                ("startingAfter", startingAfter),
                ("endingBefore", endingBefore),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    /// <summary>All assets' reefer states (v1, <c>GET /v1/fleet/assets/reefers</c>).</summary>
    public Task<IReadOnlyList<V1AssetsReefer>> V1GetAssetsReefersAsync(
        long startMs,
        long endMs,
        string? startingAfter = null,
        string? endingBefore = null,
        double? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<V1AssetsReefer>>(
            QueryBuilder.WithParams("v1/fleet/assets/reefers",
                ("startMs", startMs.ToString(CultureInfo.InvariantCulture)),
                ("endMs", endMs.ToString(CultureInfo.InvariantCulture)),
                ("startingAfter", startingAfter),
                ("endingBefore", endingBefore),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    /// <summary>Per-asset location history (v1, <c>GET /v1/fleet/assets/{asset_id}/locations</c>).</summary>
    public Task<IReadOnlyList<V1AssetLocation>> V1GetAssetLocationAsync(
        string assetId,
        long startMs,
        long endMs,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<IReadOnlyList<V1AssetLocation>>(
            QueryBuilder.WithParams($"v1/fleet/assets/{Uri.EscapeDataString(assetId)}/locations",
                ("startMs", startMs.ToString(CultureInfo.InvariantCulture)),
                ("endMs", endMs.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    /// <summary>Per-asset reefer state (v1, <c>GET /v1/fleet/assets/{asset_id}/reefer</c>).</summary>
    public Task<V1AssetReefer> V1GetAssetReeferAsync(
        string assetId,
        long startMs,
        long endMs,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<V1AssetReefer>(
            QueryBuilder.WithParams($"v1/fleet/assets/{Uri.EscapeDataString(assetId)}/reefer",
                ("startMs", startMs.ToString(CultureInfo.InvariantCulture)),
                ("endMs", endMs.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    // ── Beta ─────────────────────────────────────────────────────────────────

    /// <summary>Asset depreciation transactions (beta, <c>GET /assets/depreciation</c>).</summary>
    public IAsyncEnumerable<AssetDepreciationTransaction> GetDepreciationTransactionsAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? assetIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<AssetDepreciationTransaction>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("assets/depreciation", startTime, endTime),
                ("assetIds", assetIds is null ? null : string.Join(",", assetIds))),
            cancellationToken: cancellationToken);

    /// <summary>
    /// Asset inputs stream (beta, <c>GET /assets/inputs/stream</c>). Both
    /// <paramref name="ids"/> and <paramref name="type"/> are required by the spec.
    /// </summary>
    public IAsyncEnumerable<AssetInputReading> GetInputsStreamAsync(
        IReadOnlyList<string> ids,
        string type,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        bool? includeExternalIds = null,
        bool? includeTags = null,
        bool? includeAttributes = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<AssetInputReading>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("assets/inputs/stream", startTime, endTime),
                ("ids", string.Join(",", ids)),
                ("type", type),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("includeTags", includeTags?.ToString().ToLowerInvariant()),
                ("includeAttributes", includeAttributes?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    /// <summary>Assets missing from device-recovery (beta).</summary>
    public IAsyncEnumerable<DeviceRecoveryMissingState> ListDeviceRecoveryMissingAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DeviceRecoveryMissingState>("fleet/assets/device-recovery-missing", cancellationToken: cancellationToken);

    /// <summary>Mark an asset as missing in device-recovery (beta).</summary>
    public Task<DeviceRecoveryMissingState> MarkAssetMissingAsync(string id, MarkAssetMissingRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DeviceRecoveryMissingState>($"fleet/assets/device-recovery/{Uri.EscapeDataString(id)}/missing", request, cancellationToken);

    /// <summary>Mark an asset as recovered (beta).</summary>
    public Task<DeviceRecoveryRecoveredState> RecoverAssetAsync(string id, RecoverAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DeviceRecoveryRecoveredState>($"fleet/assets/device-recovery/{Uri.EscapeDataString(id)}/recovered", request, cancellationToken);
}
