using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.Api.Configurations;

internal static class UseCasesConfigurations
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<GetResourcesForUserUseCase>();
        return serviceCollection;
    }
}
