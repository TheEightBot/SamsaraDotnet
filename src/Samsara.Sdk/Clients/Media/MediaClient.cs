namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Media;

internal sealed class MediaClient : SamsaraServiceClientBase, IMediaClient
{
    private const string BasePath = "cameras/media";

    public MediaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<MediaFile> ListAsync(
        string vehicleIds,
        string startTime,
        string endTime,
        IReadOnlyList<string>? inputs = null,
        IReadOnlyList<string>? mediaTypes = null,
        IReadOnlyList<string>? triggerReasons = null,
        string? availableAfterTime = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<MediaFile>(
            QueryBuilder.WithParams(
                BasePath,
                ("vehicleIds", vehicleIds),
                ("startTime", startTime),
                ("endTime", endTime),
                ("inputs", inputs is null ? null : string.Join(",", inputs)),
                ("mediaTypes", mediaTypes is null ? null : string.Join(",", mediaTypes)),
                ("triggerReasons", triggerReasons is null ? null : string.Join(",", triggerReasons)),
                ("availableAfterTime", availableAfterTime)),
            cancellationToken: cancellationToken);

    public Task<MediaRetrieval> GetRetrievalAsync(string retrievalId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<MediaRetrieval>(
            QueryBuilder.WithParams("cameras/media/retrieval", ("retrievalId", retrievalId)),
            cancellationToken);

    public Task<MediaRetrieval> CreateRetrievalAsync(CreateMediaRetrievalRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MediaRetrieval>("cameras/media/retrieval", request, cancellationToken);
}
