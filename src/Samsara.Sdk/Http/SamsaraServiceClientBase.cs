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

    /// <summary>
    /// Enumerates all items across all pages of a paginated endpoint whose <c>data</c>
    /// is an object that wraps the page's items under a property (e.g.
    /// <c>{ "data": { "media": [...] }, "pagination": {...} }</c>) rather than a bare array.
    /// The <c>selectItems</c> projection extracts the item list from each page's data object.
    /// </summary>
    protected IAsyncEnumerable<TItem> PaginateAsync<TData, TItem>(
        string path,
        Func<TData, IReadOnlyList<TItem>> selectItems,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        return PaginationExtensions.PaginateAsync<TItem>(
            (cursor, ct) => HttpClient.GetPageAsync<TData, TItem>(path, selectItems, cursor, limit, ct),
            cancellationToken);
    }
}
