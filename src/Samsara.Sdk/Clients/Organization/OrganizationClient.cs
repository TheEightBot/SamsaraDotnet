namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Organization;

internal sealed class OrganizationClient : SamsaraServiceClientBase, IOrganizationClient
{
    public OrganizationClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public Task<OrganizationInfo> GetAsync(CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<OrganizationInfo>("me", cancellationToken);
}
