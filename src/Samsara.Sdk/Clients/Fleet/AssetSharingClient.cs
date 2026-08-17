namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class AssetSharingClient : SamsaraServiceClientBase, IAssetSharingClient
{
    private const string BasePath = "fleet/asset-sharing/agreements";
    private const string AssetsPath = "fleet/asset-sharing/agreements/assets";
    private const string AssetsBatchPath = "fleet/asset-sharing/agreements/assets/batch";

    public AssetSharingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>List Data Sharing Agreements (<c>GET /fleet/asset-sharing/agreements</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<AssetSharingAgreement> ListAgreementsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? statusIn = null,
        IReadOnlyList<string>? roleIn = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<AssetSharingAgreement>(
            QueryBuilder.WithParams(BasePath,
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("statusIn", statusIn is null ? null : string.Join(",", statusIn)),
                ("roleIn", roleIn is null ? null : string.Join(",", roleIn))),
            cancellationToken: cancellationToken);

    /// <summary>Create a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<AssetSharingAgreement> CreateAgreementAsync(
        CreateAssetSharingAgreementRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AssetSharingAgreement>(BasePath, request, cancellationToken);

    /// <summary>Delete a Data Sharing Agreement (<c>DELETE /fleet/asset-sharing/agreements</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task DeleteAgreementAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);

    /// <summary>Accept a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements/accept</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<AssetSharingAgreement> AcceptAgreementAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AssetSharingAgreement>(
            QueryBuilder.WithParams($"{BasePath}/accept", ("id", id)), new { }, cancellationToken);

    /// <summary>Reject a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements/reject</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<AssetSharingAgreement> RejectAgreementAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AssetSharingAgreement>(
            QueryBuilder.WithParams($"{BasePath}/reject", ("id", id)), new { }, cancellationToken);

    /// <summary>Cancel a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements/cancel</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<AssetSharingAgreement> CancelAgreementAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<AssetSharingAgreement>(
            QueryBuilder.WithParams($"{BasePath}/cancel", ("id", id)), new { }, cancellationToken);

    /// <summary>List shared assets for an agreement (<c>GET /fleet/asset-sharing/agreements/assets</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<SharedAsset> ListSharedAssetsAsync(string dsaId, CancellationToken cancellationToken = default)
        => PaginateAsync<SharedAsset>(
            QueryBuilder.WithParams(AssetsPath, ("dsaId", dsaId)),
            cancellationToken: cancellationToken);

    /// <summary>Batch-create shared assets (<c>POST /fleet/asset-sharing/agreements/assets/batch</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<IReadOnlyList<SharedAsset>> CreateSharedAssetsBatchAsync(
        string dsaId,
        CreateSharedAssetsBatchRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostListDataAsync<SharedAsset>(
            QueryBuilder.WithParams(AssetsBatchPath, ("dsaId", dsaId)), request, cancellationToken);

    /// <summary>Batch-update shared assets (<c>PATCH /fleet/asset-sharing/agreements/assets/batch</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<IReadOnlyList<SharedAsset>> UpdateSharedAssetsBatchAsync(
        UpdateSharedAssetsBatchRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IReadOnlyList<SharedAsset>>(AssetsBatchPath, request, cancellationToken);
}
