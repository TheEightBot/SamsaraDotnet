namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Beta;

/// <summary>Beta — Custom reports (configs, datasets, runs). Subject to change.</summary>
public interface IReportsClient
{
    /// <summary>
    /// Available report configurations (<c>GET /reports/configs</c>), one page at a time.
    /// Pass the previous page's cursor as <paramref name="after"/> to advance.
    /// </summary>
    Task<IReadOnlyList<ReportConfig>> ListConfigsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Available report datasets (<c>GET /reports/datasets</c>), one page at a time.
    /// Pass the previous page's cursor as <paramref name="after"/> to advance.
    /// </summary>
    Task<IReadOnlyList<ReportDataset>> ListDatasetsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>List report runs (<c>GET /reports/runs</c>).</summary>
    IAsyncEnumerable<ReportRun> ListRunsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? reportConfigIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Trigger a new report run (<c>POST /reports/runs</c>).</summary>
    Task<ReportRun> CreateRunAsync(CreateReportRunRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Data for a completed report run (<c>GET /reports/runs/data</c>) — required <paramref name="id"/>.
    /// </summary>
    Task<ReportRunData> GetRunDataAsync(
        string id,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);
}

internal sealed class ReportsClient : SamsaraServiceClientBase, IReportsClient
{
    public ReportsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<IReadOnlyList<ReportConfig>> ListConfigsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<ReportConfig>>(
            QueryBuilder.WithParams("reports/configs",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    public Task<IReadOnlyList<ReportDataset>> ListDatasetsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<IReadOnlyList<ReportDataset>>(
            QueryBuilder.WithParams("reports/datasets",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    public IAsyncEnumerable<ReportRun> ListRunsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? reportConfigIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<ReportRun>(
            QueryBuilder.WithParams("reports/runs",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("reportConfigIds", reportConfigIds is null ? null : string.Join(",", reportConfigIds))),
            cancellationToken: cancellationToken);

    public Task<ReportRun> CreateRunAsync(CreateReportRunRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<ReportRun>("reports/runs", request, cancellationToken);

    public Task<ReportRunData> GetRunDataAsync(
        string id,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<ReportRunData>(
            QueryBuilder.WithParams("reports/runs/data",
                ("id", id),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);
}
