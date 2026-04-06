namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Organization;

/// <summary>
/// Client for managing Samsara users.
/// </summary>
public interface IUsersClient
{
    IAsyncEnumerable<User> ListAsync(CancellationToken cancellationToken = default);
    Task<User> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
