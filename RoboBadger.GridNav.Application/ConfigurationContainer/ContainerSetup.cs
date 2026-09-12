using Microsoft.Extensions.DependencyInjection;
using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints;

namespace RoboBadger.GridNav.Application.ConfigurationContainer;

public static class ContainerSetup
{
    public static IServiceCollection ConfigureBadgerCentralCortex (this IServiceCollection services)
    {
        services.AddSingleton<IBadgerCrawlService, BadgerCrawlService>();
        services.AddSingleton<IBadgerSafetyEmissionService, BadgerSafetyEmissionService>();
        services.AddSingleton<IBadgerSwarmProcessor, BadgerSwarmProcessor>();
        services.AddSingleton<IGroundTransmissionValidator, GroundTransmissionValidator>();
        return services;
    }
}