using MediatR;

namespace Shared.Primitives;

/// <summary>
/// Represents the domain event interface.
/// </summary>
public interface IDomainEvent : INotification
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
