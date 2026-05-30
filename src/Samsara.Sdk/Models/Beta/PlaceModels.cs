namespace Samsara.Sdk.Models.Beta;

using System.Text.Json.Serialization;

/// <summary>
/// A soft-deletion marker for a place, returned by <c>GET /places/deletions</c>
/// (operationId <c>getPlaceDeletions</c>, beta). Poll this endpoint with the
/// previous page's end cursor to learn which places have been deleted.
/// </summary>
public sealed record PlaceDeletionMarker
{
    /// <summary>Identifier of the deleted place (spec REQUIRED).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>When the place was deleted (spec REQUIRED).</summary>
    [JsonPropertyName("deletedAtTime")]
    public required DateTimeOffset DeletedAtTime { get; init; }

    /// <summary>External IDs that were associated with the deleted place.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}
