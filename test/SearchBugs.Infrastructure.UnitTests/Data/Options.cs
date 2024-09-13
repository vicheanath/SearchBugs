using Microsoft.Extensions.Options;
using SearchBugs.Infrastructure.Options;

namespace SearchBugs.Infrastructure.UnitTests.Data;

public class OptionsTest : IOptions<GitOptions>, IDisposable
{
    public GitOptions Value => new GitOptions
    {
        BasePath = Path.Combine(Directory.GetCurrentDirectory(), "Repositories")
    };

    public OptionsTest()
    {
        if (!Directory.Exists(Value.BasePath))
        {
            Directory.CreateDirectory(Value.BasePath);
        }
    }

    public void Dispose()
    {
        if (Directory.Exists(Value.BasePath))
        {
            Directory.Delete(Value.BasePath, true);
        }
    }
}
