namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Organization;

/// <summary>
/// Client for managing Samsara user roles.
/// </summary>
public interface IUserRolesClient
{
    IAsyncEnumerable<UserRole> ListAsync(CancellationToken cancellationToken = default);
}
