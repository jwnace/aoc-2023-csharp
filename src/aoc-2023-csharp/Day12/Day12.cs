using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input)
    {
        var total = 0L;

        foreach (var line in input)
        {
            var (left, right) = line.Split(' ');

            var springs = left.ToCharArray();
            var groups = right.Split(',').Select(int.Parse).ToArray();

            total += new Solver().Solve(springs, groups, 0, 0, 0);
        }

        return total;
    }

    public static long Solve2(string[] input)
    {
        var total = 0L;

        foreach (var line in input)
        {
            var (left, right) = line.Split(' ');

            left = string.Join('?', left, left, left, left, left);
            right = string.Join(',', right, right, right, right, right);

            var springs = left.ToCharArray();
            var groups = right.Split(',').Select(int.Parse).ToArray();

            total += new Solver().Solve(springs, groups, 0, 0, 0);
        }

        return total;
    }

    private class Solver
    {
        private readonly Dictionary<(int, int, int), long> _memo = new();

        public long Solve(char[] springs, int[] groups, int springIndex, int groupIndex, int groupSize)
        {
            var key = (springIndex, groupIndex, groupSize);

            if (_memo.TryGetValue(key, out var value))
            {
                return value;
            }

            if (springIndex == springs.Length)
            {
                if (groupIndex == groups.Length && groupSize == 0)
                {
                    return 1;
                }

                if (groupIndex == groups.Length - 1 && groups[groupIndex] == groupSize)
                {
                    return 1;
                }

                return 0;
            }

            long result = 0;

            foreach (var c in ".#")
            {
                if (springs[springIndex] != c && springs[springIndex] != '?')
                {
                    continue;
                }

                switch (c)
                {
                    case '.' when groupSize == 0:
                    {
                        result += Solve(springs, groups, springIndex + 1, groupIndex, 0);
                        break;
                    }
                    case '.' when groupSize > 0 && groupIndex < groups.Length && groups[groupIndex] == groupSize:
                    {
                        result += Solve(springs, groups, springIndex + 1, groupIndex + 1, 0);
                        break;
                    }
                    case '#':
                    {
                        result += Solve(springs, groups, springIndex + 1, groupIndex, groupSize + 1);
                        break;
                    }
                }
            }

            _memo[key] = result;
            return result;
        }
    }
}
