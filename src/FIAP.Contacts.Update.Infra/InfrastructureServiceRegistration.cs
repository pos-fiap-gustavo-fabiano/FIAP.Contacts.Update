using FIAP.Contacts.Update.Domain.ContactAggregate;
using FIAP.Contacts.Update.Infra.Context;
using FIAP.Contacts.Update.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Contacts.Update.Infra
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }

        public static IServiceProvider UpdateMigrate(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();

            return serviceProvider;
        }
    }
}
