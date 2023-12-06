namespace aoc_2023_csharp.Day06;

public static class Day06
{
    private static readonly string Input = File.ReadAllText("Day06/day06.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static int Solve1(string input)
    {
        var lines = input.Split("\n");
        var times = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var distances = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

        var counts = new List<int>();

        for (var i = 0; i < times.Length; i++)
        {
            var totalTime = times[i];
            var distanceToBeat = distances[i];

            var count = 0;

            for (var speed = 0; speed < totalTime; speed++)
            {
                var distance = speed * (totalTime - speed);

                if (distance >= distanceToBeat)
                {
                    count++;
                }
            }

            counts.Add(count);
        }

        return counts.Aggregate((a, b) => a * b);
    }

    public static long Solve2(string input)
    {
        var lines = input.Split("\n");
        var totalTime = long.Parse(lines[0][9..].Replace(" ", ""));
        var distanceToBeat = long.Parse(lines[1][9..].Replace(" ", ""));

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
}
