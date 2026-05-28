namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class LiveSharingLinksClient : SamsaraServiceClientBase, ILiveSharingLinksClient
{
    private const string BasePath = "live-shares";

    public LiveSharingLinksClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<LiveSharingLink> ListAsync(
        IReadOnlyList<string>? ids = null,
        string? type = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<LiveSharingLink>(
            QueryBuilder.WithParams(BasePath,
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("type", type)),
            cancellationToken: cancellationToken);

    public Task<LiveSharingLink> CreateAsync(CreateLiveSharingLinkRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<LiveSharingLink>(BasePath, request, cancellationToken);

    public Task<LiveSharingLink> UpdateAsync(string id, UpdateLiveSharingLinkRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<LiveSharingLink>(QueryBuilder.WithParams(BasePath, ("id", id)), request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);
}
