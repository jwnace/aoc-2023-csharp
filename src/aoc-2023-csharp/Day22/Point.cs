using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day22;

public record Point(int X, int Y, int Z)
{
    public static Point Parse(string text)
    {
        var (x, y, z) = text.Split(',').Select(int.Parse).ToArray();
        return new Point(x, y, z);
    }
}
