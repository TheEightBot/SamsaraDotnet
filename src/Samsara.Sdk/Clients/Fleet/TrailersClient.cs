namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class TrailersClient : SamsaraServiceClientBase, ITrailersClient
{
    private const string BasePath = "fleet/trailers";

    public TrailersClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Trailer> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Trailer>(BasePath, cancellationToken: cancellationToken);

    public Task<Trailer> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Trailer>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Trailer> CreateAsync(CreateTrailerRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Trailer>(BasePath, request, cancellationToken);

    public Task<Trailer> UpdateAsync(string id, UpdateTrailerRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Trailer>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
