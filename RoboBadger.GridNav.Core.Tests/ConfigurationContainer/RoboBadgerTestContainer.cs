using Microsoft.Extensions.DependencyInjection;
using RoboBadger.GridNav.Application.ConfigurationContainer;

namespace RoboBadger.GridNav.Core.Tests.ConfigurationContainer;

public static class RoboBadgerTestContainer
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.ConfigureBadgerCentralCortex();
        return services;
    }
}
