# Issues — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: 🟡 Partial (2/3 endpoints implemented)  
> **SDK Client**: `IIssuesClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../IssuesClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Issues/IssueModels.cs`  

---

## Endpoints

### ✅ `GET /issues`
**Operation ID**: `getIssues`  
**Summary**: Get a list of specified issues.  
**Parameters**: `ids`, `include`  
**Request Body**: No  

- [x] Method defined in `IIssuesClient`
- [x] Method implemented in `IssuesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ✅ `PATCH /issues`
**Operation ID**: `patchIssue`  
**Summary**: Update a single issue.  
**Request Body**: Yes  

- [x] Method defined in `IIssuesClient`
- [x] Method implemented in `IssuesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [ ] Unit/integration test coverage

### ❌ `GET /issues/stream`
**Operation ID**: `getIssuesStream`  
**Summary**: Get a stream of filtered issues.  
**Parameters**: `startTime`, `endTime`, `after`, `status`, `assetIds`, `include`, `assignedToRouteStopIds`  
**Request Body**: No  

- [x] Method defined in `IIssuesClient`
- [x] Method implemented in `IssuesClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Issues/IssueModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
