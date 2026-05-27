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

    /// <summary>
    /// Stream issues created or updated within a time window (<c>GET /issues/stream</c>).
    /// </summary>
    /// <param name="startTime">Inclusive start of the time window (spec-required).</param>
    /// <param name="endTime">Optional end of the time window.</param>
    /// <param name="status">Optional status filter (e.g. <c>open</c>, <c>inProgress</c>).</param>
    /// <param name="assetIds">Optional list of asset IDs to filter by.</param>
    /// <param name="assetExternalIds">Optional list of asset external IDs to filter by.</param>
    /// <param name="include">Optional include directives.</param>
    /// <param name="assignedToRouteStopIds">Optional list of route stop IDs to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<Issue> GetStreamAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? status = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? assetExternalIds = null,
        IReadOnlyList<string>? include = null,
        IReadOnlyList<string>? assignedToRouteStopIds = null,
        CancellationToken cancellationToken = default);
}
