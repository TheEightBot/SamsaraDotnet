namespace Samsara.Sdk.Clients;

using Samsara.Sdk.Models.Assignments;

/// <summary>Client for managing driver-trailer assignments.</summary>
public interface IDriverTrailerAssignmentsClient
{
    /// <summary>
    /// Get currently active driver-trailer assignments for the supplied drivers.
    /// </summary>
    /// <param name="driverIds">
    /// Comma-separated list of driver IDs and external IDs to filter on. Spec-required.
    /// </param>
    /// <param name="includeExternalIds">
    /// Optional flag indicating whether to return external IDs on supported entities.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    IAsyncEnumerable<DriverTrailerAssignment> ListAsync(
        IReadOnlyList<string> driverIds,
        bool? includeExternalIds = null,
        CancellationToken cancellationToken = default);

    /// <summary>Create a new driver-trailer assignment.</summary>
    Task<DriverTrailerAssignment> CreateAsync(CreateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing driver-trailer assignment. The Samsara assignment <paramref name="id"/>
    /// is sent as a query parameter per spec, with the new end time in the request body.
    /// </summary>
    Task<DriverTrailerAssignment> UpdateAsync(string id, UpdateDriverTrailerAssignmentRequest request, CancellationToken cancellationToken = default);
}
