namespace aoc_2023_csharp.Day17;

public static class Day17
{
    private static readonly string[] Input = File.ReadAllLines("Day17/day17.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input) => Solve(input, 1);

    public static int Solve2(string[] input) => Solve(input, 2);

    private static int Solve(string[] input, int part)
    {
        var grid = BuildGrid(input);

        var minRow = grid.Keys.Min(x => x.row);
        var maxRow = grid.Keys.Max(x => x.row);
        var minCol = grid.Keys.Min(x => x.col);
        var maxCol = grid.Keys.Max(x => x.col);

        var seen = new HashSet<StateKey>();
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(new State(0, 0, Direction.None, 0, 0), 0);

        while (queue.Count > 0)
        {
            var (row, col, direction, steps, heatLoss) = queue.Dequeue();
            var key = new StateKey(row, col, direction, steps);

            if (!seen.Add(key))
            {
                continue;
            }

            if (part == 1 && (row, col) == (maxRow, maxCol))
            {
                return heatLoss;
            }

            if (part == 2 && (row, col) == (maxRow, maxCol) && steps > 3)
            {
                return heatLoss;
            }

            var neighbors = part == 1
                ? GetNeighbors(row, col, steps, direction, minSteps: 0, maxSteps: 3)
                : GetNeighbors(row, col, steps, direction, minSteps: 4, maxSteps: 10);

            foreach (var (nextRow, nextCol, nextDirection) in neighbors)
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
                queue.Enqueue(new State(nextRow, nextCol, nextDirection, steps + 1, nextHeatLoss), nextHeatLoss);
            }
        }

        throw new Exception("No path found");
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

    private static IEnumerable<(int row, int col, Direction direction)> GetNeighbors(
        int row,
        int col,
        int steps,
        Direction direction,
        int minSteps = 0,
        int maxSteps = int.MaxValue)
    {
        if (direction == Direction.None)
        {
            yield return (row, col + 1, Direction.Right);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Right)
        {
            if (steps < minSteps)
            {
                yield return (row, col + 1, Direction.Right);
                yield break;
            }

            if (steps < maxSteps)
            {
                yield return (row, col + 1, Direction.Right);
            }

            yield return (row - 1, col, Direction.Up);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Left)
        {
            if (steps < minSteps)
            {
                yield return (row, col - 1, Direction.Left);
                yield break;
            }

            if (steps < maxSteps)
            {
                yield return (row, col - 1, Direction.Left);
            }

            yield return (row - 1, col, Direction.Up);
            yield return (row + 1, col, Direction.Down);
        }
        else if (direction == Direction.Up)
        {
            if (steps < minSteps)
            {
                yield return (row - 1, col, Direction.Up);
                yield break;
            }

            if (steps < maxSteps)
            {
                yield return (row - 1, col, Direction.Up);
            }

            yield return (row, col - 1, Direction.Left);
            yield return (row, col + 1, Direction.Right);
        }
        else if (direction == Direction.Down)
        {
            if (steps < minSteps)
            {
                yield return (row + 1, col, Direction.Down);
                yield break;
            }

            if (steps < maxSteps)
            {
                yield return (row + 1, col, Direction.Down);
            }

            yield return (row, col - 1, Direction.Left);
            yield return (row, col + 1, Direction.Right);
        }
    }

    private record State(int Row, int Col, Direction Direction, int Steps, int HeatLoss);

    private record StateKey(int Row, int Col, Direction Direction, int Steps);

    private enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }
}
