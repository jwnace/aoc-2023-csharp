using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace aoc_2023_csharp.Day17;

public static class Day17
{
    private static readonly string[] Input = File.ReadAllLines("Day17/day17.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);

        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        var initialState = (row: 0, col: 0, direction: Direction.None, steps: 0, heatLoss: 0);

        var queue = new PriorityQueue<(int row, int col, Direction direction, int steps, int heatLoss), int>();
        queue.Enqueue(initialState, 0);

        var seen = new HashSet<(int row, int col, Direction direction, int steps)>();

        var destination = (maxRow, maxCol);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (row, col, direction, steps, heatLoss) = state;
            var key = (row, col, direction, steps);

            if (seen.Contains(key))
            {
                continue;
            }

            seen.Add(key);

            // we've reached the end
            if ((row, col) == destination)
            {
                return heatLoss;
            }

            // we went off the edge
            if (row < minRow || row > maxRow || col < minCol || col > maxCol)
            {
                continue;
            }

            // add all adjacent tiles to the queue
            foreach (var (nextRow, nextCol, nextDirection) in GetAdjacentTiles(row, col, steps, direction))
            {
                if (nextRow < minRow || nextRow > maxRow || nextCol < minCol || nextCol > maxCol)
                {
                    continue;
                }

                if (direction != nextDirection)
                {
                    steps = 0;
                }

                var nextHeatLoss = heatLoss + grid[(nextRow, nextCol)];
                queue.Enqueue((nextRow, nextCol, nextDirection, steps + 1, nextHeatLoss), nextHeatLoss);
            }
        }

        throw new Exception("No path found");
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);

        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        var initialState = (row: 0, col: 0, direction: Direction.None, steps: 0, heatLoss: 0);

        var queue = new PriorityQueue<(int row, int col, Direction direction, int steps, int heatLoss), int>();
        queue.Enqueue(initialState, 0);

        var seen = new HashSet<(int row, int col, Direction direction, int steps)>();

        var destination = (maxRow, maxCol);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (row, col, direction, steps, heatLoss) = state;
            var key = (row, col, direction, steps);

            if (seen.Contains(key))
            {
                continue;
            }

            seen.Add(key);

            // we've reached the end
            if ((row, col) == destination && steps > 3)
            {
                return heatLoss;
            }

            // we went off the edge
            if (row < minRow || row > maxRow || col < minCol || col > maxCol)
            {
                continue;
            }

            // add all adjacent tiles to the queue
            foreach (var (nextRow, nextCol, nextDirection) in GetAdjacentTiles2(row, col, steps, direction))
            {
                if (nextRow < minRow || nextRow > maxRow || nextCol < minCol || nextCol > maxCol)
                {
                    continue;
                }

                if (direction != nextDirection)
                {
                    steps = 0;
                }

                var nextHeatLoss = heatLoss + grid[(nextRow, nextCol)];
                queue.Enqueue((nextRow, nextCol, nextDirection, steps + 1, nextHeatLoss), nextHeatLoss);
            }
        }

        throw new Exception("No path found");
    }

    private static IEnumerable<(int, int, Direction)> GetAdjacentTiles(
        int row,
        int col,
        int steps,
        Direction direction)
    {
        if (direction == Direction.None)
        {
            yield return (row, col + 1, Direction.Right);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Right)
        {
            if (steps < 3)
            {
                yield return (row, col + 1, Direction.Right);
            }

            yield return (row - 1, col, Direction.Up);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Left)
        {
            if (steps < 3)
            {
                yield return (row, col - 1, Direction.Left);
            }

            yield return (row - 1, col, Direction.Up);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Up)
        {
            if (steps < 3)
            {
                yield return (row - 1, col, Direction.Up);
            }

            yield return (row, col - 1, Direction.Left);
            yield return (row, col + 1, Direction.Right);
        }
        else if (direction == Direction.Down)
        {
            if (steps < 3)
            {
                yield return (row + 1, col, Direction.Down);
            }

            yield return (row, col - 1, Direction.Left);
            yield return (row, col + 1, Direction.Right);
        }
    }

    private static IEnumerable<(int, int, Direction)> GetAdjacentTiles2(
        int row,
        int col,
        int steps,
        Direction direction)
    {
        if (direction == Direction.None)
        {
            yield return (row, col + 1, Direction.Right);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Right)
        {
            if (steps < 4)
            {
                yield return (row, col + 1, Direction.Right);
                yield break;
            }

            if (steps < 10)
            {
                yield return (row, col + 1, Direction.Right);
            }

            yield return (row - 1, col, Direction.Up);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Left)
        {
            if (steps < 4)
            {
                yield return (row, col - 1, Direction.Left);
                yield break;
            }

            if (steps < 10)
            {
                yield return (row, col - 1, Direction.Left);
            }

            yield return (row - 1, col, Direction.Up);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Up)
        {
            if (steps < 4)
            {
                yield return (row - 1, col, Direction.Up);
                yield break;
            }

            if (steps < 10)
            {
                yield return (row - 1, col, Direction.Up);
            }

            yield return (row, col - 1, Direction.Left);
            yield return (row, col + 1, Direction.Right);
        }
        else if (direction == Direction.Down)
        {
            if (steps < 4)
            {
                yield return (row + 1, col, Direction.Down);
                yield break;
            }

            if (steps < 10)
            {
                yield return (row + 1, col, Direction.Down);
            }

            yield return (row, col - 1, Direction.Left);
            yield return (row, col + 1, Direction.Right);
        }
    }

    private static Dictionary<(int row, int col), int> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int row, int col), int>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col] - '0';
            }
        }

        return grid;
    }

    private static string DrawGrid(Dictionary<(int row, int col), int> grid)
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

    private enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }
}
