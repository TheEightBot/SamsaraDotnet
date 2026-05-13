# Driver QR Codes — API Sync Checklist

> **API Version**: `2025-10-23`  
> **Status**: ⚠️ Unverified (0/3 endpoints implemented)  
> **SDK Client**: `IDriversClient`  
> **Implementation**: `src/Samsara.Sdk/Clients/.../DriversClient.cs`  
> **Models**: `src/Samsara.Sdk/Models/Drivers/DriverModels.cs`  

---

## Endpoints

### ⚠️ `DELETE /drivers/qr-codes`
**Operation ID**: `deleteDriverQrCode`  
**Summary**: Revoke driver's QR code  
**Request Body**: Yes  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `GET /drivers/qr-codes`
**Operation ID**: `getDriversQrCodes`  
**Summary**: Get driver QR codes  
**Parameters**: `driverIds`  
**Request Body**: No  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

### ⚠️ `POST /drivers/qr-codes`
**Operation ID**: `createDriverQrCode`  
**Summary**: Create new QR code for driver  
**Request Body**: Yes  

- [x] Method defined in `IDriversClient`
- [x] Method implemented in `DriversClient.cs`
- [x] Request model(s) defined (if applicable)
- [x] Response model(s) defined
- [x] JSON serialization context updated (`SamsaraJsonContext.cs`)
- [x] Unit/integration test coverage

---

## Models

See `src/Samsara.Sdk/Models/Drivers/DriverModels.cs` for model definitions used by this domain.

- [ ] All request models defined as `record` types
- [ ] All response models defined as `record` types
- [ ] All models have XML documentation
- [ ] All enum values covered
- [ ] Nullable reference types used correctly

---

## Notes

_Add any implementation notes, breaking changes, or special considerations here._
