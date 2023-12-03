namespace aoc_2023_csharp.Day03;

public static class Day03
{
    private static readonly string[] Input = File.ReadAllLines("Day03/day03.txt").Select(x => x.Trim()).ToArray();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var partNumbers = GetPartNumbers(grid);

        return partNumbers.Sum(x => x.Number);
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);
        var partNumbers = GetPartNumbers(grid);

        return grid.Where(g => g.Value == '*')
            .Where(IsAdjacentToTwoPartNumbers(partNumbers))
            .Select(AdjacentPartNumbers(partNumbers))
            .Sum(p => p[0].Number * p[1].Number);
    }

    private static Dictionary<(int Row, int Col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }

    private static List<PartNumber> GetPartNumbers(Dictionary<(int Row, int Col), char> grid)
    {
        var partNumbers = new List<PartNumber>();
        var maxRow = grid.Keys.Max(x => x.Row);
        var maxCol = grid.Keys.Max(x => x.Col);

        for (var row = 0; row <= maxRow; row++)
        {
            for (var col = 0; col <= maxCol; col++)
            {
                var (currentNumber, start, end) = (0, 0, 0);

                while (grid.TryGetValue((row, col), out var value) && char.IsDigit(value))
                {
                    if (currentNumber == 0)
                    {
                        start = col;
                        end = col;
                    }
                    else
                    {
                        end = col;
                    }

                    currentNumber *= 10;
                    currentNumber += grid[(row, col)] - '0';
                    col++;
                }

                if (currentNumber != 0)
                {
                    partNumbers.Add(new PartNumber(currentNumber, row, start, end));
                }
            }
        }

        return partNumbers.Where(x => x.IsAdjacentToSymbol(grid)).ToList();
    }

    private static Func<KeyValuePair<(int Row, int Col), char>, bool> IsAdjacentToTwoPartNumbers(List<PartNumber> partNumbers) =>
        g => partNumbers.Count(p => p.IsAdjacentTo(g.Key)) == 2;

    private static Func<KeyValuePair<(int Row, int Col), char>, PartNumber[]> AdjacentPartNumbers(List<PartNumber> partNumbers) =>
        g => partNumbers.Where(p => p.IsAdjacentTo(g.Key)).ToArray();
}
