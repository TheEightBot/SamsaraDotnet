namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>Client for managing Samsara live sharing links.</summary>
public interface ILiveSharingLinksClient
{
    IAsyncEnumerable<LiveSharingLink> ListAsync(CancellationToken cancellationToken = default);
    Task<LiveSharingLink> CreateAsync(CreateLiveSharingLinkRequest request, CancellationToken cancellationToken = default);
    Task<LiveSharingLink> UpdateAsync(UpdateLiveSharingLinkRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
