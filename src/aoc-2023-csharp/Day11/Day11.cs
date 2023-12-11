using System.Text;

namespace aoc_2023_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllLines("Day11/day11.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input)
    {
        var grid = BuildGrid(input);

        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        Console.WriteLine(DrawGrid(grid));
        Console.WriteLine();

        ExpandRows(minRow, maxRow, grid);
        ExpandColumns(minRow, maxRow, grid);

        Console.WriteLine(DrawGrid(grid));
        Console.WriteLine();

        var points = grid.Keys.ToArray();
        var total = 0L;

        for (var i = 0L; i < points.Length - 1; i++)
        {
            for (var j = i + 1; j < points.Length; j++)
            {
                var start = points[i];
                var end = points[j];

                // find the shortest distance from start to end
                var distance = Math.Abs(start.row - end.row) + Math.Abs(start.col - end.col);
                total += distance;
            }
        }

        return total;
    }

    public static long Solve2(string[] input)
    {
        return 0;
    }

    private static Dictionary<(long row, long col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(long row, long col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] != '.')
                {
                    grid[(row, col)] = input[row][col];
                }
            }
        }

        return grid;
    }

    private static void ExpandRows(long minRow, long maxRow, Dictionary<(long row, long col), char> grid)
    {
        // expand rows
        var pointsToMove = new Dictionary<(long row, long col), long>();

        for (var row = minRow; row <= maxRow; row++)
        {
            // if there are no galaxies in this row
            if (grid.Keys.All(x => x.row != row))
            {
                // find all the points that need to be moved down
                var pointsToMoveQuery = grid.Where(x => x.Key.row > row).ToArray();

                foreach (var point in pointsToMoveQuery)
                {
                    var value = pointsToMove.GetValueOrDefault(point.Key);
                    pointsToMove[point.Key] = value + 1;
                }
            }
        }

        foreach (var point in pointsToMove.Reverse())
        {
            grid[(point.Key.row + point.Value, point.Key.col)] = grid[point.Key];
            grid.Remove(point.Key);
        }
    }

    private static void ExpandColumns(long minRow, long maxRow, Dictionary<(long row, long col), char> grid)
    {
        // expand columns
        var pointsToMove = new Dictionary<(long row, long col), long>();

        for (var col = minRow; col <= maxRow; col++)
        {
            // if there are no galaxies in this row
            if (grid.Keys.All(x => x.col != col))
            {
                // find all the points that need to be moved down
                var pointsToMoveQuery = grid.Where(x => x.Key.col > col).ToArray();

                foreach (var point in pointsToMoveQuery)
                {
                    var value = pointsToMove.GetValueOrDefault(point.Key);
                    pointsToMove[point.Key] = value + 1;
                }
            }
        }

        foreach (var point in pointsToMove.Reverse())
        {
            grid[(point.Key.row, point.Key.col + point.Value)] = grid[point.Key];
            grid.Remove(point.Key);
        }
    }

    private static string DrawGrid(Dictionary<(long row, long col), char> grid)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        var sb = new StringBuilder();

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                if (grid.TryGetValue((row, col), out var value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append('.');
                }
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
