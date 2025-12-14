namespace Poc.App.Services;

public interface IEncryptService
{
    string Encrypt(string input);
    string Decrypt(string input);
}