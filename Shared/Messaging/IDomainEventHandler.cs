using MediatR;
using Shared.Primitives;

namespace Shared.Messaging;

/// <summary>
/// Represents the domain event handler interface.
/// </summary>
/// <typeparam name="TEvent">The event type.</typeparam>
public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
