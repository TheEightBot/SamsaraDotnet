namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>
/// Beta — Ground Intelligence (<c>/ground-intelligence/*</c>): road-condition
/// issues detected from dash-cam imagery, and watchpoints that schedule repeat
/// observation of a location.
/// </summary>
/// <remarks>
/// <para>
/// <c>/ground-intelligence/issues</c> is a <b>different resource</b> from
/// <c>/issues</c> (see <see cref="IIssuesClient"/>) — the two only share the
/// <c>listIssues</c> operationId — which is why these operations live on their
/// own client rather than on the issues client.
/// </para>
/// <para>
/// Every operation here is tagged <c>[beta]</c> by Samsara and is annotated
/// <c>[Experimental("SAMSARA001")]</c>; suppress that diagnostic to opt in. Both
/// update operations identify the record by <b>query string</b> (<c>?id=</c>).
/// </para>
/// </remarks>
public interface IGroundIntelligenceClient
{
    /// <summary>
    /// List Ground Intelligence issues (<c>GET /ground-intelligence/issues</c>,
    /// <c>listIssues</c>). Pagination is handled transparently.
    /// </summary>
    /// <param name="ids">Optional comma-separated list of issue IDs.</param>
    /// <param name="types">Optional comma-separated list of issue types.</param>
    /// <param name="statuses">Optional comma-separated list of statuses.</param>
    /// <param name="severities">Optional comma-separated list of severities.</param>
    /// <param name="startTime">Optional RFC 3339 lower bound.</param>
    /// <param name="endTime">Optional RFC 3339 upper bound.</param>
    /// <param name="queryByTimeField">Time field the range filters on. Defaults to <c>updatedAtTime</c>.</param>
    /// <param name="limit">Page size; default and max is 200.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<GroundIntelligenceIssue> ListIssuesAsync(
        string? ids = null,
        string? types = null,
        string? statuses = null,
        string? severities = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? queryByTimeField = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a Ground Intelligence issue (<c>PATCH /ground-intelligence/issues</c>,
    /// <c>updateGroundIntelligenceIssue</c>).
    /// </summary>
    /// <param name="id">Unique identifier for the issue. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<GroundIntelligenceIssue> UpdateIssueAsync(
        string id,
        UpdateGroundIntelligenceIssueRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a watchpoint (<c>POST /ground-intelligence/watchpoints</c>,
    /// <c>createWatchpoint</c>).
    /// </summary>
    [Experimental("SAMSARA001")]
    Task<Watchpoint> CreateWatchpointAsync(
        CreateWatchpointRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a watchpoint (<c>PATCH /ground-intelligence/watchpoints</c>,
    /// <c>updateWatchpoint</c>).
    /// </summary>
    /// <param name="id">Unique identifier for the watchpoint. Required by the spec (query param).</param>
    /// <param name="request">The fields to change.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    [Experimental("SAMSARA001")]
    Task<Watchpoint> UpdateWatchpointAsync(
        string id,
        UpdateWatchpointRequest request,
        CancellationToken cancellationToken = default);
}

internal sealed class GroundIntelligenceClient : SamsaraServiceClientBase, IGroundIntelligenceClient
{
    private const string IssuesPath = "ground-intelligence/issues";
    private const string WatchpointsPath = "ground-intelligence/watchpoints";

    public GroundIntelligenceClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>List Ground Intelligence issues (<c>GET /ground-intelligence/issues</c>).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<GroundIntelligenceIssue> ListIssuesAsync(
        string? ids = null,
        string? types = null,
        string? statuses = null,
        string? severities = null,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? queryByTimeField = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<GroundIntelligenceIssue>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(IssuesPath, startTime, endTime),
                ("ids", ids),
                ("types", types),
                ("statuses", statuses),
                ("severities", severities),
                ("queryByTimeField", queryByTimeField)),
            limit,
            cancellationToken);

    /// <summary>Update a Ground Intelligence issue (<c>PATCH /ground-intelligence/issues</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<GroundIntelligenceIssue> UpdateIssueAsync(
        string id,
        UpdateGroundIntelligenceIssueRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<GroundIntelligenceIssue>(
            QueryBuilder.WithParams(IssuesPath, ("id", id)), request, cancellationToken);

    /// <summary>Create a watchpoint (<c>POST /ground-intelligence/watchpoints</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<Watchpoint> CreateWatchpointAsync(
        CreateWatchpointRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Watchpoint>(WatchpointsPath, request, cancellationToken);

    /// <summary>Update a watchpoint (<c>PATCH /ground-intelligence/watchpoints</c>).</summary>
    [Experimental("SAMSARA001")]
    public Task<Watchpoint> UpdateWatchpointAsync(
        string id,
        UpdateWatchpointRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Watchpoint>(
            QueryBuilder.WithParams(WatchpointsPath, ("id", id)), request, cancellationToken);
}
