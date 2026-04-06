namespace Samsara.Sdk.Http;

/// <summary>
/// Utility for appending optional query parameters to a URL path.
/// </summary>
internal static class QueryBuilder
{
    /// <summary>
    /// Appends <c>startTime</c> and/or <c>endTime</c> query parameters (RFC 3339) when provided.
    /// </summary>
    public static string WithTimeRange(string path, DateTimeOffset? startTime, DateTimeOffset? endTime)
        => WithParams(path,
            ("startTime", startTime?.ToString("O")),
            ("endTime", endTime?.ToString("O")));

    /// <summary>
    /// Appends one or more key/value query parameters to the path, skipping any whose value is <c>null</c>.
    /// </summary>
    public static string WithParams(string path, params (string Key, string? Value)[] parameters)
    {
        var separator = path.Contains('?') ? '&' : '?';
        foreach (var (key, value) in parameters)
        {
            if (value is null) continue;
            path = $"{path}{separator}{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
            separator = '&';
        }

        return path;
    }
}
