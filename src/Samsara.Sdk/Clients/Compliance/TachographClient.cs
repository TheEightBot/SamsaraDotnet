namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class TachographClient : SamsaraServiceClientBase, ITachographClient
{
    public TachographClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<TachographActivity> ListActivitiesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographActivity>(QueryBuilder.WithTimeRange("fleet/drivers/tachograph-activity", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<TachographFile> ListFilesAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<TachographFile>(QueryBuilder.WithTimeRange("fleet/drivers/tachograph-files", startTime, endTime), cancellationToken: cancellationToken);
}
