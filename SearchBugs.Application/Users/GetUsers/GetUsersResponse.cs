namespace SearchBugs.Application.Users.GetUsers;

public record GetUsersResponse(string UserId,
    string FirstName,
    string LastName,
    string Email,
    string[] Role,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc);
