using System.Security.Cryptography;
using System.Text;

namespace Poc.App.Commons;

public static class Helper
{
    public static string ComputeSHA512Hash(string input)
    {
        // Convert the input string to a byte array using UTF-8 encoding
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // Compute the hash of the byte array
        byte[] hashBytes = SHA512.HashData(inputBytes);

        // Convert the hash byte array to a hexadecimal string for readability
        StringBuilder sb = new();
        foreach (byte b in hashBytes)
        {
            // "x2" formats each byte as a two-digit hexadecimal number
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    public static string Base64StringEncodeUrl(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        string base64 = Convert.ToBase64String(inputBytes);
        return base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }

    public static string Base64StringDecodeUrl(string input)
    {
        string base64 = input.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        byte[] bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }

    public static string Base64Encode(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        byte[] plainTextBytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string input)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

}