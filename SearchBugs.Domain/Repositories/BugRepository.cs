using SearchBugs.Domain.Bugs;

namespace SearchBugs.Domain.Repositories;

public class BugRepository
{
    public BugId BugId { get; set; }
    public RepositoryId RepositoryId { get; set; }

    private BugRepository()
    {
    }

    private BugRepository(BugId bugId, RepositoryId repositoryId)
    {
        BugId = bugId;
        RepositoryId = repositoryId;
    }

    public static BugRepository Create(BugId bugId, RepositoryId repositoryId)
    {
        return new BugRepository(bugId, repositoryId);
    }

}
