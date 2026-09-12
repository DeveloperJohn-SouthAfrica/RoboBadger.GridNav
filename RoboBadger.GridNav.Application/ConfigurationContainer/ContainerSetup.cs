using Microsoft.Extensions.DependencyInjection;
using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints;

namespace RoboBadger.GridNav.Application.ConfigurationContainer;

/// <summary>
/// The container setup class
/// </summary>
public static class ContainerSetup
{
    /// <summary>
    /// Configures the badger central cortex using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <returns>The services</returns>
    public static IServiceCollection ConfigureBadgerCentralCortex (this IServiceCollection services)
    {
        services.AddSingleton<IBadgerCrawlService, BadgerCrawlService>();
        services.AddSingleton<IBadgerSafetyEmissionService, BadgerSafetyEmissionService>();
        services.AddSingleton<IBadgerSwarmProcessor, BadgerSwarmProcessor>();
        services.AddSingleton<IGroundTransmissionValidator, GroundTransmissionValidator>();
        return services;
    }
}