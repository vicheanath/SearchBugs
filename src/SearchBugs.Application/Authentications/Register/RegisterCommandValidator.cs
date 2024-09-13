using FluentValidation;
using Shared.Extensions;

namespace SearchBugs.Application.Authentications.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithError(AuthValidationErrors.InvalidEmail);
        RuleFor(x => x.Password).NotEmpty()
            .MinimumLength(6)
            .ChildRules(x => x.RuleFor(y => y).Matches("[A-Z]").WithError(AuthValidationErrors.PasswordNotMatchRequirements("Password must contain at least one uppercase letter")))
            .ChildRules(x => x.RuleFor(y => y).Matches("[a-z]").WithError(AuthValidationErrors.PasswordNotMatchRequirements("Password must contain at least one lowercase letter")))
            .ChildRules(x => x.RuleFor(y => y).Matches("[0-9]").WithError(AuthValidationErrors.PasswordNotMatchRequirements("Password must contain at least one number")));

        RuleFor(x => x.FirstName).NotEmpty().WithError(AuthValidationErrors.FirstNameIsRequired);
        RuleFor(x => x.LastName).NotEmpty().WithError(AuthValidationErrors.LastNameIsRequired);
    }
}
