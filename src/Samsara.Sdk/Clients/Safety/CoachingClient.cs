namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Safety;

internal sealed class CoachingClient : SamsaraServiceClientBase, ICoachingClient
{
    public CoachingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverCoachAssignment> ListAssignmentsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DriverCoachAssignment>("coaching/driver-coach-assignments", cancellationToken: cancellationToken);

    public Task<DriverCoachAssignment> SetAssignmentAsync(SetDriverCoachAssignmentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<DriverCoachAssignment>("coaching/driver-coach-assignments", request, cancellationToken);

    public IAsyncEnumerable<CoachingSession> GetSessionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<CoachingSession>(QueryBuilder.WithTimeRange("coaching/sessions/stream", startTime, endTime), cancellationToken: cancellationToken);
}
