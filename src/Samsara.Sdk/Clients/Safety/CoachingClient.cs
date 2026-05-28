namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Safety;

internal sealed class CoachingClient : SamsaraServiceClientBase, ICoachingClient
{
    private const string AssignmentsPath = "coaching/driver-coach-assignments";
    private const string SessionsStreamPath = "coaching/sessions/stream";

    public CoachingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<DriverCoachAssignment> ListAssignmentsAsync(
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? coachIds = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<DriverCoachAssignment>(
            QueryBuilder.WithParams(AssignmentsPath,
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("coachIds", coachIds is null ? null : string.Join(",", coachIds)),
                ("includeExternalIds", includeExternalIds?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<DriverCoachAssignment> SetAssignmentAsync(
        string driverId,
        string? coachId = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(driverId))
        {
            throw new ArgumentException("driverId is required.", nameof(driverId));
        }

        var path = QueryBuilder.WithParams(AssignmentsPath,
            ("driverId", driverId),
            ("coachId", coachId));
        // PUT is sent with an empty JSON body — per the spec, the driverId
        // and coachId travel as query parameters, not in a request payload.
        return HttpClient.PutDataAsync<DriverCoachAssignment>(path, new { }, cancellationToken);
    }

    public Task<DriverCoachAssignment> SetAssignmentAsync(
        SetDriverCoachAssignmentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));
        return SetAssignmentAsync(request.DriverId, request.CoachId, cancellationToken);
    }

    public IAsyncEnumerable<CoachingSession> GetSessionsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? coachIds = null,
        IReadOnlyList<string>? sessionStatuses = null,
        bool? includeCoachableEvents = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<CoachingSession>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange(SessionsStreamPath, startTime, endTime),
                ("driverIds", driverIds is null ? null : string.Join(",", driverIds)),
                ("coachIds", coachIds is null ? null : string.Join(",", coachIds)),
                ("sessionStatuses", sessionStatuses is null ? null : string.Join(",", sessionStatuses)),
                ("includeCoachableEvents", includeCoachableEvents?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()),
                ("includeExternalIds", includeExternalIds?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant())),
            cancellationToken: cancellationToken);
}
