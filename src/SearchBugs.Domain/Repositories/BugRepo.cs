using SearchBugs.Domain.Bugs;

namespace SearchBugs.Domain.Repositories;

public class BugRepo
{
    public BugId BugId { get; set; }
    public RepositoryId RepositoryId { get; set; }

    private BugRepo()
    {
    }

    private BugRepo(BugId bugId, RepositoryId repositoryId)
    {
        BugId = bugId;
        RepositoryId = repositoryId;
    }

    public static BugRepo Create(BugId bugId, RepositoryId repositoryId)
    {
        return new BugRepo(bugId, repositoryId);
    }

}
