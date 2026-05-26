namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Preferred fuel stations (<c>/preferred-stations</c>).</summary>
public interface IPreferredStationsClient
{
    IAsyncEnumerable<object> ListAsync(CancellationToken cancellationToken = default);
    Task<object> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Update (PATCH /preferred-stations) — id in body.</summary>
    Task<object> UpdateAsync(object request, CancellationToken cancellationToken = default);
    /// <summary>Delete (DELETE /preferred-stations) — id in query.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}

internal sealed class PreferredStationsClient : SamsaraServiceClientBase, IPreferredStationsClient
{
    private const string BasePath = "preferred-stations";

    public PreferredStationsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>(BasePath, cancellationToken: cancellationToken);

    public Task<object> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<object>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(BasePath, request, cancellationToken);

    public Task<object> UpdateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);
}
