using Shared.Messaging;

namespace SearchBugs.Application.Users.GetUserDetail;

public record GetUserDetailQuery(Guid UserId) : IQuery<GetUserDetailResponse>;

