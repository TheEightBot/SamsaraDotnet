namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Communication;

internal sealed class ContactsClient : SamsaraServiceClientBase, IContactsClient
{
    private const string BasePath = "contacts";

    public ContactsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Contact> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Contact>(BasePath, cancellationToken: cancellationToken);

    public Task<Contact> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Contact>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Contact> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Contact>(BasePath, request, cancellationToken);

    public Task<Contact> UpdateAsync(string id, UpdateContactRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<Contact>($"{BasePath}/{Uri.EscapeDataString(id)}", request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);
}
