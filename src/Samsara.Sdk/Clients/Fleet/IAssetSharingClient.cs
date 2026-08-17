namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Beta — Data Sharing Agreements (<c>/fleet/asset-sharing/agreements*</c>).
/// Lets a provider organization share assets, and their telemetry data packages,
/// with a recipient organization.
/// </summary>
/// <remarks>
/// Every operation on this client is tagged <c>[beta]</c> by Samsara and is
/// annotated <c>[Experimental("SAMSARA001")]</c>; suppress that diagnostic to
/// opt in. Note that all of these operations identify the agreement by
/// <b>query string</b> (<c>?id=</c>, <c>?dsaId=</c>) rather than a path segment.
/// </remarks>
public interface IAssetSharingClient
{
    /// <summary>
    /// List Data Sharing Agreements (<c>GET /fleet/asset-sharing/agreements</c>,
    /// <c>listAssetSharingAgreements</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="ids">Optional agreement IDs to filter by.</param>
    /// <param name="statusIn">Optional statuses to filter by: <c>pending</c>, <c>accepted</c>, <c>rejected</c>, <c>canceled</c>.</param>
    /// <param name="roleIn">Optional roles to filter by: <c>provider</c>, <c>recipient</c>.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<AssetSharingAgreement> ListAgreementsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? statusIn = null,
        IReadOnlyList<string>? roleIn = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements</c>,
    /// <c>createAssetSharingAgreement</c>).
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<AssetSharingAgreement> CreateAgreementAsync(
        CreateAssetSharingAgreementRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a Data Sharing Agreement (<c>DELETE /fleet/asset-sharing/agreements</c>,
    /// <c>deleteAssetSharingAgreement</c>). Returns <c>204 No Content</c>.
    /// </summary>
    /// <param name="id">The unique identifier of the agreement. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task DeleteAgreementAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Accept a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements/accept</c>,
    /// <c>acceptAssetSharingAgreement</c>). The spec defines no request body.
    /// </summary>
    /// <param name="id">The unique identifier of the agreement. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<AssetSharingAgreement> AcceptAgreementAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reject a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements/reject</c>,
    /// <c>rejectAssetSharingAgreement</c>). The spec defines no request body.
    /// </summary>
    /// <param name="id">The unique identifier of the agreement. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<AssetSharingAgreement> RejectAgreementAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel a Data Sharing Agreement (<c>POST /fleet/asset-sharing/agreements/cancel</c>,
    /// <c>cancelAssetSharingAgreement</c>). The spec defines no request body.
    /// </summary>
    /// <param name="id">The unique identifier of the agreement. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<AssetSharingAgreement> CancelAgreementAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// List the assets shared under an agreement
    /// (<c>GET /fleet/asset-sharing/agreements/assets</c>, <c>listSharedAssets</c>).
    /// Pagination is handled transparently.
    /// </summary>
    /// <param name="dsaId">The unique identifier of the Data Sharing Agreement. Required by the spec (query param).</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<SharedAsset> ListSharedAssetsAsync(string dsaId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Batch-create shared assets
    /// (<c>POST /fleet/asset-sharing/agreements/assets/batch</c>,
    /// <c>createSharedAssetsBatch</c>).
    /// </summary>
    /// <param name="dsaId">The unique identifier of the Data Sharing Agreement. Required by the spec (query param).</param>
    /// <param name="request">The assets to share, wrapped in the spec's <c>{ data: [...] }</c> envelope.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<IReadOnlyList<SharedAsset>> CreateSharedAssetsBatchAsync(
        string dsaId,
        CreateSharedAssetsBatchRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Batch-update shared assets
    /// (<c>PATCH /fleet/asset-sharing/agreements/assets/batch</c>,
    /// <c>updateSharedAssetsBatch</c>). Unlike the create operation, this one takes
    /// no <c>dsaId</c> — each element carries the shared-asset id.
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<IReadOnlyList<SharedAsset>> UpdateSharedAssetsBatchAsync(
        UpdateSharedAssetsBatchRequest request,
        CancellationToken cancellationToken = default);
}
