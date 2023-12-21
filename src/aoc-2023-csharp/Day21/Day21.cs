using System.Text;

namespace aoc_2023_csharp.Day21;

public static class Day21
{
    private static readonly string[] Input = File.ReadAllLines("Day21/day21.txt").ToArray();

    public static int Part1() => Solve1(Input, 64);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input, int goal)
    {
        // The Elf starts at the starting position (S) which also counts as a garden plot.
        var (grid, start) = ParseInput(input);

        // Console.WriteLine(DrawGrid(grid, start));

        var queue = new Queue<(int row, int col, int steps)>();
        queue.Enqueue((start.row, start.col, 0));

        var seen = new HashSet<(int row, int col, int steps)>();

        var destinations = new HashSet<(int row, int col)>();

        while (queue.Any())
        {
            // Console.WriteLine($"Queue: {queue.Count}, Seen: {seen.Count}, Destinations: {destinations.Count}");

            var current = queue.Dequeue();

            if (seen.Contains(current))
            {
                continue;
            }

            seen.Add(current);

            var (row, col, steps) = current;
            var position = (row, col);

            if (current.steps > goal)
            {
                throw new Exception($"Too many steps ({current.steps})");
            }

            if (current.steps == goal)
            {
                // if (destinations.Contains((row, col)))
                // {
                //     throw new Exception($"Already visited ({row},{col})");
                // }

                destinations.Add((row, col));
                continue;
            }

            var neighbors = new (int row, int col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                if (grid.Contains(neighbor))
                {
                    queue.Enqueue((neighbor.row, neighbor.col, steps + 1));
                }
            }
        }

        return destinations.Count;
    }

    private static (HashSet<(int row, int col)> grid, (int row, int col) start) ParseInput(string[] input)
    {
        var grid = new HashSet<(int row, int col)>();
        var start = (0, 0);

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] is 'S')
                {
                    start = (row, col);
                }

                if (input[row][col] is '.' or 'S')
                {
                    grid.Add((row, col));
                }
            }
        }

        return (grid, start);
    }

    private static string DrawGrid(HashSet<(int row, int col)> grid, (int, int) start)
    {
        var sb = new StringBuilder();

        var minRow = grid.Min(x => x.row);
        var maxRow = grid.Max(x => x.row);
        var minCol = grid.Min(x => x.col);
        var maxCol = grid.Max(x => x.col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                if ((row, col) == start)
                {
                    sb.Append('S');
                }
                else
                {
                    sb.Append(grid.Contains((row, col)) ? '.' : '#');
                }
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
