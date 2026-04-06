namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Organization;

internal sealed class UserRolesClient : SamsaraServiceClientBase, IUserRolesClient
{
    private const string BasePath = "user-roles";

    public UserRolesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<UserRole> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<UserRole>(BasePath, cancellationToken: cancellationToken);
}
