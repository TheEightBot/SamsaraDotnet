namespace Samsara.Sdk.Clients;

using System.Diagnostics.CodeAnalysis;
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

    /// <summary>
    /// Forward-geocode an address (<c>GET /places/geocode</c>,
    /// <c>getPlaceGeocode</c>, beta). Despite being semantically a lookup, the
    /// spec paginates this operation; pagination is handled transparently.
    /// </summary>
    /// <param name="address">Address string to forward-geocode. Required by the spec; must be non-empty.</param>
    /// <param name="limit">Page size. Default 5, max 20.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<PlaceGeocodeResult> GetGeocodeAsync(
        string address,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Look up geofence suggestions around a seed point
    /// (<c>GET /places/geofence</c>, <c>getPlaceGeofence</c>, beta). Pagination is
    /// handled transparently; note the page size is <c>maxResults</c> on this
    /// operation, not <c>limit</c>.
    /// </summary>
    /// <param name="latitude">Seed point latitude in WGS84 decimal degrees. Required by the spec.</param>
    /// <param name="longitude">Seed point longitude in WGS84 decimal degrees. Required by the spec.</param>
    /// <param name="suggestionTypes">Comma-separated suggestion types in priority order: <c>building</c>, <c>parcel</c>, <c>landUse</c>, <c>boundary</c>, <c>facility</c>, <c>infrastructure</c>.</param>
    /// <param name="sizeOrder">Candidate sort order: <c>smallestFirst</c> (default) or <c>largestFirst</c>.</param>
    /// <param name="minLatitude">Search bound minimum latitude. Supply with the other three bounds.</param>
    /// <param name="minLongitude">Search bound minimum longitude.</param>
    /// <param name="maxLatitude">Search bound maximum latitude.</param>
    /// <param name="maxLongitude">Search bound maximum longitude.</param>
    /// <param name="maxAreaSquareMeters">Drop candidates with area above this value.</param>
    /// <param name="maxSourceVertices">Drop candidates whose source polygon exceeds this vertex count.</param>
    /// <param name="maxVertices">Simplify each returned candidate polygon to at most this many vertices.</param>
    /// <param name="maxResults">Page size: max candidates per page. Default 5, max 20.</param>
    /// <param name="cancellationToken">Token to cancel enumeration.</param>
    [Experimental("SAMSARA001")]
    IAsyncEnumerable<PlaceGeofenceSuggestion> GetGeofenceAsync(
        double latitude,
        double longitude,
        string? suggestionTypes = null,
        string? sizeOrder = null,
        double? minLatitude = null,
        double? minLongitude = null,
        double? maxLatitude = null,
        double? maxLongitude = null,
        double? maxAreaSquareMeters = null,
        long? maxSourceVertices = null,
        long? maxVertices = null,
        int? maxResults = null,
        CancellationToken cancellationToken = default);
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

    /// <summary>Forward-geocode an address (<c>GET /places/geocode</c>, beta).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<PlaceGeocodeResult> GetGeocodeAsync(
        string address,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<PlaceGeocodeResult>(
            QueryBuilder.WithParams($"{BasePath}/geocode", ("address", address)),
            limit,
            cancellationToken);

    /// <summary>Look up geofence suggestions around a seed point (<c>GET /places/geofence</c>, beta).</summary>
    [Experimental("SAMSARA001")]
    public IAsyncEnumerable<PlaceGeofenceSuggestion> GetGeofenceAsync(
        double latitude,
        double longitude,
        string? suggestionTypes = null,
        string? sizeOrder = null,
        double? minLatitude = null,
        double? minLongitude = null,
        double? maxLatitude = null,
        double? maxLongitude = null,
        double? maxAreaSquareMeters = null,
        long? maxSourceVertices = null,
        long? maxVertices = null,
        int? maxResults = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<PlaceGeofenceSuggestion>(
            QueryBuilder.WithParams($"{BasePath}/geofence",
                ("latitude", latitude.ToString(CultureInfo.InvariantCulture)),
                ("longitude", longitude.ToString(CultureInfo.InvariantCulture)),
                ("suggestionTypes", suggestionTypes),
                ("sizeOrder", sizeOrder),
                ("minLatitude", minLatitude?.ToString(CultureInfo.InvariantCulture)),
                ("minLongitude", minLongitude?.ToString(CultureInfo.InvariantCulture)),
                ("maxLatitude", maxLatitude?.ToString(CultureInfo.InvariantCulture)),
                ("maxLongitude", maxLongitude?.ToString(CultureInfo.InvariantCulture)),
                ("maxAreaSquareMeters", maxAreaSquareMeters?.ToString(CultureInfo.InvariantCulture)),
                ("maxSourceVertices", maxSourceVertices?.ToString(CultureInfo.InvariantCulture)),
                ("maxVertices", maxVertices?.ToString(CultureInfo.InvariantCulture)),
                ("maxResults", maxResults?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken: cancellationToken);
}
