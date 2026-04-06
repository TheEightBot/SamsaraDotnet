namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Issues;

internal sealed class IssuesClient : SamsaraServiceClientBase, IIssuesClient
{
    private const string BasePath = "issues";

    public IssuesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Issue> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Issue>(BasePath, cancellationToken: cancellationToken);

    public Task<Issue> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Issue>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Issue> UpdateAsync(string id, UpdateIssueRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Issue>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);
}
