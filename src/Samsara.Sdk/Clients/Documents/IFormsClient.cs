namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Documents;

/// <summary>
/// Client for Samsara forms (templates and submissions).
/// </summary>
public interface IFormsClient
{
    /// <summary>
    /// List form templates. The spec exposes optional <c>ids</c> for filtering
    /// to specific templates; array values are joined by <c>,</c> per the
    /// spec's <c>style=form,explode=false</c>.
    /// </summary>
    IAsyncEnumerable<FormTemplate> ListTemplatesAsync(IReadOnlyList<string>? ids = null, CancellationToken cancellationToken = default);

    /// <summary>List form submissions filtered by ids (the spec requires the ids query parameter).</summary>
    IAsyncEnumerable<FormSubmission> ListSubmissionsAsync(IReadOnlyList<string> ids, string? include = null, CancellationToken cancellationToken = default);

    /// <summary>Convenience: fetch a single form submission by id, returning the first match (or null).</summary>
    Task<FormSubmission?> GetSubmissionAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>Create a form submission via <c>POST /form-submissions</c>.</summary>
    Task<FormSubmission> CreateSubmissionAsync(CreateFormSubmissionRequest request, CancellationToken cancellationToken = default);

    /// <summary>Update a form submission via <c>PATCH /form-submissions</c>. The submission id is in the body.</summary>
    Task<FormSubmission> UpdateSubmissionAsync(UpdateFormSubmissionRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stream form submissions over a time range. All spec-defined optional
    /// filters are supported: <c>formTemplateIds</c>, <c>userIds</c>,
    /// <c>driverIds</c>, <c>assignedToRouteStopIds</c>, and <c>include</c>
    /// (array params joined by <c>,</c>).
    /// </summary>
    IAsyncEnumerable<FormSubmission> GetSubmissionsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? formTemplateIds = null,
        IReadOnlyList<string>? userIds = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? assignedToRouteStopIds = null,
        IReadOnlyList<string>? include = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a previously generated PDF export by its <paramref name="pdfId"/>
    /// (REQUIRED by the spec on <c>GET /form-submissions/pdf-exports</c>).
    /// </summary>
    IAsyncEnumerable<FormPdfExport> GetPdfExportsAsync(string pdfId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a PDF export for the form submission with the given
    /// <paramref name="id"/>. The spec models this as a required <c>id</c>
    /// query parameter on <c>POST /form-submissions/pdf-exports</c> (no body).
    /// </summary>
    Task<FormPdfExport> CreatePdfExportAsync(string id, CancellationToken cancellationToken = default);
}
