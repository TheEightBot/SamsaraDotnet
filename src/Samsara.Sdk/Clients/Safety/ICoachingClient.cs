namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Safety;

/// <summary>Client for Samsara coaching sessions and assignments.</summary>
public interface ICoachingClient
{
    IAsyncEnumerable<DriverCoachAssignment> ListAssignmentsAsync(CancellationToken cancellationToken = default);
    Task<DriverCoachAssignment> SetAssignmentAsync(SetDriverCoachAssignmentRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<CoachingSession> GetSessionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
