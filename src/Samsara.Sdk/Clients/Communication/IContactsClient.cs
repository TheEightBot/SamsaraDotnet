namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Communication;

/// <summary>
/// Client for Samsara contacts.
/// </summary>
public interface IContactsClient
{
    IAsyncEnumerable<Contact> ListAsync(CancellationToken cancellationToken = default);
    Task<Contact> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Contact> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken = default);
    Task<Contact> UpdateAsync(string id, UpdateContactRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
