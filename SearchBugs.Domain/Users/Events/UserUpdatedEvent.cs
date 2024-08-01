using Shared.Primitives;

namespace SearchBugs.Domain.Users.Events;

internal record UserUpdatedEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserId UserId,
    Name Name,
    Email Email) : DomainEvent(Id, OccurredOnUtc);