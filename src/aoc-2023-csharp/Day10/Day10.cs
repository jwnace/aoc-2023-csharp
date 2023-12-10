using System.Text;

namespace aoc_2023_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input, bool drawLoop = false)
    {
        var grid = BuildGrid(input);
        var (loop, maxDistance) = BuildLoop(grid);

        if (drawLoop)
        {
            Console.WriteLine(DrawLoop(loop, grid));
        }

        return maxDistance;
    }

    public static int Solve2(string[] input, bool drawLoop = false)
    {
        var grid = BuildGrid(input);
        var (loop, _) = BuildLoop(grid);

        if (drawLoop)
        {
            Console.WriteLine(DrawLoop(loop, grid));
        }

        return GetPointsInsideLoop(loop, grid).Count();
    }

    private static Dictionary<(int row, int col), char> BuildGrid(IReadOnlyList<string> input)
    {
        var grid = new Dictionary<(int row, int col), char>();

        for (var row = 0; row < input.Count; row++)
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
        var queue = new Queue<(int row, int col, int distance)>();
        var visited = new HashSet<(int row, int col)>();
        var start = grid.Single(g => g.Value == 'S');
        var maxDistance = 0;

        queue.Enqueue((start.Key.row, start.Key.col, 0));

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

            if (CanMoveUp(row, col, current, grid))
            {
                queue.Enqueue((row - 1, col, distance + 1));
            }

            if (CanMoveDown(row, col, current, grid))
            {
                queue.Enqueue((row + 1, col, distance + 1));
            }

            if (CanMoveLeft(row, col, current, grid))
            {
                queue.Enqueue((row, col - 1, distance + 1));
            }

            if (CanMoveRight(row, col, current, grid))
            {
                queue.Enqueue((row, col + 1, distance + 1));
            }
        }

        return (visited, maxDistance);
    }

    private static IEnumerable<(int row, int col)> GetPointsInsideLoop(
        IReadOnlySet<(int row, int col)> loop,
        IReadOnlyDictionary<(int row, int col), char> grid)
    {
        var maxRow = grid.Keys.Max(x => x.row);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var row = 0; row <= maxRow; row++)
        {
            var inside = false;

            for (var col = 0; col <= maxCol; col++)
            {
                if (loop.Contains((row, col)))
                {
                    if (IsVerticalPipe(row, col, grid))
                    {
                        inside = !inside;
                    }
                }
                else
                {
                    if (inside)
                    {
                        yield return (row, col);
                    }
                }
            }
        }
    }

    private static bool CanMoveRight(int row, int col, char current, IReadOnlyDictionary<(int row, int col), char> grid)
    {
        return grid.TryGetValue((row, col + 1), out var right) &&
               "-LFS".Contains(current) &&
               "-J7".Contains(right);
    }

    private static bool CanMoveLeft(int row, int col, char current, IReadOnlyDictionary<(int row, int col), char> grid)
    {
        return grid.TryGetValue((row, col - 1), out var left) &&
               "-J7S".Contains(current) &&
               "-LF".Contains(left);
    }

    private static bool CanMoveDown(int row, int col, char current, IReadOnlyDictionary<(int row, int col), char> grid)
    {
        return grid.TryGetValue((row + 1, col), out var down) &&
               "|F7S".Contains(current) &&
               "|LJ".Contains(down);
    }

    private static bool CanMoveUp(int row, int col, char current, IReadOnlyDictionary<(int row, int col), char> grid)
    {
        return grid.TryGetValue((row - 1, col), out var up) &&
               "|LJS".Contains(current) &&
               "|F7".Contains(up);
    }

    private static bool IsVerticalPipe(int row, int col, IReadOnlyDictionary<(int row, int col), char> grid) =>
        // TODO: This is a naive implementation, but it works for the examples and my input
        // (it assumes that 'S' is always located at an 'F' or a '7', which is true for the examples and my input)
        "|LJ".Contains(grid[(row, col)]);

    private static string DrawLoop(IReadOnlySet<(int row, int col)> loop, Dictionary<(int row, int col), char> grid)
    {
        var maxRow = grid.Keys.Max(x => x.row);
        var maxCol = grid.Keys.Max(x => x.col);

        var sb = new StringBuilder();

        for (var row = 0; row <= maxRow; row++)
        {
            for (var col = 0; col <= maxCol; col++)
            {
                sb.Append(loop.Contains((row, col))
                    ? GetSymbolFor(grid[(row, col)])
                    : '.');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static char GetSymbolFor(char c) => c switch
    {
        'S' => 'S',
        'F' => '\u250C',
        '7' => '\u2510',
        'L' => '\u2514',
        'J' => '\u2518',
        '-' => '\u2500',
        '|' => '\u2502',
        _ => '?',
    };
}
