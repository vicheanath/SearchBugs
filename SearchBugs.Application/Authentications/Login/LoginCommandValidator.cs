using FluentValidation;
using Shared.Extensions;

namespace SearchBugs.Application.Authentications.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithError(AuthValidationErrors.EmailIsRequired)
            .EmailAddress().WithError(AuthValidationErrors.InvalidEmail);
        RuleFor(x => x.Password).NotEmpty()
            .WithError(AuthValidationErrors.PasswordIsRequired);
    }
}
