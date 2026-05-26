namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Places API (<c>/places</c>). Subject to change.</summary>
public interface IPlacesClient
{
    /// <summary>List places (<c>GET /places</c>).</summary>
    IAsyncEnumerable<object> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>Create a place (<c>POST /places</c>).</summary>
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);

    /// <summary>Update a place (<c>PATCH /places</c>) — id in body.</summary>
    Task<object> UpdateAsync(object request, CancellationToken cancellationToken = default);

    /// <summary>Delete a place (<c>DELETE /places</c>) — id in query or body.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}

internal sealed class PlacesClient : SamsaraServiceClientBase, IPlacesClient
{
    private const string BasePath = "places";

    public PlacesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<object> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>(BasePath, cancellationToken: cancellationToken);

    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<object>(BasePath, request, cancellationToken);

    public Task<object> UpdateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("id", id)), cancellationToken);
}
