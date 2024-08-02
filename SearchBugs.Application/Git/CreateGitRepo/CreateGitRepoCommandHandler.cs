using SearchBugs.Domain;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Repositories;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.CreateGitRepo;

public sealed class CreateGitRepoCommandHandler : ICommandHandler<CreateGitRepoCommand>
{
    private readonly IGitService _gitRepoService;
    private readonly IGitRepository _gitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;

    public CreateGitRepoCommandHandler(IGitService gitService, IGitRepository gitRepository, IUnitOfWork unitOfWork, IProjectRepository projectRepository)
    {
        _gitRepoService = gitService;
        _gitRepository = gitRepository;
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
    }
    public async Task<Result> Handle(CreateGitRepoCommand request, CancellationToken cancellationToken)
    {
        var projectId = new ProjectId(request.ProjectId);
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);
        var repo = Repository.Create(request.Name, request.Description, request.Url, projectId);

        if (project.IsFailure)
            return Result.Failure(project.Error);
        _gitRepoService.CreateRepository(repo.Name);
        await _gitRepository.Add(repo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}