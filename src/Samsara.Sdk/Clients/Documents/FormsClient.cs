namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Documents;

internal sealed class FormsClient : SamsaraServiceClientBase, IFormsClient
{
    public FormsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<FormTemplate> ListTemplatesAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormTemplate>("fleet/forms/templates", cancellationToken: cancellationToken);

    public IAsyncEnumerable<FormSubmission> ListSubmissionsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormSubmission>("fleet/forms/submissions", cancellationToken: cancellationToken);

    public Task<FormSubmission> GetSubmissionAsync(string id, CancellationToken cancellationToken = default)
        => HttpClient.GetDataAsync<FormSubmission>($"fleet/forms/submissions/{Uri.EscapeDataString(id)}", cancellationToken);

    public IAsyncEnumerable<FormSubmission> GetSubmissionsStreamAsync(DateTimeOffset? startTime = null, DateTimeOffset? endTime = null, CancellationToken cancellationToken = default)
        => PaginateAsync<FormSubmission>(QueryBuilder.WithTimeRange("form-submissions/stream", startTime, endTime), cancellationToken: cancellationToken);

    public IAsyncEnumerable<FormPdfExport> GetPdfExportsAsync(CancellationToken cancellationToken = default)
        => PaginateAsync<FormPdfExport>("form-submissions/pdf-exports", cancellationToken: cancellationToken);

    public Task<FormPdfExport> CreatePdfExportAsync(CreateFormPdfExportRequest request, CancellationToken cancellationToken = default)
        => HttpClient.PostDataAsync<FormPdfExport>("form-submissions/pdf-exports", request, cancellationToken);
}
