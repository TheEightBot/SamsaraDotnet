namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Compliance;

/// <summary>
/// Client for Samsara IFTA reporting data.
/// </summary>
public interface IIftaClient
{
    IAsyncEnumerable<IftaDetail> ListDetailsAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<IftaSummary>> GetSummaryAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
}
