using System.ComponentModel.DataAnnotations;

namespace SearchBugs.Infrastructure.Options;

public class GitOptions
{
    [Required]
    public string BasePath { get; set; }
}
