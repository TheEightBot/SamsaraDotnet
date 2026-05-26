namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;

/// <summary>Beta — Custom reports (configs, datasets, runs). Subject to change.</summary>
public interface IReportsClient
{
    /// <summary>Available report configurations (<c>GET /reports/configs</c>).</summary>
    Task<object> ListConfigsAsync(CancellationToken cancellationToken = default);

    /// <summary>Available report datasets (<c>GET /reports/datasets</c>).</summary>
    Task<object> ListDatasetsAsync(CancellationToken cancellationToken = default);

    /// <summary>List report runs (<c>GET /reports/runs</c>).</summary>
    IAsyncEnumerable<object> ListRunsAsync(CancellationToken cancellationToken = default);

    /// <summary>Trigger a new report run (<c>POST /reports/runs</c>).</summary>
    Task<object> CreateRunAsync(object request, CancellationToken cancellationToken = default);

    /// <summary>Data for a completed report run (<c>GET /reports/runs/data</c>).</summary>
    Task<object> GetRunDataAsync(string runId, CancellationToken cancellationToken = default);
}

internal sealed class ReportsClient : SamsaraServiceClientBase, IReportsClient
{
    public ReportsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<object> ListConfigsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("reports/configs", cancellationToken);

    public Task<object> ListDatasetsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>("reports/datasets", cancellationToken);

    public IAsyncEnumerable<object> ListRunsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<object>("reports/runs", cancellationToken: cancellationToken);

    public Task<object> CreateRunAsync(object request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<object>("reports/runs", request, cancellationToken);

    public Task<object> GetRunDataAsync(string runId, CancellationToken cancellationToken = default)
        => HttpClient.GetAsync<object>(QueryBuilder.WithParams("reports/runs/data", ("runId", runId)), cancellationToken);
}
