namespace aoc_2023_csharp.Day21;

public static class Day21
{
    private static readonly string[] Input = File.ReadAllLines("Day21/day21.txt").ToArray();

    public static int Part1() => Solve1(Input, 64);

    public static long Part2() => Solve2(Input, 26501365);

    public static int Solve1(string[] input, int goal)
    {
        // The Elf starts at the starting position (S) which also counts as a garden plot.
        var (grid, start) = ParseInput(input);

        // Console.WriteLine(DrawGrid(grid, start));

        var queue = new Queue<(int row, int col, int steps)>();
        queue.Enqueue((start.row, start.col, 0));

        var seen = new HashSet<(int row, int col, int steps)>();

        var destinations = new HashSet<(int row, int col)>();

        while (queue.Any())
        {
            // Console.WriteLine($"Queue: {queue.Count}, Seen: {seen.Count}, Destinations: {destinations.Count}");

            var current = queue.Dequeue();

            if (seen.Contains(current))
            {
                continue;
            }

            seen.Add(current);

            var (row, col, steps) = current;
            var position = (row, col);

            if (current.steps > goal)
            {
                throw new Exception($"Too many steps ({current.steps})");
            }

            if (current.steps == goal)
            {
                // if (destinations.Contains((row, col)))
                // {
                //     throw new Exception($"Already visited ({row},{col})");
                // }

                // TODO: is there a smarter way to do this instead of using a HashSet for deduplication?
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
                if (grid.Contains(neighbor))
                {
                    queue.Enqueue((neighbor.row, neighbor.col, steps + 1));
                }
            }
        }

        return destinations.Count;
    }

    public static long Solve2(string[] input, int goal)
    {
        long a = GetTerm(input, 65);
        long b = GetTerm(input, 65 + 131);
        long c = GetTerm(input, 65 + 131 + 131);

        return DoMath(goal, input.Length, a, b, c);
    }

    private static (HashSet<(int row, int col)> grid, (int row, int col) start) ParseInput(string[] input)
    {
        var grid = new HashSet<(int row, int col)>();
        var start = (0, 0);

        for (var row = 0; row < input.Length; row++)
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

    private static int GetTerm(string[] input, int goal)
    {
        // TODO: figure out if this is generic enough to be used for both part 1 and part 2
        var maxRow = input.Length;
        var maxCol = input[0].Length;

        var (grid, start) = ParseInput(input);

        var queue = new Queue<(int row, int col, int steps)>();
        queue.Enqueue((start.row, start.col, 0));

        var seen = new HashSet<(int row, int col, int steps)>();

        var destinations = new HashSet<(int row, int col)>();

        while (queue.Any())
        {
            var current = queue.Dequeue();

            if (seen.Contains(current))
            {
                continue;
            }

            seen.Add(current);

            var (row, col, steps) = current;
            var position = (row, col);

            if (current.steps > goal)
            {
                throw new Exception($"Too many steps ({current.steps})");
            }

            if (current.steps == goal)
            {
                // if (destinations.Contains((row, col)))
                // {
                //     throw new Exception($"Already visited ({row},{col})");
                // }

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

    private static long DoMath(long goal, long gridSize, long a, long b, long c)
    {
        var n = goal / gridSize;
        var b0 = a;
        var b1 = b - a;
        var b2 = c - b;
        return b0 + b1 * n + (n * (n - 1L) / 2L) * (b2 - b1);
    }

    private static (int row, int col) CompressCoordinate((int row, int col) neighbor, int maxRow, int maxCol)
    {
        var (row, col) = neighbor;

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
}
