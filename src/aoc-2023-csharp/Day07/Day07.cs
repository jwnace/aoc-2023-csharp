namespace aoc_2023_csharp.Day07;

public static class Day07
{
    private static readonly string[] Input = File.ReadAllLines("Day07/day07.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(IEnumerable<string> input) => Solve(input, useJokers: false);

    public static long Solve2(IEnumerable<string> input) => Solve(input, useJokers: true);

    private static long Solve(IEnumerable<string> input, bool useJokers) =>
        input.Select(line => Hand.Parse(line, useJokers))
            .OrderBy(hand => hand.HandType)
            .ThenBy(hand => hand.Cards[0].Type)
            .ThenBy(hand => hand.Cards[1].Type)
            .ThenBy(hand => hand.Cards[2].Type)
            .ThenBy(hand => hand.Cards[3].Type)
            .ThenBy(hand => hand.Cards[4].Type)
            .Select((hand, index) => hand.Bid * (index + 1))
            .Sum();
}
