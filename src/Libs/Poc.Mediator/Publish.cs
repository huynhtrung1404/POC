using System.Net.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Poc.Mediator;

public class Publish(IServiceProvider serviceProvider) : IPublish
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);
        var method = handlerType.GetMethod("HandleAsync");
        foreach (var handler in handlers)
        {
            await (Task)method?.Invoke(handler, [notification, cancellationToken])!;
        }
    }
}