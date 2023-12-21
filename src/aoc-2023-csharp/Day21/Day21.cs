namespace aoc_2023_csharp.Day21;

public static class Day21
{
    private static readonly string[] Input = File.ReadAllLines("Day21/day21.txt").ToArray();

    public static int Part1() => Solve1(Input, 64);

    public static long Part2() => Solve2(Input, 26501365);

    public static int Solve1(string[] input, int goal) => CountPossibilities(input, goal);

    public static long Solve2(string[] input, int goal)
    {
        var size = input.Length;
        var half = size / 2;

        var term1 = CountPossibilities(input, half);
        var term2 = CountPossibilities(input, half + size);
        var term3 = CountPossibilities(input, half + size + size);

        return SolveQuadratic(goal, input.Length, term1, term2, term3);
    }

    private static (HashSet<(int row, int col)> grid, (int row, int col) start) ParseInput(IReadOnlyList<string> input)
    {
        var grid = new HashSet<(int row, int col)>();
        var start = (0, 0);

        for (var row = 0; row < input.Count; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] is 'S')
                {
                    start = (row, col);
                }

                if (input[row][col] is '.' or 'S')
                {
                    grid.Add((row, col));
                }
            }
        }

        return (grid, start);
    }

    private static int CountPossibilities(IReadOnlyList<string> input, int goal)
    {
        var (grid, start) = ParseInput(input);

        var maxRow = input.Count;
        var maxCol = input[0].Length;

        var destinations = new HashSet<(int row, int col)>();
        var seen = new HashSet<(int row, int col, int steps)>();
        var queue = new Queue<(int row, int col, int steps)>();
        queue.Enqueue((start.row, start.col, 0));

        while (queue.Any())
        {
            var current = queue.Dequeue();

            if (!seen.Add(current))
            {
                continue;
            }

            var (row, col, steps) = current;

            if (current.steps == goal)
            {
                destinations.Add((row, col));
                continue;
            }

            var neighbors = new (int row, int col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                var compressed = CompressCoordinate(neighbor, maxRow, maxCol);

                if (grid.Contains(compressed))
                {
                    queue.Enqueue((neighbor.row, neighbor.col, steps + 1));
                }
            }
        }

        return destinations.Count;
    }

    private static (int row, int col) CompressCoordinate((int row, int col) position, int maxRow, int maxCol)
    {
        var (row, col) = position;

        while (row < 0)
        {
            row += maxRow;
        }

        while (col < 0)
        {
            col += maxCol;
        }

        return (row % maxRow, col % maxCol);
    }

    private static long SolveQuadratic(long goal, long gridSize, long term1, long term2, long term3)
    {
        var x = goal / gridSize;
        var a = (term3 - 2 * term2 + term1) / 2;
        var b = term2 - term1 - a;
        var c = term1;

        // a*x^2 + b*x + c
        return a * x * x + b * x + c;
    }
}
