namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Drivers;

/// <summary>
/// Client for managing Samsara drivers.
/// </summary>
public interface IDriversClient
{
    /// <summary>List all drivers (<c>GET /fleet/drivers</c>).</summary>
    /// <param name="driverActivationStatus">Optional. <c>active</c> or
    /// <c>deactivated</c>; defaults to <c>active</c> server-side when omitted.</param>
    /// <param name="parentTagIds">Optional. Filter by descendant tags under the
    /// supplied parent tag IDs.</param>
    /// <param name="tagIds">Optional. Filter by tag IDs.</param>
    /// <param name="attributeValueIds">Optional. Filter by attribute value IDs.
    /// Only entities matching ALL supplied values are returned.</param>
    /// <param name="attributes">Optional. Filter by name-value pairs (e.g.,
    /// <c>"AttrName:value"</c>) or numeric range queries (e.g.,
    /// <c>"AttrName:range(10,20)"</c>). Only entities matching ALL supplied
    /// values are returned.</param>
    /// <param name="updatedAfterTime">Optional. RFC 3339 lower bound on
    /// <c>updatedAtTime</c>.</param>
    /// <param name="createdAfterTime">Optional. RFC 3339 lower bound on
    /// <c>createdAtTime</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<Driver> ListAsync(
        string? driverActivationStatus = null,
        IReadOnlyList<string>? parentTagIds = null,
        IReadOnlyList<string>? tagIds = null,
        IReadOnlyList<string>? attributeValueIds = null,
        IReadOnlyList<string>? attributes = null,
        string? updatedAfterTime = null,
        string? createdAfterTime = null,
        CancellationToken cancellationToken = default);
    Task<Driver> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Driver> CreateAsync(CreateDriverRequest request, CancellationToken cancellationToken = default);
    Task<Driver> UpdateAsync(string id, UpdateDriverRequest request, CancellationToken cancellationToken = default);
    Task RemoteSignOutAsync(RemoteSignOutRequest request, CancellationToken cancellationToken = default);
    Task<DriverAuthToken> CreateAuthTokenAsync(CreateDriverAuthTokenRequest request, CancellationToken cancellationToken = default);
    /// <summary>Get driver QR codes (<c>GET /drivers/qr-codes</c>) — required <paramref name="driverIds"/>.</summary>
    IAsyncEnumerable<DriverQrCode> ListQrCodesAsync(
        IReadOnlyList<string> driverIds,
        CancellationToken cancellationToken = default);
    Task<DriverQrCode> CreateQrCodeAsync(CreateDriverQrCodeRequest request, CancellationToken cancellationToken = default);
    Task DeleteQrCodeAsync(string driverId, CancellationToken cancellationToken = default);
    /// <summary>List driver workflows (beta).</summary>
    IAsyncEnumerable<object> ListWorkflowsAsync(
        string? workflowType = null,
        CancellationToken cancellationToken = default);
    Task<object> CreateWorkflowAssignmentAsync(object request, CancellationToken cancellationToken = default);
    Task<object> ResolveVoiceSignInAssignmentAsync(object request, CancellationToken cancellationToken = default);
}
