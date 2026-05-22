namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Media;

internal sealed class MediaClient : SamsaraServiceClientBase, IMediaClient
{
    private const string BasePath = "cameras/media";

    public MediaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<MediaFile> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<MediaFile>(BasePath, cancellationToken: cancellationToken);

    public Task<MediaRetrieval> GetRetrievalAsync(string retrievalId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MediaRetrieval>($"cameras/media/retrieval?retrievalId={Uri.EscapeDataString(retrievalId)}", cancellationToken);

    public Task<MediaRetrieval> CreateRetrievalAsync(CreateMediaRetrievalRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MediaRetrieval>("cameras/media/retrieval", request, cancellationToken);
}
