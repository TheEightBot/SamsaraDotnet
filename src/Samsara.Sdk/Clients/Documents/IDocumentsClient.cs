namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Documents;

/// <summary>
/// Client for managing Samsara documents.
/// </summary>
public interface IDocumentsClient
{
    /// <summary>
    /// List all documents within the given time range (<c>GET /fleet/documents</c>).
    /// </summary>
    /// <param name="startTime">Start of the time range (RFC 3339). Required by the spec.</param>
    /// <param name="endTime">End of the time range (RFC 3339). Required by the spec.</param>
    /// <param name="documentTypeId">Optional document template type ID filter.</param>
    /// <param name="queryBy">Optional secondary filter (<c>createdAtTime</c> or <c>updatedAtTime</c>).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<Document> ListAsync(
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        string? documentTypeId = null,
        string? queryBy = null,
        CancellationToken cancellationToken = default);

    Task<Document> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<Document> CreateAsync(CreateDocumentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DocumentType> ListTypesAsync(CancellationToken cancellationToken = default);
    Task<DocumentPdfJob> GeneratePdfAsync(GenerateDocumentPdfRequest request, CancellationToken cancellationToken = default);
    Task<DocumentPdfJob> GetPdfAsync(string id, CancellationToken cancellationToken = default);
}
