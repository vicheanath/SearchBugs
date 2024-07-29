using Bogus;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;
using Shared.Results;

namespace SearchBugs.Domain.UnitTests.Data.Bugs;

internal class CreateBug : TheoryData<Bug>
{
    public CreateBug()
    {
        var faker = new Faker();

        var status = BugStatus.GetValues();
        var priority = BugPriority.GetValues();

        foreach (var item in status)
        {
            foreach (var item2 in priority)
            {
                Result<Bug> bug = Bug.Create(
                    faker.Lorem.Sentence(),
                    faker.Lorem.Paragraph(),
                    item,
                    item2,
                    faker.Lorem.Sentence(),
                    new ProjectId(faker.Random.Guid()),
                    new UserId(faker.Random.Guid()),
                    new UserId(faker.Random.Guid())
                    );


                Add(bug.Value);
            }
        }
    }


}
