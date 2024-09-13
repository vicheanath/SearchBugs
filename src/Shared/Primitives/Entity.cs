namespace Shared.Primitives;

/// <summary>
/// Represents the abstract entity primitive.
/// </summary>
/// <typeparam name="TEntityId">The entity identifier type.</typeparam>
public abstract class Entity<TEntityId> : IEquatable<Entity<TEntityId>>, IEntity
    where TEntityId : class, IEntityId
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(TEntityId id)
        : this() =>
        Id = id ?? throw new ArgumentException("The entity identifier is required.", nameof(id));

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
    /// </summary>
    /// <remarks>
    /// Required for deserialization.
    /// </remarks>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public TEntityId Id { get; private init; }

    public static bool operator ==(Entity<TEntityId>? a, Entity<TEntityId>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TEntityId>? a, Entity<TEntityId>? b) => !(a == b);

    /// <inheritdoc />
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    /// <inheritdoc />
    public virtual bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is not Entity<TEntityId> other)
        {
            return false;
        }

        return Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode() * 41;

    /// <inheritdoc />
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Raises the specified domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
