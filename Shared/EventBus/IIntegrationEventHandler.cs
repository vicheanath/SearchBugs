namespace Shared.EventBus;

/// <summary>
/// Represents the integration event handler interface.
/// </summary>
/// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    /// <summary>
    /// Handles the specified integration event.
    /// </summary>
    /// <param name="integrationEvent">The integration event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The completed task.</returns>
    Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents the integration event handler interface.
/// </summary>
public interface IIntegrationEventHandler
{
    /// <summary>
    /// Handles the specified integration event.
    /// </summary>
    /// <param name="integrationEvent">The integration event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The completed task.</returns>
    Task Handle(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}
