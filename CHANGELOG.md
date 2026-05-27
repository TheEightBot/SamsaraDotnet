# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

- **Model sync 25-issues (2026-05-27)** — applied the per-domain remediation
  plan (3 HIGH, 16 MEDIUM, 6 LOW). `Issue` rebuilt to match the spec
  `IssueResponseObjectResponseBody` inner schema: three spec-REQUIRED fields
  (`issueSource`, `submittedAtTime`, `submittedBy`) added as non-nullable
  `required`; four existing fields (`title`, `status`, `createdAtTime`,
  `updatedAtTime`) tightened from nullable to `required`; four optional
  spec fields (`asset`, `assignedTo`, `dueDate`, `mediaList`) added. Four
  new nested response records (`IssueAsset`, `IssueUser`, `IssueSource`,
  `IssueMedia`) replace prior `object`/missing placeholders, mirroring
  the spec's `FormsAssetObjectResponseBody`,
  `FormsPolymorphicUserObjectResponseBody`, `IssueSourceObjectResponseBody`,
  and `FormsMediaRecordObjectResponseBody`. Six SDK-only flat scalars
  (`assigneeId`, `assigneeName`, `vehicleId`, `vehicleName`,
  `resolvedAtTime`, `type`) absent from the spec inner schema were removed.
  `CreateIssueRequest.Asset`/`AssignedTo` and `UpdateIssueRequest.AssignedTo`
  swapped from weak `object` to typed `IssueAssetRequest` /
  `IssueAssigneeRequest`; `Media` collections typed as
  `IReadOnlyList<IssueMediaItemRequest>`. `IIssuesClient.GetStreamAsync`
  gained five missing optional query parameters (`status`, `assetIds`,
  `assetExternalIds`, `include`, `assignedToRouteStopIds`) for the full
  spec query surface. See
  `docs/api-sync/model-sync-plan-2026-05-27/25-issues.md`.
- **Model sync 24-industrial (2026-05-27)** — applied the per-domain
  remediation plan (4 HIGH, 52 MEDIUM, 6 LOW). `IndustrialAsset` rebuilt to
  match the spec `AssetResponse` inner schema: three spec-REQUIRED fields
  (`id`, `name`, `isRunning`) are now non-nullable `required`, plus
  optional `statusCode`/`errorMessage` for the data-outputs endpoint;
  eight new nested records (`IndustrialAssetDataOutput`,
  `IndustrialAssetDataInput`, `IndustrialAssetDataInputLastPoint`,
  `IndustrialAssetLocation`, `IndustrialAssetLocationDataInput`,
  `IndustrialAssetParent`, `IndustrialAssetRunningStatusDataInput`,
  `IndustrialAssetTag`) replace prior placeholders. `DataInput` and
  `DataInputDataPoint` rebuilt to mirror `DataInputTinyResponse` +
  `DataInputResponse_allOf` / `DataInputSnapshot_allOf`; nine new nested
  data-point records (`NumberDataPoint`, `StringDataPoint`,
  `LocationDataPoint`, `LocationDataPointGpsLocation`,
  `LocationDataPointPlace`, `FftSpectraDataPoint`, `FftSpectraValue`,
  `J1939D1StatusDataPoint`, `J1939D1Status`) capture the per-type payload
  variants. Six SDK-only flat scalars (`DataInput.Points`,
  `DataInputDataPoint.Time`/`Value`, `IndustrialAsset.DataInputs`/`MacAddress`,
  and the legacy `DataPoint` record) absent from the spec inner schemas
  were removed. `IIndustrialClient` gained 27 optional query parameters
  across `ListAssetsAsync`, `ListDataInputsAsync`, `GetDataInputAsync`,
  the three data-point endpoints, and the v1 vision endpoints; the v1
  vision-runs endpoints now require a `long durationMs` argument.
  See `docs/api-sync/model-sync-plan-2026-05-27/24-industrial.md`.
- **Model sync 23-idling (2026-05-27)** — applied the per-domain remediation
  plan. `IdlingEvent` rebuilt to match the spec
  `IdlingEventObject_V2025_10_23ResponseBody` inner schema: nine spec-REQUIRED
  fields (`asset`, `durationMilliseconds`, `eventUuid`,
  `fuelConsumedMilliliters`, `fuelCost`, `gaseousFuelConsumedGrams`,
  `gaseousFuelCost`, `ptoState`, `startTime`) are now non-nullable `required`;
  five new nested records (`IdlingEventAsset`, `IdlingEventAddress`,
  `IdlingEventOperator`, `IdlingEventFuelCost`, `IdlingEventGaseousFuelCost`)
  replace prior flat `string?` / `object` placeholders and are registered in
  `SamsaraJsonContext`; the optional `airTemperatureMillicelsius` field was
  added. Eight SDK-only flat scalars (`id`, `vehicleId`, `vehicleName`,
  `driverId`, `driverName`, `endTime`, `durationMs`, `fuelConsumedMl`) absent
  from the spec inner schema were removed. `IIdlingClient.ListEventsAsync`
  gained 11 optional query parameters (`assetIds`, `operatorIds`, `ptoState`,
  `min/maxAirTemperatureMillicelsius`, `excludeEventsWithUnknownAirTemperature`,
  `min/maxDurationMilliseconds`, `tagIds`, `parentTagIds`, `includeExternalIds`)
  for the full spec query surface. See
  `docs/api-sync/model-sync-plan-2026-05-27/23-idling.md`.
- **Model sync 22-ifta (2026-05-27)** — applied the per-domain remediation
  plan. `IftaJurisdictionReportsResponse.Year` /
  `.JurisdictionReports`, `IftaVehicleReportsResponse.Year` /
  `.VehicleReports`, and `IftaDetailJob.Args` / `.JobStatus` /
  `.RequestedAtTime` tightened from nullable to non-nullable `required`
  (all spec-REQUIRED on their response wrappers). `IIftaClient.ListVehicleReportsAsync`
  gains the missing optional `after` query parameter for spec pagination.
  See `docs/api-sync/model-sync-plan-2026-05-27/22-ifta.md`.
- **Model sync 21-hubs (2026-05-27)** — applied the per-domain remediation
  plan, including two CRITICAL wrapper-drift fixes that previously broke the
  hub-location request bodies at runtime. **`POST /hub/locations`**: prior
  `CreateHubLocationRequest` is renamed to `CreateHubLocationInput`, gains the
  five spec-REQUIRED fields (`address`, `customerLocationId`, `hubId`,
  `isDepot`, `name` — all `required`) plus the missing optional fields
  (`driverInstructions`, `plannerNotes`, `serviceTimeSeconds`, `serviceWindows`,
  `skillsRequired`), and is now posted inside the new
  `CreateHubLocationsRequest { data: IReadOnlyList<CreateHubLocationInput> }`
  envelope per the spec. **`PATCH /hub/location/{id}`**: `UpdateLocationAsync`
  now takes the new `UpdateHubLocationEnvelopeRequest { data:
  UpdateHubLocationRequest }` so the inner update payload is wrapped in
  `{ data: T }` per the spec; `UpdateHubLocationRequest` gains nine missing
  optional fields (`customerLocationId`, `driverInstructions`, `isDepot`,
  `latitude`, `longitude`, `plannerNotes`, `serviceTimeSeconds`,
  `serviceWindows`, `skillsRequired`) and drops the spec-absent `notes` field.
  Query surface expanded: `ListCapacitiesAsync`, `ListCustomPropertiesAsync`,
  `ListLocationsAsync`, and `ListSkillsAsync` now require `hubId` (spec
  REQUIRED) and accept their full optional surface (`*Ids`, `*Names`,
  `startTime`, `endTime`); `ListHubsAsync` accepts `hubIds`, `startTime`, and
  `endTime`. Response records tightened: spec-REQUIRED fields on `Hub`
  (`createdAt`, `timeZone`, `updatedAt`), `HubLocation`
  (`address`, `name`, `customerLocationId`, `hubId`, `driverInstructions`,
  `plannerNotes`, `isDepot`, `latitude`, `longitude`, `serviceTimeSeconds`,
  `serviceWindows`, `skillsRequired`, `createdAt`, `updatedAt`), `HubCapacity`
  (`id`, `name`, `unit`, `createdAt`, `updatedAt`), `HubCustomProperty`
  (`hubId`, `name`, `csvColumns`, `createdAt`, `updatedAt`), and `HubSkill`
  (`hubId`, `name`, `createdAt`, `updatedAt`) are now non-nullable `required`.
  LOW response-side extras (`Hub.latitude/longitude/formattedAddress/geofence/tags/externalIds`,
  `HubCapacity.capacity/usedCapacity/timeSlot`, `HubCustomProperty.type`,
  `HubLocation.notes`) retained as nullable back-compat per the workflow
  precedent; request-side spec-absent `notes` removed from both
  `CreateHubLocationInput` and `UpdateHubLocationRequest`. See
  `docs/api-sync/model-sync-plan-2026-05-27/21-hubs.md`.
