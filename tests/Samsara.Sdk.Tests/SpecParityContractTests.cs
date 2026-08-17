namespace Samsara.Sdk.Tests;

using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Documents;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Models.Fuel;
using Samsara.Sdk.Models.Industrial;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests pinning the wrong-property fixes made during the 2026-08-17
/// spec-parity sweep. Each of these records previously carried a property name
/// the Samsara API never sends (or, for the sensors series, never accepts), so
/// the data silently vanished on the wire. These are behavioural bugs a future
/// refactor could reintroduce without any compiler complaint, hence the pins.
/// </summary>
public sealed class SpecParityContractTests
{
    // ── GET /assets/location-and-speed/stream ───────────────────────────────

    /// <summary>
    /// <c>AssetLocation</c> used to bind <c>heading</c> and a v1-era
    /// <c>reverseGeo</c>; the spec sends <c>headingDegrees</c> plus
    /// <c>address</c>/<c>geofence</c>/<c>accuracyMeters</c>.
    /// </summary>
    [Fact]
    public async Task GetLocationAndSpeedStreamAsync_BindsHeadingDegrees_NotHeading()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    asset = new { id = "asset-1" },
                    happenedAtTime = "2024-01-01T00:00:00Z",
                    location = new
                    {
                        latitude = 37.7749,
                        longitude = -122.4194,
                        headingDegrees = 275,
                        accuracyMeters = 12.5,
                        address = new
                        {
                            streetNumber = "350",
                            street = "Rhode Island St",
                            city = "San Francisco",
                            state = "CA",
                            postalCode = "94103",
                            country = "US",
                        },
                        geofence = new { id = "geo-1" },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AssetsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetLocationAndSpeedStreamAsync());

