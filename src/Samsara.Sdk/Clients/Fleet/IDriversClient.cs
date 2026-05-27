namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Drivers;

/// <summary>
/// Client for managing Samsara drivers.
/// </summary>
public interface IDriversClient
{
    IAsyncEnumerable<Driver> ListAsync(CancellationToken cancellationToken = default);
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
