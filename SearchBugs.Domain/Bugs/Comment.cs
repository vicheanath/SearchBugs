using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class Comment : Entity<CommentId>, IAuditable
{
    public BugId BugId { get; set; }
    public UserId UserId { get; set; }
    public string CommentText { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    private Comment()
    {

    }

    private Comment(CommentId id, BugId bugId, UserId userId, string commentText) : base(id)
    {
        BugId = bugId;
        UserId = userId;
        CommentText = commentText;
    }

    public static Comment Create(BugId bugId, UserId userId, string commentText)
    {
        var id = new CommentId(Guid.NewGuid());
        return new Comment(id, bugId, userId, commentText);
    }
}
