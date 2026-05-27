namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>
/// Client for managing carrier proposed assignments.
/// </summary>
public interface ICarrierProposedAssignmentsClient
{
    /// <summary>
    /// Lists carrier-proposed assignments that drivers would see in the future.
    /// </summary>
    /// <param name="driverIds">
    /// Optional comma-friendly filter on the data based on driver IDs and external IDs
    /// (e.g., <c>["1234", "5678", "payroll:4841"]</c>).
    /// </param>
    /// <param name="activeTime">
    /// Optional RFC 3339 timestamp; returns assignments active at the given time.
    /// Defaults to now (current active assignments) when omitted.
    /// </param>
    /// <param name="cancellationToken">Token to observe for cancellation.</param>
    IAsyncEnumerable<CarrierProposedAssignment> ListAsync(
        IReadOnlyList<string>? driverIds = null,
        string? activeTime = null,
        CancellationToken cancellationToken = default);

    Task<CarrierProposedAssignment> CreateAsync(CreateCarrierProposedAssignmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
