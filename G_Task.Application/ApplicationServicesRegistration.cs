using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace G_Task.Application
{
    public static class ApplicationServicesRegistration 
    {
        public static void ConfigurationApplicationServices (this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());
          
        }
    }
} 
