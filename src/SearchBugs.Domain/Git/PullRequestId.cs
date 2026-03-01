using Shared.Primitives;

namespace SearchBugs.Domain.Git;

public sealed record PullRequestId(Guid Value) : IEntityId;
