namespace aoc_2023_csharp.Day15;

public static class Day15
{
    private static readonly string Input = File.ReadAllText("Day15/day15.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string input) => input.Split(",").Select(ComputeHash).Sum();

    public static int Solve2(string input)
    {
        return 0;
    }

    private static int ComputeHash(string input)
    {
        var result = 0;

        foreach (var c in input)
        {
            var ascii = (int)c;
            result += ascii;
            result *= 17;
            result %= 256;
        }

        return result;
    }
}
