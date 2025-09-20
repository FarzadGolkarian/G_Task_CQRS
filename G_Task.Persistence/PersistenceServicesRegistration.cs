using G_Task.Application.Contracts.Persistence;
using G_Task.Application.Contracts.Persistence.Addresses;
using G_Task.Application.Contracts.Persistence.Persons;
using G_Task.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace G_Task.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
    IConfiguration configuration)
        {
            services.AddDbContext<G_TaskDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("G_TaskConnectionString"));

            });

            services.AddHttpContextAccessor();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;


        }
    }
}
