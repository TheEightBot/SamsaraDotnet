namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Fleet;

internal sealed class IdlingClient : SamsaraServiceClientBase, IIdlingClient
{
    public IdlingClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<IdlingEvent> ListEventsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<IdlingEvent>(QueryBuilder.WithTimeRange("idling/events", startTime, endTime), cancellationToken: cancellationToken);
}
