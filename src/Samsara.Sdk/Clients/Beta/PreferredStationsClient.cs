namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Preferred fuel stations (<c>/preferred-stations</c>).</summary>
public interface IPreferredStationsClient
{
    IAsyncEnumerable<object> ListAsync(
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);
    Task<object> GetAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Update (PATCH /preferred-stations) — required <paramref name="id"/> query param.</summary>
    Task<object> UpdateAsync(string id, object request, CancellationToken cancellationToken = default);
    /// <summary>Delete (DELETE /preferred-stations) — id in query.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}

internal sealed class PreferredStationsClient : SamsaraServiceClientBase, IPreferredStationsClient
{
    private const string BasePath = "preferred-stations";

    public PreferredStationsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> ListAsync(
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams(BasePath,
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<object> GetAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<object>(
            QueryBuilder.WithParams($"{BasePath}/{Uri.EscapeDataString(id)}",
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(BasePath, request, cancellationToken);

    public Task<object> UpdateAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>(QueryBuilder.WithParams(BasePath, ("id", id)), request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);
}
