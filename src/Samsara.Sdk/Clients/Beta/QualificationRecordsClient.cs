namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>Beta — Driver qualification records (<c>/qualification-records</c>, <c>/qualification-types</c>).</summary>
public interface IQualificationRecordsClient
{
    /// <summary>List qualification records (<c>GET /qualification-records</c>) — required <paramref name="ids"/>.</summary>
    IAsyncEnumerable<QualificationRecord> ListAsync(
        IReadOnlyList<string> ids,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream qualification records (<c>GET /qualification-records/stream</c>) — required <paramref name="entityType"/>.
    /// </summary>
    IAsyncEnumerable<QualificationRecord> GetStreamAsync(
        string entityType,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? ownerIds = null,
        IReadOnlyList<string>? qualificationTypeIds = null,
        bool? includeExternalIds = null,
        bool? includeDeleted = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Available qualification types (<c>GET /qualification-types</c>) — required
    /// <paramref name="entityType"/>. Cursor pagination is handled transparently.
    /// </summary>
    IAsyncEnumerable<QualificationType> ListTypesAsync(
        string entityType,
        IReadOnlyList<string>? ids = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a qualification record (<c>POST /qualification-records</c>).</summary>
    Task<QualificationRecord> CreateAsync(
        QualificationRecordCreateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Update (PATCH /qualification-records) — id in body.</summary>
    Task<QualificationRecord> UpdateAsync(
        QualificationRecordUpdateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Delete (DELETE /qualification-records) — id in query.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Archive a qualification record (<c>POST /qualification-records/archive</c>).</summary>
    Task ArchiveAsync(QualificationRecordIdRequest request, CancellationToken cancellationToken = default);

    /// <summary>Unarchive a qualification record (<c>POST /qualification-records/unarchive</c>).</summary>
    Task UnarchiveAsync(QualificationRecordIdRequest request, CancellationToken cancellationToken = default);
}

internal sealed class QualificationRecordsClient : SamsaraServiceClientBase, IQualificationRecordsClient
{
    private const string BasePath = "qualification-records";

    public QualificationRecordsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<QualificationRecord> ListAsync(
        IReadOnlyList<string> ids,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<QualificationRecord>(
            QueryBuilder.WithParams(BasePath,
                ("ids", string.Join(",", ids)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<QualificationRecord> GetStreamAsync(
        string entityType,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? ownerIds = null,
        IReadOnlyList<string>? qualificationTypeIds = null,
        bool? includeExternalIds = null,
        bool? includeDeleted = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<QualificationRecord>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("qualification-records/stream", startTime, endTime),
                ("entityType", entityType),
                ("ownerIds", ownerIds is null ? null : string.Join(",", ownerIds)),
                ("qualificationTypeIds", qualificationTypeIds is null ? null : string.Join(",", qualificationTypeIds)),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("includeDeleted", includeDeleted?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<QualificationType> ListTypesAsync(
        string entityType,
        IReadOnlyList<string>? ids = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<QualificationType>(
            QueryBuilder.WithParams("qualification-types",
                ("entityType", entityType),
                ("ids", ids is null ? null : string.Join(",", ids))),
            cancellationToken: cancellationToken);

    public Task<QualificationRecord> CreateAsync(
        QualificationRecordCreateRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<QualificationRecord>(BasePath, request, cancellationToken);

    public Task<QualificationRecord> UpdateAsync(
        QualificationRecordUpdateRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<QualificationRecord>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);

    public Task ArchiveAsync(QualificationRecordIdRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync("qualification-records/archive", request, cancellationToken);

    public Task UnarchiveAsync(QualificationRecordIdRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync("qualification-records/unarchive", request, cancellationToken);
}
