namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Industrial;

/// <summary>
/// Client for the Samsara <b>v1</b> sensors API. All operations are POST under <c>/v1/sensors/*</c>.
/// </summary>
internal sealed class SensorsClient : SamsaraServiceClientBase, ISensorsClient
{
    public SensorsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<V1SensorListResponse> ListAsync(CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1SensorListResponse>("v1/sensors/list", new { }, cancellationToken);

    public Task<V1SensorHistoryResponse> GetHistoryAsync(V1SensorHistoryRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1SensorHistoryResponse>("v1/sensors/history", request, cancellationToken);

    public Task<V1SensorReadingsResponse<V1CargoReading>> GetCargoAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1SensorReadingsResponse<V1CargoReading>>("v1/sensors/cargo", request, cancellationToken);

    public Task<V1SensorReadingsResponse<V1DoorReading>> GetDoorAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1SensorReadingsResponse<V1DoorReading>>("v1/sensors/door", request, cancellationToken);

    public Task<V1SensorReadingsResponse<V1HumidityReading>> GetHumidityAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1SensorReadingsResponse<V1HumidityReading>>("v1/sensors/humidity", request, cancellationToken);

    public Task<V1SensorReadingsResponse<V1TemperatureReading>> GetTemperatureAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync<V1SensorReadingsResponse<V1TemperatureReading>>("v1/sensors/temperature", request, cancellationToken);
}
