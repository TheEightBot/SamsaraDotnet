namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

internal sealed class ReadingsClient : SamsaraServiceClientBase, IReadingsClient
{
    public ReadingsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<ReadingDefinition> ListDefinitionsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<ReadingDefinition>("readings/definitions", cancellationToken: cancellationToken);

    public IAsyncEnumerable<ReadingHistory> GetHistoryAsync(string readingId, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithTimeRange("readings/history", startTime, endTime);
        path = QueryBuilder.WithParams(path, ("readingId", readingId));
        return PaginateAsync<ReadingHistory>(path, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<ReadingSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<ReadingSnapshot>("readings/latest", cancellationToken: cancellationToken);
}
