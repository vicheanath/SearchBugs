using FluentAssertions;
using Moq;
using SearchBugs.Application.BugTracking;
using SearchBugs.Application.BugTracking.Create;
using SearchBugs.Domain;
using SearchBugs.Domain.Bugs;
using Shared.Results;

namespace SearchBugs.Application.UnitTests.BugTrackingTest;

public class CreateBugCommandHandlerTest
{

    private readonly Mock<IBugRepository> _bugRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreateBugCommandHandler _sut;


    public CreateBugCommandHandlerTest()
    {
        _bugRepository = new();
        _unitOfWork = new();
        _sut = new CreateBugCommandHandler(_bugRepository.Object, _unitOfWork.Object);
    }

    [Theory]
    [ClassData(typeof(CreateBugCommandHandlerTestData))]
    public async Task Handle_WhenBugStatusIsSuccess_ShouldReturnSuccess(CreateBugCommand command)
    {
        // Arrange
        _bugRepository.Setup(x => x.Add(It.IsAny<Bug>())).Returns(Task<Result>.FromResult(Result.Success()));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(CreateBugCommandHandlerTestData.InValidBugStatus), MemberType = typeof(CreateBugCommandHandlerTestData))]
    public async Task Handle_WhenBugStatusIsInvalid_ShouldReturnFailure(CreateBugCommand command)
    {
        // Arrange
        _bugRepository.Setup(x => x.Add(It.IsAny<Bug>())).Returns(Task<Result>.FromResult(Result.Success()));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().Be(BugValidationErrors.InvalidBugStatus);
        result.IsSuccess.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(CreateBugCommandHandlerTestData.InValidBugPriority), MemberType = typeof(CreateBugCommandHandlerTestData))]
    public async Task Handle_WhenBugPriorityIsInvalid_ShouldReturnFailure(CreateBugCommand command)
    {
        // Arrange
        _bugRepository.Setup(x => x.Add(It.IsAny<Bug>())).Returns(Task<Result>.FromResult(Result.Success()));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Error.Should().Be(BugValidationErrors.InvalidBugPriority);
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenBugDataIsValid_ShouldReturnSuccess()
    {
        // Arrange
        var command = new CreateBugCommand(
            Title: "Title",
            Description: "Description",
            Status: BugStatus.Open.Name,
            Priority: BugPriority.High.Name,
            Severity: BugSeverity.Low.Name,
            ProjectId: Guid.NewGuid(),
            AssigneeId: Guid.NewGuid(),
            ReporterId: Guid.NewGuid()
        );

        _bugRepository.Setup(x => x.Add(It.IsAny<Bug>())).Returns(Task<Result>.FromResult(Result.Success()));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWork.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        result.IsSuccess.Should().BeTrue();
    }

}
