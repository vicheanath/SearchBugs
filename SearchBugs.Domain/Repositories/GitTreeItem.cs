using Shared.Primitives;

namespace SearchBugs.Domain.Repositories;

public class GitTreeItem : ValueObject
{
    public string Id { get; set; }
    public string Url { get; set; }
    public DateTime Date { get; set; }
    public string ShortMessageHtmlLink { get; set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return Url;
        yield return Date;
        yield return ShortMessageHtmlLink;
    }

    private GitTreeItem(string id, string url, DateTime date, string shortMessageHtmlLink)
    {
        Id = id;
        Url = url;
        Date = date;
        ShortMessageHtmlLink = shortMessageHtmlLink;
    }

    public static GitTreeItem Create(string id, string url, DateTime date, string shortMessageHtmlLink) =>
        new GitTreeItem(id, url, date, shortMessageHtmlLink);
}
