using System.ComponentModel.DataAnnotations;

namespace SearchBugs.Infrastructure.Authentication;

public class JwtOptions
{
    [Required]
    public string Issuer { get; set; }
    [Required]
    public string Audience { get; set; }
    [Required]
    [MinLength(16)]
    public string Secret { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int ExpiryMinutes { get; set; }
}
