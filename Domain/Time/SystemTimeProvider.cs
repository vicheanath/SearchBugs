namespace Domain.Time;

/// <summary>
/// Represents the static system time provider.
/// </summary>
public static class SystemTimeProvider
{
    /// <summary>
    /// Gets the function for getting the current date and time.
    /// </summary>
    public static Func<DateTime> UtcNow { get; internal set; } = () => DateTime.UtcNow;
}
