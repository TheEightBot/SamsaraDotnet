namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>Beta — Places API (<c>/places</c>). Subject to change.</summary>
public interface IPlacesClient
{
    /// <summary>List places (<c>GET /places</c>).</summary>
    IAsyncEnumerable<Place> ListAsync(
        string? name = null,
        string? placeIds = null,
        string? externalIds = null,
        string? placeTypes = null,
        string? tagIds = null,
        string? parentTagIds = null,
        bool? includeExternalIds = null,
        bool? includeTags = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a place (<c>POST /places</c>).</summary>
    Task<Place> CreateAsync(PlaceCreateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a place (<c>PATCH /places</c>). Identify the place by either
    /// <paramref name="placeId"/> (Samsara id) or <paramref name="externalId"/>
    /// (mutually exclusive per spec; provide exactly one).
    /// </summary>
    Task<Place> UpdateAsync(
        PlaceUpdateRequest request,
        int? placeId = null,
        string? externalId = null,
        CancellationToken cancellationToken = default);

    /// <summary>Delete a place (<c>DELETE /places</c>) — required <paramref name="placeId"/> query param.</summary>
    Task DeleteAsync(int placeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Poll place deletions (<c>GET /places/deletions</c>, <c>getPlaceDeletions</c>, beta).
    /// Yields a <see cref="PlaceDeletionMarker"/> for each soft-deleted place; pagination
    /// (cursor/limit) is handled transparently.
    /// </summary>
    IAsyncEnumerable<PlaceDeletionMarker> GetDeletionsAsync(CancellationToken cancellationToken = default);
}

internal sealed class PlacesClient : SamsaraServiceClientBase, IPlacesClient
{
    private const string BasePath = "places";

    public PlacesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Place> ListAsync(
        string? name = null,
        string? placeIds = null,
        string? externalIds = null,
        string? placeTypes = null,
        string? tagIds = null,
        string? parentTagIds = null,
        bool? includeExternalIds = null,
        bool? includeTags = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<Place>(
            QueryBuilder.WithParams(BasePath,
                ("name", name),
                ("placeIds", placeIds),
                ("externalIds", externalIds),
                ("placeTypes", placeTypes),
                ("tagIds", tagIds),
                ("parentTagIds", parentTagIds),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("includeTags", includeTags?.ToString().ToLowerInvariant())),
            cancellationToken: cancellationToken);

    public Task<Place> CreateAsync(PlaceCreateRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Place>(BasePath, request, cancellationToken);

    public Task<Place> UpdateAsync(
        PlaceUpdateRequest request,
        int? placeId = null,
        string? externalId = null,
        CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Place>(
            QueryBuilder.WithParams(BasePath,
                ("placeId", placeId?.ToString(CultureInfo.InvariantCulture)),
                ("externalId", externalId)),
            request,
            cancellationToken);

    public Task DeleteAsync(int placeId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams(BasePath, ("placeId", placeId.ToString(CultureInfo.InvariantCulture))), cancellationToken);

    public IAsyncEnumerable<PlaceDeletionMarker> GetDeletionsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<PlaceDeletionMarker>($"{BasePath}/deletions", cancellationToken: cancellationToken);
}
