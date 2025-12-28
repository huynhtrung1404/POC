using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Poc.Mediator;

namespace Poc.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.Scan(x => x.FromAssemblies(typeof(DependencyInjection).Assembly)
                    .AddClasses(x => x.Where(type => !type.Name.EndsWith("Specification") && !type.IsAssignableTo(typeof(IRequest))))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        services.AddMediator(Assembly.GetExecutingAssembly());
        return services;
    }
}