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
}
