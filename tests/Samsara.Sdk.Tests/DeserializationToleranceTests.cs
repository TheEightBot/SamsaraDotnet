namespace Samsara.Sdk.Tests;

using System.Reflection;
using System.Text.Json;
using FluentAssertions;
using Samsara.Sdk.Models.Fleet;
using Samsara.Sdk.Serialization;

/// <summary>
/// Guards the resilient-deserialization contract: the live Samsara API routinely omits
/// response fields its own OpenAPI spec marks <c>required</c> (e.g. <c>Vehicle.createdAtTime</c>).
/// The SDK must deserialize such responses anyway — never throw a "missing required properties"
/// <see cref="JsonException"/> over an absent field. This is enforced centrally by the
/// <c>TolerateMissingRequiredMembers</c> modifier on <see cref="SamsaraSerializerOptions.Default"/>.
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
                && !type.ContainsGenericParameters // skip open generic envelopes (e.g. SamsaraListResponse<>)
                && type.Namespace?.StartsWith("Samsara.Sdk.Models", StringComparison.Ordinal) == true
                && type.GetConstructor(Type.EmptyTypes) is not null)
            {
                yield return new object[] { type };
            }
        }
    }

    [Theory]
    [MemberData(nameof(AllModelTypes))]
    public void EveryModel_DeserializesFromEmptyObject_WithoutThrowing(Type modelType)
    {
        // `{}` omits EVERY field — the worst case the API can hand us. None of the SDK's
        // models may throw on it.
        var act = () => JsonSerializer.Deserialize("{}", modelType, SamsaraSerializerOptions.Default);

        act.Should().NotThrow(
            $"the Samsara API may omit any field on {modelType.Name}; the SDK must tolerate a missing member");
    }

    [Fact]
    public void ModelDiscovery_FindsTheFullModelSurface()
    {
        // Stops the Theory from passing vacuously if the reflection query ever breaks.
        AllModelTypes().Should().HaveCountGreaterThan(300);
    }

    [Fact]
    public void TheModifier_IsLoadBearing_StrictOptionsStillThrowForVehicle()
    {
        // Proves the tolerance comes from our modifier, not from Vehicle being lenient:
        // with stock options (no modifier), the exact production failure reproduces.
        var strict = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var strictAct = () => JsonSerializer.Deserialize<Vehicle>("{}", strict);
        strictAct.Should().Throw<JsonException>().WithMessage("*createdAtTime*");

        // ...and with the SDK's configured options, the same payload deserializes cleanly.
        var tolerantAct = () => JsonSerializer.Deserialize<Vehicle>("{}", SamsaraSerializerOptions.Default);
        tolerantAct.Should().NotThrow();
    }

    [Fact]
    public void Vehicle_DeserializesWhenApiOmitsCreatedAtTime()
    {
        // The exact reported failure, end to end through the model.
        const string json = """{ "id": "v-1", "name": "Truck 1" }""";

        var vehicle = JsonSerializer.Deserialize<Vehicle>(json, SamsaraSerializerOptions.Default);

        vehicle.Should().NotBeNull();
        vehicle!.Id.Should().Be("v-1");
        vehicle.Name.Should().Be("Truck 1");
        vehicle.CreatedAtTime.Should().Be(default(DateTimeOffset));
    }
}
