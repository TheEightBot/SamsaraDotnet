namespace Samsara.Sdk.Tests;

using System.Net;
using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Assignments;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Models.Maintenance;
using Samsara.Sdk.Models.Routes;
using Samsara.Sdk.Serialization;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for spec content <c>2445b573294b</c> outside the maintenance clients: the two
/// new write operations (work-order attachments, shared-asset cancellation) and the members
/// Samsara added to existing records.
/// </summary>
// SAMSARA001 is the [Experimental] diagnostic on beta-tagged operations; these tests opt in.
#pragma warning disable SAMSARA001
public sealed class SpecDriftModelContractTests
{
    [Fact]
    public async Task PostWorkOrderAttachmentAsync_PostsTheFileAndBindsTheAttachmentId()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { id = "att-1" } });
        var client = new WorkOrdersClient(TestFactory.CreateHttpClient(handler));

        var attachment = await client.PostWorkOrderAttachmentAsync(new PostWorkOrderAttachmentRequest
        {
            WorkOrderId = "wo-1",
            File = new WorkOrderAttachmentFile { Base64Content = "JVBERi0=", ContentType = "application/pdf" },
        });

        attachment.Id.Should().Be("att-1");
        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/maintenance/work-orders/attachments");
        handler.LastRequestBody.Should().Be(
            "{\"workOrderId\":\"wo-1\",\"file\":{\"base64Content\":\"JVBERi0=\",\"contentType\":\"application/pdf\"}}");
    }

    /// <summary>
    /// The cancel response is a bare <c>{ requestId, responses }</c> object rather than a
    /// <c>{ data }</c> envelope, and reports per-asset outcomes. Unwrapping <c>data</c> would
    /// throw or silently drop the failures.
    /// </summary>
    [Fact]
    public async Task CancelSharedAssetsBatchAsync_BindsPerAssetOutcomes()
    {
        var resp = new
        {
            requestId = "5c1a3c1e-2a3b-4c5d-8e9f-001122334455",
            responses = new object[]
            {
                new
                {
                    status = 200,
                    data = new { id = "sa-1", providerAssetId = "a-1", serial = "SER1", startTime = "2026-08-01T00:00:00Z", createdAtTime = "2026-08-01T00:00:00Z" },
                },
                new { status = 404, message = "shared asset not found" },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AssetSharingClient(TestFactory.CreateHttpClient(handler));

        var result = await client.CancelSharedAssetsBatchAsync(new CancelSharedAssetsBatchRequest
        {
            Data = [new SharedAssetReference { Id = "sa-1" }, new SharedAssetReference { Id = "sa-x" }],
        });

        handler.LastRequest.Method.Should().Be(HttpMethod.Post);
        handler.LastRequest.RequestUri!.PathAndQuery.Should().Be("/fleet/asset-sharing/agreements/assets/cancel/batch");
        handler.LastRequestBody.Should().Be("{\"data\":[{\"id\":\"sa-1\"},{\"id\":\"sa-x\"}]}");
        result.RequestId.Should().Be("5c1a3c1e-2a3b-4c5d-8e9f-001122334455");
        result.Responses.Should().HaveCount(2);
        result.Responses![0].Status.Should().Be(200);
        result.Responses[0].Data!.Serial.Should().Be("SER1");
        result.Responses[1].Status.Should().Be(404);
        result.Responses[1].Message.Should().Be("shared asset not found");
        result.Responses[1].Data.Should().BeNull();
    }

    [Fact]
    public void Part_BindsThePreferredVendor()
    {
        var part = Deserialize<Part>("""{"id":"p-1","preferredVendor":{"id":"ven-1"},"preferredVendorPartNumber":"AC-77"}""");

        part.PreferredVendor!.Id.Should().Be("ven-1");
        part.PreferredVendorPartNumber.Should().Be("AC-77");
    }

    [Fact]
    public void PartRequests_SerializeThePreferredVendorAndCurrentQuantity()
    {
        Serialize(new CreatePartRequest { PartNumber = "P-1", PreferredVendorId = "ven-1", PreferredVendorPartNumber = "AC-77" })
            .Should().Contain("\"preferredVendorId\":\"ven-1\"").And.Contain("\"preferredVendorPartNumber\":\"AC-77\"");
        Serialize(new UpdatePartRequest { PreferredVendorId = "ven-2" })
            .Should().Be("{\"preferredVendorId\":\"ven-2\"}");
        Serialize(new UpdatePartInventoryLocationRequest { CurrentQuantity = 12.5 })
            .Should().Be("{\"currentQuantity\":12.5}");
        Deserialize<PartInventoryLocation>("""{"isNonStock":true}""").IsNonStock.Should().BeTrue();
    }

    [Fact]
    public void PurchaseOrder_BindsTaxOnTheOrderAndItsLines()
    {
        var order = Deserialize<PurchaseOrder>("""
            {
              "id": "po-1",
              "tax": { "basisPoints": 825 },
              "taxTotal": { "amount": "8.25", "currency": "usd" },
              "parts": [ { "lineItemId": "l-1", "tax": { "money": { "amount": "1.00", "currency": "usd" } } } ]
            }
            """);

        order.Tax!.BasisPoints.Should().Be(825);
        order.Tax.Money.Should().BeNull();
        order.TaxTotal!.Amount.Should().Be("8.25");
        order.Parts![0].Tax!.Money!.Amount.Should().Be("1.00");
    }

    [Fact]
    public void PurchaseOrderRequests_SerializeTax()
    {
        var body = Serialize(new UpdatePurchaseOrderRequest
        {
            Tax = new PurchaseOrderTaxAdjustmentInput { BasisPoints = 825 },
            Parts =
            [
                new PurchaseOrderPartInput
                {
                    PartSamsaraId = "p-1",
                    QuantityOrdered = 1,
                    QuantityReceived = 0,
                    UnitOfMeasureType = "each",
                    Tax = new PurchaseOrderTaxAdjustmentInput { Money = new MaintenanceMoneyInput { Amount = "1.00", Currency = "usd" } },
                },
            ],
        });

        using var doc = JsonDocument.Parse(body);
        doc.RootElement.GetProperty("tax").GetProperty("basisPoints").GetInt64().Should().Be(825);
        doc.RootElement.GetProperty("parts")[0].GetProperty("tax").GetProperty("money").GetProperty("amount").GetString().Should().Be("1.00");
    }

    [Fact]
    public void WorkOrder_BindsAssigneesAndStartedAtTime()
    {
        var order = Deserialize<WorkOrder>("""
            {
              "id": "wo-1", "assetId": "a-1", "status": "In Progress",
              "createdAtTime": "2026-08-17T08:00:00Z", "updatedAtTime": "2026-08-17T09:00:00Z",
              "startedAtTime": "2026-08-17T08:30:00Z",
              "assignees": [ { "userId": "usr-1" }, { "userId": "usr-2" } ],
              "serviceTaskInstances": [ { "id": "sti-1", "assignees": [ { "userId": "usr-3" } ] } ]
            }
            """);

        order.StartedAtTime.Should().Be(DateTimeOffset.Parse("2026-08-17T08:30:00Z"));
        order.Assignees!.Select(a => a.UserId).Should().Equal("usr-1", "usr-2");
        order.ServiceTaskInstances![0].Assignees![0].UserId.Should().Be("usr-3");
    }

    [Fact]
    public void WorkOrderRequests_SerializeTheHistoricalTimesAndInitialStatus()
    {
        using var create = JsonDocument.Parse(Serialize(new CreateWorkOrderRequest
        {
            AssetId = "a-1",
            Status = "Completed",
            StartedAtTime = DateTimeOffset.Parse("2026-08-01T08:00:00Z"),
            CompletedAtTime = DateTimeOffset.Parse("2026-08-01T12:00:00Z"),
        }));
        create.RootElement.GetProperty("status").GetString().Should().Be("Completed");
        create.RootElement.GetProperty("startedAtTime").GetDateTimeOffset().Should().Be(DateTimeOffset.Parse("2026-08-01T08:00:00Z"));
        create.RootElement.GetProperty("completedAtTime").GetDateTimeOffset().Should().Be(DateTimeOffset.Parse("2026-08-01T12:00:00Z"));
        create.RootElement.TryGetProperty("archivedAtTime", out _).Should().BeFalse();

        using var update = JsonDocument.Parse(Serialize(new UpdateWorkOrderRequest
        {
            Id = "wo-1",
            ArchivedAtTime = DateTimeOffset.Parse("2026-08-02T00:00:00Z"),
        }));
        update.RootElement.GetProperty("archivedAtTime").GetDateTimeOffset().Should().Be(DateTimeOffset.Parse("2026-08-02T00:00:00Z"));
    }

    [Fact]
    public void Trip_BindsFinalDistanceAndPurpose()
    {
        var trip = Deserialize<Trip>("""{"completionStatus":"completed","finalDistanceMeters":48210,"tripPurpose":"business"}""");

        trip.FinalDistanceMeters.Should().Be(48210);
        trip.TripPurpose.Should().Be("business");
    }

    /// <summary>
    /// The spec dropped <c>required</c> from driverId, vehicleId and startTime: an assignment can
    /// now be identified by <c>metadata.sourceName</c> alone, so a body without the composite key
    /// must be constructible and must not send it.
    /// </summary>
    [Fact]
    public void UpdateDriverVehicleAssignmentRequest_CanIdentifyByMetadataAlone()
    {
        var body = Serialize(new UpdateDriverVehicleAssignmentRequest
        {
            Metadata = new Dictionary<string, string> { ["sourceName"] = "erp-sync-42" },
            EndTime = DateTimeOffset.Parse("2026-08-17T17:00:00Z"),
        });

        using var doc = JsonDocument.Parse(body);
        doc.RootElement.GetProperty("metadata").GetProperty("sourceName").GetString().Should().Be("erp-sync-42");
        doc.RootElement.TryGetProperty("driverId", out _).Should().BeFalse();
        doc.RootElement.TryGetProperty("vehicleId", out _).Should().BeFalse();
        doc.RootElement.TryGetProperty("startTime", out _).Should().BeFalse();
    }

    private static T Deserialize<T>(string json)
        => JsonSerializer.Deserialize<T>(json, SamsaraSerializerOptions.Default)!;

    private static string Serialize<T>(T value)
        => JsonSerializer.Serialize(value, SamsaraSerializerOptions.Default);
}
#pragma warning restore SAMSARA001
