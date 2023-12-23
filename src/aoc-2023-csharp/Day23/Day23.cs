namespace aoc_2023_csharp.Day23;

public static class Day23
{
    private static readonly string[] Input = File.ReadAllLines("Day23/day23.txt").ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input) => FindLongestPath(input);

    public static int Solve2(string[] input)
    {
        return 0;
    }

    private static int FindLongestPath(string[] input)
    {
        var (grid, start, end) = ParseInput(input);

        var stack = new Stack<(int row, int col, int steps)>();
        var seen = new HashSet<(int row, int col)>();

        stack.Push((start.row, start.col, 0));

        while (stack.Any())
        {
            var (row, col, steps) = stack.Pop();
            var current = (row, col);

            if (current == end)
            {
                return steps;
            }

            // never step onto the same tile twice
            if (!seen.Add(current))
            {
                continue;
            }

            if (!grid.TryGetValue((row, col), out var value) || value == '#')
            {
                continue;
            }

            switch (value)
            {
                case '^':
                    stack.Push((row - 1, col, steps + 1));
                    break;
                case 'v':
                    stack.Push((row + 1, col, steps + 1));
                    break;
                case '<':
                    stack.Push((row, col - 1, steps + 1));
                    break;
                case '>':
                    stack.Push((row, col + 1, steps + 1));
                    break;
                case '.':
                    stack.Push((row, col + 1, steps + 1));
                    stack.Push((row, col - 1, steps + 1));
                    stack.Push((row + 1, col, steps + 1));
                    stack.Push((row - 1, col, steps + 1));
                    break;
                default:
                    throw new Exception($"Unexpected value: {value}");
            }
        }

        throw new Exception("No path found");
    }

    private static (Dictionary<(int row, int col), char> grid, (int row, int col) start, (int row, int col) end)
        ParseInput(string[] input)
    {
        var grid = new Dictionary<(int row, int col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        var start = grid.Single(g => g.Key.row == 0 && g.Value == '.').Key;
        var end = grid.Single(g => g.Key.row == input.Length - 1 && g.Value == '.').Key;

        return (grid, start, end);
    }
}
