using FluentValidation;

namespace SearchBugs.Application.Git.CommitChanges;

internal sealed class CommitChangeCommandValidator : AbstractValidator<CommitChangeCommand>
{
    public CommitChangeCommandValidator()
    {
        RuleFor(x => x.Url).NotEmpty();
        RuleFor(x => x.AuthorName).NotEmpty();
        RuleFor(x => x.AuthorEmail).NotEmpty();
        RuleFor(x => x.CommitMessage).NotEmpty();
        RuleFor(x => x.FileContent).NotEmpty();
    }
}
