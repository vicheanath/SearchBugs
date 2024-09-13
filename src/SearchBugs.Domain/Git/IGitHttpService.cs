namespace SearchBugs.Domain.Git;

public interface IGitHttpService
{
    Task Handle(string repositoryName, CancellationToken cancellationToken = default);
}
