namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Industrial;

/// <summary>
/// Client for Samsara industrial/IoT data.
/// </summary>
public interface IIndustrialClient
{
    /// <summary>
    /// List industrial assets (<c>GET /industrial/assets</c>).
    /// </summary>
    IAsyncEnumerable<IndustrialAsset> ListAssetsAsync(
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List industrial data inputs (<c>GET /industrial/data-inputs</c>).
    /// </summary>
    IAsyncEnumerable<DataInput> ListDataInputsAsync(
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a single data input by id (uses <c>GET /industrial/data-inputs?ids=…</c>).
    /// The <paramref name="after"/> and <paramref name="limit"/> parameters mirror
    /// the spec's pagination knobs on the underlying list endpoint.
    /// </summary>
    Task<DataInput> GetDataInputAsync(
        string id,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Snapshot of the most recent data points for data inputs
    /// (<c>GET /industrial/data-inputs/data-points</c>).
    /// </summary>
    IAsyncEnumerable<DataInputDataPoint> GetDataInputSnapshotAsync(
        IReadOnlyList<string>? dataInputIds = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Real-time feed of data points for data inputs
    /// (<c>GET /industrial/data-inputs/data-points/feed</c>).
    /// </summary>
    IAsyncEnumerable<DataInputDataPoint> GetDataInputFeedAsync(
        IReadOnlyList<string>? dataInputIds = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Historical data points for data inputs
    /// (<c>GET /industrial/data-inputs/data-points/history</c>).
    /// </summary>
    IAsyncEnumerable<DataInputDataPoint> GetDataInputHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? dataInputIds = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default);

    Task<IndustrialAsset> CreateAssetAsync(object request, CancellationToken cancellationToken = default);
    Task<IndustrialAsset> UpdateAssetAsync(string id, object request, CancellationToken cancellationToken = default);
    Task<IndustrialAsset> UpdateAssetDataOutputsAsync(string id, object request, CancellationToken cancellationToken = default);
    Task DeleteAssetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>List vision cameras (v1).</summary>
    Task<object> V1ListCamerasAsync(CancellationToken cancellationToken = default);
    Task<object> V1GetVisionProgramsByCameraAsync(string cameraId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Latest vision run for a camera (<c>GET /v1/industrial/vision/run/camera/{camera_id}</c>).
    /// </summary>
    Task<object> V1GetVisionLatestRunForCameraAsync(
        string cameraId,
        long? programId = null,
        long? startedAtMs = null,
        string? include = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List vision runs (<c>GET /v1/industrial/vision/runs</c>). <paramref name="durationMs"/>
    /// is spec-required.
    /// </summary>
    Task<object> V1GetVisionRunsAsync(
        long durationMs,
        long? endMs = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vision runs filtered to a single camera
    /// (<c>GET /v1/industrial/vision/runs/{camera_id}</c>). <paramref name="durationMs"/>
    /// is spec-required.
    /// </summary>
    Task<object> V1GetVisionRunsByCameraAsync(
        string cameraId,
        long durationMs,
        long? endMs = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vision runs filtered to a single camera + program at a start ms timestamp.
    /// </summary>
    Task<object> V1GetVisionRunsByCameraAndProgramAsync(
        string cameraId,
        string programId,
        long startedAtMs,
        string? include = null,
        CancellationToken cancellationToken = default);

    Task<object> V1ListMachinesAsync(object request, CancellationToken cancellationToken = default);
    Task<object> V1GetMachineHistoryAsync(object request, CancellationToken cancellationToken = default);
}
