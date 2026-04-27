namespace Samsara.Sdk.DependencyInjection;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Samsara.Sdk.Authentication;
using Samsara.Sdk.Clients;
using Samsara.Sdk.Configuration;
using Samsara.Sdk.Http;
#if NET8_0_OR_GREATER
using Microsoft.Extensions.Http.Resilience;
using Polly;
#endif

/// <summary>
/// Extension methods for registering the Samsara SDK with the .NET dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    private static readonly string UserAgent =
        $"SamsaraDotnet/{Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "0.0.0"}";

    /// <summary>
    /// Registers the Samsara API client and all domain service clients with the DI container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">An action to configure <see cref="SamsaraClientOptions"/>.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddSamsaraClient(
        this IServiceCollection services,
        Action<SamsaraClientOptions> configure)
    {
        if (configure is null) throw new ArgumentNullException(nameof(configure));

        // Register and validate options
        services.AddOptions<SamsaraClientOptions>()
            .Configure(configure)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Register the auth handler
        services.TryAddTransient<SamsaraAuthenticationHandler>();

        // Register the typed HttpClient with resilience policies
        services.AddHttpClient<SamsaraHttpClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<SamsaraClientOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
                client.Timeout = options.Timeout;
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            })
            .AddHttpMessageHandler<SamsaraAuthenticationHandler>()
#if NET8_0_OR_GREATER
            .AddStandardResilienceHandler(options =>
            {
                // Configure retry for transient errors + rate limits
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.UseJitter = true;
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.Delay = TimeSpan.FromMilliseconds(500);
            });
#else
            .AddHttpMessageHandler<SamsaraRetryHandler>();

        services.TryAddTransient<SamsaraRetryHandler>();
#endif

        // Register domain service clients
        services.TryAddScoped<ITagsClient, TagsClient>();
        services.TryAddScoped<IAddressesClient, AddressesClient>();
        services.TryAddScoped<IVehiclesClient, VehiclesClient>();
        services.TryAddScoped<IDriversClient, DriversClient>();
        services.TryAddScoped<ISafetyClient, SafetyClient>();
        services.TryAddScoped<IRoutesClient, RoutesClient>();
        services.TryAddScoped<IComplianceClient, ComplianceClient>();
        services.TryAddScoped<IMaintenanceClient, MaintenanceClient>();
        services.TryAddScoped<IDocumentsClient, DocumentsClient>();
        services.TryAddScoped<IAlertsClient, AlertsClient>();
        services.TryAddScoped<IFuelClient, FuelClient>();
        services.TryAddScoped<IWebhooksClient, WebhooksClient>();
        services.TryAddScoped<IOrganizationClient, OrganizationClient>();
        services.TryAddScoped<IUsersClient, UsersClient>();
        services.TryAddScoped<IContactsClient, ContactsClient>();
        services.TryAddScoped<IEquipmentClient, EquipmentClient>();
        services.TryAddScoped<IIndustrialClient, IndustrialClient>();
        services.TryAddScoped<IMessagesClient, MessagesClient>();
        services.TryAddScoped<ITrailersClient, TrailersClient>();
        services.TryAddScoped<IGatewaysClient, GatewaysClient>();
        services.TryAddScoped<IUserRolesClient, UserRolesClient>();
        services.TryAddScoped<ITachographClient, TachographClient>();
        services.TryAddScoped<IIftaClient, IftaClient>();
        services.TryAddScoped<IHubsClient, HubsClient>();
        services.TryAddScoped<ITripsClient, TripsClient>();
        services.TryAddScoped<IFormsClient, FormsClient>();
        services.TryAddScoped<IAttributesClient, AttributesClient>();
        services.TryAddScoped<IDriverVehicleAssignmentsClient, DriverVehicleAssignmentsClient>();
        services.TryAddScoped<ITrailerAssignmentsClient, TrailerAssignmentsClient>();
        services.TryAddScoped<ICarrierProposedAssignmentsClient, CarrierProposedAssignmentsClient>();
        services.TryAddScoped<ITrainingClient, TrainingClient>();
        services.TryAddScoped<ISensorsClient, SensorsClient>();
        services.TryAddScoped<IIssuesClient, IssuesClient>();
        services.TryAddScoped<IMediaClient, MediaClient>();

        // Register the aggregate facade
        services.TryAddScoped<ISamsaraClient, SamsaraClient>();

        return services;
    }
}
