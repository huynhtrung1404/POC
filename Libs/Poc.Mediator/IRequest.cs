namespace Poc.Mediator;

public interface IRequest { }
public interface IRequest<TResponse> : IRequest { }
public interface IRequestHandler<TRequest> where TRequest : IRequest
{
    Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface INotification { }
public interface INotificationHandler<TNotification> where TNotification : INotification
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default);
}
