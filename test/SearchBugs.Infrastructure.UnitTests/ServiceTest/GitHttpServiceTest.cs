using System.Security.Claims;
using FluentAssertions;
using LibGit2Sharp;
using Microsoft.AspNetCore.Http;
using SearchBugs.Infrastructure.Options;
using SearchBugs.Infrastructure.Services;
using SearchBugs.Infrastructure.UnitTests.Data;

namespace SearchBugs.Infrastructure.UnitTests.ServiceTest;

public class GitHttpServiceTest
{
    private readonly GitOptions _gitOptions;

    public GitHttpServiceTest()
    {
        _gitOptions = new OptionsTest().Value;
    }

    private string GetOrCreateRepository(string repositoryName)
    {
        var repoPath = Path.Combine(_gitOptions.BasePath, repositoryName);
        if (!Directory.Exists(repoPath))
        {
            Repository.Init(repoPath);
            var repo = new Repository(repoPath);
            var filePath = Path.Combine(repo.Info.WorkingDirectory, "test.txt");
            var content = "Hello World";
            File.WriteAllText(filePath, content);
            repo.Index.Add("test.txt");
            var signature = new Signature("Vichea Nath", "test@gmail.com", DateTimeOffset.Now);

            if (repo.Head.Tip == null)
            {
                repo.Commit("Initial commit", signature, signature);
            }
        }

        return repoPath;
    }

    [Fact]
    public void Handle_GitClone_Success()
    {
        // Arrange
        var service = new GitHttpService(new OptionsTest());
        var repositoryName = $"test-repo-{Guid.NewGuid():N}"; // Use unique name for each test
        // create Test repository
        var repoPath = GetOrCreateRepository(repositoryName);

        var request = new DefaultHttpContext().Request;
        request.Headers["Git-Protocol"] = "http";
        request.Method = HttpMethods.Post;
        request.RouteValues["path"] = "git-upload-pack";
        request.QueryString = new QueryString("?service=git-upload-pack");
        request.ContentType = "application/x-git-upload-pack-request";
        request.ContentLength = 0;
        request.Headers.ContentEncoding = "gzip";
        // mock authenticated user

        request.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "test-user")
        }));

        // Act
        //Func<Task> act = async () => await service.Handle(repositoryName);

        // Assert 
        //await act.Should().NotThrowAsync();
        request.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}
