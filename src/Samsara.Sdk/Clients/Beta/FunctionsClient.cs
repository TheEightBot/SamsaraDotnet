namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>
/// Beta — Samsara Functions (<c>/functions/*</c>) and Functions storage
/// (<c>/functions-storage/*</c>). Subject to change.
/// </summary>
public interface IFunctionsClient
{
    /// <summary>Get a Function by name (<c>GET /functions/{name}</c>).</summary>
    Task<FunctionDetail> GetAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Create a Function (<c>POST /functions</c>).</summary>
    Task<FunctionCreateDetail> CreateAsync(CreateFunctionRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update a Function (<c>PATCH /functions/{name}</c>).</summary>
    Task<FunctionUpdateDetail> UpdateAsync(string name, UpdateFunctionRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete a Function (<c>DELETE /functions/{name}</c>).</summary>
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deploy the uploaded code package of a Function (<c>POST /functions/{name}/deploy</c>).
    /// The spec defines no request body for this operation.
    /// </summary>
    Task<FunctionDeployResult> DeployAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Start a Function run (<c>POST /functions/{name}/runs</c>).</summary>
    Task<FunctionRunStarted> StartRunAsync(string name, StartFunctionRunRequest request, CancellationToken cancellationToken = default);

    /// <summary>Get a single Function run (<c>GET /functions/{name}/runs/{correlationId}</c>).</summary>
    Task<FunctionRun> GetRunAsync(string name, string correlationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a page of Function log entries (<c>GET /functions/{name}/logs</c>). Pass the
    /// previous page's end cursor as <paramref name="after"/> to page forward.
    /// </summary>
    Task<IReadOnlyList<FunctionLogEntry>> GetLogsAsync(
        string name,
        string startTime,
        string endTime,
        string? after = null,
        int? limit = null,
        string? filterText = null,
        CancellationToken cancellationToken = default);

    // Functions storage

    /// <summary>
    /// List a page of Functions storage files (<c>GET /functions-storage/ls</c>). Pass the
    /// previous page's end cursor as <paramref name="after"/> to page forward.
    /// </summary>
    Task<IReadOnlyList<FunctionStorageFile>> ListStorageFilesAsync(
        string? after = null,
        int? limit = null,
        bool? includeDownloadUrls = null,
        bool? includeUploadUrls = null,
        CancellationToken cancellationToken = default);

    /// <summary>Get a Functions storage file with a presigned download URL (<c>GET /functions-storage/files</c>).</summary>
    Task<FunctionStorageFileDetail> GetStorageFileAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Create a Functions storage file (<c>POST /functions-storage/files</c>).</summary>
    Task<FunctionStorageFileCreated> CreateStorageFileAsync(CreateFunctionStorageFileRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Request a presigned URL for overwriting a Functions storage file
    /// (<c>PUT /functions-storage/files</c>). The spec defines no request body for this
    /// operation; the target file is identified by the <c>name</c> query parameter.
    /// </summary>
    Task<FunctionStorageFileUpdated> UpdateStorageFileAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>Delete a Functions storage file (<c>DELETE /functions-storage/files</c>).</summary>
    Task DeleteStorageFileAsync(string name, CancellationToken cancellationToken = default);
}

internal sealed class FunctionsClient : SamsaraServiceClientBase, IFunctionsClient
{
    public FunctionsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<FunctionDetail> GetAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FunctionDetail>($"functions/{Uri.EscapeDataString(name)}", cancellationToken);

    public Task<FunctionCreateDetail> CreateAsync(CreateFunctionRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FunctionCreateDetail>("functions", request, cancellationToken);

    public Task<FunctionUpdateDetail> UpdateAsync(string name, UpdateFunctionRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<FunctionUpdateDetail>($"functions/{Uri.EscapeDataString(name)}", request, cancellationToken);

    public Task DeleteAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"functions/{Uri.EscapeDataString(name)}", cancellationToken);

    public Task<FunctionDeployResult> DeployAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FunctionDeployResult>($"functions/{Uri.EscapeDataString(name)}/deploy", new { }, cancellationToken);

    public Task<FunctionRunStarted> StartRunAsync(string name, StartFunctionRunRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FunctionRunStarted>($"functions/{Uri.EscapeDataString(name)}/runs", request, cancellationToken);

    public Task<FunctionRun> GetRunAsync(string name, string correlationId, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FunctionRun>($"functions/{Uri.EscapeDataString(name)}/runs/{Uri.EscapeDataString(correlationId)}", cancellationToken);

    public Task<IReadOnlyList<FunctionLogEntry>> GetLogsAsync(
        string name,
        string startTime,
        string endTime,
        string? after = null,
        int? limit = null,
        string? filterText = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<FunctionLogEntry>>(
            QueryBuilder.WithParams($"functions/{Uri.EscapeDataString(name)}/logs",
                ("startTime", startTime),
                ("endTime", endTime),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("filterText", filterText)),
            cancellationToken);

    public Task<IReadOnlyList<FunctionStorageFile>> ListStorageFilesAsync(
        string? after = null,
        int? limit = null,
        bool? includeDownloadUrls = null,
        bool? includeUploadUrls = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<FunctionStorageFile>>(
            QueryBuilder.WithParams("functions-storage/ls",
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("includeDownloadUrls", includeDownloadUrls?.ToString().ToLowerInvariant()),
                ("includeUploadUrls", includeUploadUrls?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<FunctionStorageFileDetail> GetStorageFileAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FunctionStorageFileDetail>(QueryBuilder.WithParams("functions-storage/files", ("name", name)), cancellationToken);

    public Task<FunctionStorageFileCreated> CreateStorageFileAsync(CreateFunctionStorageFileRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FunctionStorageFileCreated>("functions-storage/files", request, cancellationToken);

    public Task<FunctionStorageFileUpdated> UpdateStorageFileAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<FunctionStorageFileUpdated>(QueryBuilder.WithParams("functions-storage/files", ("name", name)), new { }, cancellationToken);

    public Task DeleteStorageFileAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams("functions-storage/files", ("name", name)), cancellationToken);
}
