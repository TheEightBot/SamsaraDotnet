namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>Client for managing Samsara Live Sharing Links.</summary>
public interface ILiveSharingLinksClient
{
    /// <summary>
    /// List Live Sharing Links. Both <paramref name="ids"/> and
    /// <paramref name="type"/> are optional spec filters.
    /// </summary>
    /// <param name="ids">Optional filter by Live Sharing Link IDs.</param>
    /// <param name="type">
    /// Optional filter by link type. Valid values: <c>all</c>,
    /// <c>assetsLocation</c>, <c>assetsNearLocation</c>, <c>assetsOnRoute</c>.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<LiveSharingLink> ListAsync(
        IReadOnlyList<string>? ids = null,
        string? type = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a Live Sharing Link.</summary>
    Task<LiveSharingLink> CreateAsync(CreateLiveSharingLinkRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a non-expired Live Sharing Link. <paramref name="id"/> is the
    /// spec-required query parameter identifying the link.
    /// </summary>
    Task<LiveSharingLink> UpdateAsync(string id, UpdateLiveSharingLinkRequest request, CancellationToken cancellationToken = default);

    /// <summary>Delete a non-expired Live Sharing Link.</summary>
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
