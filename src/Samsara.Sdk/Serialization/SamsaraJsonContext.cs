namespace Samsara.Sdk.Serialization;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Samsara.Sdk.Exceptions;
using Samsara.Sdk.Models.Addresses;
using Samsara.Sdk.Models.Beta;
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
[JsonSerializable(typeof(VehicleGrossWeight))]
[JsonSerializable(typeof(VehicleSensorConfiguration))]
[JsonSerializable(typeof(VehicleSensorArea))]
[JsonSerializable(typeof(VehicleSensorDoor))]
[JsonSerializable(typeof(VehicleSensor))]
[JsonSerializable(typeof(DriverReference))]
[JsonSerializable(typeof(GatewayInfo))]
[JsonSerializable(typeof(CreateVehicleRequest))]
[JsonSerializable(typeof(UpdateVehicleRequest))]
[JsonSerializable(typeof(VehicleLocation))]
[JsonSerializable(typeof(VehicleLocationPoint))]
[JsonSerializable(typeof(VehicleStats))]
[JsonSerializable(typeof(VehicleStatsSample))]
[JsonSerializable(typeof(VehicleStatValue))]
[JsonSerializable(typeof(VehicleStatDoubleValue))]
[JsonSerializable(typeof(VehicleStatStringValue))]
[JsonSerializable(typeof(VehicleStatAuxInput))]
[JsonSerializable(typeof(VehicleStatEngineImmobilizer))]
[JsonSerializable(typeof(VehicleStatNfcCardScan))]
[JsonSerializable(typeof(VehicleStatNfcCard))]
[JsonSerializable(typeof(VehicleStatGps))]
[JsonSerializable(typeof(VehicleStatAddress))]
[JsonSerializable(typeof(VehicleStatFaultCodes))]
[JsonSerializable(typeof(VehicleStatFaultCodesJ1939))]
[JsonSerializable(typeof(VehicleStatCheckEngineLights))]
[JsonSerializable(typeof(VehicleStatJ1939Dtc))]
[JsonSerializable(typeof(VehicleStatFaultCodesObdii))]
[JsonSerializable(typeof(VehicleStatObdiiDtcGroup))]
[JsonSerializable(typeof(VehicleStatObdiiDtc))]
[JsonSerializable(typeof(VehicleStatFaultCodesOem))]
[JsonSerializable(typeof(GpsData))]
[JsonSerializable(typeof(ReverseGeo))]
[JsonSerializable(typeof(Equipment))]
[JsonSerializable(typeof(EquipmentInstalledGateway))]
[JsonSerializable(typeof(CreateEquipmentRequest))]
[JsonSerializable(typeof(UpdateEquipmentRequest))]
[JsonSerializable(typeof(EquipmentLocation))]
[JsonSerializable(typeof(EquipmentLocationPoint))]
[JsonSerializable(typeof(Trailer))]
[JsonSerializable(typeof(CreateTrailerRequest))]
[JsonSerializable(typeof(UpdateTrailerRequest))]
[JsonSerializable(typeof(Gateway))]
[JsonSerializable(typeof(GatewayAccessoryDevice))]
[JsonSerializable(typeof(GatewayConnectionStatus))]
[JsonSerializable(typeof(GatewayDataUsage))]
[JsonSerializable(typeof(CreateGatewayRequest))]
// Drivers
[JsonSerializable(typeof(Driver))]
[JsonSerializable(typeof(DriverCarrierSettings))]
[JsonSerializable(typeof(DriverVehicleRef))]
[JsonSerializable(typeof(CreateDriverRequest))]
[JsonSerializable(typeof(UpdateDriverRequest))]
// Safety
[JsonSerializable(typeof(SafetyEvent))]
[JsonSerializable(typeof(SafetyEventAsset))]
[JsonSerializable(typeof(SafetyEventDriver))]
[JsonSerializable(typeof(SafetyEventBehaviorLabel))]
[JsonSerializable(typeof(SafetyEventContextLabel))]
[JsonSerializable(typeof(SafetyEventMedia))]
[JsonSerializable(typeof(SafetyEventDismissalReason))]
[JsonSerializable(typeof(SafetyEventSpeedingMetadata))]
[JsonSerializable(typeof(SafetyEventLocation))]
[JsonSerializable(typeof(SafetyEventAddress))]
[JsonSerializable(typeof(SafetyEventGeofence))]
[JsonSerializable(typeof(SafetyEventAttribute))]
[JsonSerializable(typeof(SafetyEventTag))]
[JsonSerializable(typeof(VehicleSafetyScore))]
[JsonSerializable(typeof(DriverSafetyScore))]
[JsonSerializable(typeof(TagSafetyScore))]
[JsonSerializable(typeof(TagGroupSafetyScore))]
[JsonSerializable(typeof(SafetyScoreBehavior))]
[JsonSerializable(typeof(SafetyScoreSpeeding))]
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
[JsonSerializable(typeof(UpdateRouteStopRequest))]
[JsonSerializable(typeof(Hub))]
[JsonSerializable(typeof(Trip))]
[JsonSerializable(typeof(TripAsset))]
[JsonSerializable(typeof(TripLocation))]
[JsonSerializable(typeof(TripLocationAddress))]
[JsonSerializable(typeof(V1Trip))]
[JsonSerializable(typeof(V1TripsResponse))]
// Communication
[JsonSerializable(typeof(DriverMessage))]
[JsonSerializable(typeof(V1MessageSender))]
[JsonSerializable(typeof(SendDriverMessageRequest))]
[JsonSerializable(typeof(Contact))]
[JsonSerializable(typeof(Alert))]
[JsonSerializable(typeof(AlertVehicle))]
[JsonSerializable(typeof(AlertDriver))]
[JsonSerializable(typeof(AlertConfiguration))]
[JsonSerializable(typeof(CreateAlertConfigurationRequest))]
[JsonSerializable(typeof(UpdateAlertConfigurationRequest))]
[JsonSerializable(typeof(AlertNotificationSetting))]
[JsonSerializable(typeof(AlertIncident))]
[JsonSerializable(typeof(AlertScope))]
[JsonSerializable(typeof(AlertTrigger))]
[JsonSerializable(typeof(AlertAction))]
[JsonSerializable(typeof(AlertOperationalSettings))]
[JsonSerializable(typeof(AlertIncidentCondition))]
// Compliance
[JsonSerializable(typeof(HosLog))]
[JsonSerializable(typeof(HosLogEntry))]
[JsonSerializable(typeof(HosLogLocation))]
[JsonSerializable(typeof(HosViolation))]
[JsonSerializable(typeof(HosViolationEntry))]
[JsonSerializable(typeof(HosViolationDay))]
[JsonSerializable(typeof(HosViolationDriver))]
[JsonSerializable(typeof(HosDailyLog))]
[JsonSerializable(typeof(HosDailyLogDriver))]
[JsonSerializable(typeof(HosDailyLogEldSettings))]
[JsonSerializable(typeof(HosDailyLogDriverRuleset))]
[JsonSerializable(typeof(HosDailyLogDistanceTraveled))]
[JsonSerializable(typeof(HosDailyLogDutyStatusDurations))]
[JsonSerializable(typeof(HosDailyLogMetaData))]
[JsonSerializable(typeof(HosDailyLogVehicle))]
[JsonSerializable(typeof(HosEldEvent))]
[JsonSerializable(typeof(HosEldEventEntry))]
[JsonSerializable(typeof(HosEldEventLocation))]
[JsonSerializable(typeof(HosEldEventRemark))]
[JsonSerializable(typeof(HosClocks))]
[JsonSerializable(typeof(TachographActivity))]
[JsonSerializable(typeof(TachographActivityEntry))]
[JsonSerializable(typeof(TachographFile))]
[JsonSerializable(typeof(TachographFileEntry))]
[JsonSerializable(typeof(TachographDriver))]
[JsonSerializable(typeof(TachographVehicle))]
[JsonSerializable(typeof(CreateTachographFileUploadRequest))]
[JsonSerializable(typeof(TachographFileUpload))]
[JsonSerializable(typeof(TachographUploadRequiredHeader))]
[JsonSerializable(typeof(IftaJurisdictionSummary))]
[JsonSerializable(typeof(IftaVehicleReport))]
[JsonSerializable(typeof(IftaJurisdictionReportsResponse))]
[JsonSerializable(typeof(IftaVehicleReportsResponse))]
[JsonSerializable(typeof(IftaReportTroubleshooting))]
[JsonSerializable(typeof(IftaDetailJobArgs))]
[JsonSerializable(typeof(IftaDetailJobOutput))]
// Maintenance
[JsonSerializable(typeof(MaintenanceDvir))]
[JsonSerializable(typeof(MaintenanceDvirAssetRef))]
[JsonSerializable(typeof(MaintenanceDvirSignature))]
[JsonSerializable(typeof(MaintenanceSignatoryUser))]
[JsonSerializable(typeof(DiagnosticTroubleCode))]
[JsonSerializable(typeof(CheckEngineLight))]
// Documents
[JsonSerializable(typeof(Document))]
[JsonSerializable(typeof(DocumentTypeRef))]
[JsonSerializable(typeof(DriverRef))]
[JsonSerializable(typeof(VehicleRef))]
[JsonSerializable(typeof(RouteRef))]
[JsonSerializable(typeof(RouteStopRef))]
[JsonSerializable(typeof(ConditionalFieldSection))]
[JsonSerializable(typeof(DocumentField))]
[JsonSerializable(typeof(DocumentPhoto))]
[JsonSerializable(typeof(DocumentType))]
[JsonSerializable(typeof(DocumentFieldType))]
[JsonSerializable(typeof(NumberValueTypeMetadata))]
[JsonSerializable(typeof(CreateDocumentRequest))]
[JsonSerializable(typeof(FormTemplate))]
[JsonSerializable(typeof(FormsApprovalConfig))]
[JsonSerializable(typeof(FormsSingleApprovalConfig))]
[JsonSerializable(typeof(FormsPolymorphicUser))]
[JsonSerializable(typeof(FormSection))]
[JsonSerializable(typeof(FormFieldDefinition))]
[JsonSerializable(typeof(FormSubmission))]
[JsonSerializable(typeof(FormTemplateReference))]
[JsonSerializable(typeof(FormSubmissionApprovalDetails))]
[JsonSerializable(typeof(FormsAsset))]
[JsonSerializable(typeof(FormsGeofence))]
[JsonSerializable(typeof(FormsLocation))]
[JsonSerializable(typeof(FormsScore))]
// Webhooks
[JsonSerializable(typeof(Webhook))]
[JsonSerializable(typeof(WebhookHeader))]
[JsonSerializable(typeof(CreateWebhookRequest))]
[JsonSerializable(typeof(UpdateWebhookRequest))]
// Fuel & energy
[JsonSerializable(typeof(FuelPurchase))]
[JsonSerializable(typeof(CreateFuelPurchaseRequest))]
[JsonSerializable(typeof(FuelPurchaseMoney))]
[JsonSerializable(typeof(FuelEnergyVehicleReport))]
[JsonSerializable(typeof(FuelEnergyDriverReport))]
[JsonSerializable(typeof(FuelEnergyCost))]
[JsonSerializable(typeof(FuelEnergyVehicleReportsResponse))]
[JsonSerializable(typeof(FuelEnergyDriverReportsResponse))]
[JsonSerializable(typeof(DriverEfficiencyByDriver))]
[JsonSerializable(typeof(DriverEfficiencyByVehicle))]
[JsonSerializable(typeof(DriverEfficiencyDifficultyScore))]
[JsonSerializable(typeof(DriverEfficiencyPercentageData))]
[JsonSerializable(typeof(DriverEfficiencyRawData))]
[JsonSerializable(typeof(DriverEfficiencyScoreData))]
// Industrial
[JsonSerializable(typeof(IndustrialAsset))]
[JsonSerializable(typeof(IndustrialAssetDataOutput))]
[JsonSerializable(typeof(IndustrialAssetDataInput))]
[JsonSerializable(typeof(IndustrialAssetDataInputLastPoint))]
[JsonSerializable(typeof(IndustrialAssetLocation))]
[JsonSerializable(typeof(IndustrialAssetLocationDataInput))]
[JsonSerializable(typeof(IndustrialAssetParent))]
[JsonSerializable(typeof(IndustrialAssetRunningStatusDataInput))]
[JsonSerializable(typeof(IndustrialAssetTag))]
[JsonSerializable(typeof(DataInput))]
[JsonSerializable(typeof(NumberDataPoint))]
[JsonSerializable(typeof(StringDataPoint))]
[JsonSerializable(typeof(LocationDataPoint))]
[JsonSerializable(typeof(LocationDataPointGpsLocation))]
[JsonSerializable(typeof(LocationDataPointPlace))]
[JsonSerializable(typeof(FftSpectraDataPoint))]
[JsonSerializable(typeof(FftSpectraValue))]
[JsonSerializable(typeof(J1939D1StatusDataPoint))]
[JsonSerializable(typeof(J1939D1Status))]
[JsonSerializable(typeof(MachineHistoryEntry))]
[JsonSerializable(typeof(MachineVibration))]
[JsonSerializable(typeof(V1Sensor))]
[JsonSerializable(typeof(V1SensorListResponse))]
[JsonSerializable(typeof(V1SensorReadingsRequest))]
[JsonSerializable(typeof(V1SensorHistoryRequest))]
[JsonSerializable(typeof(V1SensorHistorySeries))]
[JsonSerializable(typeof(V1SensorHistoryResponse))]
[JsonSerializable(typeof(V1SensorHistoryDataPoint))]
[JsonSerializable(typeof(V1TemperatureReading))]
[JsonSerializable(typeof(V1DoorReading))]
[JsonSerializable(typeof(V1HumidityReading))]
[JsonSerializable(typeof(V1CargoReading))]
[JsonSerializable(typeof(V1SensorReadingsResponse<V1TemperatureReading>))]
[JsonSerializable(typeof(V1SensorReadingsResponse<V1DoorReading>))]
[JsonSerializable(typeof(V1SensorReadingsResponse<V1HumidityReading>))]
[JsonSerializable(typeof(V1SensorReadingsResponse<V1CargoReading>))]
// Organization
[JsonSerializable(typeof(OrganizationInfo))]
[JsonSerializable(typeof(OrganizationCarrierSettings))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(UserRole))]
[JsonSerializable(typeof(UserRoleAssignment))]
[JsonSerializable(typeof(UserRoleInput))]
[JsonSerializable(typeof(CreateUserRequest))]
[JsonSerializable(typeof(UpdateUserRequest))]
// Assignments
[JsonSerializable(typeof(DriverVehicleAssignment))]
[JsonSerializable(typeof(DriverVehicleAssignmentDriver))]
[JsonSerializable(typeof(DriverVehicleAssignmentVehicle))]
[JsonSerializable(typeof(DriverVehicleAssignmentMetadata))]
[JsonSerializable(typeof(CreateDriverVehicleAssignmentRequest))]
[JsonSerializable(typeof(UpdateDriverVehicleAssignmentRequest))]
[JsonSerializable(typeof(TrailerAssignment))]
[JsonSerializable(typeof(CarrierProposedAssignment))]
[JsonSerializable(typeof(CarrierProposedAssignmentDriver))]
[JsonSerializable(typeof(CarrierProposedAssignmentVehicle))]
[JsonSerializable(typeof(CarrierProposedAssignmentTrailer))]
[JsonSerializable(typeof(CreateCarrierProposedAssignmentRequest))]
[JsonSerializable(typeof(UpdateCarrierProposedAssignmentRequest))]
// Training
[JsonSerializable(typeof(TrainingAssignment))]
[JsonSerializable(typeof(TrainingAssignmentCourse))]
[JsonSerializable(typeof(TrainingAssignmentLearner))]
[JsonSerializable(typeof(TrainingCourse))]
[JsonSerializable(typeof(TrainingCourseCategory))]
[JsonSerializable(typeof(TrainingCourseLabel))]
// Issues
[JsonSerializable(typeof(Issue))]
[JsonSerializable(typeof(IssueAsset))]
[JsonSerializable(typeof(IssueUser))]
[JsonSerializable(typeof(IssueSource))]
[JsonSerializable(typeof(IssueMedia))]
[JsonSerializable(typeof(CreateIssueRequest))]
[JsonSerializable(typeof(UpdateIssueRequest))]
[JsonSerializable(typeof(IssueAssetRequest))]
[JsonSerializable(typeof(IssueAssigneeRequest))]
[JsonSerializable(typeof(IssueMediaItemRequest))]
// Media
[JsonSerializable(typeof(MediaFile))]
[JsonSerializable(typeof(MediaUrlInfo))]
[JsonSerializable(typeof(MediaRetrieval))]
[JsonSerializable(typeof(MediaListResponse))]
[JsonSerializable(typeof(MediaRetrievalListResponse))]
[JsonSerializable(typeof(CreateMediaRetrievalRequest))]
// Fleet - Assets
[JsonSerializable(typeof(Asset))]
[JsonSerializable(typeof(CreateAssetRequest))]
[JsonSerializable(typeof(UpdateAssetRequest))]
[JsonSerializable(typeof(DeleteAssetsRequest))]
[JsonSerializable(typeof(AssetLocationAndSpeed))]
[JsonSerializable(typeof(AssetLocationAndSpeedAsset))]
[JsonSerializable(typeof(AssetLocationAndSpeedSpeed))]
[JsonSerializable(typeof(AssetLocation))]
// Fleet - Idling
[JsonSerializable(typeof(IdlingEvent))]
[JsonSerializable(typeof(IdlingEventAddress))]
[JsonSerializable(typeof(IdlingEventAsset))]
[JsonSerializable(typeof(IdlingEventOperator))]
[JsonSerializable(typeof(IdlingEventFuelCost))]
[JsonSerializable(typeof(IdlingEventGaseousFuelCost))]
// Fleet - LiveSharing
[JsonSerializable(typeof(LiveSharingLink))]
[JsonSerializable(typeof(LiveSharingLinkAssetsLocationLinkConfig))]
[JsonSerializable(typeof(LiveSharingLinkAssetsNearLocationLinkConfig))]
[JsonSerializable(typeof(LiveSharingLinkAssetsOnRouteLinkConfig))]
[JsonSerializable(typeof(LiveSharingLinkLocation))]
[JsonSerializable(typeof(LiveSharingLinkTag))]
[JsonSerializable(typeof(CreateLiveSharingLinkRequest))]
[JsonSerializable(typeof(CreateAssetsLocationLinkConfig))]
[JsonSerializable(typeof(UpdateLiveSharingLinkRequest))]
// Fleet - Trailers (stats)
[JsonSerializable(typeof(TrailerStats))]
[JsonSerializable(typeof(TrailerStatsSample))]
[JsonSerializable(typeof(TrailerStatValue))]
[JsonSerializable(typeof(TrailerStatStringValue))]
[JsonSerializable(typeof(TrailerStatReeferState))]
[JsonSerializable(typeof(TrailerStatGps))]
[JsonSerializable(typeof(TrailerStatReeferAlarms))]
[JsonSerializable(typeof(TrailerStatReeferAlarm))]
// Fleet - Equipment (stats)
[JsonSerializable(typeof(SpeedingInterval))]
[JsonSerializable(typeof(SpeedingIntervalAsset))]
[JsonSerializable(typeof(EquipmentStats))]
[JsonSerializable(typeof(EquipmentStatsSample))]
[JsonSerializable(typeof(EquipmentStatValue))]
[JsonSerializable(typeof(EquipmentStatStringValue))]
[JsonSerializable(typeof(EquipmentStatGps))]
[JsonSerializable(typeof(EquipmentStatAddress))]
// Compliance - CARB CTC
[JsonSerializable(typeof(CarbCtcVehicle))]
[JsonSerializable(typeof(CarbCtcVehicleHistory))]
// Compliance - IFTA jobs
[JsonSerializable(typeof(IftaDetailJob))]
[JsonSerializable(typeof(CreateIftaDetailJobRequest))]
// Safety - Coaching
[JsonSerializable(typeof(DriverCoachAssignment))]
[JsonSerializable(typeof(SetDriverCoachAssignmentRequest))]
[JsonSerializable(typeof(CoachingSession))]
[JsonSerializable(typeof(CoachingDriver))]
[JsonSerializable(typeof(CoachingBehavior))]
// Assignments - Driver/Trailer
[JsonSerializable(typeof(DriverTrailerAssignment))]
[JsonSerializable(typeof(DriverTrailerAssignmentDriver))]
[JsonSerializable(typeof(DriverTrailerAssignmentTrailer))]
[JsonSerializable(typeof(CreateDriverTrailerAssignmentRequest))]
[JsonSerializable(typeof(UpdateDriverTrailerAssignmentRequest))]
// Maintenance - DVIRs & Defects
[JsonSerializable(typeof(DefectRecord))]
[JsonSerializable(typeof(DefectResolvedBy))]
[JsonSerializable(typeof(UpdateDefectRequest))]
[JsonSerializable(typeof(UpdateDefectResolvedBy))]
[JsonSerializable(typeof(DefectType))]
[JsonSerializable(typeof(CreateDvirRequest))]
[JsonSerializable(typeof(UpdateDvirRequest))]
// Maintenance - Work Orders
[JsonSerializable(typeof(WorkOrder))]
[JsonSerializable(typeof(CreateWorkOrderRequest))]
[JsonSerializable(typeof(UpdateWorkOrderRequest))]
[JsonSerializable(typeof(DeleteWorkOrdersRequest))]
[JsonSerializable(typeof(ServiceTask))]
[JsonSerializable(typeof(InvoiceScan))]
[JsonSerializable(typeof(PostInvoiceScanRequest))]
[JsonSerializable(typeof(InvoiceScanFile))]
[JsonSerializable(typeof(WorkOrderMaintenanceSite))]
[JsonSerializable(typeof(WorkOrderMoney))]
// Documents - PDF jobs
[JsonSerializable(typeof(DocumentPdfJob))]
[JsonSerializable(typeof(GenerateDocumentPdfRequest))]
// Documents - Form PDF exports
[JsonSerializable(typeof(FormPdfExport))]
// Documents - Form submission requests
[JsonSerializable(typeof(CreateFormSubmissionRequest))]
[JsonSerializable(typeof(UpdateFormSubmissionRequest))]
[JsonSerializable(typeof(FormTemplateRequest))]
[JsonSerializable(typeof(FormSubmissionAssignedTo))]
[JsonSerializable(typeof(FormSubmissionApprovalDetailsRequest))]
// Drivers - auth/QR
[JsonSerializable(typeof(RemoteSignOutRequest))]
[JsonSerializable(typeof(DriverAuthToken))]
[JsonSerializable(typeof(CreateDriverAuthTokenRequest))]
[JsonSerializable(typeof(DriverQrCode))]
[JsonSerializable(typeof(CreateDriverQrCodeRequest))]
// Industrial - data input data points
[JsonSerializable(typeof(DataInputDataPoint))]
// Industrial - Readings
[JsonSerializable(typeof(ReadingDefinition))]
[JsonSerializable(typeof(EnumValue))]
[JsonSerializable(typeof(ReadingHistory))]
[JsonSerializable(typeof(ReadingSnapshot))]
// Routes - audit log
[JsonSerializable(typeof(RouteAuditEvent))]
[JsonSerializable(typeof(RouteAuditChanges))]
[JsonSerializable(typeof(RouteAuditSnapshot))]
[JsonSerializable(typeof(RouteAuditStop))]
// Routes - Hubs extended
[JsonSerializable(typeof(HubCapacity))]
[JsonSerializable(typeof(HubCustomProperty))]
[JsonSerializable(typeof(HubLocation))]
[JsonSerializable(typeof(CreateHubLocationInput))]
[JsonSerializable(typeof(CreateHubLocationsRequest))]
[JsonSerializable(typeof(UpdateHubLocationRequest))]
[JsonSerializable(typeof(UpdateHubLocationEnvelopeRequest))]
[JsonSerializable(typeof(HubSkill))]
[JsonSerializable(typeof(HubPlan))]
[JsonSerializable(typeof(CreateHubPlanRequest))]
[JsonSerializable(typeof(HubPlanOrder))]
[JsonSerializable(typeof(CreateHubPlanOrderInput))]
[JsonSerializable(typeof(CreateHubPlanOrdersRequest))]
[JsonSerializable(typeof(HubOrderTask))]
[JsonSerializable(typeof(HubOrderAppointmentWindow))]
// Organization - Settings
[JsonSerializable(typeof(ComplianceSettings))]
[JsonSerializable(typeof(UpdateComplianceSettingsRequest))]
[JsonSerializable(typeof(DriverAppSettings))]
[JsonSerializable(typeof(UpdateDriverAppSettingsRequest))]
[JsonSerializable(typeof(DriverAppGamificationConfig))]
[JsonSerializable(typeof(DriverAppTrailerSelectionConfig))]
[JsonSerializable(typeof(SafetySettings))]
[JsonSerializable(typeof(SafetyDistractedDrivingAlertSettings))]
[JsonSerializable(typeof(SafetyFollowingDistanceAlertSettings))]
[JsonSerializable(typeof(SafetyForwardCollisionAlertSettings))]
[JsonSerializable(typeof(SafetyHarshEventSensitivitySettings))]
[JsonSerializable(typeof(SafetyHarshEventSensitivityV2Settings))]
[JsonSerializable(typeof(SafetyPolicyViolationsAlertSettings))]
[JsonSerializable(typeof(SafetyRollingStopAlertSettings))]
[JsonSerializable(typeof(SafetyScoreConfiguration))]
[JsonSerializable(typeof(SafetySpeedingSettings))]
[JsonSerializable(typeof(SafetyVoiceCoachingSettings))]
// Beta — Places
[JsonSerializable(typeof(PlaceDeletionMarker))]
// Source-gen completeness (kept in sync by DeserializationToleranceTests
// .EveryModelType_IsRegisteredInSourceGenContext)
[JsonSerializable(typeof(EntityReference))]
[JsonSerializable(typeof(CreateContactRequest))]
[JsonSerializable(typeof(UpdateContactRequest))]
[JsonSerializable(typeof(DeleteDriverVehicleAssignmentsRequest))]
[JsonSerializable(typeof(HosClocksForDriver))]
[JsonSerializable(typeof(HosCurrentDutyStatus))]
[JsonSerializable(typeof(HosViolationClocks))]
[JsonSerializable(typeof(HosBreakClock))]
[JsonSerializable(typeof(HosCycleClock))]
[JsonSerializable(typeof(HosDriveClock))]
[JsonSerializable(typeof(HosShiftClock))]
internal sealed partial class SamsaraJsonContext : JsonSerializerContext
{
}

