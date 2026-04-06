namespace Samsara.Sdk.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using Samsara.Sdk.Exceptions;
using Samsara.Sdk.Models.Addresses;
using Samsara.Sdk.Models.Common;
using Samsara.Sdk.Models.Communication;
using Samsara.Sdk.Models.Compliance;
using Samsara.Sdk.Models.Documents;
using Samsara.Sdk.Models.Drivers;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Models.Fuel;
using Samsara.Sdk.Models.Industrial;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Models.Organization;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Models.Safety;
using Samsara.Sdk.Models.Tags;
using Samsara.Sdk.Models.Assignments;
using Samsara.Sdk.Models.Issues;
using Samsara.Sdk.Models.Media;
using Samsara.Sdk.Models.Training;
using Samsara.Sdk.Models.Webhooks;
using Samsara.Sdk.Pagination;

/// <summary>
/// Source-generated JSON serializer context for Samsara SDK types.
/// Provides AOT-safe, high-performance serialization.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = false,
    UseStringEnumConverter = true)]
// Infrastructure
[JsonSerializable(typeof(SamsaraErrorResponse))]
[JsonSerializable(typeof(PaginationInfo))]
// Common
[JsonSerializable(typeof(TagReference))]
[JsonSerializable(typeof(ExternalId))]
// Tags
[JsonSerializable(typeof(Tag))]
[JsonSerializable(typeof(TaggedResource))]
[JsonSerializable(typeof(TaggedResourceId))]
[JsonSerializable(typeof(CreateTagRequest))]
[JsonSerializable(typeof(UpdateTagRequest))]
[JsonSerializable(typeof(AttributeDefinition))]
[JsonSerializable(typeof(AttributeEntity))]
[JsonSerializable(typeof(CreateAttributeRequest))]
[JsonSerializable(typeof(UpdateAttributeRequest))]
// Addresses
[JsonSerializable(typeof(Address))]
[JsonSerializable(typeof(Geofence))]
[JsonSerializable(typeof(GeofenceCircle))]
[JsonSerializable(typeof(GeofencePolygon))]
[JsonSerializable(typeof(GeofenceVertex))]
[JsonSerializable(typeof(CreateAddressRequest))]
[JsonSerializable(typeof(UpdateAddressRequest))]
// Fleet
[JsonSerializable(typeof(Vehicle))]
[JsonSerializable(typeof(DriverReference))]
[JsonSerializable(typeof(GatewayInfo))]
[JsonSerializable(typeof(UpdateVehicleRequest))]
[JsonSerializable(typeof(VehicleLocation))]
[JsonSerializable(typeof(VehicleStats))]
[JsonSerializable(typeof(EngineState))]
[JsonSerializable(typeof(FuelPercent))]
[JsonSerializable(typeof(ObdOdometer))]
[JsonSerializable(typeof(GpsOdometer))]
[JsonSerializable(typeof(EngineSeconds))]
[JsonSerializable(typeof(GpsData))]
[JsonSerializable(typeof(ReverseGeo))]
[JsonSerializable(typeof(Equipment))]
[JsonSerializable(typeof(EquipmentLocation))]
[JsonSerializable(typeof(Trailer))]
[JsonSerializable(typeof(CreateTrailerRequest))]
[JsonSerializable(typeof(UpdateTrailerRequest))]
[JsonSerializable(typeof(Gateway))]
// Drivers
[JsonSerializable(typeof(Driver))]
[JsonSerializable(typeof(DriverCarrierSettings))]
[JsonSerializable(typeof(DriverVehicleRef))]
[JsonSerializable(typeof(CreateDriverRequest))]
[JsonSerializable(typeof(UpdateDriverRequest))]
// Safety
[JsonSerializable(typeof(SafetyEvent))]
[JsonSerializable(typeof(SafetyEventVehicle))]
[JsonSerializable(typeof(SafetyEventDriver))]
[JsonSerializable(typeof(SafetyEventLocation))]
[JsonSerializable(typeof(VehicleSafetyScore))]
[JsonSerializable(typeof(DriverSafetyScore))]
[JsonSerializable(typeof(TimeRange))]
// Routes
[JsonSerializable(typeof(Route))]
[JsonSerializable(typeof(RouteDriver))]
[JsonSerializable(typeof(RouteVehicle))]
[JsonSerializable(typeof(RouteStop))]
[JsonSerializable(typeof(SingleUseLocation))]
[JsonSerializable(typeof(RouteSettings))]
[JsonSerializable(typeof(CreateRouteRequest))]
[JsonSerializable(typeof(CreateRouteStopRequest))]
[JsonSerializable(typeof(UpdateRouteRequest))]
[JsonSerializable(typeof(Hub))]
[JsonSerializable(typeof(CreateHubRequest))]
[JsonSerializable(typeof(UpdateHubRequest))]
[JsonSerializable(typeof(Trip))]
[JsonSerializable(typeof(TripLocation))]
// Communication
[JsonSerializable(typeof(DriverMessage))]
[JsonSerializable(typeof(SendDriverMessageRequest))]
[JsonSerializable(typeof(Contact))]
[JsonSerializable(typeof(Alert))]
[JsonSerializable(typeof(AlertVehicle))]
[JsonSerializable(typeof(AlertDriver))]
[JsonSerializable(typeof(AlertConfiguration))]
// Compliance
[JsonSerializable(typeof(HosLog))]
[JsonSerializable(typeof(HosViolation))]
[JsonSerializable(typeof(DvirEntry))]
[JsonSerializable(typeof(DvirVehicle))]
[JsonSerializable(typeof(DvirSignature))]
[JsonSerializable(typeof(DvirDefect))]
[JsonSerializable(typeof(HosClocks))]
[JsonSerializable(typeof(TachographActivity))]
[JsonSerializable(typeof(TachographFile))]
[JsonSerializable(typeof(IftaDetail))]
[JsonSerializable(typeof(IftaSummary))]
// Maintenance
[JsonSerializable(typeof(MaintenanceDvir))]
[JsonSerializable(typeof(MaintenanceDefect))]
[JsonSerializable(typeof(DiagnosticTroubleCode))]
[JsonSerializable(typeof(CheckEngineLight))]
// Documents
[JsonSerializable(typeof(Document))]
[JsonSerializable(typeof(DocumentField))]
[JsonSerializable(typeof(DocumentPhoto))]
[JsonSerializable(typeof(DocumentType))]
[JsonSerializable(typeof(DocumentFieldType))]
[JsonSerializable(typeof(NumberValueTypeMetadata))]
[JsonSerializable(typeof(CreateDocumentRequest))]
[JsonSerializable(typeof(FormTemplate))]
[JsonSerializable(typeof(FormSection))]
[JsonSerializable(typeof(FormFieldDefinition))]
[JsonSerializable(typeof(FormSubmission))]
[JsonSerializable(typeof(FormFieldValue))]
// Webhooks
[JsonSerializable(typeof(Webhook))]
[JsonSerializable(typeof(WebhookHeader))]
[JsonSerializable(typeof(CreateWebhookRequest))]
[JsonSerializable(typeof(UpdateWebhookRequest))]
// Fuel
[JsonSerializable(typeof(FuelPurchase))]
[JsonSerializable(typeof(FuelPurchaseLocation))]
[JsonSerializable(typeof(FuelEnergyLevel))]
// Industrial
[JsonSerializable(typeof(IndustrialAsset))]
[JsonSerializable(typeof(DataInput))]
[JsonSerializable(typeof(DataPoint))]
[JsonSerializable(typeof(MachineHistoryEntry))]
[JsonSerializable(typeof(MachineVibration))]
[JsonSerializable(typeof(Sensor))]
[JsonSerializable(typeof(SensorHistoryResult))]
[JsonSerializable(typeof(SensorDataPoint))]
// Organization
[JsonSerializable(typeof(OrganizationInfo))]
[JsonSerializable(typeof(OrganizationCarrierSettings))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(UserRole))]
[JsonSerializable(typeof(CreateUserRequest))]
[JsonSerializable(typeof(UpdateUserRequest))]
// Assignments
[JsonSerializable(typeof(DriverVehicleAssignment))]
[JsonSerializable(typeof(CreateDriverVehicleAssignmentRequest))]
[JsonSerializable(typeof(UpdateDriverVehicleAssignmentRequest))]
[JsonSerializable(typeof(TrailerAssignment))]
[JsonSerializable(typeof(CarrierProposedAssignment))]
[JsonSerializable(typeof(CreateCarrierProposedAssignmentRequest))]
[JsonSerializable(typeof(UpdateCarrierProposedAssignmentRequest))]
// Training
[JsonSerializable(typeof(TrainingAssignment))]
[JsonSerializable(typeof(TrainingCourse))]
// Issues
[JsonSerializable(typeof(Issue))]
[JsonSerializable(typeof(UpdateIssueRequest))]
// Media
[JsonSerializable(typeof(MediaFile))]
internal sealed partial class SamsaraJsonContext : JsonSerializerContext
{
}

/// <summary>
/// Provides the shared <see cref="JsonSerializerOptions"/> for Samsara API serialization.
/// </summary>
internal static class SamsaraSerializerOptions
{
    public static JsonSerializerOptions Default { get; } = CreateDefault();

    private static JsonSerializerOptions CreateDefault()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

        return options;
    }
}
