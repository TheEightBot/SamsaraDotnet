namespace Samsara.Sdk.Tests;

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Serialization;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Locks in the deserialization architecture: the primary path
/// (<see cref="SamsaraSerializerOptions.Default"/>) is source-generated for performance and
/// LENIENT on <c>required</c> — the live Samsara API omits spec-<c>required</c> fields on nearly
/// every response, so a single fast pass tolerates them rather than throwing/retrying. A separate
/// strict path (<see cref="SamsaraSerializerOptions.Strict"/>) enforces the spec for callers that
/// want to validate conformance.
/// </summary>
public sealed class DeserializationToleranceTests
{
    /// <summary>Every public, constructable model type in the SDK.</summary>
    public static IEnumerable<object[]> AllModelTypes()
    {
        var assembly = typeof(Vehicle).Assembly;
        foreach (var type in assembly.GetTypes())
        {
            if (type is { IsPublic: true, IsClass: true, IsAbstract: false }
                && !type.ContainsGenericParameters
                && type.Namespace?.StartsWith("Samsara.Sdk.Models", StringComparison.Ordinal) == true
                && type.GetConstructor(Type.EmptyTypes) is not null)
            {
                yield return new object[] { type };
            }
        }
    }

    [Theory]
    [MemberData(nameof(AllModelTypes))]
    public void Default_DeserializesEveryModelFromEmptyObject(Type modelType)
    {
        // `{}` omits EVERY field — the worst case the API can hand us. The default (primary) path
        // must tolerate it for every model in the SDK, with no exception and no retry.
        var act = () => JsonSerializer.Deserialize("{}", modelType, SamsaraSerializerOptions.Default);

        act.Should().NotThrow(
            $"the default deserialization path must tolerate a response that omits any field on {modelType.Name}");
    }

    [Fact]
    public void ModelDiscovery_FindsTheFullModelSurface()
    {
        // Stops the Theory from passing vacuously if the reflection query ever breaks.
        AllModelTypes().Should().HaveCountGreaterThan(300);
    }

    [Fact]
    public void Default_Tolerates_MissingSpecRequiredField()
    {
        // The exact production scenario: the API returns a Vehicle without the spec-required
        // createdAtTime. The default path deserializes it (no throw, no log, single pass).
        const string json = """{ "id": "v-1", "name": "Truck 1" }""";

        var vehicle = JsonSerializer.Deserialize<Vehicle>(json, SamsaraSerializerOptions.Default);

        vehicle.Should().NotBeNull();
        vehicle!.Id.Should().Be("v-1");
        vehicle.CreatedAtTime.Should().Be(default(DateTimeOffset));
    }

    [Fact]
    public void Strict_HonorsSpecRequired_ForConformanceValidation()
    {
        // The opt-in strict path stays honest to the spec: a payload missing a spec-`required`
        // field (Vehicle.createdAtTime) throws, so callers can validate conformance when they want.
        var act = () => JsonSerializer.Deserialize<Vehicle>("{}", SamsaraSerializerOptions.Strict);

        act.Should().Throw<JsonException>().WithMessage("*createdAtTime*");
    }

    [Fact]
    public void EveryModelType_IsRegisteredInSourceGenContext()
    {
        // For full source-gen performance (no reflection fallback), every model the SDK
        // deserializes must be registered in SamsaraJsonContext.
        var unregistered = AllModelTypes()
            .Select(row => (Type)row[0])
            .Where(t => SamsaraJsonContext.Default.GetTypeInfo(t) is null)
            .Select(t => t.FullName)
            .OrderBy(n => n)
            .ToList();

        unregistered.Should().BeEmpty(
            "these model types fall back to reflection; add [JsonSerializable(typeof(X))] to SamsaraJsonContext:\n"
            + string.Join("\n", unregistered));
    }

    [Fact]
    public void SourceGeneration_IsWired_ForModelTypes()
    {
        // The model types resolve through the source-generated context (not reflection),
        // which is where the performance win comes from.
        var info = SamsaraJsonContext.Default.GetTypeInfo(typeof(Vehicle));

        info.Should().NotBeNull("Vehicle must be registered in the source-generated context");
        info!.Kind.Should().Be(JsonTypeInfoKind.Object);
    }

    [Fact]
    public async Task HttpClient_Tolerates_WhenApiOmitsSpecRequiredField()
    {
        // End-to-end: the exact production scenario through the real client. The API returns a
        // Vehicle without the spec-required createdAtTime; the client returns it rather than throw.
        var resp = new { data = new { id = "v-1", name = "Truck 1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var act = async () => await client.GetAsync("v-1");

        var vehicle = await act.Should().NotThrowAsync();
        vehicle.Subject.Id.Should().Be("v-1");
        vehicle.Subject.Name.Should().Be("Truck 1");
    }

    [Fact]
    public async Task HttpClient_Deserializes_ConformingResponse()
    {
        var resp = new { data = new { id = "v-2", name = "Truck 2", createdAtTime = "2024-01-01T00:00:00Z" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.GetAsync("v-2");

        vehicle.Id.Should().Be("v-2");
        vehicle.CreatedAtTime.Should().Be(DateTimeOffset.Parse("2024-01-01T00:00:00Z"));
    }
}
