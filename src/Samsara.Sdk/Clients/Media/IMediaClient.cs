namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Media;

/// <summary>
/// Client for retrieving Samsara media files.
/// </summary>
public interface IMediaClient
{
    /// <summary>
    /// List uploaded media by time range (<c>GET /cameras/media</c>).
    /// </summary>
    /// <param name="vehicleIds">
    /// Spec-required. Comma-separated list of vehicle IDs whose media should
    /// be returned.
    /// </param>
    /// <param name="startTime">
    /// Spec-required. Start of the time range (RFC 3339).
    /// </param>
    /// <param name="endTime">
    /// Spec-required. End of the time range (RFC 3339).
    /// </param>
    /// <param name="inputs">
    /// Optional camera input filter. Comma-joined and sent as the
    /// <c>inputs</c> query parameter.
    /// </param>
    /// <param name="mediaTypes">
    /// Optional media-type filter. Comma-joined and sent as the
    /// <c>mediaTypes</c> query parameter.
    /// </param>
    /// <param name="triggerReasons">
    /// Optional trigger-reason filter. Comma-joined and sent as the
    /// <c>triggerReasons</c> query parameter.
    /// </param>
    /// <param name="availableAfterTime">
    /// Optional RFC 3339 timestamp; only media made available after this time
    /// is returned.
    /// </param>
    /// <param name="cancellationToken">Token to observe while waiting for the operation.</param>
    IAsyncEnumerable<MediaFile> ListAsync(
        string vehicleIds,
        string startTime,
        string endTime,
        IReadOnlyList<string>? inputs = null,
        IReadOnlyList<string>? mediaTypes = null,
        IReadOnlyList<string>? triggerReasons = null,
        string? availableAfterTime = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the media items for a retrieval request
    /// (<c>GET /cameras/media/retrieval</c>). The response nests the items under
    /// <c>data.media</c>, so this returns the list of retrieved media. The list
    /// is empty while a retrieval is still pending and has produced no media.
    /// </summary>
    /// <param name="retrievalId">
    /// Spec-required. The retrieval ID returned by <see cref="CreateRetrievalAsync"/>.
    /// </param>
    /// <param name="cancellationToken">Token to observe while waiting for the operation.</param>
    Task<IReadOnlyList<MediaRetrieval>> GetRetrievalAsync(
        string retrievalId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a media retrieval request (<c>POST /cameras/media/retrieval</c>).
    /// </summary>
    Task<MediaRetrieval> CreateRetrievalAsync(
        CreateMediaRetrievalRequest request,
        CancellationToken cancellationToken = default);
}
