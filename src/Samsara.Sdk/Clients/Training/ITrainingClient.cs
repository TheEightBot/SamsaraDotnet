namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Training;

/// <summary>
/// Client for Samsara training assignments and courses.
/// </summary>
public interface ITrainingClient
{
    IAsyncEnumerable<TrainingAssignment> ListAssignmentsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<TrainingCourse> ListCoursesAsync(CancellationToken cancellationToken = default);
    Task CreateAssignmentsAsync(string courseId, DateTimeOffset dueAtTime, IReadOnlyList<string> learnerIds, CancellationToken cancellationToken = default);
    Task UpdateAssignmentsAsync(IReadOnlyList<string> ids, DateTimeOffset dueAtTime, CancellationToken cancellationToken = default);
    Task DeleteAssignmentsAsync(IReadOnlyList<string> ids, CancellationToken cancellationToken = default);
}
