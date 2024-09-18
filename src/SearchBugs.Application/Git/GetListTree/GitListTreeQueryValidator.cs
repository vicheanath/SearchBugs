using FluentValidation;

namespace SearchBugs.Application.Git.GetListTree;

internal sealed class GitListTreeQueryValidator : AbstractValidator<GetListTreeQuery>
{
    public GitListTreeQueryValidator()
    {
        RuleFor(x => x.Url).NotEmpty();
        RuleFor(x => x.CommitSha).NotEmpty();
    }
}
