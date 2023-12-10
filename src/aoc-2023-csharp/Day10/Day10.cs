using System.Text;

namespace aoc_2023_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = new Dictionary<(int row, int col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        var start = grid.Single(g => g.Value == 'S');

        var queue = new Queue<(int row, int col, int distance)>();
        queue.Enqueue((start.Key.row, start.Key.col, 0));

        var visited = new HashSet<(int row, int col)>();

        var maxDistance = 0;

        while (queue.Any())
        {
            var currentPosition = queue.Dequeue();
            var (row, col, distance) = currentPosition;

            if (!visited.Add((row, col)))
            {
                continue;
            }

            maxDistance = Math.Max(maxDistance, distance);

            if (grid.TryGetValue((row - 1, col), out var up) && up is '|' or '7' or 'F' or 'S')
            {
                queue.Enqueue((row - 1, col, distance + 1));
            }

            if (grid.TryGetValue((row + 1, col), out var down) && down is '|' or 'L' or 'J' or 'S')
            {
                queue.Enqueue((row + 1, col, distance + 1));
            }

            if (grid.TryGetValue((row, col - 1), out var left) && left is '-' or 'L' or 'F' or 'S')
            {
                queue.Enqueue((row, col - 1, distance + 1));
            }

            if (grid.TryGetValue((row, col + 1), out var right) && right is '-' or 'J' or '7' or 'S')
            {
                queue.Enqueue((row, col + 1, distance + 1));
            }
        }

        return maxDistance;
    }

    private static string DrawGrid(Dictionary<(int row, int col), char> grid)
    {
        var sb = new StringBuilder();
        var rows = grid.Keys.Select(k => k.row).Distinct().OrderBy(r => r).ToList();
        var cols = grid.Keys.Select(k => k.col).Distinct().OrderBy(c => c).ToList();

        foreach (var row in rows)
        {
            foreach (var col in cols)
            {
                sb.Append(grid[(row, col)]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static int Solve2(string[] input)
    {
        return 0;
    }
}
