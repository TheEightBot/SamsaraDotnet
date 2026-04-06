namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class TrailerAssignmentsClient : SamsaraServiceClientBase, ITrailerAssignmentsClient
{
    private const string BasePath = "fleet/trailer-assignments";

    public TrailerAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TrailerAssignment> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<TrailerAssignment>(BasePath, cancellationToken: cancellationToken);
}
