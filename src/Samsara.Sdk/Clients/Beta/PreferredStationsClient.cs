namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>Beta — Preferred fuel stations (<c>/preferred-stations</c>).</summary>
public interface IPreferredStationsClient
{
    /// <summary>List preferred stations (<c>GET /preferred-stations</c>).</summary>
    IAsyncEnumerable<PreferredStation> ListAsync(
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get a preferred station (<c>GET /preferred-stations/{id}</c>).</summary>
    Task<PreferredStation> GetAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a preferred station (<c>POST /preferred-stations</c>).</summary>
    Task<PreferredStation> CreateAsync(
        PreferredStationCreateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Update (PATCH /preferred-stations) — required <paramref name="id"/> query param.</summary>
    Task<PreferredStation> UpdateAsync(
        string id,
        PreferredStationUpdateRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Delete (DELETE /preferred-stations) — id in query.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}

internal sealed class PreferredStationsClient : SamsaraServiceClientBase, IPreferredStationsClient
{
    private const string BasePath = "preferred-stations";

    public PreferredStationsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<PreferredStation> ListAsync(
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<PreferredStation>(
            QueryBuilder.WithParams(BasePath,
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<PreferredStation> GetAsync(
        string id,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<PreferredStation>(
            QueryBuilder.WithParams($"{BasePath}/{Uri.EscapeDataString(id)}",
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<PreferredStation> CreateAsync(
        PreferredStationCreateRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<PreferredStation>(BasePath, request, cancellationToken);

    public Task<PreferredStation> UpdateAsync(
        string id,
        PreferredStationUpdateRequest request,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<PreferredStation>(
            QueryBuilder.WithParams(BasePath, ("id", id)), request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);
}
