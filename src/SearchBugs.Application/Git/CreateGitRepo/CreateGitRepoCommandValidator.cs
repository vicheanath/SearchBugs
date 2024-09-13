using FluentValidation;
using Shared.Extensions;

namespace SearchBugs.Application.Git.CreateGitRepo;

internal sealed class CreateGitRepoCommandValidator : AbstractValidator<CreateGitRepoCommand>
{
    public CreateGitRepoCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithError(GitValidationErrors.NameIsRequired);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithError(GitValidationErrors.DescriptionIsRequired);

        RuleFor(x => x.Url)
            .NotEmpty()
            .WithError(GitValidationErrors.UrlIsRequired);
    }
}
