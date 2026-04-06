namespace Samsara.Sdk.Tests;

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Configuration;
using Samsara.Sdk.DependencyInjection;

public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddSamsaraClient_RegistersISamsaraClient()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSamsaraClient(o => o.ApiToken = "test-token");

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var client = scope.ServiceProvider.GetService<ISamsaraClient>();
        client.Should().NotBeNull();
    }

    [Fact]
    public void AddSamsaraClient_RegistersAllDomainClients()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSamsaraClient(o => o.ApiToken = "test-token");

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var sp = scope.ServiceProvider;

        sp.GetService<ITagsClient>().Should().NotBeNull();
        sp.GetService<IAddressesClient>().Should().NotBeNull();
        sp.GetService<IVehiclesClient>().Should().NotBeNull();
        sp.GetService<IDriversClient>().Should().NotBeNull();
        sp.GetService<IEquipmentClient>().Should().NotBeNull();
        sp.GetService<ISafetyClient>().Should().NotBeNull();
        sp.GetService<IRoutesClient>().Should().NotBeNull();
        sp.GetService<IComplianceClient>().Should().NotBeNull();
        sp.GetService<IMaintenanceClient>().Should().NotBeNull();
        sp.GetService<IDocumentsClient>().Should().NotBeNull();
        sp.GetService<IAlertsClient>().Should().NotBeNull();
        sp.GetService<IContactsClient>().Should().NotBeNull();
        sp.GetService<IMessagesClient>().Should().NotBeNull();
        sp.GetService<IFuelClient>().Should().NotBeNull();
        sp.GetService<IWebhooksClient>().Should().NotBeNull();
        sp.GetService<IOrganizationClient>().Should().NotBeNull();
        sp.GetService<IUsersClient>().Should().NotBeNull();
        sp.GetService<IIndustrialClient>().Should().NotBeNull();
    }

    [Fact]
    public void AddSamsaraClient_FacadeExposesSameClientInstances()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSamsaraClient(o => o.ApiToken = "test-token");

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var sp = scope.ServiceProvider;

        var facade = sp.GetRequiredService<ISamsaraClient>();
        var tagsClient = sp.GetRequiredService<ITagsClient>();

        facade.Tags.Should().BeSameAs(tagsClient);
    }

    [Fact]
    public void AddSamsaraClient_ThrowsArgumentNullException_WhenConfigureIsNull()
    {
        var services = new ServiceCollection();

        var act = () => services.AddSamsaraClient(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddSamsaraClient_DoesNotOverrideExistingRegistrations()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        // First registration
        services.AddSamsaraClient(o => o.ApiToken = "first-token");
        // Second registration should not override
        services.AddSamsaraClient(o => o.ApiToken = "second-token");

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        // Should still resolve without error (TryAddScoped won't duplicate)
        var client = scope.ServiceProvider.GetService<ISamsaraClient>();
        client.Should().NotBeNull();
    }
}
