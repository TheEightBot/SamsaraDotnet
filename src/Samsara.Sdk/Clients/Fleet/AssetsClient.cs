namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class AssetsClient : SamsaraServiceClientBase, IAssetsClient
{
    private const string BasePath = "assets";

    public AssetsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Asset> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Asset>(BasePath, cancellationToken: cancellationToken);

    public Task<Asset> CreateAsync(CreateAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Asset>(BasePath, request, cancellationToken);

    public Task<Asset> UpdateAsync(UpdateAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Asset>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string[] ids, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}?ids={string.Join("&ids=", ids.Select(Uri.EscapeDataString))}", cancellationToken);

    public IAsyncEnumerable<AssetLocationAndSpeed> GetLocationAndSpeedStreamAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<AssetLocationAndSpeed>($"{BasePath}/location-and-speed/stream", cancellationToken: cancellationToken);

    /// <summary>Legacy v1 list of all assets (<c>GET /v1/fleet/assets</c>).</summary>
    public Task<object> V1GetAllAssetsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("v1/fleet/assets", cancellationToken);

    /// <summary>All assets' current locations (v1, <c>GET /v1/fleet/assets/locations</c>).</summary>
    public Task<object> V1GetAllAssetCurrentLocationsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("v1/fleet/assets/locations", cancellationToken);

    /// <summary>All assets' reefer states (v1, <c>GET /v1/fleet/assets/reefers</c>).</summary>
    public Task<object> V1GetAssetsReefersAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("v1/fleet/assets/reefers", cancellationToken);

    /// <summary>Per-asset location history (v1, <c>GET /v1/fleet/assets/{asset_id}/locations</c>).</summary>
    public Task<object> V1GetAssetLocationAsync(string assetId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/fleet/assets/{Uri.EscapeDataString(assetId)}/locations", cancellationToken);

    /// <summary>Per-asset reefer state (v1, <c>GET /v1/fleet/assets/{asset_id}/reefer</c>).</summary>
    public Task<object> V1GetAssetReeferAsync(string assetId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/fleet/assets/{Uri.EscapeDataString(assetId)}/reefer", cancellationToken);

    // ── Beta ─────────────────────────────────────────────────────────────────

    /// <summary>Asset depreciation transactions (beta, <c>GET /assets/depreciation</c>).</summary>
    public IAsyncEnumerable<object> GetDepreciationTransactionsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("assets/depreciation", cancellationToken: cancellationToken);

    /// <summary>Asset inputs stream (beta, <c>GET /assets/inputs/stream</c>).</summary>
    public IAsyncEnumerable<object> GetInputsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("assets/inputs/stream", startTime, endTime), cancellationToken: cancellationToken);

    /// <summary>Assets missing from device-recovery (beta).</summary>
    public IAsyncEnumerable<object> ListDeviceRecoveryMissingAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("fleet/assets/device-recovery-missing", cancellationToken: cancellationToken);

    /// <summary>Mark an asset as missing in device-recovery (beta).</summary>
    public Task<object> MarkAssetMissingAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>($"fleet/assets/device-recovery/{Uri.EscapeDataString(id)}/missing", request, cancellationToken);

    /// <summary>Mark an asset as recovered (beta).</summary>
    public Task<object> RecoverAssetAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>($"fleet/assets/device-recovery/{Uri.EscapeDataString(id)}/recovered", request, cancellationToken);
}
