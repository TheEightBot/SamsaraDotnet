namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Documents;

/// <summary>
/// Client for Samsara forms (templates and submissions).
/// </summary>
public interface IFormsClient
{
    IAsyncEnumerable<FormTemplate> ListTemplatesAsync(CancellationToken cancellationToken = default);
    /// <summary>List form submissions filtered by ids (the spec requires the ids query parameter).</summary>
    IAsyncEnumerable<FormSubmission> ListSubmissionsAsync(IReadOnlyList<string> ids, string? include = null, CancellationToken cancellationToken = default);
    /// <summary>Convenience: fetch a single form submission by id, returning the first match (or null).</summary>
    Task<FormSubmission?> GetSubmissionAsync(string id, CancellationToken cancellationToken = default);
    Task<FormSubmission> CreateSubmissionAsync(CreateFormSubmissionRequest request, CancellationToken cancellationToken = default);
    Task<FormSubmission> UpdateSubmissionAsync(UpdateFormSubmissionRequest request, CancellationToken cancellationToken = default);
    IAsyncEnumerable<FormSubmission> GetSubmissionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<FormPdfExport> GetPdfExportsAsync(CancellationToken cancellationToken = default);
    Task<FormPdfExport> CreatePdfExportAsync(CreateFormPdfExportRequest request, CancellationToken cancellationToken = default);
}
