namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Training;

/// <summary>
/// Client for Samsara training assignments and courses.
/// </summary>
public interface ITrainingClient
{
    /// <summary>
    /// Stream filtered training assignments (<c>GET /training-assignments/stream</c>).
    /// <paramref name="startTime"/> is spec-required.
    /// </summary>
    IAsyncEnumerable<TrainingAssignment> ListAssignmentsAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? categoryIds = null,
        IReadOnlyList<string>? courseIds = null,
        IReadOnlyList<string>? learnerIds = null,
        IReadOnlyList<string>? status = null,
        bool? isOverdue = null,
        CancellationToken cancellationToken = default);
    IAsyncEnumerable<TrainingCourse> ListCoursesAsync(
        IReadOnlyList<string>? categoryIds = null,
        IReadOnlyList<string>? courseIds = null,
        IReadOnlyList<string>? status = null,
        CancellationToken cancellationToken = default);
    Task CreateAssignmentsAsync(string courseId, DateTimeOffset dueAtTime, IReadOnlyList<string> learnerIds, CancellationToken cancellationToken = default);
    /// <summary>
    /// Update training assignments' due-by time (<c>PATCH /training-assignments</c>),
    /// returning the updated assignments.
    /// </summary>
    Task<IReadOnlyList<TrainingAssignment>> UpdateAssignmentsAsync(IReadOnlyList<string> ids, DateTimeOffset dueAtTime, CancellationToken cancellationToken = default);
    Task DeleteAssignmentsAsync(IReadOnlyList<string> ids, CancellationToken cancellationToken = default);
}