- **Model sync 20-hours-of-service (2026-05-27)** — applied the per-domain
  remediation plan. `HosLog` (response of `GET /fleet/hos/logs`) re-shaped to
  the spec's per-driver shape (`driver` + `hosLogs[]`), backed by new typed
  nested records `HosLogEntry` (with `logStartTime` required, codriver list,
  vehicle ref) and `HosLogLocation` (latitude/longitude). `HosViolation`
  (response of `GET /fleet/hos/violations`) re-shaped to spec's
  `{ violations[]: HosViolationEntry }` form with required `violations`; new
  `HosViolationEntry`, `HosViolationDay` (required start/end times), and
  `HosViolationDriver` (required `id`). `HosDailyLog` (response of
  `GET /fleet/hos/daily-logs`) tightens spec-required `driver`,
  `startTime`, `endTime` to non-nullable `required`, and gains five new
  typed nested records: `HosDailyLogDriver` (with timezone + ELD settings),
  `HosDailyLogEldSettings`, `HosDailyLogDriverRuleset`,
  `HosDailyLogDistanceTraveled` (drive/PC/yard-move distance meters),
  `HosDailyLogDutyStatusDurations` (also used for pending durations),
  `HosDailyLogMetaData` (carrier, home terminal, certification, shipping
  docs, trailers, vehicles), and `HosDailyLogVehicle`. LOW extras
  (`id`/`driverId`/`vehicleId`/etc. on `HosLog`, `HosViolation`,
  `HosDailyLog`) are retained as nullable back-compat per the workflow
  precedent. `IComplianceClient` query surfaces expanded:
  `ListHosLogsAsync` gains `driverIds`/`tagIds`/`parentTagIds`;
  `ListHosViolationsAsync` gains `driverIds`/`tagIds`/`parentTagIds`/`types`;
  `GetHosClocksAsync` gains `tagIds`/`parentTagIds`/`after`/`limit`;
  `ListHosDailyLogsAsync` gains
  `driverIds`/`startDate`/`endDate`/`tagIds`/`parentTagIds`/`driverActivationStatus`/`expand`
  (legacy `startTime`/`endTime` now fall through to `startDate`/`endDate`
  when those are null); `V1ListHosAuthenticationLogsAsync` now requires
  `long driverId` (spec REQUIRED) and converts the existing
  `startTime`/`endTime` `DateTimeOffset`s to the v1 endpoint's `startMs`/`endMs`
  query parameters. See `docs/api-sync/model-sync-plan-2026-05-27/20-hours-of-service.md`.
- **Model sync 19-gateways (2026-05-27)** — applied the per-domain
  remediation plan. `Gateway` (response) tightens spec-required `model` and
  `serial` to non-nullable `required string`, and gains three typed nested
  records: `GatewayAccessoryDevice` (`accessoryDevices`),
  `GatewayConnectionStatus` (`connectionStatus` — `healthStatus`,
  `lastConnected`), and `GatewayDataUsage` (`dataUsageLast30Days` —
  `cellularDataUsageBytes`, `hotspotUsageBytes`). `IGatewaysClient.ListAsync`
  adds an optional `models` (`IReadOnlyList<string>?`) query filter for
  `GET /gateways`, joined with `,`. LOW extras (`id`, `name`, `mainBus`,
  `firmwareVersion`, `wifiMacAddress`, `simCardId`, `vehicle`, `tags`) are
  retained as nullable back-compat per the workflow precedent. See
  `docs/api-sync/model-sync-plan-2026-05-27/19-gateways.md`.
- **Model sync 18-fuel-and-energy (2026-05-27)** — applied the per-domain
  remediation plan. `FuelEnergyVehicleReport` and `FuelEnergyDriverReport`
  (response rows) tighten `distanceTraveledMeters`, `efficiencyMpge`,
  `estFuelEnergyCost`, and `vehicle`/`driver` to non-nullable `required`
  per the spec. `DriverEfficiencyByDriver` and `DriverEfficiencyByVehicle`
  tighten `driverId` / `vehicleId` to `required string` and replace the four
  `object?` weak typings with four new typed nested records:
  `DriverEfficiencyDifficultyScore`, `DriverEfficiencyPercentageData`,
  `DriverEfficiencyRawData`, and `DriverEfficiencyScoreData`. New
  `FuelPurchaseMoney` (required `amount` + `currency`) replaces the
  previously-untyped `object` on `CreateFuelPurchaseRequest.TransactionPrice`
  (now required) and `Discount` (optional). `FuelPurchase` (response of
  `POST /fuel-purchase`) gains required `uuid`; legacy `id`, `driverId`,
  `vehicleId`, and other echoed fields are retained as nullable back-compat.
  `IFuelClient.GetDriverEfficiencyByDriverAsync` and
  `GetDriverEfficiencyByVehicleAsync` now require `startTime` / `endTime`
  (previously took no parameters) and expose optional
  `driverIds`/`vehicleIds`, `dataFormats`, `tagIds`, and `parentTagIds`.
  `ListVehicleFuelEnergyReportsAsync` and `ListDriverFuelEnergyReportsAsync`
  gain an optional `after` cursor. See
  `docs/api-sync/model-sync-plan-2026-05-27/18-fuel-and-energy.md`.
