namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Issues;

internal sealed class IssuesClient : SamsaraServiceClientBase, IIssuesClient
{
    private const string BasePath = "issues";

    public IssuesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>
    /// List issues filtered by id(s). The spec's <c>getIssues</c> requires the
    /// <c>ids</c> query parameter — pass one or more ids.
    /// </summary>
    public IAsyncEnumerable<Issue> ListAsync(IReadOnlyList<string> ids, string? include = null, CancellationToken cancellationToken = default)
        => PaginateAsync<Issue>(
            QueryBuilder.WithParams(BasePath,
                ("ids", string.Join(",", ids)),
                ("include", include)),
            cancellationToken: cancellationToken);

    /// <summary>Convenience: list a single issue by id, returning the first match (or null).</summary>
    public async Task<Issue?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        await foreach (var issue in ListAsync(new[] { id }, cancellationToken: cancellationToken).ConfigureAwait(false))
        {
            return issue;
        }
        return null;
    }

    public Task<Issue> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Issue>(BasePath, request, cancellationToken);

    /// <summary>Update an issue. The id is sent in the request body, not in the URL path.</summary>
    public Task<Issue> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Issue>(BasePath, request, cancellationToken);

    /// <summary>
    /// Stream issues created or updated within a time window (<c>GET /issues/stream</c>).
    /// </summary>
    public IAsyncEnumerable<Issue> GetStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? status = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? assetExternalIds = null,
        IReadOnlyList<string>? include = null,
        IReadOnlyList<string>? assignedToRouteStopIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Issue>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("issues/stream", startTime, endTime),
                ("status", status is null ? null : string.Join(",", status)),
                ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
                ("assetExternalIds", assetExternalIds is null ? null : string.Join(",", assetExternalIds)),
                ("include", include is null ? null : string.Join(",", include)),
                ("assignedToRouteStopIds", assignedToRouteStopIds is null ? null : string.Join(",", assignedToRouteStopIds))),
            cancellationToken: cancellationToken);
}
