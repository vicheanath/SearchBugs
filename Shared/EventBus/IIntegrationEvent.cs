namespace Shared.EventBus;

/// <summary>
/// Represents the integration event interface.
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}
