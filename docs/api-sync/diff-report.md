# Samsara API Sync Diff Report

> **Generated**: 2026-05-22 00:15 UTC  
> **Old version**: `2025-10-23`  
> **New version**: `2025-10-23`  
> **Endpoint changes**: 6  
> **Schema changes**: 67  

---

## 🆕 New Endpoints (4)

### Beta APIs
- `DELETE /places` — [beta] Delete a place *(operationId: `deletePlace`)*
- `GET /places` — [beta] List or batch-get places *(operationId: `getPlaces`)*
- `PATCH /places` — [beta] Update a place *(operationId: `patchPlace`)*
- `POST /places` — [beta] Create a place *(operationId: `postPlace`)*

## 🔄 Changed Endpoints (2)

### `GET /assets`
- new param: `includeAttributes` (in=query, required=False)

### `GET /issues/stream`
- new param: `assetExternalIds` (in=query, required=False)

## 📦 Schema Changes (67)

**Added schemas** (67):
- `GeofenceVertexInputRequestBody`
- `HubLocationCapacityServiceTimeResponseResponseBody`
- `HubLocationOrderServiceTimeResponseResponseBody`
- `HubLocationRequiredSkillResponseResponseBody`
- `HubLocationResponseResponseBody`
- `HubLocationServiceTimeResponseResponseBody`
- `HubLocationServiceWindowResponseResponseBody`
- `OutOfSequenceStopArrivalDataResponseBody`
- `PatchPlaceHubLocationUpsertBodyRequestBody`
- `PatchPlaceHubLocationsBodyRequestBody`
- `PlaceGeofenceVertexResponseResponseBody`
- `PlaceNavigationLocationResponseResponseBody`
- `PlaceNavigationResponseResponseBody`
- `PlaceResponseObjectResponseBody`
- `PlaceStreetViewResponseResponseBody`
- `PlaceTagResponseResponseBody`
- `PlacesDeletePlaceBadGatewayErrorResponseBody`
- `PlacesDeletePlaceBadRequestErrorResponseBody`
- `PlacesDeletePlaceGatewayTimeoutErrorResponseBody`
- `PlacesDeletePlaceInternalServerErrorResponseBody`
- `PlacesDeletePlaceMethodNotAllowedErrorResponseBody`
- `PlacesDeletePlaceNotFoundErrorResponseBody`
- `PlacesDeletePlaceNotImplementedErrorResponseBody`
- `PlacesDeletePlaceServiceUnavailableErrorResponseBody`
- `PlacesDeletePlaceTooManyRequestsErrorResponseBody`
- `PlacesDeletePlaceUnauthorizedErrorResponseBody`
- `PlacesGetPlacesBadGatewayErrorResponseBody`
- `PlacesGetPlacesBadRequestErrorResponseBody`
- `PlacesGetPlacesGatewayTimeoutErrorResponseBody`
- `PlacesGetPlacesInternalServerErrorResponseBody`
- `PlacesGetPlacesMethodNotAllowedErrorResponseBody`
- `PlacesGetPlacesNotFoundErrorResponseBody`
- `PlacesGetPlacesNotImplementedErrorResponseBody`
- `PlacesGetPlacesResponseBody`
- `PlacesGetPlacesServiceUnavailableErrorResponseBody`
- `PlacesGetPlacesTooManyRequestsErrorResponseBody`
- `PlacesGetPlacesUnauthorizedErrorResponseBody`
- `PlacesPatchPlaceBadGatewayErrorResponseBody`
- `PlacesPatchPlaceBadRequestErrorResponseBody`
- `PlacesPatchPlaceGatewayTimeoutErrorResponseBody`
- `PlacesPatchPlaceInternalServerErrorResponseBody`
- `PlacesPatchPlaceMethodNotAllowedErrorResponseBody`
- `PlacesPatchPlaceNotFoundErrorResponseBody`
- `PlacesPatchPlaceNotImplementedErrorResponseBody`
- `PlacesPatchPlaceRequestBody`
- `PlacesPatchPlaceResponseBody`
- `PlacesPatchPlaceServiceUnavailableErrorResponseBody`
- `PlacesPatchPlaceTooManyRequestsErrorResponseBody`
- `PlacesPatchPlaceUnauthorizedErrorResponseBody`
- `PlacesPostPlaceBadGatewayErrorResponseBody`
- *(and 17 more...)*

---

## Next Steps

1. Review each new endpoint and decide if it should be implemented in the SDK
2. Update the relevant checklist file(s) in `docs/api-sync/`
3. Implement the endpoint(s), models, and serialization context
4. Update `CHANGELOG.md` with the changes
5. Update the baseline: `python3 tools/check-api-sync.py --update-baseline`
