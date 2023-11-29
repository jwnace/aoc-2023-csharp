using System.Security.Cryptography;
using System.Text;

namespace aoc_2023_csharp.Extensions;

public static class StringExtensions
{
    public static string ToMd5String(this string input)
    {
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes).ToLower();
    }
}
