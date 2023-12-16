using System.Collections;
using System.Data;
using System.Text;

namespace aoc_2023_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var energizedTiles = new HashSet<(int row, int col)>();
        var queue = new Queue<(int row, int col, Direction direction)>();
        queue.Enqueue((0, 0, Direction.Right));

        var seen = new HashSet<(int row, int col, Direction direction)>();

        while (queue.Any())
        {
            var (row, col, direction) = queue.Dequeue();

            if (!seen.Add((row, col, direction)))
            {
                continue;
            }

            if (row < 0 || row >= input.Length || col < 0 || col >= input[row].Length)
            {
                continue;
            }

            energizedTiles.Add((row, col));

            if (grid.TryGetValue((row, col), out var value))
            {
                switch (value)
                {
                    case '.':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                        }

                        break;
                    }
                    case '|':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                        }

                        break;
                    }
                    case '-':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                        }

                        break;
                    }
                    case '/':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                        }

                        break;
                    }
                    case '\\':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }

        Console.WriteLine(DrawGrid(grid, energizedTiles));

        return energizedTiles.Count;
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);

        var maxRow = grid.Keys.Max(x => x.row);
        var maxCol = grid.Keys.Max(x => x.col);

        var max = 0;

        // left edge
        for (var row = 0; row <= maxRow; row++)
        {
            max = Math.Max(max, CountEnergizedTiles(grid, row, 0, Direction.Right));
        }

        // right edge
        for (var row = 0; row <= maxRow; row++)
        {
            max = Math.Max(max, CountEnergizedTiles(grid, row, maxCol, Direction.Left));
        }

        // top edge
        for (var col = 0; col <= maxCol; col++)
        {
            max = Math.Max(max, CountEnergizedTiles(grid, 0, col, Direction.Down));
        }

        // bottom edge
        for (var col = 0; col <= maxCol; col++)
        {
            max = Math.Max(max, CountEnergizedTiles(grid, maxRow, col, Direction.Up));
        }

        return max;
    }

    private static int CountEnergizedTiles(
        Dictionary<(int row, int col), char> grid,
        int startRow,
        int startCol,
        Direction startDirection)
    {
        var maxRow = grid.Keys.Max(x => x.row);
        var maxCol = grid.Keys.Max(x => x.col);

        var energizedTiles = new HashSet<(int row, int col)>();
        var queue = new Queue<(int row, int col, Direction direction)>();
        queue.Enqueue((startRow, startCol, startDirection));

        var seen = new HashSet<(int row, int col, Direction direction)>();

        while (queue.Any())
        {
            var (row, col, direction) = queue.Dequeue();

            if (!seen.Add((row, col, direction)))
            {
                continue;
            }

            if (row < 0 || row > maxRow || col < 0 || col > maxCol)
            {
                continue;
            }

            energizedTiles.Add((row, col));

            if (grid.TryGetValue((row, col), out var value))
            {
                switch (value)
                {
                    case '.':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                        }

                        break;
                    }
                    case '|':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                        }

                        break;
                    }
                    case '-':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                        }

                        break;
                    }
                    case '/':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                        }

                        break;
                    }
                    case '\\':
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                            {
                                queue.Enqueue((row, col - 1, Direction.Left));
                                break;
                            }
                            case Direction.Down:
                            {
                                queue.Enqueue((row, col + 1, Direction.Right));
                                break;
                            }
                            case Direction.Left:
                            {
                                queue.Enqueue((row - 1, col, Direction.Up));
                                break;
                            }
                            case Direction.Right:
                            {
                                queue.Enqueue((row + 1, col, Direction.Down));
                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }

        return energizedTiles.Count;
    }

    private static Dictionary<(int row, int col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int row, int col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                // if (input[row][col] != '.')
                // {
                grid[(row, col)] = input[row][col];
                // }
            }
        }

        return grid;
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
                var value = grid.TryGetValue((row, col), out var v) ? v : '.';
                sb.Append(value);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static string DrawGrid(Dictionary<(int row, int col), char> grid, HashSet<(int row, int col)> energized)
    {
        var sb = new StringBuilder();
        var rows = grid.Keys.Select(k => k.row).Distinct().OrderBy(r => r).ToList();
        var cols = grid.Keys.Select(k => k.col).Distinct().OrderBy(c => c).ToList();

        foreach (var row in rows)
        {
            foreach (var col in cols)
            {
                var value = grid.GetValueOrDefault((row, col), '.');
                sb.Append(energized.Contains((row, col)) ? '#' : value);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
}
