using System.Security.Cryptography;
using System.Text;

namespace WargameApi.Auth;

public static class HashUtils
{
    public static string ToSha512(string sourceValue)
    {
        var bytes = Encoding.UTF8.GetBytes(sourceValue);
        var hash = SHA512.HashData(bytes);

        return Convert.ToBase64String(hash);
    }
}