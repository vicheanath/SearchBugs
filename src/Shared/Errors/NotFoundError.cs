namespace Shared.Errors;

/// <summary>
/// Represents the not found error.
/// </summary>
public sealed class NotFoundError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundError"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    public NotFoundError(string code, string message)
        : base(code, message)
    {
    }
}
