namespace Shared.EventBus;

/// <summary>
/// Represents the abstract integration event handler.
/// </summary>
/// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
public abstract class IntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>, IIntegrationEventHandler
    where TIntegrationEvent : IIntegrationEvent
{
    /// <inheritdoc />
    public abstract Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);

    /// <inheritdoc />
    public Task Handle(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
        Handle((TIntegrationEvent)integrationEvent, cancellationToken);
}
