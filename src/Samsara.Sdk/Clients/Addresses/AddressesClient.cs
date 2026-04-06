namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Addresses;

internal sealed class AddressesClient : SamsaraServiceClientBase, IAddressesClient
{
    private const string BasePath = "addresses";

    public AddressesClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Address> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Address>(BasePath, cancellationToken: cancellationToken);

    public Task<Address> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Address>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Address> CreateAsync(CreateAddressRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Address>(BasePath, request, cancellationToken);

    public Task<Address> UpdateAsync(string id, UpdateAddressRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Address>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
