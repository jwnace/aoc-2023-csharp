using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day19;

public record Part(int X, int M, int A, int S)
{
    public static Part Parse(string text)
    {
        var (x, m, a, s) = text[1..^1].Split(',').Select(x => int.Parse(x[2..])).ToArray();
        return new Part(x, m, a, s);
    }
}
