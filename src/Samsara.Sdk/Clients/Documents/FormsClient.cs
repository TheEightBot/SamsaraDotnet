namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Documents;

internal sealed class FormsClient : SamsaraServiceClientBase, IFormsClient
{
    public FormsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<FormTemplate> ListTemplatesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormTemplate>("form-templates", cancellationToken: cancellationToken);

    /// <summary>
    /// List form submissions filtered by id(s). The spec's <c>getFormSubmissions</c>
    /// requires the <c>ids</c> query parameter — pass one or more ids.
    /// </summary>
    public IAsyncEnumerable<FormSubmission> ListSubmissionsAsync(IReadOnlyList<string> ids, string? include = null, CancellationToken cancellationToken = default)
        => PaginateAsync<FormSubmission>(
            QueryBuilder.WithParams("form-submissions",
                ("ids", string.Join(",", ids)),
                ("include", include)),
            cancellationToken: cancellationToken);

    /// <summary>Convenience: fetch a single form submission by id, returning the first match (or null).</summary>
    public async Task<FormSubmission?> GetSubmissionAsync(string id, CancellationToken cancellationToken = default)
    {
        await foreach (var submission in ListSubmissionsAsync(new[] { id }, cancellationToken: cancellationToken).ConfigureAwait(false))
        {
            return submission;
        }
        return null;
    }

    public Task<FormSubmission> CreateSubmissionAsync(CreateFormSubmissionRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FormSubmission>("form-submissions", request, cancellationToken);

    public Task<FormSubmission> UpdateSubmissionAsync(UpdateFormSubmissionRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PatchDataAsync<FormSubmission>("form-submissions", request, cancellationToken);

    public IAsyncEnumerable<FormSubmission> GetSubmissionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<FormSubmission>(QueryBuilder.WithTimeRange("form-submissions/stream", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<FormPdfExport> GetPdfExportsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormPdfExport>("form-submissions/pdf-exports", cancellationToken: cancellationToken);

    public Task<FormPdfExport> CreatePdfExportAsync(CreateFormPdfExportRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FormPdfExport>("form-submissions/pdf-exports", request, cancellationToken);
}
