using FluentAssertions;
using SearchBugs.Infrastructure.Services;

namespace SearchBugs.Infrastructure.UnitTests.ServiceTest;

public class DataEncryptionServiceTest
{
    [Fact]
    public void Encrypt_WhenCalled_ReturnEncryptedString()
    {
        // Arrange
        var service = new DataEncryptionService();
        var plainText = "Hello World";
        var _32ByteKey = "XdhXLy^{8Pzs~O!Jm*MJLg^NA)4;(44m";

        // Act
        var encryptedText = service.Encrypt(plainText, _32ByteKey);

        // Assert
        encryptedText.Should().NotBe(plainText);
        encryptedText.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Decrypt_WhenCalled_ReturnDecryptedString()
    {
        // Arrange
        var service = new DataEncryptionService();
        var plainText = "Hello World";
        var _32ByteKey = "XdhXLy^{8Pzs~O!Jm*MJLg^NA)4;(44m";

        // Act
        var encryptedText = service.Encrypt(plainText, _32ByteKey);
        var decryptedText = service.Decrypt(encryptedText, _32ByteKey);

        // Assert
        decryptedText.Should().Be(plainText);
    }

    [Fact]
    public void Encrypt_WhenPlainTextIsNull_ThrowArgumentNullException()
    {
        // Arrange
        var service = new DataEncryptionService();
        var _32ByteKey = "XdhXLy^{8Pzs~O!Jm*MJLg^NA)4;(44m";

        // Act
        Action act = () => service.Encrypt(null, _32ByteKey);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'plainText')");
    }


    [Fact]
    public void Encrypt_WhenKeyIsNull_ThrowArgumentNullException()
    {
        // Arrange
        var service = new DataEncryptionService();
        var plainText = "Hello World";

        // Act
        Action act = () => service.Encrypt(plainText, null);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'key')");
    }

    [Fact]
    public void Decrypt_WhenCipherTextIsNull_ThrowArgumentNullException()
    {
        // Arrange
        var service = new DataEncryptionService();
        var _32ByteKey = "XdhXLy^{8Pzs~O!Jm*MJLg^NA)4;(44m";

        // Act
        Action act = () => service.Decrypt(null, _32ByteKey);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'cipherText')");
    }

    [Fact]
    public void Decrypt_WhenKeyIsNull_ThrowArgumentNullException()
    {
        // Arrange
        var service = new DataEncryptionService();
        var plainText = "Hello World";

        // Act
        Action act = () => service.Decrypt(plainText, null);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'key')");
    }
}
