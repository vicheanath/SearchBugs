namespace Shared.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IReadOnlyCollection<ValidationError> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }

    public IReadOnlyCollection<ValidationError> Errors { get; }
}

public record ValidationError(string PropertyName, string ErrorMessage);