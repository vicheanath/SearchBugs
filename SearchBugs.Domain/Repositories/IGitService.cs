namespace SearchBugs.Domain.Repositories;

public interface IGitService
{
    Task Handle(string repositoryName, CancellationToken cancellationToken = default);
}