- **Model sync 17-forms (2026-05-27)** — applied the per-domain remediation
  plan. `FormSubmission` (response) now mirrors the spec's nested shape with
  required `createdAtTime`, `updatedAtTime`, `submittedAtTime`, `status`,
  `isRequired`, `fields`, `formTemplate` (object), and `submittedBy` (object),
  plus optional `title`, `approvalDetails`, `asset`, `geofence`, `location`,
  `score`, `externalIds`, `assignedTo`, `assignedAtTime`, `dueAtTime`,
  `routeId`, and `routeStopId`. Legacy flat-scalar fields (`formTemplateId`,
  `formTemplateName`, `driverId`, `driverName`, `vehicleId`, `vehicleName`,
  `state`, `fieldValues`) are retained as nullable back-compat. `FormTemplate`
  (response) gains required `title` (paired with the legacy `name` for
  back-compat per the rename guidance), `revisionId`, `fields`, `createdBy`,
  and `updatedBy`; `createdAtTime`/`updatedAtTime`/`sections` tightened from
  nullable to non-nullable; optional `approvalConfig` and `formCategory`
  added. `FormPdfExport` (response) tightened with required `pdfId`,
  `jobStatus`, `requestedAtTime`, and `expiresAtTime`, plus optional
  `completedAtTime`, `errorMessage`, and `pdfUrlExpiresAtTime`; the legacy
  `status`, `formSubmissionId`, and `createdAt` fields are retained as
  nullable back-compat. `CreateFormSubmissionRequest` now exposes the
  spec-required `formTemplate` (object) and `status` plus optional `title`,
  `assignedTo`, `dueAtTime`, `fields`, `isRequired`, and `routeStopId`;
  legacy `formTemplateId`, `driver`, `vehicle`, and `fieldValues` are
  retained as nullable back-compat. `UpdateFormSubmissionRequest` gains
  optional `title`, `status`, `isRequired`, `approvalDetails`, `assignedTo`,
  `dueAtTime`, and `routeStopId`; legacy `fieldValues` retained.
  `IFormsClient.ListTemplatesAsync` now accepts optional `ids` (array,
  joined by `,`). `GetSubmissionsStreamAsync` adds optional
  `formTemplateIds`, `userIds`, `driverIds`, `assignedToRouteStopIds`, and
  `include` array parameters. `GetPdfExportsAsync(string pdfId)` and
  `CreatePdfExportAsync(string id)` now accept the spec-required query
  parameters (breaking: `CreatePdfExportAsync` previously took
  `CreateFormPdfExportRequest`, which has been removed since the spec
  endpoint has no request body). See
  `docs/api-sync/model-sync-plan-2026-05-27/17-forms.md`.
- **Model sync 16-equipment (2026-05-27)** — applied the per-domain remediation
  plan. `IEquipmentClient` now exposes the spec's optional `parentTagIds`,
  `tagIds`, and `equipmentIds` query parameters across `ListAsync`,
  `ListLocationsAsync`, `GetLocationsFeedAsync`, `GetLocationsHistoryAsync`,
  `GetStatsAsync`, `GetStatsFeedAsync`, and `GetStatsHistoryAsync` (array
  params joined by `,` per the spec's `style=form,explode=false`). `Equipment`
  (response) now carries spec-defined `assetSerial` and a nested
  `installedGateway` (new `EquipmentInstalledGateway`); the legacy
  `equipmentSerialNumber` property is retained as nullable back-compat.
  `EquipmentLocation` now exposes the required spec shape: `location` for
  `GET /fleet/equipment/locations` and `locations` (array) for the
  `/locations/feed` and `/locations/history` endpoints, both backed by a new
  `EquipmentLocationPoint` record (required `latitude`/`longitude`/`time`,
  optional `heading`/`speed`); `Name` is tightened to required, and the legacy
  flat `latitude`/`longitude`/`time` properties are retained as nullable
  back-compat. `EquipmentStats` (response) gains the 12 spec-defined nested
  properties: `engineRpm`, `engineSeconds`, `engineTotalIdleTimeMinutes`,
  `gatewayEngineSeconds`, `gatewayEngineState` (singular object on `/stats`),
  `gatewayEngineStates` (array on `/feed`/`/history`),
  `gatewayJ1939EngineSeconds`, `gps`, `gpsOdometerMeters`, `obdEngineSeconds`,
  `obdEngineState` (singular), and `obdEngineStates` (array), plus
  `engineStates` and `fuelPercents`. Because the spec serializes
  `engineRpm`/`engineSeconds`/`engineTotalIdleTimeMinutes`/`gatewayEngineSeconds`/`gps`/`gpsOdometerMeters`/`obdEngineSeconds`
  as a single object on the snapshot endpoint and as an array on the
  feed/history endpoints, those fields are exposed as
  `System.Text.Json.JsonElement?` so both shapes deserialize without error.
  `Name` is tightened to required, and the legacy `engineState`,
  `fuelPercent`, `obdOdometer`, and `time` flat-scalar conveniences are
  retained as nullable back-compat. See
  `docs/api-sync/model-sync-plan-2026-05-27/16-equipment.md`.
- **Model sync 15-drivers (2026-05-27)** — applied the per-domain remediation
  plan. `IDriversClient.ListAsync` now accepts the 7 spec-defined optional
  query parameters (`driverActivationStatus`, `parentTagIds`, `tagIds`,
  `attributeValueIds`, `attributes`, `updatedAfterTime`, `createdAfterTime`),
  with array params joined by `,` per the spec's `style=form,explode=false`.
  `Driver` (response), `CreateDriverRequest`, and `UpdateDriverRequest` now
  carry typed properties for fields previously modeled as
  `System.Text.Json.JsonElement?`: `attributes` (`IReadOnlyList<object>?`
  matching the spec's `attributeTiny`/`CreateDriverRequest_attributes` inner
  schemas; precedent: `Equipment`, `Vehicle`, `Attributes` domains),
  `hasDrivingFeaturesHidden` and `hasVehicleUnpinningEnabled` (`bool?`), and
  `profileImageBase64` / `profileImageUrl` (`string?`). The remaining
  `JsonElement?` properties (`eldSettings`, `carrierSettings`, `hosSetting`,
  `peerGroupTag`, `trailerGroupTag`, `vehicleGroupTag`,
  `usDriverRulesetOverride`) are left intentionally untyped pending separate
  spec-shaped plans. See
  `docs/api-sync/model-sync-plan-2026-05-27/15-drivers.md`.
- **Model sync 14-driver-vehicle-assignments (2026-05-27)** — applied the
  per-domain remediation plan. `DriverVehicleAssignment` (response) now mirrors
  the spec's nested shape: required `driver` (new
  `DriverVehicleAssignmentDriver`, mirrors `GoaDriverTinyResponseResponseBody`),
  required `vehicle` (new `DriverVehicleAssignmentVehicle`, mirrors
  `GoaVehicleTinyResponseResponseBody`), required `isPassenger`, and required
  `startTime`, plus optional `assignedAtTime`, `endTime`, `assignmentType`,
  `metadata` (new typed `DriverVehicleAssignmentMetadata` with `sourceName`),
  and `message` (POST/PATCH outcome). The legacy flat-scalar conveniences
  `id`, `driverId`, `driverName`, `vehicleId`, and `vehicleName` are retained
  as nullable, documented back-compat properties (not in spec inner schema).
  `IDriverVehicleAssignmentsClient.ListAsync` now accepts optional
  `driverTagIds` and `vehicleTagIds` query parameters per spec; the `filterBy`
  doc string was corrected to the spec's `drivers`/`vehicles` valid values.
  See `docs/api-sync/model-sync-plan-2026-05-27/14-driver-vehicle-assignments.md`.
