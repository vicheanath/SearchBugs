using FluentAssertions;
using LibGit2Sharp;
using SearchBugs.Domain.Git;
using SearchBugs.Infrastructure.Options;
using SearchBugs.Infrastructure.Services;
using SearchBugs.Infrastructure.UnitTests.Data;

namespace SearchBugs.Infrastructure.UnitTests.ServiceTest;

public class GitRepositoryServiceTest
{
    private readonly GitOptions _gitOptions;

    public GitRepositoryServiceTest()
    {
        _gitOptions = new OptionsTest().Value;
    }

    private string GetSetupNewRepository(string repositoryName)
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

            repo.Commit("Initial commit", signature, signature);
        }

        return repoPath;
    }

    [Fact]
    public void ListTree_WhenCalled_ReturnGitTreeItems()
    {
        // Arrange
        var service = new GitRepositoryService(new OptionsTest());
        var repositoryName = "test-repo";
        var filePath = "test.txt";
        var repoPath = GetSetupNewRepository(repositoryName);
        var commitSha = new Repository(repoPath).Commits.First().Sha;

        // Act
        var result = service.ListTree(commitSha, repositoryName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().HaveCount(1);
        result.Value.First().Path.Should().Be(filePath);
        result.Value.First().Name.Should().Be("test.txt");
        result.Value.First().Type.Should().Be("Blob");
    }

    [Fact]
    public void ListTree_WhenCalledWithInvalidRepositoryName_ReturnError()
    {
        // Arrange
        var service = new GitRepositoryService(new OptionsTest());

        var repositoryName = "invalid-repo";
        var commitSha = "invalid-commit-sha";
        var repo = GetSetupNewRepository("test-repo");


        // Act
        var result = service.ListTree(commitSha, repositoryName);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(GitErrors.RepositoryNotFound);
    }

    [Fact]
    public void GetFileContent_WhenCalled_ReturnFileContent()
    {
        // Arrange
        var service = new GitRepositoryService(new OptionsTest());
        var repositoryName = "test-repo";
        var repoPath = GetSetupNewRepository(repositoryName);
        var commitSha = new Repository(repoPath).Commits.First().Sha;
        var content = "Hello World";

        // Act
        var result = service.GetFileContent(repositoryName, commitSha, "test.txt");


        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(content);
    }
}
