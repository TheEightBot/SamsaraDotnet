namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class LiveSharingLinksClient : SamsaraServiceClientBase, ILiveSharingLinksClient
{
    private const string BasePath = "live-shares";

    public LiveSharingLinksClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<LiveSharingLink> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<LiveSharingLink>(BasePath, cancellationToken: cancellationToken);

    public Task<LiveSharingLink> CreateAsync(CreateLiveSharingLinkRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<LiveSharingLink>(BasePath, request, cancellationToken);

    public Task<LiveSharingLink> UpdateAsync(UpdateLiveSharingLinkRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<LiveSharingLink>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}?id={Uri.EscapeDataString(id)}", cancellationToken);
}
