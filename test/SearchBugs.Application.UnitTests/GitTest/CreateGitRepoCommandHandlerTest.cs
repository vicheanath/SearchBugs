using Moq;
using SearchBugs.Application.Git.CreateGitRepo;
using SearchBugs.Domain;
using SearchBugs.Domain.Git;
using SearchBugs.Domain.Projects;

namespace SearchBugs.Application.UnitTests.GitTest;

public class CreateGitRepoCommandHandlerTest
{
    private readonly Mock<IGitRepository> _gitRepository;
    private readonly Mock<IGitHttpService> _gitService;
    private readonly Mock<IProjectRepository> _projectRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateGitRepoCommandHandler _sut;

    public CreateGitRepoCommandHandlerTest()
    {
        _gitRepository = new();
        _gitService = new();
        _projectRepository = new();
        _unitOfWork = new();
    }

    //[Theory]

}
