using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SearchBugs.Infrastructure.Options;

public class GitOptionsSetup : IConfigureOptions<GitOptions>
{
    private const string GitOptions = "GitOptions";
    private readonly IConfiguration _configuration;

    public GitOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(GitOptions options)
    {
        _configuration.GetSection(GitOptions).Bind(options);
    }
}
