namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Industrial;

/// <summary>
/// Client for the Samsara <b>v1</b> sensors API. All operations are POST under <c>/v1/sensors/*</c>.
/// </summary>
public interface ISensorsClient
{
    /// <summary>List all sensors (<c>POST /v1/sensors/list</c>).</summary>
    Task<V1SensorListResponse> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>Historical sensor data (<c>POST /v1/sensors/history</c>).</summary>
    Task<V1SensorHistoryResponse> GetHistoryAsync(V1SensorHistoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>Current cargo (red-eye) status for the given sensors.</summary>
    Task<V1SensorReadingsResponse<V1CargoReading>> GetCargoAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default);

    /// <summary>Current door status for the given sensors.</summary>
    Task<V1SensorReadingsResponse<V1DoorReading>> GetDoorAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default);

    /// <summary>Current humidity readings for the given sensors.</summary>
    Task<V1SensorReadingsResponse<V1HumidityReading>> GetHumidityAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default);

    /// <summary>Current temperature readings for the given sensors.</summary>
    Task<V1SensorReadingsResponse<V1TemperatureReading>> GetTemperatureAsync(V1SensorReadingsRequest request, CancellationToken cancellationToken = default);
}
