namespace Shared.Time;

public sealed class SystemTime : ISystemTime
{
    public static DateTime UtcNow => DateTime.UtcNow;
}
