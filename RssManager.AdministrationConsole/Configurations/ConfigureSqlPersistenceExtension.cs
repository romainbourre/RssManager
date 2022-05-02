using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RssManager.Adapters.Persistence.MySqlEfCorePersistence;
using RssManager.Adapters.Persistence.MySqlEfCorePersistence.Repositories;
using RssManager.Application.Interfaces;


namespace RssManager.AdministrationConsole.Configurations;

internal static class ConfigureSqlPersistenceExtension
{
    public static IServiceCollection UseMySqlPersistence(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection
            .AddDbContext<ApplicationDbContext>(options =>
            {
                ServerVersion version = GetServerVersion(connectionString);
                options.UseMySql(connectionString, version);
            })
            .AddScoped<ApplicationDbContext>()
            .AddScoped<IUserRepository, MySqlUserRepository>()
            .AddScoped<IResourceRepository, MySqlResourceRepository>();

        return serviceCollection;
    }
    
    private static ServerVersion GetServerVersion(string connectionString)
    {
        ServerVersion? version = ServerVersion.AutoDetect(connectionString);

        if (version == null)
            throw new ArgumentNullException(nameof(version));

        return version;
    }
}
