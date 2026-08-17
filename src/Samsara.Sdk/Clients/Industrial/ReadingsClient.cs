namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

internal sealed class ReadingsClient : SamsaraServiceClientBase, IReadingsClient
{
    public ReadingsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<ReadingDefinition> ListDefinitionsAsync(
        string? ids = null,
        string? entityTypes = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<ReadingDefinition>(
            QueryBuilder.WithParams("readings/definitions",
                ("ids", ids),
                ("entityTypes", entityTypes)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<ReadingHistory> GetHistoryAsync(
        string readingId,
        string entityType,
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        string? entityIds = null,
        string? externalIds = null,
        bool? feed = null,
        bool? includeExternalIds = null,
        string? assetTypes = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<ReadingHistory>(
            QueryBuilder.WithParams(
                QueryBuilder.WithTimeRange("readings/history", startTime, endTime),
                ("readingId", readingId),
                ("entityType", entityType),
                ("entityIds", entityIds),
                ("externalIds", externalIds),
                ("feed", feed?.ToString().ToLowerInvariant()),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("assetTypes", assetTypes)),
            cancellationToken: cancellationToken);

    public IAsyncEnumerable<ReadingSnapshot> GetSnapshotAsync(
        string readingIds,
        string entityType,
        string? entityIds = null,
        string? externalIds = null,
        string? asOfTime = null,
        bool? includeExternalIds = null,
        string? assetTypes = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<ReadingSnapshot>(
            QueryBuilder.WithParams("readings/latest",
                ("readingIds", readingIds),
                ("entityType", entityType),
                ("entityIds", entityIds),
                ("externalIds", externalIds),
                ("asOfTime", asOfTime),
                ("includeExternalIds", includeExternalIds?.ToString().ToLowerInvariant()),
                ("assetTypes", assetTypes)),
            cancellationToken: cancellationToken);

    /// <summary>Submit one or more readings (<c>POST /readings</c>).</summary>
    /// <remarks>
    /// The spec's success response is <c>201</c> with <c>content: {}</c> — no body at all —
    /// so this uses the bodyless <c>PostAsync</c> overload and returns <see cref="Task"/>.
    /// (It previously returned <c>Task&lt;object&gt;</c> via <c>PostAsync&lt;object&gt;</c>,
    /// which threw on the empty payload.)
    /// </remarks>
    public Task CreateAsync(CreateReadingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync("readings", request, cancellationToken);
}
