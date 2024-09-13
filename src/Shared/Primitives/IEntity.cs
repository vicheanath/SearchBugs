namespace Shared.Primitives;

/// <summary>
/// Represents the entity interface.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <returns>The readonly list of domain events.</returns>
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    /// <summary>
    /// Clears the list of domain events.
    /// </summary>
    void ClearDomainEvents();
}
