namespace Samsara.Sdk.Http;

using System.Net;
using System.Net.Http.Json;
using System.Text;
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

        var wrapper = await DeserializeAsync<SamsaraResponse<T>>(response, cancellationToken, body).ConfigureAwait(false);
        return wrapper.Data;
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

        return await DeserializeAsync<T>(response, cancellationToken, body).ConfigureAwait(false);
    }

    /// <summary>
    /// Sends a PATCH request with a JSON body and deserializes the <c>{ "data": T }</c> response.
    /// </summary>
    public async Task<T> PatchDataAsync<T>(string path, object body, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(body, options: _jsonOptions);

        using var response = await SendAndValidateAsync(PatchMethod, path, content, cancellationToken)
            .ConfigureAwait(false);

        var wrapper = await DeserializeAsync<SamsaraResponse<T>>(response, cancellationToken, body).ConfigureAwait(false);
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

        var wrapper = await DeserializeAsync<SamsaraResponse<T>>(response, cancellationToken, body).ConfigureAwait(false);
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

    private async Task<T> DeserializeAsync<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken,
        object? requestBody = null)
    {
        // Buffer the full payload up front so it can be attached to a SamsaraDeserializationException
        // when parsing fails. The happy path deserializes straight from the UTF-8 bytes; the response
        // is only decoded to a string on the failure path (for diagnostics).
#if NET5_0_OR_GREATER
        var payload = await response.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
#else
        var payload = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
#endif

        try
        {
            // Single source-generated pass with `required` relaxed (SamsaraSerializerOptions.Default).
            // The Samsara API omits spec-`required` fields on nearly every response, so an absent field
            // is left at its default rather than throwing. Callers that want to validate conformance can
            // use SamsaraSerializerOptions.Strict.
            var result = JsonSerializer.Deserialize<T>(payload, _jsonOptions);

            return result ?? throw new SamsaraApiException(
                response.StatusCode,
                "Received null response body from Samsara API.",
                requestId: null);
        }
        catch (JsonException ex)
        {
            var exception = SamsaraDeserializationException.Create(
                response.StatusCode,
                typeof(T),
                responseBody: DecodeForDiagnostics(payload),
                requestBody: SerializeForDiagnostics(requestBody),
                requestPath: DescribeRequest(response),
                requestId: TryGetRequestId(response),
                innerException: ex);

            _logger.LogError(
                ex,
                "Failed to deserialize Samsara API response into {TargetType} for {Request} (RequestId: {RequestId})",
                typeof(T),
                exception.RequestPath,
                exception.RequestId);

            throw exception;
        }
    }

    private static readonly string[] RequestIdHeaderNames =
    {
        "x-request-id",
        "x-samsara-request-id",
        "request-id",
    };

    /// <summary>Best-effort lookup of a request-correlation id from the response headers.</summary>
    private static string? TryGetRequestId(HttpResponseMessage response)
    {
        foreach (var name in RequestIdHeaderNames)
        {
            if (response.Headers.TryGetValues(name, out var values))
            {
                var value = values.FirstOrDefault();
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
        }

        return null;
    }

    /// <summary>Describes the originating request (method + absolute URI) for diagnostics, when known.</summary>
    private static string? DescribeRequest(HttpResponseMessage response)
    {
        var request = response.RequestMessage;
        return request is null ? null : $"{request.Method} {request.RequestUri}";
    }

    private static string DecodeForDiagnostics(byte[] payload)
        => payload.Length == 0 ? string.Empty : Encoding.UTF8.GetString(payload);

    /// <summary>
    /// Re-serializes the request body to JSON (using the same options it was sent with) so it can be
    /// embedded in a <see cref="SamsaraDeserializationException"/>. Never throws — serialization
    /// problems are reported inline rather than masking the original deserialization failure.
    /// </summary>
    private string? SerializeForDiagnostics(object? requestBody)
    {
        if (requestBody is null)
        {
            return null;
        }

        try
        {
            return JsonSerializer.Serialize(requestBody, requestBody.GetType(), _jsonOptions);
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException)
        {
            return $"<unable to serialize request body: {ex.Message}>";
        }
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
