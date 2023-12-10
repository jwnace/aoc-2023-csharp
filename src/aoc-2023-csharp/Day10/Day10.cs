namespace aoc_2023_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var (_, maxDistance) = BuildLoop(grid);

        return maxDistance;
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);
        var (loop, _) = BuildLoop(grid);

        return CountPointsInsideLoop(loop, grid);
    }

    private static Dictionary<(int row, int col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int row, int col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }

    private static (HashSet<(int row, int col)> loop, int distance) BuildLoop(
        IReadOnlyDictionary<(int row, int col), char> grid)
    {
        var start = grid.Single(g => g.Value == 'S');
        var queue = new Queue<(int row, int col, int distance)>();
        queue.Enqueue((start.Key.row, start.Key.col, 0));

        var visited = new HashSet<(int row, int col)>();

        var maxDistance = 0;

        while (queue.Any())
        {
            var currentPosition = queue.Dequeue();
            var (row, col, distance) = currentPosition;
            var current = grid[(row, col)];

            if (!visited.Add((row, col)))
            {
                continue;
            }

            maxDistance = Math.Max(maxDistance, distance);

            if (grid.TryGetValue((row - 1, col), out var up) &&
                "|LJS".Contains(current) &&
                "|F7".Contains(up))
            {
                queue.Enqueue((row - 1, col, distance + 1));
            }

            if (grid.TryGetValue((row + 1, col), out var down) &&
                "|F7S".Contains(current) &&
                "|LJ".Contains(down))
            {
                queue.Enqueue((row + 1, col, distance + 1));
            }

            if (grid.TryGetValue((row, col - 1), out var left) &&
                "-J7S".Contains(current) &&
                "-LF".Contains(left))
            {
                queue.Enqueue((row, col - 1, distance + 1));
            }

            if (grid.TryGetValue((row, col + 1), out var right) &&
                "-LFS".Contains(current) &&
                "-J7".Contains(right))
            {
                queue.Enqueue((row, col + 1, distance + 1));
            }
        }

        return (visited, maxDistance);
    }

    private static int CountPointsInsideLoop(
        IReadOnlySet<(int row, int col)> loop,
        Dictionary<(int row, int col), char> grid)
    {
        var count = 0;
        var maxRow = grid.Keys.Max(x => x.row);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var row = 0; row <= maxRow; row++)
        {
            var inside = false;

            for (var col = 0; col <= maxCol; col++)
            {
                if (loop.Contains((row, col)))
                {
                    if ("|JL".Contains(grid[(row, col)]))
                    {
                        inside = !inside;
                    }
                }
                else
                {
                    if (inside)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
}
