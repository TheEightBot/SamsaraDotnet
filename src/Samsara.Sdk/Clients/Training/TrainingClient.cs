namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Training;

internal sealed class TrainingClient : SamsaraServiceClientBase, ITrainingClient
{
    public TrainingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TrainingAssignment> ListAssignmentsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<TrainingAssignment>("fleet/training/assignments", cancellationToken: cancellationToken);

    public IAsyncEnumerable<TrainingCourse> ListCoursesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<TrainingCourse>("fleet/training/courses", cancellationToken: cancellationToken);
}
