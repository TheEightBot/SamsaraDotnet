namespace Samsara.Sdk.Exceptions;

/// <summary>
/// Represents the standard error response body from the Samsara API.
/// </summary>
internal sealed record SamsaraErrorResponse(
    string? Message,
    string? RequestId);
