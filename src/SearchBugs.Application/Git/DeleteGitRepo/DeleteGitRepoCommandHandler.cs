using SearchBugs.Domain;
using SearchBugs.Domain.Repositories;
using Shared.Messaging;
using Shared.Results;

namespace SearchBugs.Application.Git.DeleteGitRepo;

public sealed class DeleteGitRepoCommandHandler : ICommandHandler<DeleteGitRepoCommand>
{
    private readonly IGitRepository _gitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGitRepoCommandHandler(IGitRepository gitRepository, IUnitOfWork unitOfWork)
    {
        _gitRepository = gitRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteGitRepoCommand request, CancellationToken cancellationToken)
    {
        var gitRepo = await _gitRepository.GetByUrlAsync(request.Url, cancellationToken);

        if (gitRepo.IsFailure)
        {
            return Result.Failure(GitValidationErrors.GitRepoNotFound);
        }

        await _gitRepository.Remove(gitRepo.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
