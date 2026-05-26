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
    Task<IndustrialAsset> CreateAssetAsync(object request, CancellationToken cancellationToken = default);
    Task<IndustrialAsset> UpdateAssetAsync(string id, object request, CancellationToken cancellationToken = default);
    Task<IndustrialAsset> UpdateAssetDataOutputsAsync(string id, object request, CancellationToken cancellationToken = default);
    Task DeleteAssetAsync(string id, CancellationToken cancellationToken = default);
    /// <summary>List vision cameras (v1).</summary>
    Task<object> V1ListCamerasAsync(CancellationToken cancellationToken = default);
    Task<object> V1GetVisionProgramsByCameraAsync(string cameraId, CancellationToken cancellationToken = default);
    Task<object> V1GetVisionLatestRunForCameraAsync(string cameraId, CancellationToken cancellationToken = default);
    Task<object> V1GetVisionRunsAsync(CancellationToken cancellationToken = default);
    Task<object> V1GetVisionRunsByCameraAsync(string cameraId, CancellationToken cancellationToken = default);
    Task<object> V1GetVisionRunsByCameraAndProgramAsync(string cameraId, string programId, long startedAtMs, CancellationToken cancellationToken = default);
    Task<object> V1ListMachinesAsync(object request, CancellationToken cancellationToken = default);
    Task<object> V1GetMachineHistoryAsync(object request, CancellationToken cancellationToken = default);
}
