namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Organization;

internal sealed class UsersClient : SamsaraServiceClientBase, IUsersClient
{
    private const string BasePath = "users";

    public UsersClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<User> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<User>(BasePath, cancellationToken: cancellationToken);

    public Task<User> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<User>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<User> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<User>(BasePath, request, cancellationToken);

    public Task<User> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<User>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
