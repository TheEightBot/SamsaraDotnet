namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;

/// <summary>
/// Beta — Samsara Functions (<c>/functions/*</c>) and Functions storage
/// (<c>/functions-storage/*</c>). Subject to change.
/// </summary>
public interface IFunctionsClient
{
    Task<object> GetAsync(string name, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object request, CancellationToken cancellationToken = default);
    Task<object> UpdateAsync(string name, object request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string name, CancellationToken cancellationToken = default);

    Task<object> DeployAsync(string name, object request, CancellationToken cancellationToken = default);
    Task<object> StartRunAsync(string name, object request, CancellationToken cancellationToken = default);
    Task<object> GetRunAsync(string name, string correlationId, CancellationToken cancellationToken = default);
    Task<object> GetLogsAsync(
        string name,
        string startTime,
        string endTime,
        string? after = null,
        int? limit = null,
        string? filterText = null,
        CancellationToken cancellationToken = default);

    // Functions storage
    Task<object> ListStorageFilesAsync(
        string? after = null,
        int? limit = null,
        bool? includeDownloadUrls = null,
        bool? includeUploadUrls = null,
        CancellationToken cancellationToken = default);
    Task<object> GetStorageFileAsync(string name, CancellationToken cancellationToken = default);
    Task<object> CreateStorageFileAsync(object request, CancellationToken cancellationToken = default);
    Task<object> UpdateStorageFileAsync(string name, object request, CancellationToken cancellationToken = default);
    Task DeleteStorageFileAsync(string name, CancellationToken cancellationToken = default);
}

internal sealed class FunctionsClient : SamsaraServiceClientBase, IFunctionsClient
{
    public FunctionsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<object> GetAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"functions/{Uri.EscapeDataString(name)}", cancellationToken);

    public Task<object> CreateAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("functions", request, cancellationToken);

    public Task<object> UpdateAsync(string name, object request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<object>($"functions/{Uri.EscapeDataString(name)}", request, cancellationToken);

    public Task DeleteAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"functions/{Uri.EscapeDataString(name)}", cancellationToken);

    public Task<object> DeployAsync(string name, object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>($"functions/{Uri.EscapeDataString(name)}/deploy", request, cancellationToken);

    public Task<object> StartRunAsync(string name, object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>($"functions/{Uri.EscapeDataString(name)}/runs", request, cancellationToken);

    public Task<object> GetRunAsync(string name, string correlationId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>($"functions/{Uri.EscapeDataString(name)}/runs/{Uri.EscapeDataString(correlationId)}", cancellationToken);

    public Task<object> GetLogsAsync(
        string name,
        string startTime,
        string endTime,
        string? after = null,
        int? limit = null,
        string? filterText = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams($"functions/{Uri.EscapeDataString(name)}/logs",
                ("startTime", startTime),
                ("endTime", endTime),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("filterText", filterText)),
            cancellationToken);

    public Task<object> ListStorageFilesAsync(
        string? after = null,
        int? limit = null,
        bool? includeDownloadUrls = null,
        bool? includeUploadUrls = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("functions-storage/ls",
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("includeDownloadUrls", includeDownloadUrls?.ToString().ToLowerInvariant()),
                ("includeUploadUrls", includeUploadUrls?.ToString().ToLowerInvariant())),
            cancellationToken);

    public Task<object> GetStorageFileAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(QueryBuilder.WithParams("functions-storage/files", ("name", name)), cancellationToken);

    public Task<object> CreateStorageFileAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("functions-storage/files", request, cancellationToken);

    public Task<object> UpdateStorageFileAsync(string name, object request, CancellationToken cancellationToken = default)
        => HttpClient.PutDataAsync<object>(QueryBuilder.WithParams("functions-storage/files", ("name", name)), request, cancellationToken);

    public Task DeleteStorageFileAsync(string name, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync(QueryBuilder.WithParams("functions-storage/files", ("name", name)), cancellationToken);
}
