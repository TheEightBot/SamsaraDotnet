namespace Samsara.Sdk.Http;

using Samsara.Sdk.Pagination;

/// <summary>
/// Base class for all Samsara API service clients.
/// Provides access to the internal HTTP client and pagination helpers.
/// </summary>
public abstract class SamsaraServiceClientBase
{
    private protected readonly SamsaraHttpClient HttpClient;

    private protected SamsaraServiceClientBase(SamsaraHttpClient httpClient)
    {
        HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <summary>
    /// Enumerates all items across all pages of a paginated endpoint.
    /// </summary>
    protected IAsyncEnumerable<T> PaginateAsync<T>(
        string path,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        return PaginationExtensions.PaginateAsync<T>(
            (cursor, ct) => HttpClient.GetPageAsync<T>(path, cursor, limit, ct),
            cancellationToken);
    }
}
