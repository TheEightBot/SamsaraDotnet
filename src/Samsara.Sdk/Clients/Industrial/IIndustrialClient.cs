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

    /// <summary>Create an industrial asset (<c>POST /industrial/assets</c>).</summary>
    Task<IndustrialAsset> CreateAssetAsync(CreateIndustrialAssetRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update an industrial asset (<c>PATCH /industrial/assets/{id}</c>).</summary>
    Task<IndustrialAsset> UpdateAssetAsync(string id, UpdateIndustrialAssetRequest request, CancellationToken cancellationToken = default);

    /// <summary>Write values to an asset's data outputs (<c>PATCH /industrial/assets/{id}/data-outputs</c>).</summary>
    Task<IndustrialAsset> UpdateAssetDataOutputsAsync(string id, UpdateIndustrialAssetDataOutputsRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete an industrial asset (<c>DELETE /industrial/assets/{id}</c>).</summary>
    Task DeleteAssetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>List vision cameras (v1). The v1 body is a bare JSON array.</summary>
    Task<IReadOnlyList<V1VisionCamera>> V1ListCamerasAsync(CancellationToken cancellationToken = default);

    /// <summary>Programs configured on a vision camera (v1). The v1 body is a bare JSON array.</summary>
    Task<IReadOnlyList<V1VisionProgram>> V1GetVisionProgramsByCameraAsync(string cameraId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Latest vision run for a camera (<c>GET /v1/industrial/vision/run/camera/{camera_id}</c>).
    /// </summary>
    Task<V1VisionLatestRun> V1GetVisionLatestRunForCameraAsync(
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
    Task<V1VisionRunsResponse> V1GetVisionRunsAsync(
        long durationMs,
        long? endMs = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vision runs filtered to a single camera
    /// (<c>GET /v1/industrial/vision/runs/{camera_id}</c>). <paramref name="durationMs"/>
    /// is spec-required. The v1 body is a bare JSON array.
    /// </summary>
    Task<IReadOnlyList<V1VisionCameraRun>> V1GetVisionRunsByCameraAsync(
        string cameraId,
        long durationMs,
        long? endMs = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Vision runs filtered to a single camera + program at a start ms timestamp.
    /// </summary>
    Task<V1VisionProgramRun> V1GetVisionRunsByCameraAndProgramAsync(
        string cameraId,
        string programId,
        long startedAtMs,
        string? include = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List industrial machines (<c>POST /v1/machines/list</c>). The spec defines
    /// no request body for this operation, so <paramref name="request"/> stays
    /// untyped pending a live-API check.
    /// </summary>
    Task<V1MachineListResponse> V1ListMachinesAsync(object request, CancellationToken cancellationToken = default);

    /// <summary>Industrial machine history (<c>POST /v1/machines/history</c>).</summary>
    Task<V1MachineHistoryResponse> V1GetMachineHistoryAsync(V1MachineHistoryRequest request, CancellationToken cancellationToken = default);
}
