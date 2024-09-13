using Shared.Errors;

namespace SearchBugs.Domain.Services;

public static class Ensure
{
    public static Error NotNull<T>(T value, Error error)
    {
        return value == null ? error : Error.None;
    }

    public static Error NotNullOrEmpty<T>(T value, Error error)
    {
        return value == null || string.IsNullOrEmpty(value.ToString()) ? error : Error.None;
    }

    public static Error NotNullOrWhiteSpace(string value, Error error)
    {
        return string.IsNullOrWhiteSpace(value) ? error : Error.None;
    }

    public static Error NotNegative<T>(T value, Error error)
    {
        return value switch
        {
            int i when i < 0 => error,
            long l when l < 0 => error,
            decimal d when d < 0 => error,
            _ => Error.None
        };
    }
}
