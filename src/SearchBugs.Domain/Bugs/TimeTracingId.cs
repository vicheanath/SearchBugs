using Shared.Primitives;

namespace SearchBugs.Domain.Bugs;

public record TimeTracingId(Guid Value) : IEntityId
{
    public static TimeTracingId New => new(Guid.NewGuid());
}
