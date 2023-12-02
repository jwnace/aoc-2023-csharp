namespace aoc_2023_csharp.Day02;

public static class Day02
{
    private static readonly string[] Input = File.ReadAllLines("Day02/day02.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(IEnumerable<string> input) =>
        input.Select(Game.Parse)
            .Where(game => game.IsPossible)
            .Sum(game => game.Id);

    public static int Solve2(IEnumerable<string> input) =>
        input.Select(Game.Parse)
            .Sum(game => game.Power);
}
