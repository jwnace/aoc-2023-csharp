namespace aoc_2023_csharp.Day06;

public static class Day06
{
    private static readonly string[] Input = File.ReadAllLines("Day06/day06.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input)
    {
        var times = ParseLineForPart1(input[0]);
        var distances = ParseLineForPart1(input[1]);
        var counts = new List<long>();

        for (var i = 0; i < times.Length; i++)
        {
            var totalTime = times[i];
            var distanceToBeat = distances[i];
            var count = CountWaysToWin(totalTime, distanceToBeat);
            counts.Add(count);
        }

        return counts.Aggregate((a, b) => a * b);
    }

    public static long Solve2(string[] input)
    {
        var totalTime = ParseLineForPart2(input[0]);
        var distanceToBeat = ParseLineForPart2(input[1]);

        return CountWaysToWin(totalTime, distanceToBeat);
    }

    private static long CountWaysToWin(long totalTime, long distanceToBeat)
    {
        var count = 0L;

        for (var speed = 0L; speed < totalTime; speed++)
        {
            var distance = speed * (totalTime - speed);

            if (distance >= distanceToBeat)
            {
                count++;
            }
        }

        return count;
    }

    private static long[] ParseLineForPart1(string line) =>
        line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToArray();

    private static long ParseLineForPart2(string line) =>
        long.Parse(line[9..].Replace(" ", ""));
}
