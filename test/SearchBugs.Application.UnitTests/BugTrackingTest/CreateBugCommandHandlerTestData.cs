using Bogus;
using SearchBugs.Application.BugTracking.Create;
using SearchBugs.Domain.Bugs;
namespace SearchBugs.Application.UnitTests.BugTrackingTest;

internal class CreateBugCommandHandlerTestData : TheoryData<CreateBugCommand>
{
    public CreateBugCommandHandlerTestData()
    {
        var faker = new Faker();
        string[] status = BugStatus.GetValues().Select(x => x.Name).ToArray();
        string[] priority = BugPriority.GetValues().Select(x => x.Name).ToArray();
        Add(new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
           faker.PickRandom(BugSeverity.GetValues().Select(x => x.Name).ToArray()),
           faker.Random.Guid(),
           faker.Random.Guid(),
           faker.Random.Guid()));
    }

    public static IEnumerable<object[]> InValidBugStatus()
    {
        var faker = new Faker();
        string[] status = new string[] { "Invalid" };
        string[] priority = BugPriority.GetValues().Select(x => x.Name).ToArray();
        yield return new object[] { new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
           faker.PickRandom(BugSeverity.GetValues().Select(x => x.Name).ToArray()),
           faker.Random.Guid(),
           faker.Random.Guid(),
           faker.Random.Guid()) };
    }

    public static IEnumerable<object[]> InValidBugPriority()
    {
        var faker = new Faker();
        string[] status = BugStatus.GetValues().Select(x => x.Name).ToArray();
        string[] priority = new string[] { "Invalid" };
        yield return new object[] { new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
           faker.PickRandom(BugSeverity.GetValues().Select(x => x.Name).ToArray()),
           faker.Random.Guid(),
           faker.Random.Guid(),
           faker.Random.Guid()) };
    }

    public static IEnumerable<object[]> InValidBugSeverity()
    {
        var faker = new Faker();
        string[] status = BugStatus.GetValues().Select(x => x.Name).ToArray();
        string[] priority = BugPriority.GetValues().Select(x => x.Name).ToArray();
        yield return new object[] { new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
            "Invalid",
           faker.Random.Guid(),
           faker.Random.Guid(),
           faker.Random.Guid()) };
    }

    public static IEnumerable<object[]> InValidProjectId()
    {
        var faker = new Faker();
        string[] status = BugStatus.GetValues().Select(x => x.Name).ToArray();
        string[] priority = BugPriority.GetValues().Select(x => x.Name).ToArray();
        yield return new object[] { new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
           faker.PickRandom(BugSeverity.GetValues().Select(x => x.Name).ToArray()),
           Guid.Empty,
           faker.Random.Guid(),
           faker.Random.Guid()) };
    }

    public static IEnumerable<object[]> InValidAssigneeId()
    {
        var faker = new Faker();
        string[] status = BugStatus.GetValues().Select(x => x.Name).ToArray();
        string[] priority = BugPriority.GetValues().Select(x => x.Name).ToArray();
        yield return new object[] { new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
           faker.PickRandom(BugSeverity.GetValues().Select(x => x.Name).ToArray()),
           faker.Random.Guid(),
           Guid.Empty,
           faker.Random.Guid()) };
    }

    public static IEnumerable<object[]> InValidReporterId()
    {
        var faker = new Faker();
        string[] status = BugStatus.GetValues().Select(x => x.Name).ToArray();
        string[] priority = BugPriority.GetValues().Select(x => x.Name).ToArray();
        yield return new object[] { new CreateBugCommand(faker.Lorem.Sentence(),
           faker.Lorem.Paragraph(),
           faker.PickRandom(status),
           faker.PickRandom(priority),
           faker.PickRandom(BugSeverity.GetValues().Select(x => x.Name).ToArray()),
           faker.Random.Guid(),
           faker.Random.Guid(),
           Guid.Empty) };
    }
}
