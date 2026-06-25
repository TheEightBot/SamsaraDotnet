namespace Samsara.Sdk.Exceptions;

using System.Net;
using System.Text;

/// <summary>
/// Base exception for errors returned by the Samsara API.
/// </summary>
public class SamsaraApiException : Exception
{
    /// <summary>
    /// The HTTP status code returned by the API.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// The unique request identifier returned by the API for tracing.
    /// </summary>
    public string? RequestId { get; }

    public SamsaraApiException(HttpStatusCode statusCode, string message, string? requestId)
        : base(message)
    {
        StatusCode = statusCode;
        RequestId = requestId;
    }

    public SamsaraApiException(HttpStatusCode statusCode, string message, string? requestId, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        RequestId = requestId;
    }

    internal static SamsaraApiException Create(HttpStatusCode statusCode, string message, string? requestId)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => new SamsaraBadRequestException(message, requestId),
            HttpStatusCode.Unauthorized => new SamsaraAuthenticationException(message, requestId),
            HttpStatusCode.NotFound => new SamsaraNotFoundException(message, requestId),
            HttpStatusCode.MethodNotAllowed => new SamsaraApiException(statusCode, message, requestId),
            (HttpStatusCode)429 => new SamsaraRateLimitException(message, requestId, retryAfter: null),
            HttpStatusCode.InternalServerError or
            HttpStatusCode.BadGateway or
            HttpStatusCode.ServiceUnavailable or
            HttpStatusCode.GatewayTimeout => new SamsaraServerException(statusCode, message, requestId),
            _ => new SamsaraApiException(statusCode, message, requestId),
        };
    }
}

/// <summary>
/// Thrown when the API returns 400 Bad Request.
/// </summary>
public sealed class SamsaraBadRequestException : SamsaraApiException
{
    public SamsaraBadRequestException(string message, string? requestId)
        : base(HttpStatusCode.BadRequest, message, requestId) { }
}

/// <summary>
/// Thrown when the API returns 401 Unauthorized.
/// </summary>
public sealed class SamsaraAuthenticationException : SamsaraApiException
{
    public SamsaraAuthenticationException(string message, string? requestId)
        : base(HttpStatusCode.Unauthorized, message, requestId) { }
}

/// <summary>
/// Thrown when the API returns 404 Not Found.
/// </summary>
public sealed class SamsaraNotFoundException : SamsaraApiException
{
    public SamsaraNotFoundException(string message, string? requestId)
        : base(HttpStatusCode.NotFound, message, requestId) { }
}

/// <summary>
/// Thrown when the API returns 429 Too Many Requests.
/// </summary>
public sealed class SamsaraRateLimitException : SamsaraApiException
{
    /// <summary>
    /// The amount of time to wait before retrying, as indicated by the Retry-After header.
    /// </summary>
    public TimeSpan? RetryAfter { get; }

    public SamsaraRateLimitException(string message, string? requestId, TimeSpan? retryAfter)
        : base((HttpStatusCode)429, message, requestId)
    {
        RetryAfter = retryAfter;
    }
}

/// <summary>
/// Thrown when the API returns a 5xx server error (500, 502, 503, 504).
/// </summary>
public sealed class SamsaraServerException : SamsaraApiException
{
    public SamsaraServerException(HttpStatusCode statusCode, string message, string? requestId)
        : base(statusCode, message, requestId) { }
}

/// <summary>
/// Thrown when an otherwise-successful Samsara API response cannot be deserialized into the
/// expected model type. Carries the raw response body — and, for requests that send one, the
/// JSON request body — so the offending payload can be inspected directly (or logged) instead
/// of debugging from a bare <see cref="System.Text.Json.JsonException"/>. The original
/// <c>JsonException</c> is preserved as <see cref="Exception.InnerException"/>.
/// </summary>
public sealed class SamsaraDeserializationException : SamsaraApiException
{
    /// <summary>
    /// Maximum number of characters of each payload embedded in <see cref="Exception.Message"/>
    /// (so logs that capture only the message stay bounded). The full, untruncated payloads are
    /// always available on <see cref="ResponseBody"/> and <see cref="RequestBody"/>.
    /// </summary>
    public const int MessagePreviewLength = 4096;

    /// <summary>The CLR type the response body was being deserialized into.</summary>
    public Type TargetType { get; }

    /// <summary>The raw response body returned by the API, verbatim. Empty when the body was empty.</summary>
    public string? ResponseBody { get; }

    /// <summary>The JSON request body that was sent, or <c>null</c> for requests without one (e.g. GET/DELETE).</summary>
    public string? RequestBody { get; }

    /// <summary>
    /// The HTTP method and URI of the request (e.g. <c>POST https://api.samsara.com/hub/locations</c>),
    /// when it can be determined from the response.
    /// </summary>
    public string? RequestPath { get; }

    public SamsaraDeserializationException(
        HttpStatusCode statusCode,
        string message,
        string? requestId,
        Type targetType,
        string? responseBody,
        string? requestBody,
        string? requestPath,
        Exception innerException)
        : base(statusCode, message, requestId, innerException)
    {
        TargetType = targetType;
        ResponseBody = responseBody;
        RequestBody = requestBody;
        RequestPath = requestPath;
    }

    internal static SamsaraDeserializationException Create(
        HttpStatusCode statusCode,
        Type targetType,
        string? responseBody,
        string? requestBody,
        string? requestPath,
        string? requestId,
        Exception innerException)
    {
        var builder = new StringBuilder("Failed to deserialize the Samsara API response into ")
            .Append(targetType.FullName ?? targetType.Name)
            .Append('.');

        if (!string.IsNullOrEmpty(requestPath))
        {
            builder.Append(" Request: ").Append(requestPath).Append('.');
        }

        if (!string.IsNullOrEmpty(requestId))
        {
            builder.Append(" RequestId: ").Append(requestId).Append('.');
        }

        builder.Append(' ').Append(innerException.Message);

        builder.Append(Environment.NewLine).Append("Response body: ").Append(Preview(responseBody));

        if (requestBody is not null)
        {
            builder.Append(Environment.NewLine).Append("Request body: ").Append(Preview(requestBody));
        }

        builder.Append(Environment.NewLine)
            .Append("(Full payloads are on the ResponseBody and RequestBody properties of this exception.)");

        return new SamsaraDeserializationException(
            statusCode,
            builder.ToString(),
            requestId,
            targetType,
            responseBody,
            requestBody,
            requestPath,
            innerException);
    }

    private static string Preview(string? body)
    {
        if (string.IsNullOrEmpty(body))
        {
            return "<empty>";
        }

        return body!.Length <= MessagePreviewLength
            ? body
            : body.Substring(0, MessagePreviewLength) + $"...[truncated; {body.Length} chars total]";
    }
}
