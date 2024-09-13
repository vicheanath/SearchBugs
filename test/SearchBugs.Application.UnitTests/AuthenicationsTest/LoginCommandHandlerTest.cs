using FluentAssertions;
using Moq;
using SearchBugs.Application.Authentications.Login;
using SearchBugs.Domain.Services;
using SearchBugs.Domain.Users;
using Shared.Results;

namespace SearchBugs.Application.UnitTests.AuthenicationsTest;

public class LoginCommandHandlerTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IJwtProvider> _jwtProvider;
    private readonly Mock<IPasswordHashingService> _hasher;
    private readonly LoginCommandHandler _sut;

    public LoginCommandHandlerTest()
    {
        _userRepository = new();
        _jwtProvider = new();
        _hasher = new();
        _sut = new LoginCommandHandler(_userRepository.Object, _jwtProvider.Object, _hasher.Object);
    }

    [Fact]
    public async Task Handle_WhenUserNotFound_ShouldReturnFailure_WithNotFoundByEmailError()
    {
        // Arrange
        var email = "teat@email.com";
        var password = "password";
        var command = new LoginCommand(email, password);
        _userRepository.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<User>(UserErrors.NotFoundByEmail(email)));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.NotFoundByEmail(email));
    }

    [Fact]
    public async Task Handle_WhenPasswordIsInvalid_ShouldReturnFailure_WithInvalidPasswordError()
    {
        // Arrange
        var email = "test@email.com";
        var password = "password";
        var command = new LoginCommand(email, password);
        _userRepository.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(User.Create(Name.Create("First", "Last"), Email.Create(email), "hashedPassword").Value));

        _hasher.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidPassword);
    }

    [Fact]
    public async Task Handle_WhenUserFound_ShouldReturnSuccess_WithJwtToken()
    {
        // Arrange
        var email = "test@gmail.com";
        var password = "password";
        var command = new LoginCommand(email, password);
        var user = User.Create(Name.Create("First", "Last"), Email.Create(email), "hashedPassword").Value;
        _userRepository.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(user));

        _hasher.Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        _jwtProvider.Setup(x => x.GenerateJwtToken(It.IsAny<User>()))
            .Returns("jwtToken");

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
        result.Value.Token.Should().Be("jwtToken");

    }

}