        items.Should().HaveCount(1);
        var location = items[0].Location;
        location.HeadingDegrees.Should().Be(275);
        location.Latitude.Should().Be(37.7749);
        location.AccuracyMeters.Should().Be(12.5);
        location.Address.Should().NotBeNull();
        location.Address!.City.Should().Be("San Francisco");
        location.Address.StreetNumber.Should().Be("350");
        location.Geofence.Should().NotBeNull();
        location.Geofence!.Id.Should().Be("geo-1");
    }

    /// <summary>
    /// The old <c>heading</c> / <c>reverseGeo</c> names must NOT be revived: a
    /// payload using them binds nothing.
    /// </summary>
    [Fact]
    public async Task GetLocationAndSpeedStreamAsync_IgnoresLegacyHeadingAndReverseGeo()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    asset = new { id = "asset-1" },
                    happenedAtTime = "2024-01-01T00:00:00Z",
                    location = new
                    {
                        latitude = 37.7749,
                        longitude = -122.4194,
                        heading = 275.0,
                        reverseGeo = new { formattedLocation = "350 Rhode Island St" },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new AssetsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.GetLocationAndSpeedStreamAsync());

        items[0].Location.HeadingDegrees.Should().BeNull();
        items[0].Location.Address.Should().BeNull();
    }

    // ── POST /v1/sensors/history ────────────────────────────────────────────

    /// <summary>
    /// The series object used to post <c>sensorId</c> + <c>widgetField</c>,
    /// neither of which appears in the spec, while omitting the required
    /// <c>widgetId</c> — the SDK sent a body the API cannot accept.
    /// </summary>
    [Fact]
    public async Task GetHistoryAsync_PostsWidgetId_NotSensorIdOrWidgetField()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { results = Array.Empty<object>() });
        var client = new SensorsClient(TestFactory.CreateHttpClient(handler));

        await client.GetHistoryAsync(new V1SensorHistoryRequest
        {
            StartMs = 1_600_000_000_000,
            EndMs = 1_600_003_600_000,
            StepMs = 60_000,
            Series = new[]
            {
                new V1SensorHistorySeries { Field = "ambientTemperature", WidgetId = 4_242 },
            },
        });

        using var body = JsonDocument.Parse(handler.LastRequestBody!);
        var series = body.RootElement.GetProperty("series")[0];
        series.GetProperty("widgetId").GetInt64().Should().Be(4_242);
        series.GetProperty("field").GetString().Should().Be("ambientTemperature");
        series.TryGetProperty("sensorId", out _).Should().BeFalse();
        series.TryGetProperty("widgetField", out _).Should().BeFalse();
    }

    // ── GET /form-templates ─────────────────────────────────────────────────

    /// <summary>
    /// A form-template section carries a <c>label</c> and an inclusive index
    /// range into the template's flat <c>fields</c> array — NOT its own
    /// <c>title</c> and nested <c>fields</c> list, which the old record invented.
    /// </summary>
    [Fact]
    public async Task ListTemplatesAsync_BindsSectionLabelAndFieldIndexRange()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "tmpl-1",
                    title = "Pre-Trip Inspection",
                    revisionId = "rev-1",
                    sections = new[]
                    {
                        new
                        {
                            id = "22222222-2222-2222-2222-222222222222",
                            label = "Exterior",
                            fieldIndexFirstInclusive = 0,
                            fieldIndexLastInclusive = 3,
                        },
                    },
                    createdAtTime = "2024-01-01T00:00:00Z",
                    updatedAtTime = "2024-01-02T00:00:00Z",
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new FormsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListTemplatesAsync());

        var section = items[0].Sections.Should().ContainSingle().Subject;
        section.Id.Should().Be("22222222-2222-2222-2222-222222222222");
        section.Label.Should().Be("Exterior");
        section.FieldIndexFirstInclusive.Should().Be(0);
        section.FieldIndexLastInclusive.Should().Be(3);
    }

    // ── GET /fleet/document-types ───────────────────────────────────────────

    /// <summary>
    /// A document-type field carries <c>fieldType</c> and <c>requiredField</c>;
    /// the old record modelled <c>valueType</c> and
    /// <c>numberValueTypeMetadata</c>, neither of which the API sends.
    /// </summary>
    [Fact]
    public async Task ListTypesAsync_BindsFieldTypeAndRequiredField()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    id = "dt-1",
                    name = "Bill of Lading",
                    fieldTypes = new object[]
                    {
                        new
                        {
                            label = "Weight",
                            fieldType = "number",
                            requiredField = true,
                            numberFieldTypeMetaData = new { numberOfDecimalPlaces = 2 },
                        },
                        new
                        {
                            label = "Signature",
                            fieldType = "signature",
                            requiredField = false,
                            signatureFieldTypeMetaData = new { legalText = "I agree" },
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DocumentsClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListTypesAsync());

        var fieldTypes = items[0].FieldTypes!;
        fieldTypes.Should().HaveCount(2);
        fieldTypes[0].FieldType.Should().Be("number");
        fieldTypes[0].RequiredField.Should().BeTrue();
        fieldTypes[0].NumberFieldTypeMetaData!.NumberOfDecimalPlaces.Should().Be(2);
        fieldTypes[1].FieldType.Should().Be("signature");
        fieldTypes[1].RequiredField.Should().BeFalse();
        fieldTypes[1].SignatureFieldTypeMetaData!.LegalText.Should().Be("I agree");
    }

    // ── GET /fleet/reports/vehicles/fuel-energy ─────────────────────────────

    /// <summary>
    /// The cost object is keyed <c>currencyCode</c> (not <c>currency</c>), and
    /// the report's vehicle carries an <c>energyType</c> discriminator that the
    /// shared <c>EntityReference</c> could not express.
    /// </summary>
    [Fact]
    public async Task ListVehicleFuelEnergyReportsAsync_BindsCurrencyCodeAndEnergyType()
    {
        var resp = new
        {
            data = new
            {
                vehicleReports = new[]
                {
                    new
                    {
                        vehicle = new { id = "veh-1", name = "Truck 1", energyType = "electric" },
                        distanceTraveledMeters = 1000.0,
                        efficiencyMpge = 42.0,
                        estFuelEnergyCost = new { amount = 12.34, currencyCode = "USD" },
                    },
                },
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new FuelClient(TestFactory.CreateHttpClient(handler));

        var result = await client.ListVehicleFuelEnergyReportsAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));

        var report = result.VehicleReports.Should().ContainSingle().Subject;
        report.Vehicle.Id.Should().Be("veh-1");
        report.Vehicle.EnergyType.Should().Be("electric");
        report.EstFuelEnergyCost.Amount.Should().Be(12.34);
        report.EstFuelEnergyCost.CurrencyCode.Should().Be("USD");
    }

    // ── GET /fleet/hos/violations ───────────────────────────────────────────

    /// <summary>
    /// <c>violationStartTime</c> is spec-required on the response and was
    /// dropped entirely by the old record.
    /// </summary>
    [Fact]
    public async Task ListHosViolationsAsync_BindsViolationStartTime()
    {
        var resp = new
        {
            data = new[]
            {
                new
                {
                    violations = new[]
                    {
                        new
                        {
                            type = "shiftDrivingHours",
                            description = "Shift driving hours exceeded",
                            durationMs = 3_600_000L,
                            violationStartTime = "2024-01-01T08:00:00Z",
                            day = new { startTime = "2024-01-01T00:00:00Z", endTime = "2024-01-02T00:00:00Z" },
                            driver = new { id = "drv-1", name = "Alex" },
                        },
                    },
                },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new ComplianceClient(TestFactory.CreateHttpClient(handler));

        var items = await CollectAsync(client.ListHosViolationsAsync(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero)));

        var violation = items[0].Violations.Should().ContainSingle().Subject;
        violation.ViolationStartTime.Should().Be("2024-01-01T08:00:00Z");
        violation.Type.Should().Be("shiftDrivingHours");
    }

    // ── POST /fleet/drivers ─────────────────────────────────────────────────

    /// <summary>
    /// The ruleset override is applied wholesale by the API, so the request DTO
    /// serializes all four spec-required members.
    /// </summary>
    [Fact]
    public async Task CreateDriverAsync_PostsCompleteUsDriverRulesetOverride()
    {
        var handler = MockHttpMessageHandler.WithJsonResponse(new { data = new { id = "drv-1", name = "Alex" } });
        var client = new DriversClient(TestFactory.CreateHttpClient(handler));

        await client.CreateAsync(new Models.Drivers.CreateDriverRequest
        {
            Name = "Alex",
            Username = "alex",
            Password = "hunter2hunter2",
            UsDriverRulesetOverride = new Models.Drivers.UsDriverRulesetOverrideInput
            {
                Cycle = "USA Property (8/70)",
                Restart = "34-hour Restart",
                Restbreak = "Property (off-duty/sleeper)",
                UsStateToOverride = "CA",
            },
        });

        using var body = JsonDocument.Parse(handler.LastRequestBody!);
        var over = body.RootElement.GetProperty("usDriverRulesetOverride");
        over.GetProperty("cycle").GetString().Should().Be("USA Property (8/70)");
        over.GetProperty("restart").GetString().Should().Be("34-hour Restart");
        over.GetProperty("restbreak").GetString().Should().Be("Property (off-duty/sleeper)");
        over.GetProperty("usStateToOverride").GetString().Should().Be("CA");
    }

    private static async Task<IReadOnlyList<T>> CollectAsync<T>(IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
        }

        return list;
    }
}
