namespace Samsara.Sdk.Http;

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Samsara.Sdk.Exceptions;
using Samsara.Sdk.Models.Common;
using Samsara.Sdk.Pagination;
using Samsara.Sdk.Serialization;

/// <summary>
/// Internal HTTP client that wraps <see cref="HttpClient"/> with Samsara-specific
/// error handling, deserialization, and pagination support.
/// </summary>
internal sealed class SamsaraHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SamsaraHttpClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

#if NET5_0_OR_GREATER
    private static readonly HttpMethod PatchMethod = HttpMethod.Patch;
#else
    private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");
#endif

    public SamsaraHttpClient(HttpClient httpClient, ILogger<SamsaraHttpClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jsonOptions = SamsaraSerializerOptions.Default;
    }

    /// <summary>
    /// Sends a GET request and deserializes the top-level response.
    /// </summary>
    public async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        using var response = await SendAndValidateAsync(HttpMethod.Get, path, content: null, cancellationToken)
            .ConfigureAwait(false);

        return await DeserializeAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a GET request and unwraps a <c>{ "data": T }</c> envelope.
    /// </summary>
    public async Task<T> GetDataAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        var wrapper = await GetAsync<SamsaraResponse<T>>(path, cancellationToken).ConfigureAwait(false);
        return wrapper.Data;
    }

    /// <summary>
    /// Sends a GET request for a paginated list endpoint and returns a <see cref="PagedResponse{T}"/>.
    /// </summary>
    public async Task<PagedResponse<T>> GetPageAsync<T>(
        string path,
        string? cursor = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        var url = AppendPaginationParams(path, cursor, limit);

        var wrapper = await GetAsync<SamsaraListResponse<T>>(url, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<T>
        {
            Data = wrapper.Data,
            Pagination = wrapper.Pagination,
        };
    }

    /// <summary>
    /// Sends a GET request for a paginated list endpoint whose <c>data</c> is an object
    /// that wraps the page's items (e.g. <c>{ "data": { "media": [...] }, "pagination": {...} }</c>)
    /// rather than a bare array, and returns a <see cref="PagedResponse{TItem}"/>. The
    /// <c>selectItems</c> projection extracts the item list from the deserialized data object.
    /// </summary>
    public async Task<PagedResponse<TItem>> GetPageAsync<TData, TItem>(
        string path,
        Func<TData, IReadOnlyList<TItem>> selectItems,
        string? cursor = null,
        int? limit = null,
        CancellationToken cancellationToken = default)
    {
        var url = AppendPaginationParams(path, cursor, limit);

        var wrapper = await GetAsync<SamsaraNestedListResponse<TData>>(url, cancellationToken).ConfigureAwait(false);

        return new PagedResponse<TItem>
        {
            Data = selectItems(wrapper.Data),
            Pagination = wrapper.Pagination,
        };
    }

    /// <summary>
    /// Sends a POST request with a JSON body and deserializes the <c>{ "data": T }</c> response.
    /// </summary>
    public async Task<T> PostDataAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(HttpMethod.Post, path, content, cancellationToken)
            .ConfigureAwait(false);

        var wrapper = await DeserializeAsync<SamsaraResponse<T>>(response, cancellationToken).ConfigureAwait(false);
        return wrapper.Data;
    }

    /// <summary>
    /// Sends a POST request with a JSON body and unwraps a <c>{ "data": [ T ] }</c> list envelope.
    /// Used by bulk endpoints (e.g. <c>POST /hub/locations</c>) whose response wraps the created
    /// resources in a <c>data</c> array rather than a single <c>data</c> object.
    /// </summary>
    public async Task<IReadOnlyList<T>> PostListDataAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(HttpMethod.Post, path, content, cancellationToken)
            .ConfigureAwait(false);

        var wrapper = await DeserializeAsync<SamsaraListResponse<T>>(response, cancellationToken).ConfigureAwait(false);
        return wrapper.Data ?? [];
    }

    /// <summary>
    /// Sends a POST request with a JSON body.
    /// </summary>
    public async Task PostAsync(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(HttpMethod.Post, path, content, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a POST request with a JSON body and deserializes the response directly
    /// (no <c>{ "data": ... }</c> envelope). Used by legacy v1 endpoints.
    /// </summary>
    public async Task<T> PostAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(HttpMethod.Post, path, content, cancellationToken)
            .ConfigureAwait(false);

        return await DeserializeAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a PATCH request with a JSON body and deserializes the <c>{ "data": T }</c> response.
    /// </summary>
    public async Task<T> PatchDataAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(PatchMethod, path, content, cancellationToken)
            .ConfigureAwait(false);

        var wrapper = await DeserializeAsync<SamsaraResponse<T>>(response, cancellationToken).ConfigureAwait(false);
        return wrapper.Data;
    }

    /// <summary>
    /// Sends a PUT request with a JSON body and deserializes the <c>{ "data": T }</c> response.
    /// </summary>
    public async Task<T> PutDataAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(HttpMethod.Put, path, content, cancellationToken)
            .ConfigureAwait(false);

        var wrapper = await DeserializeAsync<SamsaraResponse<T>>(response, cancellationToken).ConfigureAwait(false);
        return wrapper.Data;
    }

    /// <summary>
    /// Sends a DELETE request.
    /// </summary>
    public async Task DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        using var response = await SendAndValidateAsync(HttpMethod.Delete, path, content: null, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a DELETE request with a JSON body. Used by endpoints (e.g. driver-vehicle
    /// assignments) where the resource identifier(s) live in the request body.
    /// </summary>
    public async Task DeleteAsync(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(HttpMethod.Delete, path, content, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<HttpResponseMessage> SendAndValidateAsync(
        HttpMethod method,
        string path,
        HttpContent? content,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, path);
        request.Content = content;

        _logger.LogDebug("Samsara API {Method} {Path}", method, path);

        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        await ThrowForApiErrorAsync(response, cancellationToken).ConfigureAwait(false);
        return response; // unreachable, but satisfies compiler
    }

    private async Task ThrowForApiErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        string? message = null;
        string? requestId = null;

        try
        {
            var errorBody = await response.Content.ReadFromJsonAsync<SamsaraErrorResponse>(
                _jsonOptions, cancellationToken).ConfigureAwait(false);

            message = errorBody?.Message;
            requestId = errorBody?.RequestId;
        }
        catch (JsonException)
        {
            // Couldn't parse error body; use status code description
        }

        message ??= $"Samsara API returned {(int)response.StatusCode} {response.ReasonPhrase}";

        _logger.LogWarning(
            "Samsara API error {StatusCode}: {Message} (RequestId: {RequestId})",
            (int)response.StatusCode,
            message,
            requestId);

        if ((int)response.StatusCode == 429)
        {
            TimeSpan? retryAfter = null;
            if (response.Headers.TryGetValues("Retry-After", out var retryValues))
            {
                var retryValue = retryValues.FirstOrDefault();
                if (retryValue is not null && double.TryParse(retryValue, out var seconds))
                {
                    retryAfter = TimeSpan.FromSeconds(seconds);
                }
            }

            throw new SamsaraRateLimitException(message, requestId, retryAfter);
        }

        throw SamsaraApiException.Create(response.StatusCode, message, requestId);
    }

    private async Task<T> DeserializeAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        // Single source-generated pass with `required` relaxed (SamsaraSerializerOptions.Default).
        // The Samsara API omits spec-`required` fields on nearly every response, so an absent field
        // is left at its default rather than throwing. Callers that want to validate conformance can
        // use SamsaraSerializerOptions.Strict.
        var result = await response.Content.ReadFromJsonAsync<T>(_jsonOptions, cancellationToken)
            .ConfigureAwait(false);

        return result ?? throw new SamsaraApiException(
            response.StatusCode,
            "Received null response body from Samsara API.",
            requestId: null);
    }

    private static string AppendPaginationParams(string path, string? cursor, int? limit)
    {
        var separator = path.Contains("?") ? '&' : '?';

        if (cursor is not null)
        {
            path = $"{path}{separator}after={Uri.EscapeDataString(cursor)}";
            separator = '&';
        }

        if (limit.HasValue)
        {
            path = $"{path}{separator}limit={limit.Value}";
        }

        return path;
    }
}