/// <summary>
/// Provides the shared <see cref="JsonSerializerOptions"/> for Samsara API serialization.
/// </summary>
internal static class SamsaraSerializerOptions
{
    /// <summary>
    /// Primary (de)serialization options — source-generated for performance, and LENIENT on
    /// <c>required</c> members. The live Samsara API omits fields its own spec marks <c>required</c>
    /// on nearly every response, so enforcing them at runtime would fail constantly; instead an
    /// absent field is left at its default/<c>null</c>. Resolves through
    /// <see cref="SamsaraJsonContext"/> source generation with a reflection fallback only for the
    /// thin generic <c>{ data, pagination }</c> envelopes (their inner model types still bind via
    /// source generation). This relaxes the RUNTIME deserialization check only — models still
    /// declare <c>required</c> per the spec, and the C# <c>required</c> modifier still enforces
    /// request-DTO construction at compile time.
    /// </summary>
    public static JsonSerializerOptions Default { get; } = Create(relaxRequired: true);

    /// <summary>
    /// Strict source-generated options that ENFORCE every spec-declared <c>required</c> member
    /// (deserialization throws if one is absent). NOT used on the runtime response path — provided
    /// for callers that want to VALIDATE a payload against the spec (conformance tests / audits).
    /// </summary>
    public static JsonSerializerOptions Strict { get; } = Create(relaxRequired: false);

    private static JsonSerializerOptions Create(bool relaxRequired)
    {
        // Source generation first; reflection only as a fallback for unregistered types.
        IJsonTypeInfoResolver resolver = JsonTypeInfoResolver.Combine(
            SamsaraJsonContext.Default,
            new DefaultJsonTypeInfoResolver());

        if (relaxRequired)
        {
            resolver = resolver.WithAddedModifier(RelaxRequiredMembers);
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            TypeInfoResolver = resolver,
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

        return options;
    }

    /// <summary>
    /// Failover modifier: clears the runtime <see cref="JsonPropertyInfo.IsRequired"/> check on
    /// every property. The live Samsara API omits fields its own OpenAPI spec marks
    /// <c>required</c> (e.g. <c>Vehicle.createdAtTime</c>); when the strict path throws on one,
    /// the HTTP layer retries with these relaxed options so the caller still gets data. The
    /// missing field is left at its default/<c>null</c>. Deserialization only.
    /// </summary>
    private static void RelaxRequiredMembers(JsonTypeInfo typeInfo)
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object)
        {
            return;
        }

        foreach (var property in typeInfo.Properties)
        {
            property.IsRequired = false;
        }
    }
}
