namespace Samsara.Sdk.Clients;

/// <summary>
/// Aggregate facade providing access to all Samsara API domain clients.
/// </summary>
public interface ISamsaraClient
{
    ITagsClient Tags { get; }
    IAddressesClient Addresses { get; }
    IVehiclesClient Vehicles { get; }
    IDriversClient Drivers { get; }
    ISafetyClient Safety { get; }
    IRoutesClient Routes { get; }
    IComplianceClient Compliance { get; }
    IMaintenanceClient Maintenance { get; }
    IDocumentsClient Documents { get; }
    IAlertsClient Alerts { get; }
    IFuelClient Fuel { get; }
    IWebhooksClient Webhooks { get; }
    IOrganizationClient Organization { get; }
    IUsersClient Users { get; }
    IContactsClient Contacts { get; }
    IEquipmentClient Equipment { get; }
    IIndustrialClient Industrial { get; }
    IMessagesClient Messages { get; }
    ITrailersClient Trailers { get; }
    IGatewaysClient Gateways { get; }
    IUserRolesClient UserRoles { get; }
    ITachographClient Tachograph { get; }
    IIftaClient Ifta { get; }
    IHubsClient Hubs { get; }
    ITripsClient Trips { get; }
    IFormsClient Forms { get; }
    IAttributesClient Attributes { get; }
    IDriverVehicleAssignmentsClient DriverVehicleAssignments { get; }
    ITrailerAssignmentsClient TrailerAssignments { get; }
    ICarrierProposedAssignmentsClient CarrierProposedAssignments { get; }
    ITrainingClient Training { get; }
    ISensorsClient Sensors { get; }
    IIssuesClient Issues { get; }
    IMediaClient Media { get; }
}
