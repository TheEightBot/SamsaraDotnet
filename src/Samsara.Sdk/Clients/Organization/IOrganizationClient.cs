namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Organization;

/// <summary>
/// Client for Samsara organization information.
/// </summary>
public interface IOrganizationClient
{
    Task<OrganizationInfo> GetAsync(CancellationToken cancellationToken = default);
}
