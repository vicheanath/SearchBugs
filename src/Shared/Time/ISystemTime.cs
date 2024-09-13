namespace Shared.Time;

/// <summary>
/// Represents the system time interface.
/// </summary>
public interface ISystemTime
{
    /// <summary>
    /// Gets the current UTC date and time.
    /// </summary>
    public static DateTime UtcNow { get; }
}
