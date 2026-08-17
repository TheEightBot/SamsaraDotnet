namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Training;

internal sealed class TrainingClient : SamsaraServiceClientBase, ITrainingClient
{
    public TrainingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TrainingAssignment> ListAssignmentsAsync(
        DateTimeOffset startTime,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? categoryIds = null,
        IReadOnlyList<string>? courseIds = null,
        IReadOnlyList<string>? learnerIds = null,
        IReadOnlyList<string>? status = null,
        bool? isOverdue = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<TrainingAssignment>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("training-assignments/stream", startTime, endTime),
                ("categoryIds", categoryIds is null ? null : string.Join(",", categoryIds)),
                ("courseIds", courseIds is null ? null : string.Join(",", courseIds)),
                ("learnerIds", learnerIds is null ? null : string.Join(",", learnerIds)),
                ("status", status is null ? null : string.Join(",", status)),
                ("isOverdue", isOverdue?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TrainingCourse> ListCoursesAsync(
        IReadOnlyList<string>? categoryIds = null,
        IReadOnlyList<string>? courseIds = null,
        IReadOnlyList<string>? status = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<TrainingCourse>(
            QueryBuilder.WithParams("training-courses",
                ("categoryIds", categoryIds is null ? null : string.Join(",", categoryIds)),
                ("courseIds", courseIds is null ? null : string.Join(",", courseIds)),
                ("status", status is null ? null : string.Join(",", status))),
            cancellationToken: cancellationToken);

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
    public Task<IReadOnlyList<TrainingAssignment>> UpdateAssignmentsAsync(IReadOnlyList<string> ids, DateTimeOffset dueAtTime, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IReadOnlyList<TrainingAssignment>>(
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