- **Model sync 13-driver-trailer-assignments (2026-05-27)** — applied the
  per-domain remediation plan. `DriverTrailerAssignment` (response) now mirrors
  the spec's nested shape: required `id`, `driver` (new
  `DriverTrailerAssignmentDriver` with required `driverId` and optional
  `externalIds`), `trailer` (new `DriverTrailerAssignmentTrailer` with required
  `trailerId`), and `startTime` (RFC 3339 string), plus optional
  `createdAtTime`, `endTime`, and `updatedAtTime`. The legacy flat-scalar
  conveniences `driverId`, `driverName`, `trailerId`, `trailerName`, and `time`
  are retained as nullable, documented back-compat properties (not in spec
  inner schema). `CreateDriverTrailerAssignmentRequest` gains optional
  `startTime` (RFC 3339 string). `UpdateDriverTrailerAssignmentRequest` now
  carries only `required string EndTime` (spec marks `endTime` REQUIRED) — the
  SDK-only `driverId`/`trailerId` body fields were dropped (not in spec body).
  `IDriverTrailerAssignmentsClient.ListAsync` now requires
  `IReadOnlyList<string> driverIds` (spec REQUIRED on
  `GET /driver-trailer-assignments`) and accepts optional
  `bool? includeExternalIds`. `UpdateAsync` now takes the assignment `id`
  separately and appends it via `QueryBuilder.WithParams("id", ...)` to match
  the spec's required `id` query parameter on `PATCH /driver-trailer-assignments`.
  See `docs/api-sync/model-sync-plan-2026-05-27/13-driver-trailer-assignments.md`.
- **Model sync 12-driver-qr-codes (2026-05-27)** — applied the per-domain
  remediation plan. `IDriversClient.ListQrCodesAsync` now requires
  `IReadOnlyList<string> driverIds` (spec REQUIRED on `GET /drivers/qr-codes`)
  and appends it via `QueryBuilder.WithParams`. `DriverQrCode` now uses
  `long DriverId` (spec `integer/int64`) and exposes optional `qrCodeLink`;
  removed SDK-only `qrCodeUrl` and `expiresAt` (not in spec inner schema).
  `CreateDriverQrCodeRequest.DriverId` is now `required long` to match the
  spec's required `integer/int64`. See
  `docs/api-sync/model-sync-plan-2026-05-27/12-driver-qr-codes.md`.
- **Model sync 11-documents (2026-05-27)** — applied the per-domain
  remediation plan. `Document` now exposes the spec-required `createdAtTime`,
  `documentType`, `driver`, `state`, and `fields` plus optional `vehicle`,
  `route`, `routeStop`, `updatedAtTime`, and `conditionalFieldSections`. The
  nested objects are backed by new typed records — `DocumentTypeRef`,
  `DriverRef`, `VehicleRef`, `RouteRef`, `RouteStopRef`, and
  `ConditionalFieldSection` — mirroring the spec's `Goa*TinyResponse` shapes.
  Removed the SDK-only flat scalars `documentTypeId`, `driverId`, `vehicleId`,
  `createdAtMs`, and `updatedAtMs` (not in spec; superseded by the nested
  references). `CreateDocumentRequest.DriverId` is now `required` (spec
  marks it REQUIRED) and gains optional `name`, `vehicleId`, `routeStopId`,
  `state`. `DocumentPdfJob` gains optional `jobStatus`, `requestedAtTime`,
  `completedAtTime`, `downloadDocumentPdfUrl` and drops SDK-only `status` and
  `pdfUrl`. `DocumentType` gains optional `conditionalFieldSections`.
  `IDocumentsClient.ListAsync` now requires `startTime` and `endTime`
  (spec REQUIRED) and accepts optional `documentTypeId` and `queryBy`. See
  `docs/api-sync/model-sync-plan-2026-05-27/11-documents.md`.
- **Model sync 10-contacts (2026-05-27)** — applied the per-domain
  remediation plan. Relaxed `CreateContactRequest.FirstName`, `LastName`,
  `Email`, and `Phone` from `required` to optional (`string?`) — the spec
  declares no required properties on `CreateContactRequest`, so the prior
  `required` modifier blocked otherwise valid partial requests. The
  `Contact` response (which DOES require all five fields per spec) and
  `UpdateContactRequest` (already nullable) are unchanged. See
  `docs/api-sync/model-sync-plan-2026-05-27/10-contacts.md`.
- **Model sync 09-coaching (2026-05-27)** — applied the per-domain
  remediation plan. `CoachingSession` now exposes spec-required `behaviors`,
  `coachingType`, `driver`, `dueAtTime`, `sessionStatus`, `updatedAtTime` plus
  optional `assignedCoachId`, `completedCoachId`, `sessionNote`.
  `DriverCoachAssignment` now exposes spec-required nested `driver`,
  `createdAtTime`, `updatedAtTime`, and tightens `coachId` to non-nullable
  (spec marks REQUIRED). Two new shared records, `CoachingDriver`
  (`DriverWithExternalIdObjectResponseBody`) and `CoachingBehavior`
  (`behaviorResponseBody`), back the nested `driver` and `behaviors`
  properties. Legacy flat scalars (`CoachingSession.DriverId/CoachId/Status/
  ScheduledAtTime/SessionType`, `DriverCoachAssignment.DriverId/DriverName/
  CoachName`) retained for back-compat. `ICoachingClient.ListAssignmentsAsync`
  gains optional `driverIds`, `coachIds`, `includeExternalIds`;
  `GetSessionsStreamAsync` gains optional `driverIds`, `coachIds`,
  `sessionStatuses`, `includeCoachableEvents`, `includeExternalIds`.
  `SetAssignmentAsync` gains a primary `(string driverId, string? coachId)`
  overload that sends both as query parameters (spec-compliant — no JSON
  body); the legacy `(SetDriverCoachAssignmentRequest)` overload is preserved
  and now forwards to the primary overload. See
  `docs/api-sync/model-sync-plan-2026-05-27/09-coaching.md`.
