using System.Text;

namespace aoc_2023_csharp.Day14;

public static class Day14
{
    private static readonly string[] Input = File.ReadAllLines("Day14/day14.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);

        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        TiltNorth(grid);

        return grid.Where(x => x.Value == 'O').Sum(x => maxRow - x.Key.row + 1);
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);
        var seen = new Dictionary<string, int>();

        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var step = 0; step < 1_000_000_000; step++)
        {
            TiltNorth(grid);
            TiltWest(grid);
            TiltSouth(grid);
            TiltEast(grid);

            if (seen.TryGetValue(DrawGrid(grid), out var value))
            {
                var cycleLength = step - value;
                var remainingCycles = 1_000_000_000 - step;
                var cyclesToSkip = remainingCycles % cycleLength;
                var stepsToSkip = remainingCycles - cyclesToSkip;

                step += stepsToSkip;
            }
            else
            {
                seen[DrawGrid(grid)] = step;
            }
        }

        return grid.Where(x => x.Value == 'O').Sum(x => maxRow - x.Key.row + 1);
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

    private static void TiltNorth(Dictionary<(int row, int col), char> grid)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                if (grid[(row, col)] != 'O')
                {
                    continue;
                }

                while (grid.TryGetValue((row - 1, col), out var value) && value != 'O' && value != '#')
                {
                    grid[(row - 1, col)] = 'O';
                    grid[(row, col)] = '.';
                    row--;
                }
            }
        }
    }

    private static void TiltSouth(Dictionary<(int row, int col), char> grid)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var row = maxRow; row >= minRow; row--)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                if (grid[(row, col)] != 'O')
                {
                    continue;
                }

                while (grid.TryGetValue((row + 1, col), out var value) && value != 'O' && value != '#')
                {
                    grid[(row + 1, col)] = 'O';
                    grid[(row, col)] = '.';
                    row++;
                }
            }
        }
    }

    private static void TiltWest(Dictionary<(int row, int col), char> grid)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var col = minCol; col <= maxCol; col++)
        {
            for (var row = minRow; row <= maxRow; row++)
            {
                if (grid[(row, col)] != 'O')
                {
                    continue;
                }

                while (grid.TryGetValue((row, col - 1), out var value) && value != 'O' && value != '#')
                {
                    grid[(row, col - 1)] = 'O';
                    grid[(row, col)] = '.';
                    col--;
                }
            }
        }
    }

    private static void TiltEast(Dictionary<(int row, int col), char> grid)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        for (var col = maxCol; col >= minCol; col--)
        {
            for (var row = minRow; row <= maxRow; row++)
            {
                if (grid[(row, col)] != 'O')
                {
                    continue;
                }

                while (grid.TryGetValue((row, col + 1), out var value) && value != 'O' && value != '#')
                {
                    grid[(row, col + 1)] = 'O';
                    grid[(row, col)] = '.';
                    col++;
                }
            }
        }
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
}
