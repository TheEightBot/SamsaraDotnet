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

    /// <summary>
    /// Enumerates all items across all pages of a paginated endpoint whose page items sit in a
    /// <b>top-level named array</b> beside a top-level <c>pagination</c> block — the legacy v1
    /// shape <c>{ "vehicles": [...], "pagination": {...} }</c> — rather than under <c>data</c>.
    /// </summary>
    /// <remarks>
    /// The two-argument overload above expects the v2 <c>{ "data": { ... }, "pagination": {...} }</c>
    /// envelope and would find no <c>data</c> member on these v1 bodies, so this overload takes an
    /// explicit projection for both the items and the cursor.
    /// <para>
    /// <paramref name="cursorParam"/> must match the forward-cursor query parameter the
    /// operation declares in the spec — v1 bodies use both <c>after</c> and
    /// <c>startingAfter</c>, and a wrong name makes the server re-serve page 1 forever.
    /// </para>
    /// </remarks>
    protected IAsyncEnumerable<TItem> PaginateAsync<TResponse, TItem>(
        string path,
        Func<TResponse, IReadOnlyList<TItem>?> selectItems,
        Func<TResponse, PaginationInfo?> selectPagination,
        int? limit = null,
        string cursorParam = SamsaraHttpClient.DefaultCursorParam,
        CancellationToken cancellationToken = default)
    {
        return PaginationExtensions.PaginateAsync<TItem>(
            (cursor, ct) => HttpClient.GetPageFromAsync(path, selectItems, selectPagination, cursor, limit, cursorParam, ct),
            cancellationToken);
    }
}
