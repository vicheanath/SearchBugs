using SearchBugs.Domain;
using SearchBugs.Domain.Projects;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Projects.CreateProject;

internal sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = Project.Create(request.Name, request.Description);
        //TODO: add user to project
        await _projectRepository.Add(project.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
