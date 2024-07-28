using SearchBugs.Domain.Users;
using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class Comment : Entity<CommentId>
{
    public BugId BugId { get; set; }
    public UserId UserId { get; set; }
    public string CommentText { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    private Comment()
    {

    }

    private Comment(CommentId id, BugId bugId, UserId userId, string commentText, DateTime createdAt, DateTime updatedAt) : base(id)
    {
        BugId = bugId;
        UserId = userId;
        CommentText = commentText;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Comment Create(BugId bugId, UserId userId, string commentText, DateTime createdAt, DateTime updatedAt)
    {
        var id = new CommentId(Guid.NewGuid());
        return new Comment(id, bugId, userId, commentText, createdAt, updatedAt);
    }
}
