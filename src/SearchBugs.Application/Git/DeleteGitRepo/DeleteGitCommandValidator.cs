using FluentValidation;
using Shared.Extensions;

namespace SearchBugs.Application.Git.DeleteGitRepo;

internal sealed class DeleteGitCommandValidator : AbstractValidator<DeleteGitRepoCommand>
{
    public DeleteGitCommandValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty()
            .WithError(GitValidationErrors.UrlIsRequired);
    }
}
