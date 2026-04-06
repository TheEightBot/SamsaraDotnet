namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Training;

/// <summary>
/// Client for Samsara training assignments and courses.
/// </summary>
public interface ITrainingClient
{
    IAsyncEnumerable<TrainingAssignment> ListAssignmentsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<TrainingCourse> ListCoursesAsync(CancellationToken cancellationToken = default);
}
