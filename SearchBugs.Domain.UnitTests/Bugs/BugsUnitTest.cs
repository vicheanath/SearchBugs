using FluentAssertions;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.UnitTests.Data.Bugs;

namespace SearchBugs.Domain.UnitTests.Bugs;

public class BugsUnitTest
{

    [Theory]
    [ClassData(typeof(CreateBug))]
    public void CreateBug(Bug bug)
    {
        var bugTest = Bug.Create(
            bug.Title,
            bug.Description,
            bug.StatusId,
            bug.PriorityId,
            bug.Severity,
            bug.ProjectId,
            bug.AssigneeId,
            bug.ReporterId

        );

        bug.Title.Should().Be(bugTest.Value.Title);
        bug.Description.Should().Be(bugTest.Value.Description);
        bug.StatusId.Should().Be(bugTest.Value.StatusId);
        bug.PriorityId.Should().Be(bugTest.Value.PriorityId);
        bug.Severity.Should().Be(bugTest.Value.Severity);
        bug.ProjectId.Should().Be(bugTest.Value.ProjectId);
        bug.ReporterId.Should().Be(bugTest.Value.ReporterId);
        bug.AssigneeId.Should().Be(bugTest.Value.AssigneeId);

    }
}
