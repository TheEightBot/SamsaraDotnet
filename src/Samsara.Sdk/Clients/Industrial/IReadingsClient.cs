namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Industrial;

/// <summary>Client for Samsara readings (definitions, history, snapshots).</summary>
public interface IReadingsClient
{
    /// <summary>List reading definitions (<c>GET /readings/definitions</c>).</summary>
    /// <param name="ids">Comma-separated reading IDs (up to 50). Optional.</param>
    /// <param name="entityTypes">Comma-separated list of entity types to filter by
    /// (e.g. <c>asset,sensor</c>). Optional.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<ReadingDefinition> ListDefinitionsAsync(
        string? ids = null,
        string? entityTypes = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get reading history and feed (<c>GET /readings/history</c>).</summary>
    /// <param name="readingId">The reading ID to query history for. Spec REQUIRED.</param>
    /// <param name="entityType">Entity type the readings are bound to. Spec REQUIRED.</param>
    /// <param name="startTime">Lower bound (inclusive) of the time window. Optional.</param>
    /// <param name="endTime">Upper bound (inclusive) of the time window. Optional.</param>
    /// <param name="entityIds">Comma-separated list of entity IDs to filter by. Optional.</param>
    /// <param name="externalIds">Comma-separated <c>name:value</c> external ID pairs. Optional.</param>
    /// <param name="feed">When <c>true</c>, returns a continuous feed of new readings. Optional.</param>
    /// <param name="includeExternalIds">When <c>true</c>, includes external IDs on the response. Optional.</param>
    /// <param name="assetTypes">Comma-separated asset types to filter by. Only supported when <paramref name="entityType"/> is <c>asset</c>. Optional.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<ReadingHistory> GetHistoryAsync(
        string readingId,
        string entityType,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? entityIds = null,
        string? externalIds = null,
        bool? feed = null,
        bool? includeExternalIds = null,
        string? assetTypes = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get the latest reading snapshot (<c>GET /readings/latest</c>).</summary>
    /// <param name="readingIds">Comma-separated reading IDs to fetch the latest value for. Spec REQUIRED.</param>
    /// <param name="entityType">Entity type the readings are bound to. Spec REQUIRED.</param>
    /// <param name="entityIds">Comma-separated list of entity IDs to filter by. Optional.</param>
    /// <param name="externalIds">Comma-separated <c>name:value</c> external ID pairs. Optional.</param>
    /// <param name="asOfTime">RFC 3339 timestamp to query a historical snapshot at. Optional.</param>
    /// <param name="includeExternalIds">When <c>true</c>, includes external IDs on the response. Optional.</param>
    /// <param name="assetTypes">Comma-separated asset types to filter by. Only supported when <paramref name="entityType"/> is <c>asset</c>. Optional.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<ReadingSnapshot> GetSnapshotAsync(
        string readingIds,
        string entityType,
        string? entityIds = null,
        string? externalIds = null,
        string? asOfTime = null,
        bool? includeExternalIds = null,
        string? assetTypes = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Ingest a batch of readings (<c>POST /readings</c>, up to 1000 data points).
    /// </summary>
    /// <remarks>
    /// Returns no value: the spec's success response is a <c>201</c> with an empty
    /// content block. Ingestion only works for assets created via <c>POST /assets</c>
    /// with <c>readingsIngestionEnabled</c> set; see <see cref="ListDefinitionsAsync"/>
    /// for which readings accept ingestion.
    /// </remarks>
    /// <param name="request">The batch of data points to ingest.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task CreateAsync(CreateReadingsRequest request, CancellationToken cancellationToken = default);
}
