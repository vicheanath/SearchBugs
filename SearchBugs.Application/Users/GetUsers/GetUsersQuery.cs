using Shared.Messaging;

namespace SearchBugs.Application.Users.GetUsers;

public record GetUsersQuery() : IQuery<List<GetUsersResponse>>;

