using Microsoft.Extensions.DependencyInjection;

namespace Poc.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblies(typeof(DependencyInjection).Assembly)
                    .AddClasses(x => x.Where(type => !type.Name.EndsWith("Specification")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        return services;
    }
}