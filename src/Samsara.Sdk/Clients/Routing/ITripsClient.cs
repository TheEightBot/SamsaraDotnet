namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Routes;

/// <summary>
/// Client for retrieving Samsara trip data.
/// </summary>
public interface ITripsClient
{
    IAsyncEnumerable<Trip> ListAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, string? vehicleId = null, string? driverId = null, CancellationToken cancellationToken = default);
}
