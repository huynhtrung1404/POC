using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Poc.Mediator;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddTransient<ISender, Sender>();
        services.AddTransient<IPublish, Publish>();

        var handlerType = typeof(IRequestHandler<,>);

        var implementations = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
            .Where(x => x.Interface.IsGenericType && x.Interface.GetGenericTypeDefinition() == handlerType)
            .ToList();

        foreach (var handler in implementations)
        {
            services.AddTransient(handler.Interface, handler.Type);
        }

        return services;
    }

}