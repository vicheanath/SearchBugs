namespace SearchBugs.Application.Users.GetUserDetail;

public sealed record GetUserDetailResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string[]? Roles,
    DateTime? CreatedOnUtc,
    DateTime? ModifiedOnUtc);

