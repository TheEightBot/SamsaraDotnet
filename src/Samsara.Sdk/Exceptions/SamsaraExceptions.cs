namespace Samsara.Sdk.Exceptions;

using System.Net;

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
