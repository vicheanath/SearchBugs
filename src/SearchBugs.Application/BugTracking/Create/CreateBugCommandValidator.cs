using FluentValidation;

namespace SearchBugs.Application.BugTracking.Create;

internal sealed class CreateBugCommandValidator : AbstractValidator<CreateBugCommand>
{
    public CreateBugCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.Status)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Priority)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Severity)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.AssigneeId)
            .NotEmpty();

        RuleFor(x => x.ReporterId)
            .NotEmpty();
    }
}
