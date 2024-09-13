using Shared.Errors;

namespace Shared.Results;

/// <summary>
/// Represents the validation result containing an array of errors.
/// </summary>
public interface IValidationResult
{
    public static readonly Error ValidationError = new("ValidationError", "A validation problem occurred.");

    /// <summary>
    /// Gets the errors.
    /// </summary>
    Error[] Errors { get; }
}
