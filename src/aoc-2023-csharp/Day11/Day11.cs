namespace aoc_2023_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllLines("Day11/day11.txt").ToArray();

    public static long Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    public static long Solve1(string[] input) => Solve(input, 1);

    public static long Solve2(string[] input, int expansion = 999999) => Solve(input, expansion);

    private static long Solve(IReadOnlyList<string> input, int expansion)
    {
        var grid = BuildGrid(input);

        ExpandRows(grid, expansion);
        ExpandColumns(grid, expansion);

        return SumDistances(grid);
    }

    private static Dictionary<(long row, long col), char> BuildGrid(IReadOnlyList<string> input)
    {
        var grid = new Dictionary<(long row, long col), char>();

        for (var row = 0; row < input.Count; row++)
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

    private static void ExpandRows(Dictionary<(long row, long col), char> grid, int amount = 1)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);

        var pointsToMove = new Dictionary<(long row, long col), long>();

        for (var row = minRow; row <= maxRow; row++)
        {
            if (grid.Keys.Any(x => x.row == row))
            {
                continue;
            }

            var pointsToMoveQuery = grid.Where(x => x.Key.row > row).ToArray();

            foreach (var point in pointsToMoveQuery)
            {
                var value = pointsToMove.GetValueOrDefault(point.Key);
                pointsToMove[point.Key] = value + amount;
            }
        }

        foreach (var point in pointsToMove.Reverse())
        {
            grid[(point.Key.row + point.Value, point.Key.col)] = grid[point.Key];
            grid.Remove(point.Key);
        }
    }

    private static void ExpandColumns(Dictionary<(long row, long col), char> grid, int amount = 1)
    {
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        var pointsToMove = new Dictionary<(long row, long col), long>();

        for (var col = minCol; col <= maxCol; col++)
        {
            if (grid.Keys.Any(x => x.col == col))
            {
                continue;
            }

            var pointsToMoveQuery = grid.Where(x => x.Key.col > col).ToArray();

            foreach (var point in pointsToMoveQuery)
            {
                var value = pointsToMove.GetValueOrDefault(point.Key);
                pointsToMove[point.Key] = value + amount;
            }
        }

        foreach (var point in pointsToMove.Reverse())
        {
            grid[(point.Key.row, point.Key.col + point.Value)] = grid[point.Key];
            grid.Remove(point.Key);
        }
    }

    private static long SumDistances(Dictionary<(long row, long col), char> grid)
    {
        var galaxies = grid.Keys.ToArray();
        var total = 0L;

        for (var i = 0L; i < galaxies.Length - 1; i++)
        {
            for (var j = i + 1; j < galaxies.Length; j++)
            {
                var start = galaxies[i];
                var end = galaxies[j];
                var distance = Math.Abs(start.row - end.row) + Math.Abs(start.col - end.col);
                total += distance;
            }
        }

        return total;
    }
}
