namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

internal sealed class SensorsClient : SamsaraServiceClientBase, ISensorsClient
{
    private const string BasePath = "sensors";

    public SensorsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Sensor> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Sensor>(BasePath, cancellationToken: cancellationToken);

    public Task<Sensor> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Sensor>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<IReadOnlyList<SensorHistoryResult>> GetHistoryAsync(IReadOnlyList<string> sensorIds, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<IReadOnlyList<SensorHistoryResult>>($"{BasePath}/history", new { sensors = sensorIds }, cancellationToken);
}
