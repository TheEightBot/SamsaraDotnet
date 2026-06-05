namespace Samsara.Sdk.Tests;

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Serialization;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Locks in the deserialization architecture: spec-honest <c>required</c> on the strict path
/// (<see cref="SamsaraSerializerOptions.Default"/>), source generation for performance, and a
/// resilient failover (<see cref="SamsaraSerializerOptions.Resilient"/>) so a response that
/// doesn't match the spec — e.g. the live API omitting <c>Vehicle.createdAtTime</c> — still
/// deserializes instead of crashing.
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
    public void Resilient_DeserializesEveryModelFromEmptyObject(Type modelType)
    {
        // `{}` omits EVERY field — the worst case the API can hand us. The resilient failover
        // path must tolerate it for every model in the SDK.
        var act = () => JsonSerializer.Deserialize("{}", modelType, SamsaraSerializerOptions.Resilient);

        act.Should().NotThrow(
            $"the resilient failover must tolerate a response that omits any field on {modelType.Name}");
    }

    [Fact]
    public void ModelDiscovery_FindsTheFullModelSurface()
    {
        // Stops the Theory from passing vacuously if the reflection query ever breaks.
        AllModelTypes().Should().HaveCountGreaterThan(300);
    }

    [Fact]
    public void Default_IsStrict_AndHonorsSpecRequired()
    {
        // The strict/source-gen path stays honest to the spec: a payload missing a
        // spec-`required` field (Vehicle.createdAtTime) throws here.
        var act = () => JsonSerializer.Deserialize<Vehicle>("{}", SamsaraSerializerOptions.Default);

        act.Should().Throw<JsonException>().WithMessage("*createdAtTime*");
    }

    [Fact]
    public void Resilient_IsTheFailover_DeserializesWhatStrictRejects()
    {
        const string json = """{ "id": "v-1", "name": "Truck 1" }""";

        var vehicle = JsonSerializer.Deserialize<Vehicle>(json, SamsaraSerializerOptions.Resilient);

        vehicle.Should().NotBeNull();
        vehicle!.Id.Should().Be("v-1");
        vehicle.CreatedAtTime.Should().Be(default(DateTimeOffset));
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
    public async Task HttpClient_FailsOver_WhenApiOmitsSpecRequiredField()
    {
        // End-to-end: the exact production scenario through the real client + deserialization
        // path. The API returns a Vehicle without the spec-required createdAtTime; the client
        // must fail over and return the vehicle rather than throw.
        var resp = new { data = new { id = "v-1", name = "Truck 1" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var act = async () => await client.GetAsync("v-1");

        var vehicle = await act.Should().NotThrowAsync();
        vehicle.Subject.Id.Should().Be("v-1");
        vehicle.Subject.Name.Should().Be("Truck 1");
    }

    [Fact]
    public async Task HttpClient_UsesStrictPath_WhenResponseConforms()
    {
        // A conforming response deserializes on the strict/source-gen path (no failover).
        var resp = new { data = new { id = "v-2", name = "Truck 2", createdAtTime = "2024-01-01T00:00:00Z" } };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new VehiclesClient(TestFactory.CreateHttpClient(handler));

        var vehicle = await client.GetAsync("v-2");

        vehicle.Id.Should().Be("v-2");
        vehicle.CreatedAtTime.Should().Be(DateTimeOffset.Parse("2024-01-01T00:00:00Z"));
    }
}
