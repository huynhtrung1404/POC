namespace Poc.Mediator;

public interface IPublish
{
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
}