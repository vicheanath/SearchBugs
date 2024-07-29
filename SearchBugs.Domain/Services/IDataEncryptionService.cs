namespace SearchBugs.Domain.Services;

public interface IDataEncryptionService
{
    string Encrypt(string plainText, string key);
    string Decrypt(string cipherText, string key);
}