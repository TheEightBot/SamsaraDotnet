namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Issues;

/// <summary>
/// Client for managing Samsara issues.
/// </summary>
public interface IIssuesClient
{
    IAsyncEnumerable<Issue> ListAsync(CancellationToken cancellationToken = default);
    Task<Issue> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Issue> UpdateAsync(string id, UpdateIssueRequest request, CancellationToken cancellationToken = default);
}
