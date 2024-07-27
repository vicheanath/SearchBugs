using SearchBugs.Domain.BugTracking;
using Shared.Primitives;

namespace SearchBugs.Domain.Repository;

public sealed record BugRepoId(Guid Value) : IEntityId;

public class BugRepository : Entity<BugRepoId>
{
    public Guid Id { get; private set; }
    public BugId BugId { get; set; }
    public RepoId RepoId { get; set; }

    public Repository Repository { get; set; } = null;
    public Bug Bug { get; set; } = null;

    private BugRepository()
    {
    }

    private BugRepository(BugRepoId Id, BugId bugId, RepoId repoId)
        : base(Id)
    {
        BugId = bugId;
        RepoId = repoId;
    }

    public static BugRepository Create(BugId bugId, RepoId repoId)
    {
        var id = new BugRepoId(Guid.NewGuid());
        return new BugRepository(id, bugId, repoId);
    }

}
