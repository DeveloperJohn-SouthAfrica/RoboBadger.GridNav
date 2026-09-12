using Microsoft.Extensions.DependencyInjection;
using RoboBadger.GridNav.Application.ConfigurationContainer;

namespace RoboBadger.GridNav.Core.Tests.ConfigurationContainer;

/// <summary>
/// The robo badger test container class
/// </summary>
public static class RoboBadgerTestContainer
{
    /// <summary>
    /// Configures the services using the specified services
    /// </summary>
    /// <param name="services">The services</param>
    /// <returns>The services</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.ConfigureBadgerCentralCortex();
        return services;
    }
}
