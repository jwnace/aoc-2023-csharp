namespace aoc_2023_csharp.Day13;

public static class Day13
{
    private static readonly string Input = File.ReadAllText("Day13/day13.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string input) => Solve(input);

    public static int Solve2(string input) => Solve(input, tolerance: 1);

    private static int Solve(string input, int tolerance = 0)
    {
        var result = 0;
        var patterns = input.Split("\n\n");

        foreach (var pattern in patterns)
        {
            var lines = pattern.Split("\n");
            var grid = BuildGrid(lines);

            var minRow = grid.Keys.Min(k => k.row);
            var maxRow = grid.Keys.Max(k => k.row);
            var minCol = grid.Keys.Min(k => k.col);
            var maxCol = grid.Keys.Max(k => k.col);

            for (var row = minRow; row < maxRow; row++)
            {
                var mismatchCount = 0;

                for (var offset = 0; offset < maxRow; offset++)
                {
                    var top = row - offset;
                    var bottom = row + 1 + offset;

                    if (top < 0 || bottom > maxRow)
                    {
                        break;
                    }

                    var topRow = grid.Where(x => x.Key.row == top).Select(x => x.Value).ToArray();
                    var bottomRow = grid.Where(x => x.Key.row == bottom).Select(x => x.Value).ToArray();

                    for (var col = minCol; col <= maxCol; col++)
                    {
                        if (topRow[col] != bottomRow[col])
                        {
                            mismatchCount++;
                        }
                    }
                }

                if (mismatchCount == tolerance)
                {
                    result += 100 * (row + 1);
                }
            }

            for (var col = minCol; col < maxCol; col++)
            {
                var mismatchCount = 0;

                for (var offset = 0; offset < maxCol; offset++)
                {
                    var left = col - offset;
                    var right = col + 1 + offset;

                    if (left < 0 || right > maxCol)
                    {
                        break;
                    }

                    var leftCol = grid.Where(x => x.Key.col == left).Select(x => x.Value).ToArray();
                    var rightCol = grid.Where(x => x.Key.col == right).Select(x => x.Value).ToArray();

                    for (var row = minRow; row <= maxRow; row++)
                    {
                        if (leftCol[row] != rightCol[row])
                        {
                            mismatchCount++;
                        }
                    }
                }

                if (mismatchCount == tolerance)
                {
                    result += col + 1;
                }
            }
        }

        return result;
    }

    private static Dictionary<(int row, int col), char> BuildGrid(string[] lines)
    {
        var grid = new Dictionary<(int row, int col), char>();

        for (var row = 0; row < lines.Length; row++)
        {
            for (var col = 0; col < lines[0].Length; col++)
            {
                grid[(row, col)] = lines[row][col];
            }
        }

        return grid;
    }
}
