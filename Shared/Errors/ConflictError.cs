namespace Shared.Errors;

/// <summary>
/// Represents the conflict error.
/// </summary>
public sealed class ConflictError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictError"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    public ConflictError(string code, string message)
        : base(code, message)
    {
    }
}
