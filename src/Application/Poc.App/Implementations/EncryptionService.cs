
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Poc.App.Options;
using Poc.App.Services;

namespace Poc.App.Implementations;

public class EncryptionService(IOptionsSnapshot<EncryptionConfig> encryptionConfig) : IEncryptService
{
    private readonly EncryptionConfig _encryptionKey = encryptionConfig?.Value!;

    public string Decrypt(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var fullData = Convert.FromBase64String(input);
        var iv = fullData[..16];
        var cipher = fullData[16..];

        using var aes = Aes.Create();
        aes.Key = GetKey();
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipher);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }

    public string Encrypt(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;
        using var aes = Aes.Create();
        aes.Key = GetKey();
        aes.GenerateIV();

        using var ms = new MemoryStream();
        ms.Write(aes.IV, 0, aes.IV.Length);

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using var sw = new StreamWriter(cs);
        sw.Write(input);
        sw.Close();

        return Convert.ToBase64String(ms.ToArray());
    }

    private byte[] GetKey()
    {
        using var sha256 = SHA256.Create();
        var key = sha256.ComputeHash(Encoding.UTF8.GetBytes(_encryptionKey.DatabaseKey!));

        if (key.Length != 32)
            throw new InvalidOperationException("Encryption key must produce 32 bytes (AES-256)");
        return key;
    }
}