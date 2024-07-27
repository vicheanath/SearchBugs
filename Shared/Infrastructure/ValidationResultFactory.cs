using Shared.Errors;
using Shared.Results;

namespace Shared.Infrastructure;

/// <summary>
/// Represents the validation result factory.
/// </summary>
internal static class ValidationResultFactory
{
    private static readonly Type ResultType = typeof(Result);
    private static readonly Type ValidationResultGenericTypeDefinition = typeof(ValidationResult<>).GetGenericTypeDefinition();

    /// <summary>
    /// Creates a new validation result with the specified errors.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="errors">The errors.</param>
    /// <returns>The new validation result with the specified errors.</returns>
    internal static TResult Create<TResult>(Error[] errors)
        where TResult : Result =>
        typeof(TResult) == ResultType
            ? (ValidationResult.WithErrors(errors) as TResult)!
            : CreateGenericValidationResult<TResult>(typeof(TResult).GenericTypeArguments[0], errors);

    private static TResult CreateGenericValidationResult<TResult>(Type resultGenericType, Error[] errors) =>
        (TResult)ValidationResultGenericTypeDefinition
            .MakeGenericType(resultGenericType)
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;
}
