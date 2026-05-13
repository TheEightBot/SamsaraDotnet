namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Industrial;

/// <summary>
/// Client for Samsara industrial/IoT data.
/// </summary>
public interface IIndustrialClient
{
    IAsyncEnumerable<IndustrialAsset> ListAssetsAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<DataInput> ListDataInputsAsync(CancellationToken cancellationToken = default);
    Task<DataInput> GetDataInputAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DataInputDataPoint> GetDataInputSnapshotAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<DataInputDataPoint> GetDataInputFeedAsync(CancellationToken cancellationToken = default);
    IAsyncEnumerable<DataInputDataPoint> GetDataInputHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
