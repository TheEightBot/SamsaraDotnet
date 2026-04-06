namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Industrial;

/// <summary>
/// Client for Samsara sensors.
/// </summary>
public interface ISensorsClient
{
    IAsyncEnumerable<Sensor> ListAsync(CancellationToken cancellationToken = default);
    Task<Sensor> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SensorHistoryResult>> GetHistoryAsync(IReadOnlyList<string> sensorIds, CancellationToken cancellationToken = default);
}
