using FluentValidation;

namespace SearchBugs.Application.Git.GetFileContents;

internal sealed class GetFileContentQueryValidator : AbstractValidator<GetFileContentQuery>
{
    public GetFileContentQueryValidator()
    {
        RuleFor(x => x.Url).NotEmpty();
        RuleFor(x => x.CommitSha).NotEmpty();
        RuleFor(x => x.FilePath).NotEmpty();
    }
}
