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
        // GET /cameras/media returns { data: { media: [...] }, pagination: {...} } — the
        // item array is nested under data.media, not data itself, so project it out.
        => PaginateAsync<MediaListResponse, MediaFile>(
            QueryBuilder.WithParams(
                BasePath,
                ("vehicleIds", vehicleIds),
                ("startTime", startTime),
                ("endTime", endTime),
                ("inputs", inputs is null ? null : string.Join(",", inputs)),
                ("mediaTypes", mediaTypes is null ? null : string.Join(",", mediaTypes)),
                ("triggerReasons", triggerReasons is null ? null : string.Join(",", triggerReasons)),
                ("availableAfterTime", availableAfterTime)),
            static data => data.Media ?? Array.Empty<MediaFile>(),
            cancellationToken: cancellationToken);

    public async Task<IReadOnlyList<MediaRetrieval>> GetRetrievalAsync(string retrievalId, CancellationToken cancellationToken = default)
    {
        // GET /cameras/media/retrieval returns { data: { media: [...] } } — unwrap the
        // { data } envelope, then return the nested media array.
        var response = await HttpClient.GetDataAsync<MediaRetrievalListResponse>(
            QueryBuilder.WithParams("cameras/media/retrieval", ("retrievalId", retrievalId)),
            cancellationToken).ConfigureAwait(false);

        return response.Media ?? Array.Empty<MediaRetrieval>();
    }

    public Task<MediaRetrieval> CreateRetrievalAsync(CreateMediaRetrievalRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<MediaRetrieval>("cameras/media/retrieval", request, cancellationToken);
}
