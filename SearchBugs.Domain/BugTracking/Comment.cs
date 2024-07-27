using SearchBugs.Domain.User;

namespace SearchBugs.Domain.BugTracking;

public class Comment
{
    public Guid Id { get; set; }
    public BugId BugId { get; set; }
    public UserId UserId { get; set; }
    public string CommentText { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    private Comment()
    {

    }

    private Comment(Guid id, BugId bugId, UserId userId, string commentText, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        BugId = bugId;
        UserId = userId;
        CommentText = commentText;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Comment Create(BugId bugId, UserId userId, string commentText, DateTime createdAt, DateTime updatedAt)
    {
        var id = Guid.NewGuid();
        return new Comment(id, bugId, userId, commentText, createdAt, updatedAt);
    }

}
