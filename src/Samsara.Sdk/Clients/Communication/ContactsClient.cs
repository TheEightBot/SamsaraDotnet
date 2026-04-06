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
}
