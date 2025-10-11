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

}