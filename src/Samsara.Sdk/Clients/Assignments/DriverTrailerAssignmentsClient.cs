namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Assignments;

internal sealed class DriverTrailerAssignmentsClient : SamsaraServiceClientBase, IDriverTrailerAssignmentsClient
{
    private const string BasePath = "driver-trailer-assignments";

    public DriverTrailerAssignmentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverTrailerAssignment> ListAsync(
        IReadOnlyList<string> driverIds,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
    {
        if (driverIds is null) throw new ArgumentNullException(nameof(driverIds));

        return PaginateAsync<DriverTrailerAssignment>(
            QueryBuilder.WithParams(
                BasePath,
                ("driverIds", string.Join(",", driverIds)),
                ("includeExternalIds", includeExternalIds?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant())),
            cancellationToken: cancellationToken);
    }

    public Task<DriverTrailerAssignment> CreateAsync(CreateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<DriverTrailerAssignment>(BasePath, request, cancellationToken);

    public Task<DriverTrailerAssignment> UpdateAsync(string id, UpdateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DriverTrailerAssignment>(
            QueryBuilder.WithParams(BasePath, ("id", id)),
            request,
            cancellationToken);
}
