namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class DriverTrailerAssignmentsClient : SamsaraServiceClientBase, IDriverTrailerAssignmentsClient
{
    private const string BasePath = "driver-trailer-assignments";

    public DriverTrailerAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverTrailerAssignment> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DriverTrailerAssignment>(BasePath, cancellationToken: cancellationToken);

    public Task<DriverTrailerAssignment> CreateAsync(CreateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverTrailerAssignment>(BasePath, request, cancellationToken);

    public Task<DriverTrailerAssignment> UpdateAsync(UpdateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DriverTrailerAssignment>(BasePath, request, cancellationToken);
}
