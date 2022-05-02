using Microsoft.EntityFrameworkCore;
using RssManager.Adapters.Persistence.MySqlEfCorePersistence;
using RssManager.Adapters.Persistence.MySqlEfCorePersistence.Repositories;
using RssManager.Application.Interfaces;


namespace RssManager.Api.Configurations;

internal static class MySqlDatabaseConfigurations
{
    public static IServiceCollection UseMySqlDatabase(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection
            .AddDbContext<ApplicationDbContext>(options =>
            {
                ServerVersion version = GetServerVersion(connectionString);
                options.UseMySql(connectionString, version);
            })
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
