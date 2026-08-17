namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Fleet;

/// <summary>
/// Client for listing Samsara gateways.
/// </summary>
public interface IGatewaysClient
{
    /// <summary>
    /// Lists all activated gateways.
    /// </summary>
    /// <param name="models">
    /// Optional comma-separated list of gateway models to filter on (e.g.
    /// <c>VG34</c>, <c>AG46</c>). When <c>null</c> the filter is omitted.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<Gateway> ListAsync(
        IReadOnlyList<string>? models = null,
        CancellationToken cancellationToken = default);

    Task<Gateway> CreateAsync(CreateGatewayRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pair gateways to devices (<c>POST /gateways/pair</c>) — beta. Replaces the
    /// removed <c>POST /preview/gateways/pair</c>. The response wraps one result
    /// per requested pairing in a <c>{ data: [...] }</c> envelope, which the SDK
    /// unwraps.
    /// </summary>
    Task<IReadOnlyList<GatewayPairResult>> PairGatewaysAsync(PairGatewaysRequest request, CancellationToken cancellationToken = default);
}
