namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Tags;

/// <summary>
/// Client for managing Samsara tags.
/// </summary>
internal sealed class TagsClient : SamsaraServiceClientBase, ITagsClient
{
    private const string BasePath = "fleet/tags";

    public TagsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Tag> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Tag>(BasePath, cancellationToken: cancellationToken);

    public Task<Tag> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Tag>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Tag> CreateAsync(CreateTagRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Tag>(BasePath, request, cancellationToken);

    public Task<Tag> UpdateAsync(string id, UpdateTagRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Tag>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
