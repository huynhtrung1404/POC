namespace Poc.Mediator;

public interface IRequest { }
public interface IRequest<TResponse> : IRequest { }
public interface IRequestHandler<TRequest> where TRequest : IRequest
{

}

public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> { }

public interface INotification { }
public interface INotificationHandler<TNotification> where TNotification : INotification
{

}
