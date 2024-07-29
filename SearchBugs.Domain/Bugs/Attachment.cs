using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public class Attachment : Entity<AttachmentId>, IAuditable
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
    public BugId BugId { get; set; }
    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    private Attachment(AttachmentId id, string fileName, string contentType, byte[] content, BugId bugId) : base(id)
    {
        FileName = fileName;
        ContentType = contentType;
        Content = content;
        BugId = bugId;
    }

    private Attachment()
    {

    }

    public static Attachment Create(string fileName, string contentType, byte[] content, BugId bugId)
    {
        var id = new AttachmentId(Guid.NewGuid());
        return new Attachment(id, fileName, contentType, content, bugId);
    }
}
