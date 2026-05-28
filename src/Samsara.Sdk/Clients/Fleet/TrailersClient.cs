namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class TrailersClient : SamsaraServiceClientBase, ITrailersClient
{
    private const string BasePath = "fleet/trailers";

    public TrailersClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Trailer> ListAsync(string? parentTagIds = null, string? tagIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<Trailer>(
            QueryBuilder.WithParams(BasePath, ("parentTagIds", parentTagIds), ("tagIds", tagIds)),
            cancellationToken: cancellationToken);

    public Task<Trailer> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Trailer>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Trailer> CreateAsync(CreateTrailerRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Trailer>(BasePath, request, cancellationToken);

    public Task<Trailer> UpdateAsync(string id, UpdateTrailerRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Trailer>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<TrailerStats> GetStatsSnapshotAsync(string types, string? parentTagIds = null, string? tagIds = null, string? time = null, string? trailerIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TrailerStats>(
            QueryBuilder.WithParams($"{BasePath}/stats", ("types", types), ("parentTagIds", parentTagIds), ("tagIds", tagIds), ("time", time), ("trailerIds", trailerIds)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TrailerStats> GetStatsFeedAsync(string types, string? decorations = null, string? parentTagIds = null, string? tagIds = null, string? trailerIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TrailerStats>(
            QueryBuilder.WithParams($"{BasePath}/stats/feed", ("types", types), ("decorations", decorations), ("parentTagIds", parentTagIds), ("tagIds", tagIds), ("trailerIds", trailerIds)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<TrailerStats> GetStatsHistoryAsync(string types, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, string? decorations = null, string? parentTagIds = null, string? tagIds = null, string? trailerIds = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TrailerStats>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange($"{BasePath}/stats/history", startTime, endTime),
                ("types", types), ("decorations", decorations), ("parentTagIds", parentTagIds), ("tagIds", tagIds), ("trailerIds", trailerIds)),
            cancellationToken: cancellationToken);
}
