using Shared.Errors;

namespace Shared.Results;

/// <summary>
/// Represents a validation result of some operation, with the errors that occurred.
/// </summary>
public sealed class ValidationResult : Result, IValidationResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class.
    /// </summary>
    /// <param name="errors">The errors.</param>
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) =>
        Errors = errors;

    /// <inheritdoc />
    public Error[] Errors { get; }

    /// <summary>
    /// Creates a new <see cref="ValidationResult"/> with the specified errors.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns>The new validation result instance with the specified errors.</returns>
    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
