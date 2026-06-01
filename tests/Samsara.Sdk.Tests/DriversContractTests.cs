namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Tests.Helpers;

/// <summary>
/// Contract tests for the Drivers domain (Phase 3). <see cref="Samsara.Sdk.Models.Drivers.Driver"/>
/// was relaxed so <c>id</c> and <c>name</c> are nullable: a response that omits those
/// keys must still deserialize rather than throw the way an over-tightened
/// <c>required</c> model would (the Hubs-style deserialization throw). These tests also
/// confirm <c>dateOfBirth</c> binds as a string.
/// </summary>
public sealed class DriversContractTests
{
    // ── GET /fleet/drivers/{id} — full payload ──────────────────────────────
    [Fact]
    public async Task GetAsync_BindsDateOfBirthAndCoreFields()
    {
        var resp = new
        {
            data = new
            {
                id = "drv-1",
                name = "Jane Doe",
                dateOfBirth = "1985-07-15",
                username = "jdoe",
                licenseNumber = "D1234567",
                licenseState = "CA",
            },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DriversClient(TestFactory.CreateHttpClient(handler));

        var driver = await client.GetAsync("drv-1");

        driver.Id.Should().Be("drv-1");
        driver.Name.Should().Be("Jane Doe");
        driver.DateOfBirth.Should().Be("1985-07-15");
        driver.LicenseState.Should().Be("CA");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/drivers/drv-1");
    }

    // ── GET /fleet/drivers — payload omitting id/name still binds ───────────
    [Fact]
    public async Task ListAsync_DeserializesWhenIdAndNameOmitted()
    {
        // The over-tightening fix: a driver entry without `id`/`name` must NOT throw
        // on deserialization (System.Text.Json `required` would have thrown).
        var resp = new
        {
            data = new[]
            {
                new { username = "anon", licenseState = "TX" },
            },
            pagination = new { hasNextPage = false },
        };
        var handler = MockHttpMessageHandler.WithJsonResponse(resp);
        var client = new DriversClient(TestFactory.CreateHttpClient(handler));

        var drivers = await CollectAsync(client.ListAsync());

        drivers.Should().HaveCount(1);
        drivers[0].Id.Should().BeNull();
        drivers[0].Name.Should().BeNull();
        drivers[0].Username.Should().Be("anon");

        handler.LastRequest.RequestUri!.PathAndQuery.Should().Contain("fleet/drivers");
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
