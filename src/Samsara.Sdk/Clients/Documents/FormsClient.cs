namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Http;
using Samsara.Sdk.Models.Documents;

internal sealed class FormsClient : SamsaraServiceClientBase, IFormsClient
{
    public FormsClient(SamsaraHttpClient httpClient) : base(httpClient) { }

    public IAsyncEnumerable<FormTemplate> ListTemplatesAsync(IReadOnlyList<string>? ids = null, CancellationToken cancellationToken = default)
        => PaginateAsync<FormTemplate>(
            QueryBuilder.WithParams("form-templates",
                ("ids", JoinIds(ids))),
            cancellationToken: cancellationToken);

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

    public IAsyncEnumerable<FormSubmission> GetSubmissionsStreamAsync(
        DateTimeOffset? startTime = null,
        DateTimeOffset? endTime = null,
        IReadOnlyList<string>? formTemplateIds = null,
        IReadOnlyList<string>? userIds = null,
        IReadOnlyList<string>? driverIds = null,
        IReadOnlyList<string>? assignedToRouteStopIds = null,
        IReadOnlyList<string>? include = null,
        CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithTimeRange("form-submissions/stream", startTime, endTime);
        path = QueryBuilder.WithParams(path,
            ("formTemplateIds", JoinIds(formTemplateIds)),
            ("userIds", JoinIds(userIds)),
            ("driverIds", JoinIds(driverIds)),
            ("assignedToRouteStopIds", JoinIds(assignedToRouteStopIds)),
            ("include", JoinIds(include)));
        return PaginateAsync<FormSubmission>(path, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Fetch a previously generated PDF export by its <paramref name="pdfId"/>.
    /// The spec returns a single <c>FormPdfExport</c> object (not a list).
    /// </summary>
    public IAsyncEnumerable<FormPdfExport> GetPdfExportsAsync(string pdfId, CancellationToken cancellationToken = default)
        => YieldAsync(GetSinglePdfExportAsync(pdfId, cancellationToken));

    private Task<FormPdfExport> GetSinglePdfExportAsync(string pdfId, CancellationToken cancellationToken)
    {
        var path = QueryBuilder.WithParams("form-submissions/pdf-exports", ("pdfId", pdfId));
        return HttpClient.GetDataAsync<FormPdfExport>(path, cancellationToken);
    }

    public Task<FormPdfExport> CreatePdfExportAsync(string id, CancellationToken cancellationToken = default)
    {
        var path = QueryBuilder.WithParams("form-submissions/pdf-exports", ("id", id));
        // Spec: POST has no request body — required `id` is a query parameter.
        return HttpClient.PostDataAsync<FormPdfExport>(path, new { }, cancellationToken);
    }

    private static string? JoinIds(IReadOnlyList<string>? values)
        => values is { Count: > 0 } ? string.Join(",", values) : null;

    private static async IAsyncEnumerable<T> YieldAsync<T>(Task<T> task)
    {
        yield return await task.ConfigureAwait(false);
    }
}
