using FluentValidation;
using MediatR;
using Shared.Errors;
using Shared.Infrastructure;
using Shared.Results;

namespace Shared.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        Error[] errors = Validate(new ValidationContext<TRequest>(request));

        if (errors.Length != 0)
        {
            return ValidationResultFactory.Create<TResponse>(errors);
        }

        return await next();
    }

    private Error[] Validate(IValidationContext validationContext) =>
        _validators.Select(validator => validator.Validate(validationContext))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(validationFailure => new Error(validationFailure.ErrorCode, validationFailure.ErrorMessage))
            .Distinct()
            .ToArray();
}
