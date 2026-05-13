namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

internal sealed class IndustrialClient : SamsaraServiceClientBase, IIndustrialClient
{
    public IndustrialClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<IndustrialAsset> ListAssetsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<IndustrialAsset>("industrial/assets", cancellationToken: cancellationToken);

    public IAsyncEnumerable<DataInput> ListDataInputsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DataInput>("industrial/data", cancellationToken: cancellationToken);

    public Task<DataInput> GetDataInputAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DataInput>($"industrial/data/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputSnapshotAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DataInputDataPoint>("industrial/data-inputs/data-points", cancellationToken: cancellationToken);

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputFeedAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DataInputDataPoint>("industrial/data-inputs/data-points/feed", cancellationToken: cancellationToken);

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DataInputDataPoint>(QueryBuilder.WithTimeRange("industrial/data-inputs/data-points/history", startTime, endTime), cancellationToken: cancellationToken);
}
