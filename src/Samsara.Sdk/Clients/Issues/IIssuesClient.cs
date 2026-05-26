namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Issues;

/// <summary>
/// Client for managing Samsara issues.
/// </summary>
public interface IIssuesClient
{
    /// <summary>List issues filtered by ids (the spec requires the ids query parameter).</summary>
    IAsyncEnumerable<Issue> ListAsync(IReadOnlyList<string> ids, string? include = null, CancellationToken cancellationToken = default);

    /// <summary>Convenience: list a single issue by id, returning the first match (or null).</summary>
    Task<Issue?> GetAsync(string id, CancellationToken cancellationToken = default);

    Task<Issue> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update an issue. The id is sent in the request body, not in the URL path.</summary>
    Task<Issue> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken = default);

    IAsyncEnumerable<Issue> GetStreamAsync(DateTimeOffset startTime, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
