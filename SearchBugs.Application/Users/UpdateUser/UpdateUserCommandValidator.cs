using FluentValidation;
using Shared.Extensions;

namespace SearchBugs.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithError(UserValidationErrors.UserNotFound);
        RuleFor(x => x.FirstName)
            .NotEmpty().WithError(UserValidationErrors.FirstNameIsRequired)
            .MaximumLength(50).WithError(UserValidationErrors.FirstNameMaxLength);
        RuleFor(x => x.LastName)
            .NotEmpty().WithError(UserValidationErrors.LastNameIsRequired)
            .MaximumLength(50).WithError(UserValidationErrors.LastNameMaxLength);
    }
}
