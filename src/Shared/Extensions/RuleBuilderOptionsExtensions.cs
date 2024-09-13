using FluentValidation;
using Shared.Errors;

namespace Shared.Extensions;

/// <summary>
/// Contains extension methods for the <see cref="IRuleBuilderOptions{T,TProperty}"/> class.
/// </summary>
public static class RuleBuilderOptionsExtensions
{
    /// <summary>
    /// Specifies a custom validation error to use if validation fails.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <typeparam name="TProperty">The type of the property being validated.</typeparam>
    /// <param name="rule">The rule.</param>
    /// <param name="error">The validation error.</param>
    /// <returns>The same rule, to allow for multiple calls to be chained.</returns>
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Error error) =>
        rule.WithErrorCode(error.Code).WithMessage(error.Message);
}
