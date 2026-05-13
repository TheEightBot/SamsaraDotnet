namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Media;

internal sealed class MediaClient : SamsaraServiceClientBase, IMediaClient
{
    private const string BasePath = "fleet/media";

    public MediaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<MediaFile> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<MediaFile>(BasePath, cancellationToken: cancellationToken);

    public Task<MediaFile> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MediaFile>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<MediaRetrieval> GetRetrievalAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MediaRetrieval>($"media/retrievals/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<MediaRetrieval> CreateRetrievalAsync(CreateMediaRetrievalRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MediaRetrieval>("media/retrievals", request, cancellationToken);
}
