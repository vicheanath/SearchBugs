using Shared.Messaging;

namespace SearchBugs.Application.BugTracking.GetBugs;

public record GetBugsQuery(Guid ProjectId) : IQuery<List<BugsResponse>>;