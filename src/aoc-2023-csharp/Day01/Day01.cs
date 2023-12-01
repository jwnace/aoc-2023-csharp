namespace aoc_2023_csharp.Day01;

public static class Day01
{
    private static readonly string[] Input = File.ReadAllLines("Day01/day01.txt");

    private static readonly Dictionary<string, int> Digits = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(IEnumerable<string> input) =>
        input.Select(x => x.Where(char.IsDigit).ToArray())
            .Select(x => $"{x.First()}{x.Last()}")
            .Sum(int.Parse);

    public static int Solve2(IEnumerable<string> input) =>
        input.Sum(line => GetFirstDigit(line) * 10 + GetLastDigit(line));

    private static int GetFirstDigit(string input)
    {
        var firstDigit = Digits.Select(pair => new
            {
                Index = input.IndexOf(pair.Value.ToString(), StringComparison.Ordinal),
                Digit = pair.Value
            })
            .Where(x => x.Index >= 0)
            .MinBy(x => x.Index) ?? new { Index = int.MaxValue, Digit = 0 };

        var firstDigitString = Digits.Select(pair => new
            {
                Index = input.IndexOf(pair.Key, StringComparison.Ordinal),
                Digit = pair.Value
            })
            .Where(x => x.Index >= 0)
            .MinBy(x => x.Index) ?? new { Index = int.MaxValue, Digit = 0 };

        return firstDigitString.Index < firstDigit.Index
            ? firstDigitString.Digit
            : firstDigit.Digit;
    }

    private static int GetLastDigit(string input)
    {
        var lastDigit = Digits.Select(pair => new
            {
                Index = input.LastIndexOf(pair.Value.ToString(), StringComparison.Ordinal),
                Digit = pair.Value
            })
            .Where(x => x.Index >= 0)
            .MaxBy(x => x.Index) ?? new { Index = int.MinValue, Digit = 0 };

        var lastDigitString = Digits.Select(pair => new
            {
                Index = input.LastIndexOf(pair.Key, StringComparison.Ordinal),
                Digit = pair.Value
            })
            .Where(x => x.Index >= 0)
            .MaxBy(x => x.Index) ?? new { Index = int.MinValue, Digit = 0 };

        return lastDigitString.Index > lastDigit.Index
            ? lastDigitString.Digit
            : lastDigit.Digit;
    }
}
