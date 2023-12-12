using aoc_2023_csharp.Extensions;

namespace aoc_2023_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input) =>
        input.Sum(line => new Solver(line).Solve());

    public static long Solve2(string[] input) =>
        input.Sum(line => new Solver(line, unfold: true).Solve());

    private class Solver
    {
        private readonly Dictionary<(int, int, int), long> _memo = new();
        private readonly char[] _springs;
        private readonly int[] _groups;

        public Solver(string line, bool unfold = false)
        {
            var (left, right) = line.Split(' ');

            if (unfold)
            {
                left = string.Join("?", left, left, left, left, left);
                right = string.Join(",", right, right, right, right, right);
            }

            _springs = left.ToCharArray();
            _groups = right.Split(',').Select(int.Parse).ToArray();
        }

        public long Solve(int springIndex = 0, int groupIndex = 0, int currentLength = 0)
        {
            var key = (springIndex, groupIndex, currentLength);

            if (_memo.TryGetValue(key, out var value))
            {
                return value;
            }

            if (springIndex == _springs.Length)
            {
                if (groupIndex == _groups.Length && currentLength == 0)
                {
                    return 1;
                }

                if (groupIndex == _groups.Length - 1 && _groups[groupIndex] == currentLength)
                {
                    return 1;
                }

                return 0;
            }

            var result = 0L;

            if (_springs[springIndex] is '.' or '?' && currentLength == 0)
            {
                result += Solve(springIndex + 1, groupIndex, 0);
            }

            if (_springs[springIndex] is '.' or '?' &&
                currentLength > 0 &&
                groupIndex < _groups.Length &&
                _groups[groupIndex] == currentLength)
            {
                result += Solve(springIndex + 1, groupIndex + 1, 0);
            }

            if (_springs[springIndex] is '#' or '?')
            {
                result += Solve(springIndex + 1, groupIndex, currentLength + 1);
            }

            _memo[key] = result;
            return result;
        }
    }
}
