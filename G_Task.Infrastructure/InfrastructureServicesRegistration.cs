using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace G_Task.Infrastructure
{
    public static class InfrastructureServicesRegistration 
    {
        public static IServiceCollection ConfigurationInfrastructureServices(this IServiceCollection  services
            , IConfiguration configuration)
        {

            return services;
        }

    }
}
