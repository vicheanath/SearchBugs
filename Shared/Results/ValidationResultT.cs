using Shared.Errors;

namespace Shared.Results;

/// <summary>
/// Represents a validation result of some operation, with the errors that occurred.
/// </summary>
/// <typeparam name="TValue">The result value type.</typeparam>
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult{TValue}"/> class.
    /// </summary>
    /// <param name="errors">The errors.</param>
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) =>
        Errors = errors;

    /// <inheritdoc />
    public Error[] Errors { get; }

    /// <summary>
    /// Creates a new <see cref="ValidationResult{TValue}"/> with the specified errors.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns>The new validation result instance with the specified errors.</returns>
    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}
