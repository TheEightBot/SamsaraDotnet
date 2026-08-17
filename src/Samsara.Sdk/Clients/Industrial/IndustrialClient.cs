namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

internal sealed class IndustrialClient : SamsaraServiceClientBase, IIndustrialClient
{
    public IndustrialClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<IndustrialAsset> ListAssetsAsync(
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "industrial/assets",
            ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
            ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
            ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)));

        return PaginateAsync<IndustrialAsset>(path, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<DataInput> ListDataInputsAsync(
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "industrial/data-inputs",
            ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
            ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
            ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)));

        return PaginateAsync<DataInput>(path, cancellationToken: cancellationToken);
    }

    public Task<DataInput> GetDataInputAsync(
        string id,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "industrial/data-inputs",
            ("ids", id),
            ("after", after),
            ("limit", limit?.ToString(CultureInfo.InvariantCulture)));
        return HttpClient.GetDataAsync<DataInput>(path, cancellationToken);
    }

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputSnapshotAsync(
        IReadOnlyList<string>? dataInputIds = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "industrial/data-inputs/data-points",
            ("dataInputIds", dataInputIds is null ? null : string.Join(",", dataInputIds)),
            ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
            ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
            ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)));

        return PaginateAsync<DataInputDataPoint>(path, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputFeedAsync(
        IReadOnlyList<string>? dataInputIds = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "industrial/data-inputs/data-points/feed",
            ("dataInputIds", dataInputIds is null ? null : string.Join(",", dataInputIds)),
            ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
            ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
            ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)));

        return PaginateAsync<DataInputDataPoint>(path, cancellationToken: cancellationToken);
    }

    public IAsyncEnumerable<DataInputDataPoint> GetDataInputHistoryAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? dataInputIds = null,
        IReadOnlyList<string>? assetIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? parentTagIds = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "industrial/data-inputs/data-points/history",
            ("startTime", startTime?.ToString("O")),
            ("endTime", endTime?.ToString("O")),
            ("dataInputIds", dataInputIds is null ? null : string.Join(",", dataInputIds)),
            ("assetIds", assetIds is null ? null : string.Join(",", assetIds)),
            ("tagIds", tagIds is null ? null : string.Join(",", tagIds)),
            ("parentTagIds", parentTagIds is null ? null : string.Join(",", parentTagIds)));

        return PaginateAsync<DataInputDataPoint>(path, cancellationToken: cancellationToken);
    }

    // ── Industrial assets CRUD (spec adds POST/PATCH/DELETE) ──────────────────

    public Task<IndustrialAsset> CreateAssetAsync(CreateIndustrialAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<IndustrialAsset>("industrial/assets", request, cancellationToken);

    public Task<IndustrialAsset> UpdateAssetAsync(string id, UpdateIndustrialAssetRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IndustrialAsset>($"industrial/assets/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task<IndustrialAsset> UpdateAssetDataOutputsAsync(string id, UpdateIndustrialAssetDataOutputsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<IndustrialAsset>($"industrial/assets/{Uri.EscapeDataString(id)}/data-outputs", request, cancellationToken);

    public Task DeleteAssetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"industrial/assets/{Uri.EscapeDataString(id)}", cancellationToken);

    // ── v1 Vision API ────────────────────────────────────────────────────────

    /// <summary>List vision cameras (<c>GET /v1/industrial/vision/cameras</c>). The v1 body is a bare JSON array.</summary>
    public Task<IReadOnlyList<V1VisionCamera>> V1ListCamerasAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<IReadOnlyList<V1VisionCamera>>("v1/industrial/vision/cameras", cancellationToken);

    /// <summary>Programs for a vision camera (<c>GET /v1/industrial/vision/cameras/{cameraId}/programs</c>). The v1 body is a bare JSON array.</summary>
    public Task<IReadOnlyList<V1VisionProgram>> V1GetVisionProgramsByCameraAsync(string cameraId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<IReadOnlyList<V1VisionProgram>>($"v1/industrial/vision/cameras/{Uri.EscapeDataString(cameraId)}/programs", cancellationToken);

    /// <summary>Latest vision run for a camera (<c>GET /v1/industrial/vision/run/camera/{cameraId}</c>).</summary>
    public Task<V1VisionLatestRun> V1GetVisionLatestRunForCameraAsync(
        string cameraId,
        long? programId = null,
        long? startedAtMs = null,
        string? include = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            $"v1/industrial/vision/run/camera/{Uri.EscapeDataString(cameraId)}",
            ("program_id", programId?.ToString(CultureInfo.InvariantCulture)),
            ("startedAtMs", startedAtMs?.ToString(CultureInfo.InvariantCulture)),
            ("include", include),
            ("limit", limit?.ToString(CultureInfo.InvariantCulture)));
        return HttpClient.GetAsync<V1VisionLatestRun>(path, cancellationToken);
    }

    /// <summary>List vision runs (<c>GET /v1/industrial/vision/runs</c>).</summary>
    public Task<V1VisionRunsResponse> V1GetVisionRunsAsync(
        long durationMs,
        long? endMs = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            "v1/industrial/vision/runs",
            ("durationMs", durationMs.ToString(CultureInfo.InvariantCulture)),
            ("endMs", endMs?.ToString(CultureInfo.InvariantCulture)));
        return HttpClient.GetAsync<V1VisionRunsResponse>(path, cancellationToken);
    }

    /// <summary>Vision runs filtered to a single camera (<c>GET /v1/industrial/vision/runs/{cameraId}</c>). The v1 body is a bare JSON array.</summary>
    public Task<IReadOnlyList<V1VisionCameraRun>> V1GetVisionRunsByCameraAsync(
        string cameraId,
        long durationMs,
        long? endMs = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            $"v1/industrial/vision/runs/{Uri.EscapeDataString(cameraId)}",
            ("durationMs", durationMs.ToString(CultureInfo.InvariantCulture)),
            ("endMs", endMs?.ToString(CultureInfo.InvariantCulture)));
        return HttpClient.GetAsync<IReadOnlyList<V1VisionCameraRun>>(path, cancellationToken);
    }

    /// <summary>Vision runs filtered to a single camera + program at a start ms timestamp.</summary>
    public Task<V1VisionProgramRun> V1GetVisionRunsByCameraAndProgramAsync(
        string cameraId,
        string programId,
        long startedAtMs,
        string? include = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams(
            $"v1/industrial/vision/runs/{Uri.EscapeDataString(cameraId)}/{Uri.EscapeDataString(programId)}/{startedAtMs.ToString(CultureInfo.InvariantCulture)}",
            ("include", include));
        return HttpClient.GetAsync<V1VisionProgramRun>(path, cancellationToken);
    }

    // ── v1 Machines API ──────────────────────────────────────────────────────

    /// <summary>
    /// List industrial machines (<c>POST /v1/machines/list</c>). The spec defines
    /// no request body for this operation, so <paramref name="request"/> stays
    /// untyped pending a live-API check.
    /// </summary>
    public Task<V1MachineListResponse> V1ListMachinesAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1MachineListResponse>("v1/machines/list", request, cancellationToken);

    /// <summary>Industrial machine history (<c>POST /v1/machines/history</c>).</summary>
    public Task<V1MachineHistoryResponse> V1GetMachineHistoryAsync(V1MachineHistoryRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1MachineHistoryResponse>("v1/machines/history", request, cancellationToken);
}
