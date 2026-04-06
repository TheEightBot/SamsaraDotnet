namespace Samsara.Sdk.Clients;

internal sealed class SamsaraClient : ISamsaraClient
{
    public SamsaraClient(
        ITagsClient tags,
        IAddressesClient addresses,
        IVehiclesClient vehicles,
        IDriversClient drivers,
        ISafetyClient safety,
        IRoutesClient routes,
        IComplianceClient compliance,
        IMaintenanceClient maintenance,
        IDocumentsClient documents,
        IAlertsClient alerts,
        IFuelClient fuel,
        IWebhooksClient webhooks,
        IOrganizationClient organization,
        IUsersClient users,
        IContactsClient contacts,
        IEquipmentClient equipment,
        IIndustrialClient industrial,
        IMessagesClient messages,
        ITrailersClient trailers,
        IGatewaysClient gateways,
        IUserRolesClient userRoles,
        ITachographClient tachograph,
        IIftaClient ifta,
        IHubsClient hubs,
        ITripsClient trips,
        IFormsClient forms,
        IAttributesClient attributes,
        IDriverVehicleAssignmentsClient driverVehicleAssignments,
        ITrailerAssignmentsClient trailerAssignments,
        ICarrierProposedAssignmentsClient carrierProposedAssignments,
        ITrainingClient training,
        ISensorsClient sensors,
        IIssuesClient issues,
        IMediaClient media)
    {
        Tags = tags;
        Addresses = addresses;
        Vehicles = vehicles;
        Drivers = drivers;
        Safety = safety;
        Routes = routes;
        Compliance = compliance;
        Maintenance = maintenance;
        Documents = documents;
        Alerts = alerts;
        Fuel = fuel;
        Webhooks = webhooks;
        Organization = organization;
        Users = users;
        Contacts = contacts;
        Equipment = equipment;
        Industrial = industrial;
        Messages = messages;
        Trailers = trailers;
        Gateways = gateways;
        UserRoles = userRoles;
        Tachograph = tachograph;
        Ifta = ifta;
        Hubs = hubs;
        Trips = trips;
        Forms = forms;
        Attributes = attributes;
        DriverVehicleAssignments = driverVehicleAssignments;
        TrailerAssignments = trailerAssignments;
        CarrierProposedAssignments = carrierProposedAssignments;
        Training = training;
        Sensors = sensors;
        Issues = issues;
        Media = media;
    }

    public ITagsClient Tags { get; }
    public IAddressesClient Addresses { get; }
    public IVehiclesClient Vehicles { get; }
    public IDriversClient Drivers { get; }
    public ISafetyClient Safety { get; }
    public IRoutesClient Routes { get; }
    public IComplianceClient Compliance { get; }
    public IMaintenanceClient Maintenance { get; }
    public IDocumentsClient Documents { get; }
    public IAlertsClient Alerts { get; }
    public IFuelClient Fuel { get; }
    public IWebhooksClient Webhooks { get; }
    public IOrganizationClient Organization { get; }
    public IUsersClient Users { get; }
    public IContactsClient Contacts { get; }
    public IEquipmentClient Equipment { get; }
    public IIndustrialClient Industrial { get; }
    public IMessagesClient Messages { get; }
    public ITrailersClient Trailers { get; }
    public IGatewaysClient Gateways { get; }
    public IUserRolesClient UserRoles { get; }
    public ITachographClient Tachograph { get; }
    public IIftaClient Ifta { get; }
    public IHubsClient Hubs { get; }
    public ITripsClient Trips { get; }
    public IFormsClient Forms { get; }
    public IAttributesClient Attributes { get; }
    public IDriverVehicleAssignmentsClient DriverVehicleAssignments { get; }
    public ITrailerAssignmentsClient TrailerAssignments { get; }
    public ICarrierProposedAssignmentsClient CarrierProposedAssignments { get; }
    public ITrainingClient Training { get; }
    public ISensorsClient Sensors { get; }
    public IIssuesClient Issues { get; }
    public IMediaClient Media { get; }
}
