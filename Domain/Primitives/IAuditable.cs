namespace Domain.Primitives;

/// <summary>
/// Represents the auditable interface.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets the created on date and time.
    /// </summary>
    DateTime CreatedOnUtc { get; }

    /// <summary>
    /// Gets the modified on date and time, if it exists.
    /// </summary>
    DateTime? ModifiedOnUtc { get; }
}
