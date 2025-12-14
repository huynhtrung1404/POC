using Microsoft.Extensions.DependencyInjection;

namespace Poc.Mediator;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddTransient<ISender, Sender>();
        services.AddTransient<IPublish, Publish>();
        return services;
    }
}