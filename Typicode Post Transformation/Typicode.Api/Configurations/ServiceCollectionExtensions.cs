using Typicode.Api.Controllers;

namespace Typicode.Api.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        return services
            .AddHttpClient()
            .AddSingleton<ITypicodeRestService, TypicodeRestService>();
    }
}
