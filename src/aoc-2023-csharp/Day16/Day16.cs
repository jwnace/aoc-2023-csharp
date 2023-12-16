namespace aoc_2023_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);

        return CountEnergizedTiles(grid, new Beam(0, 0, Direction.Right));
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);
        var result = 0;

        for (var row = minRow; row <= maxRow; row++)
        {
            result = Math.Max(result, CountEnergizedTiles(grid, new Beam(row, minCol, Direction.Right)));
            result = Math.Max(result, CountEnergizedTiles(grid, new Beam(row, maxCol, Direction.Left)));
        }

        for (var col = minCol; col <= maxCol; col++)
        {
            result = Math.Max(result, CountEnergizedTiles(grid, new Beam(minRow, col, Direction.Down)));
            result = Math.Max(result, CountEnergizedTiles(grid, new Beam(maxRow, col, Direction.Up)));
        }

        return result;
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

    private static int CountEnergizedTiles(Dictionary<(int row, int col), char> grid, Beam start)
    {
        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);
        var energizedTiles = new HashSet<(int row, int col)>();
        var queue = new Queue<Beam>();
        var seen = new HashSet<Beam>();

        queue.Enqueue(start);

        while (queue.Any())
        {
            var beam = queue.Dequeue();
            var (row, col, direction) = beam;

            if (!seen.Add(beam))
            {
                continue;
            }

            if (row < minRow || row > maxRow || col < minCol || col > maxCol)
            {
                continue;
            }

            energizedTiles.Add((row, col));

            switch (grid[(row, col)])
            {
                case '.' when direction is Direction.Up:
                    queue.Enqueue(beam with { Row = row - 1 });
                    break;
                case '.' when direction is Direction.Down:
                    queue.Enqueue(beam with { Row = row + 1 });
                    break;
                case '.' when direction is Direction.Left:
                    queue.Enqueue(beam with { Col = col - 1 });
                    break;
                case '.' when direction is Direction.Right:
                    queue.Enqueue(beam with { Col = col + 1 });
                    break;
                case '|' when direction is Direction.Up:
                    queue.Enqueue(beam with { Row = row - 1 });
                    break;
                case '|' when direction is Direction.Down:
                    queue.Enqueue(beam with { Row = row + 1 });
                    break;
                case '|' when direction is Direction.Left or Direction.Right:
                    queue.Enqueue(new Beam(row - 1, col, Direction.Up));
                    queue.Enqueue(new Beam(row + 1, col, Direction.Down));
                    break;
                case '-' when direction is Direction.Up or Direction.Down:
                    queue.Enqueue(new Beam(row, col - 1, Direction.Left));
                    queue.Enqueue(new Beam(row, col + 1, Direction.Right));
                    break;
                case '-' when direction is Direction.Left:
                    queue.Enqueue(beam with { Col = col - 1 });
                    break;
                case '-' when direction is Direction.Right:
                    queue.Enqueue(beam with { Col = col + 1 });
                    break;
                case '/' when direction is Direction.Up:
                    queue.Enqueue(new Beam(row, col + 1, Direction.Right));
                    break;
                case '/' when direction is Direction.Down:
                    queue.Enqueue(new Beam(row, col - 1, Direction.Left));
                    break;
                case '/' when direction is Direction.Left:
                    queue.Enqueue(new Beam(row + 1, col, Direction.Down));
                    break;
                case '/' when direction is Direction.Right:
                    queue.Enqueue(new Beam(row - 1, col, Direction.Up));
                    break;
                case '\\' when direction is Direction.Up:
                    queue.Enqueue(new Beam(row, col - 1, Direction.Left));
                    break;
                case '\\' when direction is Direction.Down:
                    queue.Enqueue(new Beam(row, col + 1, Direction.Right));
                    break;
                case '\\' when direction is Direction.Left:
                    queue.Enqueue(new Beam(row - 1, col, Direction.Up));
                    break;
                case '\\' when direction is Direction.Right:
                    queue.Enqueue(new Beam(row + 1, col, Direction.Down));
                    break;
            }
        }

        return energizedTiles.Count;
    }
}
