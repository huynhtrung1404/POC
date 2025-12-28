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
            .SelectMany(t => t.GetInterfaces().Where(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => new { Interface = i, Type = t }))
            .ToList();

        foreach (var handler in implementations)
        {
            services.AddTransient(handler.Interface, handler.Type);
        }

        return services;
    }

}