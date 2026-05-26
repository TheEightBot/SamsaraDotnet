namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class TrailerAssignmentsClient : SamsaraServiceClientBase, ITrailerAssignmentsClient
{
    private const string BasePath = "v1/fleet/trailers/assignments";

    public TrailerAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TrailerAssignment> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<TrailerAssignment>(BasePath, cancellationToken: cancellationToken);

    /// <summary>Assignments for a specific trailer (<c>GET /v1/fleet/trailers/{id}/assignments</c>).</summary>
    public IAsyncEnumerable<TrailerAssignment> GetByTrailerAsync(string trailerId, CancellationToken cancellationToken = default)
        => PaginateAsync<TrailerAssignment>($"v1/fleet/trailers/{Uri.EscapeDataString(trailerId)}/assignments", cancellationToken: cancellationToken);
}
