namespace Samsara.Sdk.Models.Assignments;

using System.Text.Json.Serialization;
using Samsara.Sdk.Models.Common;

/// <summary>
/// Represents a driver-to-vehicle assignment.
/// </summary>
public sealed record DriverVehicleAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    [JsonPropertyName("assignmentType")]
    public string? AssignmentType { get; init; }
}

/// <summary>
/// Request body for creating or updating a driver-vehicle assignment.
/// </summary>
public sealed record CreateDriverVehicleAssignmentRequest
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }

    [JsonPropertyName("assignmentType")]
    public string? AssignmentType { get; init; }
}

/// <summary>
/// Request body for updating a driver-vehicle assignment.
/// </summary>
public sealed record UpdateDriverVehicleAssignmentRequest
{
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("isPassenger")]
    public bool? IsPassenger { get; init; }
}

/// <summary>
/// Represents a trailer assignment to a vehicle.
/// </summary>
public sealed record TrailerAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("trailerId")]
    public string? TrailerId { get; init; }

    [JsonPropertyName("trailerName")]
    public string? TrailerName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// Represents a carrier-proposed assignment.
/// </summary>
public sealed record CarrierProposedAssignment
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    [JsonPropertyName("driverName")]
    public string? DriverName { get; init; }

    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    [JsonPropertyName("vehicleName")]
    public string? VehicleName { get; init; }

    [JsonPropertyName("shippingId")]
    public string? ShippingId { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// Request body for creating a carrier-proposed assignment.
/// </summary>
public sealed record CreateCarrierProposedAssignmentRequest
{
    [JsonPropertyName("driverId")]
    public required string DriverId { get; init; }

    [JsonPropertyName("vehicleId")]
    public required string VehicleId { get; init; }

    [JsonPropertyName("shippingId")]
    public string? ShippingId { get; init; }

    [JsonPropertyName("startTime")]
    public DateTimeOffset? StartTime { get; init; }

    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }
}

/// <summary>
/// Request body for updating a carrier-proposed assignment.
/// </summary>
public sealed record UpdateCarrierProposedAssignmentRequest
{
    [JsonPropertyName("endTime")]
    public DateTimeOffset? EndTime { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
