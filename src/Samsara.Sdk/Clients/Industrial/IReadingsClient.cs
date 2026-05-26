namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Industrial;

/// <summary>Client for Samsara readings (definitions, history, snapshots).</summary>
public interface IReadingsClient
{
    IAsyncEnumerable<ReadingDefinition> ListDefinitionsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<ReadingHistory> GetHistoryAsync(string readingId, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<ReadingSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default);
    /// <summary>Submit one or more readings (beta).</summary>
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
}
