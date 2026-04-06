namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Compliance;

internal sealed class IftaClient : SamsaraServiceClientBase, IIftaClient
{
    public IftaClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<IftaDetail> ListDetailsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<IftaDetail>(QueryBuilder.WithTimeRange("fleet/reports/ifta/detail", startTime, endTime), cancellationToken: cancellationToken);

    public Task<IReadOnlyList<IftaSummary>> GetSummaryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<IftaSummary>>(QueryBuilder.WithTimeRange("fleet/reports/ifta/summary", startTime, endTime), cancellationToken);
}
