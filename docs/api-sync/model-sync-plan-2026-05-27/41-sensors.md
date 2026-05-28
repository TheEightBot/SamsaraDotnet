# Sensors — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/41-sensors.md`](../41-sensors.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `961698b` on 2026-05-27**

## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `V1SensorHistoryRequest` | request | 0 | 1 | 0 | 0 |

**Counts**: CRITICAL=0, HIGH=1, MEDIUM=0, LOW=0  
**Total deduped findings**: 1

## HIGH (1)

### `V1SensorHistoryRequest` (request)

- **[missing_required]** V1SensorHistoryRequest is missing REQUIRED property `stepMs` (spec type=integer).
  - Endpoints: `POST /v1/sensors/history`
  - Recommended fix: Add `[JsonPropertyName("stepMs")] public required int StepMs { get; init; }` to `V1SensorHistoryRequest`.

