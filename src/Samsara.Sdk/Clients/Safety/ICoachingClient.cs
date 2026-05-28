namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Safety;

/// <summary>Client for Samsara coaching sessions and assignments.</summary>
public interface ICoachingClient
{
    /// <summary>
    /// Lists driver-coach assignments (<c>GET /coaching/driver-coach-assignments</c>).
    /// </summary>
    /// <param name="driverIds">Optional comma-separated list of driver IDs (Samsara IDs or external IDs).</param>
    /// <param name="coachIds">Optional comma-separated list of coach (Samsara user) IDs.</param>
    /// <param name="includeExternalIds">Optional flag to return external IDs on supported entities.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<DriverCoachAssignment> ListAssignmentsAsync(
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? coachIds = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the coach assignment for a driver
    /// (<c>PUT /coaching/driver-coach-assignments</c>). Both <c>driverId</c>
    /// and <c>coachId</c> are sent as query parameters per the OpenAPI spec.
    /// Passing a <c>null</c> <paramref name="coachId"/> removes the existing
    /// coach assignment for the driver.
    /// </summary>
    /// <param name="driverId">Required Samsara driver ID.</param>
    /// <param name="coachId">Optional Samsara user ID of the coach. <c>null</c> removes the assignment.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<DriverCoachAssignment> SetAssignmentAsync(
        string driverId,
        string? coachId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the coach assignment for a driver using the existing request
    /// record. Equivalent to
    /// <see cref="SetAssignmentAsync(string, string?, CancellationToken)"/>.
    /// </summary>
    /// <param name="request">Request specifying <c>driverId</c> and optional <c>coachId</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<DriverCoachAssignment> SetAssignmentAsync(
        SetDriverCoachAssignmentRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams coaching sessions
    /// (<c>GET /coaching/sessions/stream</c>). <paramref name="startTime"/> is
    /// required by the spec.
    /// </summary>
    /// <param name="startTime">Required RFC 3339 timestamp; compared against <c>updatedAtTime</c>.</param>
    /// <param name="endTime">Optional RFC 3339 timestamp; when unset, behaves as an unending feed.</param>
    /// <param name="driverIds">Optional comma-separated driver IDs filter.</param>
    /// <param name="coachIds">Optional comma-separated coach (user) IDs filter.</param>
    /// <param name="sessionStatuses">Optional list of statuses (<c>upcoming</c>, <c>completed</c>, <c>deleted</c>).</param>
    /// <param name="includeCoachableEvents">Optional flag to include coachable events in behaviors.</param>
    /// <param name="includeExternalIds">Optional flag to return external IDs on supported entities.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<CoachingSession> GetSessionsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? coachIds = null,
        IReadOnlyList<string>? sessionStatuses = null,
        bool? includeCoachableEvents = null,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);
}
