namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

internal sealed class IndustrialClient : SamsaraServiceClientBase, IIndustrialClient
{
    public IndustrialClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<IndustrialAsset> ListAssetsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<IndustrialAsset>("industrial/assets", cancellationToken: cancellationToken);

    public IAsyncEnumerable<DataInput> ListDataInputsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DataInput>("industrial/data-inputs", cancellationToken: cancellationToken);

    public Task<DataInput> GetDataInputAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DataInput>($"industrial/data-inputs?ids={Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputSnapshotAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DataInputDataPoint>("industrial/data-inputs/data-points", cancellationToken: cancellationToken);

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputFeedAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DataInputDataPoint>("industrial/data-inputs/data-points/feed", cancellationToken: cancellationToken);

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputHistoryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<DataInputDataPoint>(QueryBuilder.WithTimeRange("industrial/data-inputs/data-points/history", startTime, endTime), cancellationToken: cancellationToken);

    // ── Industrial assets CRUD (spec adds POST/PATCH/DELETE) ──────────────────

    public Task<IndustrialAsset> CreateAssetAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<IndustrialAsset>("industrial/assets", request, cancellationToken);

    public Task<IndustrialAsset> UpdateAssetAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IndustrialAsset>($"industrial/assets/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task<IndustrialAsset> UpdateAssetDataOutputsAsync(string id, object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IndustrialAsset>($"industrial/assets/{Uri.EscapeDataString(id)}/data-outputs", request, cancellationToken);

    public Task DeleteAssetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"industrial/assets/{Uri.EscapeDataString(id)}", cancellationToken);

    // ── v1 Vision API ────────────────────────────────────────────────────────

    /// <summary>List vision cameras (<c>GET /v1/industrial/vision/cameras</c>).</summary>
    public Task<object> V1ListCamerasAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("v1/industrial/vision/cameras", cancellationToken);

    /// <summary>Programs for a vision camera (<c>GET /v1/industrial/vision/cameras/{cameraId}/programs</c>).</summary>
    public Task<object> V1GetVisionProgramsByCameraAsync(string cameraId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/industrial/vision/cameras/{Uri.EscapeDataString(cameraId)}/programs", cancellationToken);

    /// <summary>Latest vision run for a camera (<c>GET /v1/industrial/vision/run/camera/{cameraId}</c>).</summary>
    public Task<object> V1GetVisionLatestRunForCameraAsync(string cameraId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/industrial/vision/run/camera/{Uri.EscapeDataString(cameraId)}", cancellationToken);

    /// <summary>List vision runs (<c>GET /v1/industrial/vision/runs</c>).</summary>
    public Task<object> V1GetVisionRunsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("v1/industrial/vision/runs", cancellationToken);

    /// <summary>Vision runs filtered to a single camera (<c>GET /v1/industrial/vision/runs/{cameraId}</c>).</summary>
    public Task<object> V1GetVisionRunsByCameraAsync(string cameraId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"v1/industrial/vision/runs/{Uri.EscapeDataString(cameraId)}", cancellationToken);

    /// <summary>Vision runs filtered to a single camera + program at a start ms timestamp.</summary>
    public Task<object> V1GetVisionRunsByCameraAndProgramAsync(string cameraId, string programId, long startedAtMs, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            $"v1/industrial/vision/runs/{Uri.EscapeDataString(cameraId)}/{Uri.EscapeDataString(programId)}/{startedAtMs}",
            cancellationToken);

    // ── v1 Machines API ──────────────────────────────────────────────────────

    /// <summary>List industrial machines (<c>POST /v1/machines/list</c>).</summary>
    public Task<object> V1ListMachinesAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("v1/machines/list", request, cancellationToken);

    /// <summary>Industrial machine history (<c>POST /v1/machines/history</c>).</summary>
    public Task<object> V1GetMachineHistoryAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("v1/machines/history", request, cancellationToken);
}
