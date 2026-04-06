namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Documents;

/// <summary>
/// Client for managing Samsara documents.
/// </summary>
public interface IDocumentsClient
{
    IAsyncEnumerable<Document> ListAsync(CancellationToken cancellationToken = default);
    Task<Document> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Document> CreateAsync(CreateDocumentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DocumentType> ListTypesAsync(CancellationToken cancellationToken = default);
}
