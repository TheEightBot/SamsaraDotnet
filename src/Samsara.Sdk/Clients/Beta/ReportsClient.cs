namespace Samsara.Sdk.Clients;

using System.Globalization;
using Samsara.Sdk.Http;

/// <summary>Beta — Custom reports (configs, datasets, runs). Subject to change.</summary>
public interface IReportsClient
{
    /// <summary>Available report configurations (<c>GET /reports/configs</c>).</summary>
    Task<object> ListConfigsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>Available report datasets (<c>GET /reports/datasets</c>).</summary>
    Task<object> ListDatasetsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);

    /// <summary>List report runs (<c>GET /reports/runs</c>).</summary>
    IAsyncEnumerable<object> ListRunsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? reportConfigIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Trigger a new report run (<c>POST /reports/runs</c>).</summary>
    Task<object> CreateRunAsync(object request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Data for a completed report run (<c>GET /reports/runs/data</c>) — required <paramref name="id"/>.
    /// </summary>
    Task<object> GetRunDataAsync(
        string id,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default);
}

internal sealed class ReportsClient : SamsaraServiceClientBase, IReportsClient
{
    public ReportsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<object> ListConfigsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("reports/configs",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    public Task<object> ListDatasetsAsync(
        IReadOnlyList<string>? ids = null,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("reports/datasets",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);

    public IAsyncEnumerable<object> ListRunsAsync(
        IReadOnlyList<string>? ids = null,
        IReadOnlyList<string>? reportConfigIds = null,
        CancellationToken cancellationToken = default)
        => PaginateAsync<object>(
            QueryBuilder.WithParams("reports/runs",
                ("ids", ids is null ? null : string.Join(",", ids)),
                ("reportConfigIds", reportConfigIds is null ? null : string.Join(",", reportConfigIds))),
            cancellationToken: cancellationToken);

    public Task<object> CreateRunAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("reports/runs", request, cancellationToken);

    public Task<object> GetRunDataAsync(
        string id,
        string? after = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(
            QueryBuilder.WithParams("reports/runs/data",
                ("id", id),
                ("after", after),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture))),
            cancellationToken);
}
