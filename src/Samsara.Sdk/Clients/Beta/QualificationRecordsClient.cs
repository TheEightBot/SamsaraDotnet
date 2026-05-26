namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Driver qualification records (<c>/qualification-records</c>, <c>/qualification-types</c>).</summary>
public interface IQualificationRecordsClient
{
    /// <summary>List qualification records (<c>GET /qualification-records</c>).</summary>
    IAsyncEnumerable<object> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>Stream qualification records (<c>GET /qualification-records/stream</c>).</summary>
    IAsyncEnumerable<object> GetStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);

    /// <summary>Available qualification types (<c>GET /qualification-types</c>).</summary>
    Task<object> ListTypesAsync(CancellationToken cancellationToken = default);

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

    public IAsyncEnumerable<object> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>(BasePath, cancellationToken: cancellationToken);

    public IAsyncEnumerable<object> GetStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<object>(QueryBuilder.WithTimeRange("qualification-records/stream", startTime, endTime), cancellationToken: cancellationToken);

    public Task<object> ListTypesAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("qualification-types", cancellationToken);

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