- **Model sync 08-carrier-proposed-assignments (2026-05-27)** — applied the
  per-domain remediation plan. `CarrierProposedAssignment` now exposes the
  spec-required `activeTime` plus optional `acceptedTime`, `firstSeenTime`,
  `rejectedTime`, `shippingDocs`, and nested `driver` / `vehicle` / `trailers`
  objects (new `CarrierProposedAssignmentDriver`,
  `CarrierProposedAssignmentVehicle`, `CarrierProposedAssignmentTrailer`
  records mirroring the spec's tiny-response composition). Removed SDK-only
  `endTime`, `startTime`, `status`, `shippingId` (not in spec); retained the
  flat-scalar `driverId`, `driverName`, `vehicleId`, `vehicleName` properties
  alongside the new nested objects for back-compat.
  `CreateCarrierProposedAssignmentRequest` gains optional `activeTime`,
  `shippingDocs`, `trailerIds`, `trailerNames`; removed SDK-only `endTime`,
  `startTime`, `shippingId`. `ICarrierProposedAssignmentsClient.ListAsync`
  gains optional `driverIds` and `activeTime` query parameters. See
  `docs/api-sync/model-sync-plan-2026-05-27/08-carrier-proposed-assignments.md`.
- **Model sync 07-carb-ctc (2026-05-27)** — applied the per-domain
  remediation plan. `CarbCtcVehicle` now exposes spec-required
  `enrollmentId`, `enrollmentVin`, `testStatus` plus optional
  `testStatusDetails`, `lastCollectionAtTime`, `nextCollectionAtTime`;
  removed SDK-only `name`, `vin`, `licensePlate`, `complianceStatus`,
  `modelYear`, `fuelType` (not in spec). `CarbCtcVehicleHistory` now
  exposes spec-required `enrollmentId`, `enrollmentVin`, `happenedAtTime`,
  `testResult` plus optional `testResultDetails`; removed SDK-only
  `vehicleId`, `time`, `event`, `details`. `ICarbCtcClient.ListVehiclesAsync`
  gains optional `tagIds`, `parentTagIds`, `testStatus` query parameters;
  `ListVehicleHistoryAsync` promotes `vehicleIds` to a required parameter
  (spec marks `required: true`). See
  `docs/api-sync/model-sync-plan-2026-05-27/07-carb-ctc.md`.
- **Model sync 06-beta-apis (2026-05-27)** — applied the per-domain
  remediation plan. Added spec-required and spec-optional query parameters
  across all Beta clients (`BetaClient`, `FunctionsClient`, `PlacesClient`,
  `PreferredStationsClient`, `QualificationRecordsClient`, `ReportsClient`,
  `RidershipClient`) plus the Beta operations on `ComplianceClient` (HOS
  ELD events filters, `UpdateShippingDocsAsync` now requires `driverID` and
  `hosDate`), `TachographClient` (live-data filters), `VehiclesClient`
  (`vehicleIds` required for immobilizer stream), `AssetsClient`
  (depreciation `assetIds`/time-range; inputs stream `ids` + `type`
  required), `DriversClient` (`workflowType` for workflow listing),
  `HubsClient` (`planId` required for plan orders, `hubId` required for
  route templates), and `MaintenanceClient` (vendor `ids`/`includeExternalIds`).
  `HubPlanOrder` response now exposes all 8 spec-required fields
  (`hubId`, `customerOrderId`, `priority`, `createdAtTime`, `updatedAtTime`,
  `customProperties`, `quantities`, `skillsRequired`) and tightens `planId`
  to non-nullable; adds optional `routeId`, `pickup`, `delivery` (typed
  as `object?` per Beta posture). `UpdateEquipmentRequest` adds `id`,
  `equipmentSerialNumber`, `engineHours`, `odometerMeters`, `tagIds`, and
  `attributes`. `Equipment` response adds optional `attributes`.
  `HosEldEvent` adds `name` and `driverActivationStatus`. Deeply nested
  `object?` payloads on Beta records (e.g., `HosEldEvent.eldEvents`,
  `HosEldEvent.externalIds`) intentionally remain weakly-typed per the
  documented Beta subject-to-change posture; 2 MEDIUM weak-typing items
  deferred. See
  `docs/api-sync/model-sync-plan-2026-05-27/06-beta-apis.md`.
- **Model sync 05-auth-token-for-driver (2026-05-27)** — applied the
  per-domain remediation plan. `CreateDriverAuthTokenRequest` now exposes
  the spec-required `code` (`required string`), plus optional `externalId`
  and `username` (`string?`); `driverId` switches from `required string`
  to `long?` (spec `integer/int64`, optional — one of `driverId`,
  `externalId`, or `username` is required). `DriverAuthToken` response
  drops the non-spec `driverId` and `expiresAt` and adds the spec-required
  `expirationTime` (`required long`, Unix milliseconds). See
  `docs/api-sync/model-sync-plan-2026-05-27/05-auth-token-for-driver.md`.
- **Model sync 04-attributes (2026-05-27)** — applied the per-domain
  remediation plan. `IAttributesClient.ListAsync`, `GetAsync`, and `DeleteAsync`
  now take the spec-required `entityType` query parameter. `AttributeDefinition`
  response adds `unit` (string) and `values` (`IReadOnlyList<object>?`) and
  tightens `entities` to non-nullable (defaults to an empty list). Request
  bodies gain `entities` (`CreateAttributeRequest` and `UpdateAttributeRequest`)
  and `unit` (`CreateAttributeRequest`). CLI menu updated to prompt for entity
  type on list/get/delete. See
  `docs/api-sync/model-sync-plan-2026-05-27/04-attributes.md`.
- **Model sync 03-assets (2026-05-27)** — applied the per-domain remediation
  plan. `Asset` response now exposes `attributes` (raw `JsonElement` list,
  matching the spec inner `GoaAttributeTinyResponseBody`) and tightens
  `createdAtTime`/`updatedAtTime` to non-nullable `DateTimeOffset` (both marked
  REQUIRED by the spec). `CreateAssetRequest.Name` is no longer `required`
  (spec marks it optional). `IAssetsClient.ListAsync` now exposes all 11
  documented optional query parameters (`type`, `updatedAfterTime`,
  `includeExternalIds`, `includeTags`, `includeAttributes`, `tagIds`,
  `parentTagIds`, `ids`, `externalIds`, `attributeValueIds`, `attributes`).
  `UpdateAsync(string id, ...)` and `DeleteAsync(string id, ...)` now thread
  the spec-required `id` query parameter (the previous `DeleteAsync` accepted
  `string[] ids` and hand-built a non-spec `ids[]=` query string). Legacy v1
  reefer/location operations now take their spec-required `startMs`/`endMs`
  (Unix epoch milliseconds, `long`); reefer-list and current-locations also
  surface the optional `startingAfter`/`endingBefore`/`limit` pagination
  parameters. See `docs/api-sync/model-sync-plan-2026-05-27/03-assets.md`.
- **Model sync 02-alerts (2026-05-27)** — applied the per-domain remediation
  plan. `AlertConfiguration` and `AlertIncident` response records now expose
  every spec-required field with the correct `required`/non-nullable typing
  (`actions`/`triggers`/`scope`/`createdAtTime`/`lastModifiedAtTime`/`isEnabled`
  on configs; `conditions`/`happenedAtTime`/`incidentUrl`/`isResolved`/
  `updatedAtTime` on incidents). SDK-only extras absent from the spec inner
  schema were removed (`AlertConfiguration.ConditionType`;
  `AlertIncident.{Id,AlertId,Driver,Vehicle,TriggeredAtTime}`). Weak `object?`
  on `scope`/`operationalSettings`/`triggers`/`actions` replaced with new typed
  wrappers `AlertScope`, `AlertTrigger`, `AlertAction`,
  `AlertOperationalSettings`, and `AlertIncidentCondition`. CLI rendering
  updated to surface incident `ConfigurationId`/`HappenedAtTime`/`IncidentUrl`
  (the prior `Id` column is not in the spec). See
  `docs/api-sync/model-sync-plan-2026-05-27/02-alerts.md`.
- **Model sync 01-addresses (2026-05-27)** — applied the per-domain remediation
  plan: `Address.formattedAddress` and `Address.geofence` are now non-nullable
  on the response (spec marks both REQUIRED); `IAddressesClient.ListAsync` now
  exposes the spec's `parentTagIds`, `tagIds`, and `createdAfterTime` query
  parameters. Remaining plan findings (`addressTypes`/`contacts`/`notes`/
  `createdAtTime` on `Address`; `addressTypes`/`contactIds`/`notes`/`geofence`
  on the create/update requests) were already present in the SDK and required
  no edits. See `docs/api-sync/model-sync-plan-2026-05-27/01-addresses.md`.
- **Broken-domain reworks (API sync)** — five domains rebuilt to call the real spec endpoints:
  - **Issues** — `getIssues` requires the `ids` query parameter (no true get-by-id);
    `patchIssue` and `postIssue` exist; SDK now takes `ListAsync(IReadOnlyList<string> ids)`,
    `UpdateAsync(UpdateIssueRequest)` with id in body, `CreateAsync(CreateIssueRequest)`.
  - **Driver-Vehicle Assignments** — `Update` takes `UpdateDriverVehicleAssignmentRequest`
    (driverId/vehicleId/startTime in body); `Delete` takes `DeleteDriverVehicleAssignmentsRequest`
    in the body (DELETE with body); `List` requires `filterBy`.
  - **Sensors** — rebuilt as the v1 POST API (`/v1/sensors/{list,history,cargo,door,humidity,temperature}`)
    with proper long-int models and a new `PostAsync<T>` helper for v1's no-envelope responses.
  - **Fuel** — replaced non-existent `/fleet/vehicles/fuel/*` with `ListVehicleFuelEnergyReports`,
    `ListDriverFuelEnergyReports`, `GetDriverEfficiencyByDriver/ByVehicle`, and
    `CreateFuelPurchase` (POST `/fuel-purchase`).
  - **IFTA** — replaced non-existent detail/summary with `ListJurisdictionReports` and
    `ListVehicleReports` (year-required); CSV jobs moved to `/ifta-detail/csv` with
    spec-correct startHour/endHour/vehicleIds/vehicleTagIds/vehicleParentTagIds.
  - **Alerts** — top-level `/alerts` CRUD removed (not in spec); `UpdateConfiguration` and
    `DeleteConfiguration` move id to body/query respectively; `GetIncidentsStream` now requires
    `startTime` and `configurationIds`. `CreateAlertConfigurationRequest` gains required
    `isEnabled`/`scope`/`triggers`/`actions`.
  - **Compliance** — `GetHosClocks` now returns `IReadOnlyList<HosClocksForDriver>` with the
    correct nested `break`/`cycle`/`drive`/`shift` model.
- **Request-body required fields tightened** (per spec): `CreateDriverRequest.username`,
  `CreateUserRequest.authType`/`roles`, `CreateAddressRequest.formattedAddress`/`geofence`,
  `CreateAttributeRequest.attributeType`, `UpdateAttributeRequest.entityType`.
- **Response models corrected:**
  - `Tag` — `parentTagId: string` → `parentTag: EntityReference`; added `externalIds`.
  - `Address` — added `contacts: EntityReference[]` and `createdAtTime` (the v1-style
    `contactIds: string[]` is not in the spec).
  - `VehicleLocation` — `latitude`/`longitude`/`time` are now required; added `reverseGeo`.
  - `Vehicle` — added `auxInputType3..13`, `attributes`, `vehicleType`, `esn`, `cameraSerial`,
    `grossVehicleWeight`, `sensorConfiguration`, `engineHours`, `odometerMeters`, `gatewaySerial`.
  - `AttributeEntity` — added `entityId` and `values`.

### Status (as of these changes)

**SDK ↔ spec parity is complete and independently verified** — all 317 spec operations
have at least one matched SDK method. `tools/check-sdk-sync.py` reports
`matched=323, mismatched=0, missing=0` against the 2025-10-23 spec; a separate
independent agent verification on 2026-05-27 reached the same conclusion.
`dotnet build` and the 59-test suite pass. The cached baseline has been refreshed.

### Fixed (post-parity)

- **`IFormsClient.ListSubmissionsAsync` / `GetSubmissionAsync`** — the spec's
  `getFormSubmissions` requires an `ids[]` query parameter (plural, array). The
  prior signatures (no-arg list, `?id=` get) would have 400'd at runtime. List now
  takes `IReadOnlyList<string> ids`; `GetSubmissionAsync` is a convenience wrapper
  that calls list with a single id and returns the first match (mirrors
  `Issues.GetAsync`). Caught by independent agent verification.

### Added

- **10 new domain clients** for full coverage of the spec:
  - `ILegacyApisClient` (v1 fleet/reports endpoints — `/fleet/defects/history`,
    `/fleet/dvirs/history`, legacy safety events feed, vehicle harsh-event lookup, etc.)
  - `IPreviewApisClient` (`/preview/*` — vehicle lock/unlock, gateway pairing,
    driver auth-token preview)
  - `IRouteEventsClient` (`/route-events/stream`)
  - `IPlacesClient` (Beta — `/places` CRUD)
  - `IPreferredStationsClient` (Beta — `/preferred-stations` CRUD)
  - `IQualificationRecordsClient` (Beta — records CRUD + stream + types + archive/unarchive)
  - `IRidershipClient` (Beta — passengers + route-setups CRUD)
  - `IFunctionsClient` (Beta — Samsara Functions + Functions Storage)
  - `IReportsClient` (Beta — `/reports/configs`, `/reports/datasets`, `/reports/runs*`)
  - `IBetaClient` (Beta misc — industrial jobs, devices, detections stream, AEMP equipment,
    driver-efficiency)
- **Extensions to existing clients** covering the rest of the gaps:
  - `IContactsClient` — Create / Update / Delete
  - `IFormsClient` — Create / Update form submissions
  - `IGatewaysClient` — Create / Delete
  - `IHubsClient` — `ListHubsAsync` (the canonical `GET /hubs`), `ListPlanOrdersAsync`,
    `ListPlanRoutesAsync`, `DeletePlanOrdersAsync`, `ListRouteTemplatesAsync`,
    `DeleteRouteTemplatesAsync`
  - `IEquipmentClient` — `GetStatsAsync` (snapshot)
  - `ITachographClient` — `ListVehicleFilesAsync`, `ListLiveDataAsync`
  - `ITrailerAssignmentsClient` — `GetByTrailerAsync`
  - `ITrainingClient` — `CreateAssignmentsAsync` / `UpdateAssignmentsAsync` / `DeleteAssignmentsAsync`
  - `IRoutesClient` — `V1DeleteDispatchRouteAsync`
  - `IComplianceClient` — `V1ListHosAuthenticationLogsAsync`, `V1SetCurrentDutyStatusAsync`,
    `UpdateShippingDocsAsync`
  - `ISafetyClient` — `V1GetDriverSafetyScoreAsync`, `V1GetVehicleSafetyScoreAsync`,
    `PatchEventsBatchAsync`
  - `IMaintenanceClient` — `V1ListMaintenanceAsync`, `ListVendorsAsync`, `ListVendorCategoriesAsync`
  - `IAssetsClient` — `V1GetAllAssetsAsync`, `V1GetAllAssetCurrentLocationsAsync`,
    `V1GetAssetsReefersAsync`, `V1GetAssetLocationAsync`, `V1GetAssetReeferAsync`,
    `GetDepreciationTransactionsAsync`, `GetInputsStreamAsync`,
    `ListDeviceRecoveryMissingAsync`, `MarkAssetMissingAsync`, `RecoverAssetAsync`
  - `IIndustrialClient` — `CreateAssetAsync` / `UpdateAssetAsync` / `UpdateAssetDataOutputsAsync` /
    `DeleteAssetAsync` plus v1 Vision (`V1ListCamerasAsync`, runs, programs) and
    v1 Machines (`V1ListMachinesAsync`, `V1GetMachineHistoryAsync`)
  - `IReadingsClient` — `CreateAsync` (POST `/readings`)
  - `IDriversClient` — `ListWorkflowsAsync`, `CreateWorkflowAssignmentAsync`,
    `ResolveVoiceSignInAssignmentAsync`
  - `IVehiclesClient` — `GetImmobilizerStreamAsync`, `UpdateImmobilizerStateAsync`
- New request/response models: `CreateContactRequest`/`UpdateContactRequest`,
  `CreateFormSubmissionRequest`/`UpdateFormSubmissionRequest`, `CreateGatewayRequest`.
- **`tools/check-sdk-sync.py`** — extended to follow class-level `const string` aliases
  inside interpolated strings, and to scan files that combine interface + impl declarations.

- **Endpoint path corrections (API sync)** — fixed wrong URL paths that returned 404:
  Tags `/fleet/tags`→`/tags`; Messages→`/v1/fleet/messages`; Forms→`/form-templates`,
  `/form-submissions`; Tachograph +`/history`; Training→`/training-assignments/stream`,
  `/training-courses`; Trailer Assignments→`/v1/fleet/trailers/assignments`; Routes audit
  log→`/fleet/routes/audit-logs/feed`; Industrial→`/industrial/data-inputs`; Gateways→
  `/gateways`; Trips list→`/v1/fleet/trips`; Maintenance DVIR/defect paths→`/dvirs/*`,
  `/defects/*`, `/defect-types`; Compliance ELD events→`/beta/fleet/hos/drivers/eld-events`;
  Equipment update→`/beta/fleet/equipment/{id}`; Media→`/cameras/media` + `/cameras/media/retrieval`.
- **`IComplianceClient.GetHosClocksAsync`** — now takes `IReadOnlyList<string> driverIds` and
  returns `IReadOnlyList<HosClocksForDriver>` (was single driver / flat `HosClocks`); path
  corrected to `/fleet/hos/clocks`. `HosClocks` is now the nested break/cycle/drive/shift object.

### Removed

- **Fabricated operations** that have no Samsara API endpoint (breaking; all previously 404'd):
  `Drivers.DeleteAsync`, `Vehicles.CreateAsync`/`DeleteAsync`, `Equipment.CreateAsync`/`DeleteAsync`,
  `CarrierProposedAssignments.UpdateAsync`, `Gateways.GetAsync` (no get-by-id),
  `Safety.GetEventAsync` (no get-by-id), `Media.GetAsync` (no get-by-id),
  `Maintenance.ListDtcsAsync`. Duplicate DVIR methods removed from `IComplianceClient`
  (DVIRs live on `IMaintenanceClient`).

### Added

- **API sync documentation** — `docs/api-sync/` folder with 56 per-domain Markdown checklists
  tracking coverage of every endpoint in the Samsara OpenAPI spec (version `2025-10-23`)
- **`tools/check-api-sync.py`** — CLI script that fetches the live Samsara spec, diffs it against
  a cached baseline, and emits a `docs/api-sync/diff-report.md` with new/removed/changed endpoints
- **`.github/workflows/api-sync-check.yml`** — Weekly GitHub Actions workflow (Mondays 08:00 UTC)
  that runs the sync check and opens a labeled `api-sync` issue when API drift is detected
- **`IAssetsClient` / `AssetsClient`** — CRUD for fleet assets (`/assets`), plus location-and-speed
  stream (`/assets/location-and-speed/stream`)
- **`ICarbCtcClient` / `CarbCtcClient`** — CARB CTC compliance vehicle list and history
  (`/fleet/carb-ctc/vehicles`, `/fleet/carb-ctc/vehicles/history`)
- **`ICoachingClient` / `CoachingClient`** — Driver coach assignments and coaching sessions stream
  (`/coaching/driver-coach-assignments`, `/coaching/sessions/stream`)
- **`IDriverTrailerAssignmentsClient` / `DriverTrailerAssignmentsClient`** — Driver-trailer pairing
  CRUD (`/driver-trailer-assignments`)
- **`IIdlingClient` / `IdlingClient`** — Idling events stream (`/idling/events`)
- **`ILiveSharingLinksClient` / `LiveSharingLinksClient`** — Live sharing link management
  (`/live-shares`)
- **`IReadingsClient` / `ReadingsClient`** — Readings definitions, history, and latest snapshot
  (`/readings/definitions`, `/readings/history`, `/readings/latest`)
- **`ISettingsClient` / `SettingsClient`** — Compliance, driver-app, and safety fleet settings
  (`/fleet/settings/compliance`, `/fleet/settings/driver-app`, `/fleet/settings/safety`)
- **`IWorkOrdersClient` / `WorkOrdersClient`** — Work orders CRUD, stream, service tasks, and
  invoice scans (`/maintenance/work-orders`, `/maintenance/service-tasks`, `/maintenance/invoice-scans`)
- **`IVehiclesClient`** — Added `GetLocationsFeedAsync`, `GetLocationsHistoryAsync`,
  `GetStatsFeedAsync`, `GetStatsHistoryAsync`, `GetSpeedingIntervalsStreamAsync`
- **`ITrailersClient`** — Added `GetStatsSnapshotAsync`, `GetStatsFeedAsync`, `GetStatsHistoryAsync`
- **`IEquipmentClient`** — Added `GetLocationsFeedAsync`, `GetLocationsHistoryAsync`,
  `GetStatsFeedAsync`, `GetStatsHistoryAsync`
- **`IDocumentsClient`** — Added `GeneratePdfAsync`, `GetPdfAsync`, `DeleteAsync`
- **`IDriversClient`** — Added `RemoteSignOutAsync`, `CreateAuthTokenAsync`, `ListQrCodesAsync`,
  `CreateQrCodeAsync`, `DeleteQrCodeAsync`
- **`IFormsClient`** — Added `GetSubmissionsStreamAsync`, `GetPdfExportsAsync`, `CreatePdfExportAsync`
- **`IHubsClient`** — Added `ListCapacitiesAsync`, `ListCustomPropertiesAsync`, `ListLocationsAsync`,
  `CreateLocationAsync`, `UpdateLocationAsync`, `ListSkillsAsync`, `CreatePlanAsync`,
  `ListPlansAsync`, `CreatePlanOrdersAsync`
- **`IIftaClient`** — Added `CreateDetailJobAsync`, `GetDetailJobAsync`
- **`IIndustrialClient`** — Added `ListDataInputsAsync`, `GetDataInputSnapshotAsync`,
  `GetDataInputFeedAsync`, `GetDataInputHistoryAsync`
- **`IIssuesClient`** — Added `GetStreamAsync`
- **`IMaintenanceClient`** — Added `GetDvirsStreamAsync`, `GetDvirByIdAsync`, `CreateDvirAsync`,
  `UpdateDvirAsync`, `GetDefectsStreamAsync`, `GetDefectAsync`, `UpdateDefectAsync`,
  `ListDefectTypesAsync`
- **`IMediaClient`** — Added `GetRetrievalAsync`, `CreateRetrievalAsync`
- **`IRoutesClient`** — Added `GetAuditLogFeedAsync`
- **`ISafetyClient`** — Fixed all endpoint paths to current API (safety-events, safety-scores/*);
  added `GetEventsStreamAsync`, `ListDriverSafetyScoresAsync` (v2), `ListVehicleSafetyScoresAsync` (v2),
  `ListTagSafetyScoresAsync` (v2), `ListTagGroupSafetyScoresAsync` (v2)
- **`ITagsClient`** — Added `ReplaceAsync` (PUT /tags/{id})
- **`ITripsClient`** — Added `GetStreamAsync` (GET /trips/stream)
- **`IAlertsClient`** — Added `GetIncidentsStreamAsync`
- **`ISamsaraClient`** — Added 9 new domain client properties: `Assets`, `CarbCtc`, `Coaching`,
  `DriverTrailerAssignments`, `Idling`, `LiveSharingLinks`, `Readings`, `Settings`, `WorkOrders`
- New model types: `Asset`, `AssetLocationAndSpeed`, `CarbCtcVehicle`, `DriverCoachAssignment`,
  `CoachingSession`, `DriverTrailerAssignment`, `IdlingEvent`, `LiveSharingLink`,
  `ReadingDefinition`, `ReadingHistory`, `ReadingSnapshot`, `ComplianceSettings`,
  `DriverAppSettings`, `SafetySettings`, `WorkOrder`, `ServiceTask`, `InvoiceScan`,
  `TrailerStats`, `EquipmentStats`, `SpeedingInterval`, `RouteAuditEvent`, `DocumentPdfJob`,
  `FormPdfExport`, `MediaRetrieval`, `IftaDetailJob`, `DataInputDataPoint`, `HubCapacity`,
  `HubLocation`, `HubSkill`, `HubPlan`, `MaintenanceDefect`, `DefectType`, `CreateDvirRequest`,
  `DriverAuthToken`, `DriverQrCode`, and more

### Fixed

- **`CreateRouteRequest`** — removed non-existent `scheduledStartMs`/`scheduledEndMs` fields; added
  `externalIds`, `recomputeScheduledTimes`, `tagIds`; made `driverId` optional; `stops` is now required
- **`UpdateRouteRequest`** — same field corrections as above; added `stops` (was missing entirely)
- **`Route`** response — replaced deprecated ms-epoch time fields with ISO string equivalents
  (`scheduledRouteStartTime`, `scheduledRouteEndTime`, `actualRouteStartTime`, `actualRouteEndTime`);
  added `externalIds`, `tagIds`, `orgLocalTimezone`, `createdAt`, `updatedAt`, `dispatchRouteId`,
  `distanceMeters`, `durationSeconds`, `hubId`, `isEdited`, `isPinned`, `planId`, `type`, `quantities`
- **`RouteStop`** response — removed `addressId`, `latitude`, `longitude`; added `sequenceNumber`,
  `ontimeWindowAfterArrivalMs`, `ontimeWindowBeforeArrivalMs`, `enRouteTime`, `eta`, `skippedTime`,
  `actualDistanceMeters`, `plannedDistanceMeters`, `liveSharingUrl`, `address`, `hubLocationId`, `orders`
- **`RouteSettings`** — added `sequencingMethod`
- **`CreateRouteStopRequest`** — removed `latitude`/`longitude`; added `sequenceNumber`, ontime window fields
- **`UpdateRouteStopRequest`** — new class (was missing from the SDK entirely)
- **`Asset`** response — fixed `assetType` → `type`; added `createdAtTime`, `updatedAtTime`, `vin`,
  `readingsIngestionEnabled`, `regulationMode`, `serialNumber`
- **`CreateAssetRequest`** — renamed `assetType` → `type`; added `serialNumber`, `vin`,
  `readingsIngestionEnabled`, `regulationMode`, `attributes`
- **`UpdateAssetRequest`** — removed `id` and `tagIds` (neither belongs in the PATCH body);
  added `type`, `serialNumber`, `vin`, `readingsIngestionEnabled`, `regulationMode`
- **`Driver`** response — removed fabricated fields (`password`, `status`, `vehicleId`, `currentVehicleId`);
  added `isDeactivated`, `currentIdCardCode`, `profileImageUrl`, `attributes`, `eldSettings`,
  `hasDrivingFeaturesHidden`, `hasVehicleUnpinningEnabled`, `peerGroupTag`, `trailerGroupTag`,
  `vehicleGroupTag`, `usDriverRulesetOverride`, `waitingTimeDutyStatusEnabled`
- **`CreateDriverRequest`** / **`UpdateDriverRequest`** — removed non-existent `vehicleId`; added 14+
  missing fields including `staticAssignedVehicleId`, `currentIdCardCode`, `carrierSettings`, `hosSetting`,
  `usDriverRulesetOverride`, `peerGroupTagId`, `trailerGroupTagId`, `vehicleGroupTagId`, and more
- **`CreateDvirRequest`** — completely replaced v1 fields (`inspectorName`, `odometer`, `safeToOperate`,
  `trailerIds`) with correct v2 fields: `authorId` (required), `safetyStatus` (required), `type` (required),
  `vehicleId`, `trailerId`, `licensePlate`, `location`, `mechanicNotes`, `odometerMeters`, `resolvedDefectIds`
- **`UpdateDvirRequest`** — completely replaced v1 fields (`authorizedSignatoryId`, `safeToOperate`) with
  correct v2 fields: `authorId` (required), `isResolved` (required), `mechanicNotes`, `signedAtTime`
- **`SafetyEvent`** — replaced `behaviorLabel` (singular) with `behaviorLabels` (array); removed v1-only
  fields (`maxGForce`, `location`, `coachingState`, video download URLs); removed `SafetyEventLocation`
- **`CreateTagRequest`** / **`UpdateTagRequest`** — added `assets`, `machines`, `sensors`, `externalIds`
- **`CreateUserRequest`** — added `expireAt`
- **`UpdateUserRequest`** — added `authType`, `expireAt`
- **`UpdateVehicleRequest`** — added `auxInputType3`–`auxInputType13`, `engineHours`,
  `grossVehicleWeight`, `gatewaySerial`, `vehicleType`, `attributes`, `odometerMeters`
- **`WorkOrder`** / **`CreateWorkOrderRequest`** / **`UpdateWorkOrderRequest`** — completely rebuilt
  all three with correct API fields (`assetId`, `serviceTaskInstances`, `items`, `discount`, `tax`, etc.)

### Documentation

- **Full SDK-vs-spec audit (2026-05-21)** — added `docs/api-sync/full-sync-review-2026-05-21.md`,
  a mechanical comparison of every wired SDK endpoint and model against the live OpenAPI spec.
  Key finding: roughly **1 in 3 wired endpoints does not match the spec** (wrong URL path or
  fabricated operation). Domains previously marked "Complete" — **Tags, Gateways, Media,
  Messages, Sensors, Tachograph, Fuel and Energy, Training** — are mis-pathed and return 404.
  Corrected the status table in `docs/api-sync/README.md` and added a per-domain audit banner to
  33 checklist files. **No SDK runtime code has been changed yet** — fixes are pending review.
- **Correction to the `SafetyEvent` note above** — the SDK's `SafetyEvent` targets the v2
  endpoint (`getSafetyEventsV2`), whose schema *does* include `location` and
  `maxAccelerationGForce` (these are not "v1-only" as the note implies). The current model is a
  minimal v2 stub also missing `asset`, `eventState`, `media`, object-typed `behaviorLabels`, and
  timestamps — see the full review, Part 2.

## [0.1.0] - 2025-04-06

### Added

- Initial release of `Samsara.Sdk`
- Typed `ISamsaraClient` facade exposing 33 domain service clients
- `IAsyncEnumerable<T>` automatic cursor-based pagination across all list endpoints
- Source-generated `System.Text.Json` serialization (zero-reflection)
- `IHttpClientFactory` integration via `AddSamsaraClient()` extension method
- Built-in resilience via `Microsoft.Extensions.Http.Resilience` — exponential backoff with jitter
- Bearer token authentication via `SamsaraAuthenticationHandler`
- Dynamic `TokenProvider` delegate for OAuth 2.0 / rotating-token scenarios
- EU region support via `SamsaraClientOptions.EuBaseUrl`
- Typed exception hierarchy: `SamsaraBadRequestException`, `SamsaraAuthenticationException`,
  `SamsaraNotFoundException`, `SamsaraRateLimitException`, `SamsaraServerException`
- Configurable `RetryCount`, `Timeout`, `DefaultPageSize` options
- Immutable `record` request/response models for all API domains
- Domain clients: Tags, Addresses, Vehicles, Drivers, Safety, Routes, Compliance,
  Maintenance, Documents, Alerts, Fuel, Webhooks, Organization, Users, Contacts,
  Equipment, Industrial, Messages, Trailers, Gateways, UserRoles, Tachograph,
  IFTA, Hubs, Trips, Forms, Attributes, DriverVehicleAssignments,
  TrailerAssignments, CarrierProposedAssignments, Training, Sensors, Issues, Media
- `Samsara.Cli` interactive terminal tool (Spectre.Console TUI)
- XML documentation on all public API surface

[Unreleased]: https://github.com/TheEightBot/SamsaraDotnet/compare/v0.1.0...HEAD
[0.1.0]: https://github.com/TheEightBot/SamsaraDotnet/releases/tag/v0.1.0
