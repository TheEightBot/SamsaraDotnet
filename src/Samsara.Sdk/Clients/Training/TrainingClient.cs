namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Training;

internal sealed class TrainingClient : SamsaraServiceClientBase, ITrainingClient
{
    public TrainingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TrainingAssignment> ListAssignmentsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<TrainingAssignment>("training-assignments/stream", cancellationToken: cancellationToken);

    public IAsyncEnumerable<TrainingCourse> ListCoursesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<TrainingCourse>("training-courses", cancellationToken: cancellationToken);

    /// <summary>Create training assignments (<c>POST /training-assignments</c>). All params are query-side.</summary>
    public Task CreateAssignmentsAsync(string courseId, DateTimeOffset dueAtTime, IReadOnlyList<string> learnerIds, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync(
            QueryBuilder.WithParams("training-assignments",
                ("courseId", courseId),
                ("dueAtTime", dueAtTime.ToString("O")),
                ("learnerIds", string.Join(",", learnerIds))),
            new { },
            cancellationToken);

    /// <summary>Update training assignments' due-by time (<c>PATCH /training-assignments</c>).</summary>
    public Task UpdateAssignmentsAsync(IReadOnlyList<string> ids, DateTimeOffset dueAtTime, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>(
            QueryBuilder.WithParams("training-assignments",
                ("ids", string.Join(",", ids)),
                ("dueAtTime", dueAtTime.ToString("O"))),
            new { },
            cancellationToken);

    /// <summary>Delete training assignments (<c>DELETE /training-assignments</c>).</summary>
    public Task DeleteAssignmentsAsync(IReadOnlyList<string> ids, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(
            QueryBuilder.WithParams("training-assignments", ("ids", string.Join(",", ids))),
            cancellationToken);
}
