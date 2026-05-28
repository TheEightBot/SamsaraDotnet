namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Organization;

/// <summary>Client for Samsara organization settings.</summary>
public interface ISettingsClient
{
    Task<ComplianceSettings> GetComplianceSettingsAsync(CancellationToken cancellationToken = default);
    Task<ComplianceSettings> UpdateComplianceSettingsAsync(UpdateComplianceSettingsRequest request, CancellationToken cancellationToken = default);
    Task<DriverAppSettings> GetDriverAppSettingsAsync(CancellationToken cancellationToken = default);
    Task<DriverAppSettings> UpdateDriverAppSettingsAsync(UpdateDriverAppSettingsRequest request, CancellationToken cancellationToken = default);
    Task<SafetySettings> GetSafetySettingsAsync(CancellationToken cancellationToken = default);
}
