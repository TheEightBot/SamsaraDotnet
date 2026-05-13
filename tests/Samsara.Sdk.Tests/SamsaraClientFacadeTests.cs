namespace Samsara.Sdk.Tests;

using FluentAssertions;
using NSubstitute;
using Samsara.Sdk.Clients;

public sealed class SamsaraClientFacadeTests
{
    [Fact]
    public void AllProperties_ReturnInjectedInstances()
    {
        var tags = Substitute.For<ITagsClient>();
        var addresses = Substitute.For<IAddressesClient>();
        var vehicles = Substitute.For<IVehiclesClient>();
        var drivers = Substitute.For<IDriversClient>();
        var safety = Substitute.For<ISafetyClient>();
        var routes = Substitute.For<IRoutesClient>();
        var compliance = Substitute.For<IComplianceClient>();
        var maintenance = Substitute.For<IMaintenanceClient>();
        var documents = Substitute.For<IDocumentsClient>();
        var alerts = Substitute.For<IAlertsClient>();
        var fuel = Substitute.For<IFuelClient>();
        var webhooks = Substitute.For<IWebhooksClient>();
        var organization = Substitute.For<IOrganizationClient>();
        var users = Substitute.For<IUsersClient>();
        var contacts = Substitute.For<IContactsClient>();
        var equipment = Substitute.For<IEquipmentClient>();
        var industrial = Substitute.For<IIndustrialClient>();
        var messages = Substitute.For<IMessagesClient>();
        var trailers = Substitute.For<ITrailersClient>();
        var gateways = Substitute.For<IGatewaysClient>();
        var userRoles = Substitute.For<IUserRolesClient>();
        var tachograph = Substitute.For<ITachographClient>();
        var ifta = Substitute.For<IIftaClient>();
        var hubs = Substitute.For<IHubsClient>();
        var trips = Substitute.For<ITripsClient>();
        var forms = Substitute.For<IFormsClient>();
        var attributes = Substitute.For<IAttributesClient>();
        var driverVehicleAssignments = Substitute.For<IDriverVehicleAssignmentsClient>();
        var trailerAssignments = Substitute.For<ITrailerAssignmentsClient>();
        var carrierProposedAssignments = Substitute.For<ICarrierProposedAssignmentsClient>();
        var training = Substitute.For<ITrainingClient>();
        var sensors = Substitute.For<ISensorsClient>();
        var issues = Substitute.For<IIssuesClient>();
        var media = Substitute.For<IMediaClient>();
        var assets = Substitute.For<IAssetsClient>();
        var carbCtc = Substitute.For<ICarbCtcClient>();
        var coaching = Substitute.For<ICoachingClient>();
        var driverTrailerAssignments = Substitute.For<IDriverTrailerAssignmentsClient>();
        var idling = Substitute.For<IIdlingClient>();
        var liveSharingLinks = Substitute.For<ILiveSharingLinksClient>();
        var readings = Substitute.For<IReadingsClient>();
        var settings = Substitute.For<ISettingsClient>();
        var workOrders = Substitute.For<IWorkOrdersClient>();

        var facade = new SamsaraClient(
            tags, addresses, vehicles, drivers, safety, routes,
            compliance, maintenance, documents, alerts, fuel, webhooks,
            organization, users, contacts, equipment, industrial, messages,
            trailers, gateways, userRoles, tachograph, ifta, hubs, trips, forms,
            attributes, driverVehicleAssignments, trailerAssignments,
            carrierProposedAssignments, training, sensors, issues, media,
            assets, carbCtc, coaching, driverTrailerAssignments, idling,
            liveSharingLinks, readings, settings, workOrders);

        facade.Tags.Should().BeSameAs(tags);
        facade.Addresses.Should().BeSameAs(addresses);
        facade.Vehicles.Should().BeSameAs(vehicles);
        facade.Drivers.Should().BeSameAs(drivers);
        facade.Equipment.Should().BeSameAs(equipment);
        facade.Safety.Should().BeSameAs(safety);
        facade.Routes.Should().BeSameAs(routes);
        facade.Compliance.Should().BeSameAs(compliance);
        facade.Maintenance.Should().BeSameAs(maintenance);
        facade.Documents.Should().BeSameAs(documents);
        facade.Alerts.Should().BeSameAs(alerts);
        facade.Contacts.Should().BeSameAs(contacts);
        facade.Messages.Should().BeSameAs(messages);
        facade.Fuel.Should().BeSameAs(fuel);
        facade.Webhooks.Should().BeSameAs(webhooks);
        facade.Organization.Should().BeSameAs(organization);
        facade.Users.Should().BeSameAs(users);
        facade.Industrial.Should().BeSameAs(industrial);
        facade.Trailers.Should().BeSameAs(trailers);
        facade.Gateways.Should().BeSameAs(gateways);
        facade.UserRoles.Should().BeSameAs(userRoles);
        facade.Tachograph.Should().BeSameAs(tachograph);
        facade.Ifta.Should().BeSameAs(ifta);
        facade.Hubs.Should().BeSameAs(hubs);
        facade.Trips.Should().BeSameAs(trips);
        facade.Forms.Should().BeSameAs(forms);
        facade.Attributes.Should().BeSameAs(attributes);
        facade.DriverVehicleAssignments.Should().BeSameAs(driverVehicleAssignments);
        facade.TrailerAssignments.Should().BeSameAs(trailerAssignments);
        facade.CarrierProposedAssignments.Should().BeSameAs(carrierProposedAssignments);
        facade.Training.Should().BeSameAs(training);
        facade.Sensors.Should().BeSameAs(sensors);
        facade.Issues.Should().BeSameAs(issues);
        facade.Media.Should().BeSameAs(media);
        facade.Assets.Should().BeSameAs(assets);
        facade.CarbCtc.Should().BeSameAs(carbCtc);
        facade.Coaching.Should().BeSameAs(coaching);
        facade.DriverTrailerAssignments.Should().BeSameAs(driverTrailerAssignments);
        facade.Idling.Should().BeSameAs(idling);
        facade.LiveSharingLinks.Should().BeSameAs(liveSharingLinks);
        facade.Readings.Should().BeSameAs(readings);
        facade.Settings.Should().BeSameAs(settings);
        facade.WorkOrders.Should().BeSameAs(workOrders);
    }
}
