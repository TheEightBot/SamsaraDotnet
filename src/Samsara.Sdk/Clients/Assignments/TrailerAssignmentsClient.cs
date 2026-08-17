namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;
using Samsara.Sdk.Pagination;

internal sealed class TrailerAssignmentsClient : SamsaraServiceClientBase, ITrailerAssignmentsClient
{
    private const string BasePath = "v1/fleet/trailers/assignments";

    /// <summary>
    /// This legacy v1 list endpoint spends its forward cursor on <c>startingAfter</c>, not
    /// the v2 <c>after</c> (spec parameters of <c>V1getAllTrailerAssignments</c>).
    /// </summary>
    private const string CursorParam = "startingAfter";

    public TrailerAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    /// <summary>Assignments for every trailer (<c>GET /v1/fleet/trailers/assignments</c>).</summary>
    /// <remarks>
    /// The v1 body puts its page items in a top-level <c>trailers</c> array beside a
    /// top-level <c>pagination</c> block (spec <c>inline_response_200_7</c>) — there is no
    /// <c>data</c> member — so this paginates through
    /// <see cref="V1TrailerAssignmentsListResponse"/> rather than the v2 <c>{ data: [...] }</c>
    /// helper, which bound a null <c>data</c> and threw a NullReferenceException on every call.
    /// </remarks>
    public IAsyncEnumerable<V1TrailerWithAssignments> ListAsync(long? startMs = null, long? endMs = null, CancellationToken cancellationToken = default)
        => PaginateAsync<V1TrailerAssignmentsListResponse, V1TrailerWithAssignments>(
            QueryBuilder.WithParams(BasePath,
                ("startMs", startMs?.ToString()),
                ("endMs", endMs?.ToString())),
            static page => page.Trailers,
            static page => page.Pagination is null
                ? null
                : new PaginationInfo
                {
                    EndCursor = page.Pagination.EndCursor,
                    HasNextPage = page.Pagination.HasNextPage ?? false,
                },
            cursorParam: CursorParam,
            cancellationToken: cancellationToken);

    /// <summary>Assignments for a specific trailer (<c>GET /v1/fleet/trailers/{trailerId}/assignments</c>).</summary>
    /// <remarks>
    /// The spec returns a single <c>V1TrailerAssignmentsResponse</c> object here — no
    /// <c>pagination</c> block and no cursor parameters — so this is a plain GET rather
    /// than a paginated stream.
    /// </remarks>
    public Task<V1TrailerWithAssignments> GetByTrailerAsync(string trailerId, long? startMs = null, long? endMs = null, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<V1TrailerWithAssignments>(
            QueryBuilder.WithParams(
                $"v1/fleet/trailers/{Uri.EscapeDataString(trailerId)}/assignments",
                ("startMs", startMs?.ToString()),
                ("endMs", endMs?.ToString())),
            cancellationToken);
}
