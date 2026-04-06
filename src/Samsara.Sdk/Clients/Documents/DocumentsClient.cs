namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Documents;

internal sealed class DocumentsClient : SamsaraServiceClientBase, IDocumentsClient
{
    private const string BasePath = "fleet/documents";

    public DocumentsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<Document> ListAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<Document>(BasePath, cancellationToken: cancellationToken);

    public Task<Document> GetAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<Document>($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public Task<Document> CreateAsync(CreateDocumentRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<Document>(BasePath, request, cancellationToken);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{BasePath}/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<DocumentType> ListTypesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<DocumentType>("fleet/document-types", cancellationToken: cancellationToken);
}
