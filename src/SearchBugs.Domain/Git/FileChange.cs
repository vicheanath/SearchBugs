namespace SearchBugs.Domain.Git;

public class FileChange
{
    public string FileName { get; set; }
    public string Status { get; set; }
    public int Additions { get; set; }
    public int Deletions { get; set; }

    public FileChange(string fileName, string status, int additions, int deletions)
    {
        FileName = fileName;
        Status = status;
        Additions = additions;
        Deletions = deletions;
    }
}
