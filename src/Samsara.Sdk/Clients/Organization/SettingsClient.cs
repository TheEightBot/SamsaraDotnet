namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Organization;

internal sealed class SettingsClient : SamsaraServiceClientBase, ISettingsClient
{
    public SettingsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<ComplianceSettings> GetComplianceSettingsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<ComplianceSettings>("fleet/settings/compliance", cancellationToken);

    public Task<ComplianceSettings> UpdateComplianceSettingsAsync(UpdateComplianceSettingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<ComplianceSettings>("fleet/settings/compliance", request, cancellationToken);

    public Task<DriverAppSettings> GetDriverAppSettingsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<DriverAppSettings>("fleet/settings/driver-app", cancellationToken);

    public Task<DriverAppSettings> UpdateDriverAppSettingsAsync(UpdateDriverAppSettingsRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<DriverAppSettings>("fleet/settings/driver-app", request, cancellationToken);

    public Task<SafetySettings> GetSafetySettingsAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<SafetySettings>("fleet/settings/safety", cancellationToken);
}
