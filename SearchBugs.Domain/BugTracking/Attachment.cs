namespace SearchBugs.Domain.BugTracking;

public class Attachment
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
    public BugId BugId { get; set; }
    public Bug Bug { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    private Attachment(Guid id, string fileName, string contentType, byte[] content, BugId bugId)
    {
        Id = id;
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
        var id = Guid.NewGuid();
        return new Attachment(id, fileName, contentType, content, bugId);
    }
}
