using Bogus;
using SearchBugs.Application.Git.CreateGitRepo;

namespace SearchBugs.Application.UnitTests.GitTest;

internal class CreateGitRepoCommandHandlerData : TheoryData<CreateGitRepoCommand>
{
    public CreateGitRepoCommandHandlerData()
    {
        var faker = new Faker();
        Add(new CreateGitRepoCommand(faker.Lorem.Sentence(), faker.Lorem.Paragraph(), faker.Internet.Url(), faker.Random.Guid()));
    }
}
