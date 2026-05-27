# IFTA — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/22-ifta.md`](../22-ifta.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

## Implementation notes

All 8 MEDIUM findings were applied (no CRITICAL/HIGH/LOW). The seven
`response_required_drift` findings tighten spec-REQUIRED scalar / array
fields from nullable to non-nullable `required` on three response records:

- **`IftaDetailJob`**: `Args` (was `IftaDetailJobArgs?`), `JobStatus` (was
  `string?`), and `RequestedAtTime` (was `DateTimeOffset?`) are now
  non-nullable `required`. Spec marks all three REQUIRED on the
  `IftaDetailJobResponseBody` inner schema for both `GET /ifta-detail/csv/{id}`
  and `POST /ifta-detail/csv`. `JobId` was already `required`.
- **`IftaJurisdictionReportsResponse`**: `Year` (was `int?`) and
  `JurisdictionReports` (was `IReadOnlyList<IftaJurisdictionSummary>?`) are
  now non-nullable `required`. Spec's `IftaJurisdictionReportDataObjectResponseBody`
  marks both REQUIRED on the `data` wrapper for `GET /fleet/reports/ifta/jurisdiction`.
- **`IftaVehicleReportsResponse`**: `Year` (was `int?`) and `VehicleReports`
  (was `IReadOnlyList<IftaVehicleReport>?`) are now non-nullable `required`.
  Spec's `IftaVehicleReportDataObjectResponseBody` marks both REQUIRED for
  `GET /fleet/reports/ifta/vehicle`.

The eighth MEDIUM is the missing optional `after` query parameter on
`ListVehicleReportsAsync` (`GET /fleet/reports/ifta/vehicle`). Added as
`string? after = null` (declared last among optionals, before
`CancellationToken cancellationToken = default`) on both `IIftaClient`
and `IftaClient`, with `("after", after)` appended to the existing
`QueryBuilder.WithParams(...)` call.

Files touched: `src/Samsara.Sdk/Models/Compliance/IftaModels.cs`,
`src/Samsara.Sdk/Clients/Compliance/IftaClient.cs`,
`src/Samsara.Sdk/Clients/Compliance/IIftaClient.cs`. No `SamsaraJsonContext`
or test fixture updates required — the affected type registrations and
test substitutes are unchanged because the responses are only consumed
via deserialization (no SDK code constructs these records).

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `IftaDetailJob` | response | 0 | 0 | 3 | 0 |
| `IftaJurisdictionReportsResponse` | response | 0 | 0 | 2 | 0 |
| `IftaVehicleReportsResponse` | response | 0 | 0 | 2 | 0 |
| `(no SDK type)` | query | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=8, LOW=0  
**Total deduped findings**: 8

## MEDIUM (8)

### `(no SDK type)` (query)

- **[missing_optional_query]** ListVehicleReportsAsync (GET /fleet/reports/ifta/vehicle) is missing query parameter `after` (spec optional, type=string).
  - Endpoints: `GET /fleet/reports/ifta/vehicle`
  - Recommended fix: Add an optional parameter `string? after = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

### `IftaDetailJob` (response)

- **[response_required_drift]** IftaDetailJob.args (response): spec marks REQUIRED but SDK exposes as nullable (`IftaDetailJobArgs?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /ifta-detail/csv/{id}`, `POST /ifta-detail/csv`
  - Recommended fix: Tighten `IftaDetailJob.Args` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** IftaDetailJob.jobStatus (response): spec marks REQUIRED but SDK exposes as nullable (`string?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /ifta-detail/csv/{id}`, `POST /ifta-detail/csv`
  - Recommended fix: Tighten `IftaDetailJob.JobStatus` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** IftaDetailJob.requestedAtTime (response): spec marks REQUIRED but SDK exposes as nullable (`DateTimeOffset?`). Consumers cannot rely on a non-null value despite the spec guarantee. (affects 2 endpoints)
  - Endpoints: `GET /ifta-detail/csv/{id}`, `POST /ifta-detail/csv`
  - Recommended fix: Tighten `IftaDetailJob.RequestedAtTime` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `IftaJurisdictionReportsResponse` (response)

- **[response_required_drift]** IftaJurisdictionReportsResponse.jurisdictionReports (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<IftaJurisdictionSummary>?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/reports/ifta/jurisdiction`
  - Recommended fix: Tighten `IftaJurisdictionReportsResponse.JurisdictionReports` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** IftaJurisdictionReportsResponse.year (response): spec marks REQUIRED but SDK exposes as nullable (`int?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/reports/ifta/jurisdiction`
  - Recommended fix: Tighten `IftaJurisdictionReportsResponse.Year` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

### `IftaVehicleReportsResponse` (response)

- **[response_required_drift]** IftaVehicleReportsResponse.vehicleReports (response): spec marks REQUIRED but SDK exposes as nullable (`IReadOnlyList<IftaVehicleReport>?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/reports/ifta/vehicle`
  - Recommended fix: Tighten `IftaVehicleReportsResponse.VehicleReports` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.
- **[response_required_drift]** IftaVehicleReportsResponse.year (response): spec marks REQUIRED but SDK exposes as nullable (`int?`). Consumers cannot rely on a non-null value despite the spec guarantee.
  - Endpoints: `GET /fleet/reports/ifta/vehicle`
  - Recommended fix: Tighten `IftaVehicleReportsResponse.Year` to non-nullable (drop the `?`). Spec marks this field REQUIRED in the response, so consumers should be able to use it directly without null-checks.

