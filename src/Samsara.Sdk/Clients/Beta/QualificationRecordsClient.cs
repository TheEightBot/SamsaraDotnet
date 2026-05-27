namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Driver qualification records (<c>/qualification-records</c>, <c>/qualification-types</c>).</summary>
public interface IQualificationRecordsClient
{
    /// <summary>List qualification records (<c>GET /qualification-records</c>) — required <paramref name="ids"/>.</summary>
    IAsyncEnumerable<object> ListAsync(
        IReadOnlyList<string> ids,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream qualification records (<c>GET /qualification-records/stream</c>) — required <paramref name="entityType"/>.
    /// </summary>
    IAsyncEnumerable<object> GetStreamAsync(
        string entityType,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? ownerIds = null,
        IReadOnlyList<string>? qualificationTypeIds = null,
        bool? includeExternalIds = null,
        bool? includeDeleted = null,
        CancellationToken cancellationToken = default);

    /// <summary>Available qualification types (<c>GET /qualification-types</c>) — required <paramref name="entityType"/>.</summary>
    Task<object> ListTypesAsync(
        string entityType,
        IReadOnlyList<string>? ids = null,
        string? after = null,
        CancellationToken cancellationToken = default);

    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Update (PATCH /qualification-records) — id in body.</summary>
    Task<object> UpdateAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Delete (DELETE /qualification-records) — id in query.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    Task<object> ArchiveAsync(object request, CancellationToken cancellationToken = default);
    Task<object> UnarchiveAsync(object request, CancellationToken cancellationToken = default);
}

internal sealed class QualificationRecordsClient : SamsaraServiceClientBase, IQualificationRecordsClient
{
    private const string BasePath = "qualification-records";

    public QualificationRecordsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> ListAsync(
        IReadOnlyList<string> ids,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(BasePath,
                ("ids", string.Join(",", ids)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetStreamAsync(
        string entityType,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? ownerIds = null,
        IReadOnlyList<string>? qualificationTypeIds = null,
        bool? includeExternalIds = null,
        bool? includeDeleted = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("qualification-records/stream", startTime, endTime),
                ("entityType", entityType),
                ("ownerIds", ownerIds is null ? null : string.Join(",", ownerIds)),
                ("qualificationTypeIds", qualificationTypeIds is null ? null : string.Join(",", qualificationTypeIds)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("includeDeleted", includeDeleted?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<object> ListTypesAsync(
        string entityType,
        IReadOnlyList<string>? ids = null,
        string? after = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("qualification-types",
                ("entityType", entityType),
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("after", after)),
            cancellationToken);

    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(BasePath, request, cancellationToken);

    public Task<object> UpdateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);

    public Task<object> ArchiveAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("qualification-records/archive", request, cancellationToken);

    public Task<object> UnarchiveAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("qualification-records/unarchive", request, cancellationToken);
}
