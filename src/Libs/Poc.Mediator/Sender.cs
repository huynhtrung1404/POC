
using Microsoft.Extensions.DependencyInjection;

namespace Poc.Mediator;

public class Sender(IServiceProvider serviceProvider) : ISender
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod("HandleAsync");
        return await (Task<TResponse>)method?.Invoke(handler, [request, cancellationToken])!;
    }

    public async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod("HandleAsync");
        await (Task)method?.Invoke(handler, [request, cancellationToken])!;
    }
}