# Route Events — Model Sync Plan (2026-05-27)

> Companion to [`docs/api-sync/37-route-events.md`](../37-route-events.md).  
> Spec: `samsara-api.json` v`2025-10-23` (local).

> **✅ Implemented in commit `<pending>` on 2026-05-27**

## Implementation notes

- Added optional `bool? includeExternalIds = null` to `IRouteEventsClient.GetStreamAsync` /
  `RouteEventsClient.GetStreamAsync`. The parameter is appended via `QueryBuilder.WithParams`
  with lowercase boolean conversion.


## Quick reference

| SDK Type | Context | CRIT | HIGH | MED | LOW |
|---|---|---:|---:|---:|---:|
| `(no SDK type)` | query | 0 | 0 | 1 | 0 |

**Counts**: CRITICAL=0, HIGH=0, MEDIUM=1, LOW=0  
**Total deduped findings**: 1

## MEDIUM (1)

### `(no SDK type)` (query)

- **[missing_optional_query]** GetStreamAsync (GET /route-events/stream) is missing query parameter `includeExternalIds` (spec optional, type=boolean).
  - Endpoints: `GET /route-events/stream`
  - Recommended fix: Add an optional parameter `bool? includeExternalIds = null` to the SDK method and append it conditionally via `QueryBuilder.WithParams(...)`.

